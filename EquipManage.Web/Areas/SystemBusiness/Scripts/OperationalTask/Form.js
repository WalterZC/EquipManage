var FId = $.request("FId");//经验ID
var tableWrapper = $("#EquipTable_wrapper");
var equiptable = $('#EquipTable');
var partstable = $('#PartTable');
var partselecttable = $('#PartSelect');
var oTable = null;
var pTable = null;
var sTable = null;
var nEditing = null;
var nNew = false;
var currentRow = null;
var arrayData = new Array();
$(function () {
    initControl();
    initEquipTable();
    PartSelectTable();
    initPartsTable();
    if (!!FId) {
        $.ajax({
            url: "/SystemBusiness/OperationalPlan/GetFormJson",
            data: { FId: FId },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form").formSerialize(data);
            }
        });
        $.ajax({
            url: "/SystemBusiness/OperationalPlan/GetEquipFormJson",
            data: { FId: FId },
            dataType: "json",
            async: false,
            success: function (data) {
                $(data).each(function (i, model) {
                    var aiNew = oTable.fnAddData([model.FId, model.FItemId, model.FEntryId, model.FObjectTypeId, model.FObjectId, model.FProjectId, model.FOperationProjectId, model.FOperationClassId, model.FOperatorId, model.FDescription]);
                    var nRow = oTable.fnGetNodes(aiNew[0]);
                    editRow(oTable, nRow);
                    RowNum = model.FEntryId + 1;
                });
                oTable.fnDeleteRow(oTable[0].rows[1]);
            }
        });
        $.ajax({
            url: "/SystemBusiness/OperationalPlan/GetPartFormJson",
            data: { FId: FId },
            dataType: "json",
            async: false,
            success: function (data) {
                //$("#form").formSerialize(data);
                $(data).each(function (i, aData) {
                    var aiNew = pTable.fnAddData([aData.FId, aData.FItemId, aData.FEntryId, aData.FPartsId, top.clients.parts[aData.FPartsId].FNumber, top.clients.parts[aData.FPartsId].FFullName, top.clients.parts[aData.FPartsId].FModel, top.clients.parts[aData.FPartsId].FUnit, aData.FQty, top.clients.parts[aData.FPartsId].FWarehouse, '']);
                    var nRow = pTable.fnGetNodes(aiNew[0]);
                    editPartRow(pTable, nRow);
                });
            }
        });

    } else {
        $("#EM-back").css("display", "none");
        $.ajax({
            url: "/SystemManage/BillCodeRule/GetNewBillInfoJson",
            data: { FBillName: 'Sys_OperationalTask', FROB: true },
            dataType: "json",
            async: false,
            success: function (data) {
                var billInfo = new Array();
                billInfo = data.data.split(',');
                $("#FId").val(billInfo[0]);
                $("#FNumber").val(billInfo[1]);
            }
        });
    }

    handleValidation();
});
function initControl() {
    $('.date-picker').datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true,
        format: "yyyy-mm-dd",
        language: "zh-CN",
        todayBtn: true,
    });

    $("#FStartDate").on("change", function () {
        var startdate = $(this).val();
        var enddate = $("#FEndDate").val();
        $("#FUseDay").val(parseInt(DateDiff(startdate, enddate)) + 1);
    });
    $("#FEndDate").on("change", function () {
        var enddate = $(this).val();
        var startdate = $("#FStartDate").val();
        $("#FUseDay").val(parseInt(DateDiff(startdate, enddate)) + 1);
    });

    $("#FOrganizeId").empty().prepend("<option value=''>==请选择==</option>");
    $("#FOrganizeId").bindSelect({
        url: "/SystemDocument/Organize/GetOperationTreeSelectJson",
    });

    $("#FUseDeptId").empty().prepend("<option value=''>==请选择==</option>");
    $("#FUseDeptId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson",
    });

    //组织部门事件触发改变表体中的作业班组和作业人员
    $("#FOrganizeId").on("change", function () {
        var FOrganizeId = $(this).val();
        $("select[name='FOperationClassId']", $('#EquipTable')).empty().prepend("<option value=''>==请选择==</option>");
        $("select[name='FOperatorId']", $('#EquipTable')).empty().prepend("<option value=''>==请选择==</option>");
        if (!!FOrganizeId) {
            $("select[name='FOperationClassId']", oTable).bindSelect({
                url: "/SystemDocument/OperationClass/GetSelectJson",
                id: "FId",
                text: "FShortName",
                param: { itemId: FOrganizeId, keyword: "" }
            });
        }
    });

    $("#FOperationTypeId").bindSelect({
        url: "/SystemDocument/ItemsType/GetGridSelectJson",
        id: "FId",
        text: "FFullName",
        param: { itemId: "OperationType", keyword: "" }
    });

    var UserListData = getUserItems();
    
    $("#FOnlineMonitorId").select2({
        data: UserListData,
    });
    $("#FEquipManagerId").select2({
        data: UserListData
    });
    $("#FPickingUserId").select2({
        data: UserListData
    });
    $("#FUseDeptManagerId").select2({
        data: UserListData
    });
    
    $("#FRunningStatus").select2({
        data: getdataItems("RunningStatus")
    });
    $("#FOperationTypeId").on("change", function () {
        var FOperationTypeId = $(this).val();
        var FOperationLevelId = $("#FOperationLevelId").val();
        if (!!FOperationTypeId) {
            $("#FOperationLevelId").empty().prepend("<option value=''>==请选择==</option>");
            $("#FOperationLevelId").bindSelect({
                url: "/SystemDocument/ItemsData/GetGridJson",
                async: false,
                id: "FId",
                text: "FItemName",
                param: { itemId: FOperationTypeId, keyword: "" }
            });
        }
        $("select[name='FOperationProjectId']", oTable).empty().prepend("<option value=''>==请选择==</option>");
    });
    $("#FOperationLevelId").on("change", function () {
        var FOperationTypeId = $("#FOperationTypeId").val();
        var FOperationLevelId = $(this).val();
        var FObjectTypeId;
        var FObjectId;

        FObjectTypeId = $("#FObjectTypeId").val();    //需要修改为当前行
        FObjectId = $("#FObjectId").val();
        bindOperationProjectId(FOperationTypeId, FOperationLevelId, FObjectTypeId, FObjectId)
    });

    //通过对象类型绑定对象列表
    $("#FObjectTypeId").on("change", function () {
        var ItemId = $(this).val();
        var FOrgId = $("#FUseDeptId").val();
        getOperationItem(ItemId, FOrgId);
    });

    //通过对象类型绑定对象列表
    $("#FObjectId").on("change", function () {
        var FOperationTypeId = $("#FOperationTypeId").val();
        var FOperationLevelId = $("#FOperationLevelId").val();
        var FObjectTypeId = $("#FObjectTypeId").val();    //需要修改为当前行
        var FObjectId = $(this).val();

        $("#FExpWareId").empty().prepend("<option value=''>==请选择==</option>");
        if (!!FObjectId) {
            bindOperationProjectId(FOperationTypeId, FOperationLevelId, FObjectTypeId, FObjectId);
            getExpWare(FOperationTypeId, FOperationLevelId, FObjectTypeId, FObjectId, '');
        }
        if (FObjectTypeId == "Equipment") {
            if (!!FObjectId) {
                $.ajax({
                    url: "/SystemDocument/Equipment/GetFormJson",
                    data: { keyValue: FObjectId },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        $("#FEquipManagerId").val(data.FPrincipalId).trigger("change");
                        $("#FPositionId").val(data.FPositionID).trigger("change");
                    }
                });
            }
        }
    });

    $("#FUseDeptId").on("change", function () {
        var UseOrgID = $(this).val();
        if (!!UseOrgID) {
            $("#FPositionId").empty().prepend("<option value=''>==请选择==</option>");
            $("#FPositionId").bindSelect({
                url: "/SystemDocument/Position/GetTreeSelectJson",
                param: { FOrganize: UseOrgID }
            });
            var ItemId = $("#FObjectTypeId").val();

            getOperationItem(ItemId, UseOrgID);
        }
    });

    $("#FPositionId").on("change", function () {
        var positionId = $(this).val();
        if (!!positionId) {
            $("#FOnlineMonitorId").val(top.clients.position[positionId].FPrincipalId).trigger("change");
        }
    });

    $("#FCyclicTypeId").select2({
        minimumResultsForSearch: Infinity,
        placeholder: '请选择',
        language: "zh-CN",//汉化
    });
    $(".select2-FObjectType").select2({
        placeholder: '请选择',
        minimumResultsForSearch: 20,
        language: "zh-CN",//汉化
    });

    $("select", oTable).select2({
        placeholder: '请选择',
        minimumResultsForSearch: 20,
        language: "zh-CN",//汉化
    });
}

function bindOperationProjectId(FOperationTypeId, FOperationLevelId, FObjectTypeId, FObjectId) {
    $("#FOperationProjectId").empty().prepend("<option value=''>==请选择==</option>");//需要设定当前行
    if ((!!FOperationTypeId) && (!!FOperationLevelId) && (!!FObjectTypeId) && (!!FObjectId)) {
        $("#FOperationProjectId").bindSelect({   //需要设定当前行
            url: "/SystemDocument/OperationProject/GetConditionsGridJson",
            id: "FId",
            text: "FShortName",
            param: {
                FOperationTypeId: FOperationTypeId,
                FOperationLevelId: FOperationLevelId,
                FObjectTypeId: FObjectTypeId,
                FObjectId: FObjectId
            }
        });
    }
}

function initEquipTable() {
    if (!jQuery().dataTable) {
        return;
    }

    oTable = equiptable.dataTable({
        "language": {
            "sProcessing": "处理中...",
            "sLengthMenu": "显示 _MENU_ 项结果",
            "sZeroRecords": "没有匹配结果",
            "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
            "sInfoPostFix": "",
            "sSearch": "搜索:",
            "sUrl": "",
            "sEmptyTable": "表中数据为空",
            "sLoadingRecords": "载入中...",
            "sInfoThousands": ",",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            },
            "oAria": {
                "sSortAscending": ": 以升序排列此列",
                "sSortDescending": ": 以降序排列此列"
            }
        },
        "columns": [
            { "sTitle": "FId", "sClass": "hiddenCol" },
            { "sTitle": "FItemId", "sClass": "hiddenCol" },
            { "sTitle": "序号", "sClass": "dt-center table-td-vertical-align" },
            { "sTitle": "项目" },
            { "sTitle": "设备部位" },
            { "sTitle": "作业小组" },
            { "sTitle": "负责人" },
            { "sTitle": "备注", "sClass": "dt-center table-td-vertical-align" }
        ],
        "aoColumnDefs": [
            { "sWidth": "20px", "aTargets": [2] },
            { "sWidth": "70px", "aTargets": [3] },
            { "sWidth": "150px", "aTargets": [4] },
            { "sWidth": "150px", "aTargets": [5] },
            { "sWidth": "80px", "aTargets": [6] }
        ],
        "bFilter": false, //过滤功能 
        "bSort": false, //排序功能 
        "bInfo": false,//页脚信息 
        "bPaginate": false, //翻页功能
        "bLengthChange": false, //改变每页显示数据数量
        "pageLength": -1,
        "bAutoWidth": false //自动宽度
    });

    //记住行号和点击行改变颜色
    oTable.on('mousedown', 'tbody tr', function (e) {
        if ($(this).hasClass('selected')) {
            if ($(this)[0] == currentRow) {
                return;
            } else {
                currentRow = null;//清楚选中行
                $(this).removeClass('selected');
            }
        } else {
            currentRow = this;//记录选中行
            oTable.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            var aData = oTable.fnGetData(currentRow);
        }
    });

    $("select[name='FOperationClassId']", oTable[0].rows[1]).on("change", function () {
        var FOperationClassId = $(this).val();
        $("select[name='FOperatorId']", oTable[0].rows[1]).empty().prepend("<option value=''>==请选择==</option>");
        if (!!FOperationClassId) {
            $("select[name='FOperatorId']", oTable[0].rows[1]).bindSelect({
                url: "/SystemDocument/OperationClassMember/GetSelectJson",
                id: "FId",
                text: "FRealName",
                param: { keyValue: FOperationClassId }
            });
        }
    });

    //初始化时绑定作业班组
    $("select[name='FOperationClassId']", oTable[0].rows[1]).bindSelect({
        url: "/SystemDocument/OperationClass/GetSelectJson",
        id: "FId",
        text: "FShortName",
        param: { itemId: $("#FOrganizeId").val(), keyword: "" }
    });

    $("select[name='FProjectId']", oTable[0].rows[1]).bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: 'ProjectItem' },
    });
}

var PartSelectTable = function () {

    var partselecttable = $('#PartSelect');

    sTable = partselecttable.dataTable({
        "processing": true,
        //"serverSide": true,
        "bAutoWidth": false,//自动宽度
        "ajax": {
            "type": "GET",
            "url": "/SystemDocument/Parts/GetPermissionGridJson",
            "dataType": "json",
            "data": { itemId: $("#FOrganizeId").val(), keyword: '' },
            "dataSrc": function (data) {
                var json = new Array();
                json["data"] = data;
                return json.data;
            }
        },
        "columns": [
            {
                "sClass": "text-center",
                "data": "FId",
                "render": function (data, type, full, meta) {
                    return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"/><span></span></label>';
                },
                "bSortable": false,
                "sWidth": "20px;"
            },
            { "data": "FId", "bSortable": false, "sTitle": "FId", "sClass": "hiddenCol" },
            { "data": "FNumber", "sTitle": "编码", "sWidth": "25%;" },
            { "data": "FFullName", "sTitle": "名称", "sWidth": "25%;" },
            { "data": "FUnit", "bSortable": false, "sTitle": "单位", "sWidth": "20%;" },
            { "data": "FModel", "sTitle": "规格" },
            { "data": "FCategory", "sTitle": "类别" },
            { "data": "FFigure", "sTitle": "图号" },
            { "data": "FWarehouse", "sTitle": "仓库" },
            { "data": "FSupply", "sTitle": "供应商" }
        ],
        "language": {
            "sProcessing": "处理中...",
            "sLengthMenu": "显示 _MENU_ 项结果",
            "sZeroRecords": "没有匹配结果",
            "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
            "sInfoPostFix": "",
            "sSearch": "搜索:",
            "sUrl": "",
            "sEmptyTable": "表中数据为空",
            "sLoadingRecords": "载入中...",
            "sInfoThousands": ",",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            },
            "oAria": {
                "sSortAscending": ": 以升序排列此列",
                "sSortDescending": ": 以降序排列此列"
            }
        },

        "lengthMenu": [
            [5, 15, 20, -1],
            [5, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 8,
        "pagingType": "bootstrap_full_number",
        "columnDefs": [
            {  // set default column settings
                'orderable': false,
                'targets': [0]
            },
            {
                "searchable": false,
                "targets": [0]
            },
            {
                "className": "dt-right",
                //"targets": [2]
            }
        ],
        "order": [
            [1, "asc"]
        ] // set first column as a default sort by asc
    });

    var PartSelectWrapper = jQuery('#PartSelect_wrapper');

    sTable.on('change', 'tbody tr .checkboxes', function () {
        var checked = jQuery(this).is(":checked");
        if (checked) {
            arrayData[arrayData.length] = $(this).parents('tr')[0];
        } else {
            var dx = arrayData.getIndexByValue($(this).parents('tr').children('td:eq(1)').text());
            arrayData.remove(dx); //删除下标为dx的元素
        }
        $(this).parents('tr').toggleClass("active");
    });

    //经常用的是通过遍历,重构数组.
    Array.prototype.remove = function (dx) {
        if (isNaN(dx) || dx > this.length) { return false; }
        for (var i = 0, n = 0; i < this.length; i++) {
            if (this[i] != this[dx]) {
                this[n++] = this[i]
            }
        }
        this.length -= 1
    }

    //在数组中获取指定值的元素索引
    Array.prototype.getIndexByValue = function (value) {
        var index = -1;
        for (var i = 0; i < this.length; i++) {
            if ($('td:eq(3)', this[i]).text() == value) {
                index = i;
                break;
            }
        }
        return index;
    }
}

function initPartsTable() {
    if (!jQuery().dataTable) {
        return;
    }

    pTable = partstable.dataTable({
        "language": {
            "sProcessing": "处理中...",
            "sLengthMenu": "显示 _MENU_ 项结果",
            "sZeroRecords": "没有匹配结果",
            "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
            "sInfoPostFix": "",
            "sSearch": "搜索:",
            "sUrl": "",
            "sEmptyTable": "表中数据为空",
            "sLoadingRecords": "载入中...",
            "sInfoThousands": ",",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            },
            "oAria": {
                "sSortAscending": ": 以升序排列此列",
                "sSortDescending": ": 以降序排列此列"
            }
        },
        "columns": [
            { "sTitle": "FId", "sClass": "hiddenCol" },
            { "sTitle": "FItemId", "sClass": "hiddenCol" },
            { "sTitle": "序号", "sClass": "dt-center table-td-vertical-align" },
            { "sTitle": "零件内码", "sClass": "hiddenCol" },
            { "sTitle": "零件编号", "sClass": "table-td-vertical-align" },
            { "sTitle": "零件名称", "sClass": "table-td-vertical-align" },
            { "sTitle": "规格型号", "sClass": "table-td-vertical-align" },
            { "sTitle": "单位", "sClass": "table-td-vertical-align" },
            { "sTitle": "数量" },
            { "sTitle": "仓库", "sClass": "table-td-vertical-align" },
            { "sTitle": "操作", "sClass": "dt-center table-td-vertical-align" }
        ],
        "aoColumnDefs": [
            { "sWidth": "20px", "aTargets": [2] },
            { "sWidth": "150px", "aTargets": [4] },
            { "sWidth": "150px", "aTargets": [5] },
            { "sWidth": "80px", "aTargets": [6] },
            { "sWidth": "40px", "aTargets": [7] },
            { "sWidth": "60px", "aTargets": [8] },
            { "sWidth": "100px", "aTargets": [9] },
            { "sWidth": "40px", "aTargets": [10] },
        ],
        "bFilter": false, //过滤功能 
        "bSort": false, //排序功能 
        "bInfo": false,//页脚信息 
        "bPaginate": false, //翻页功能
        "bLengthChange": false, //改变每页显示数据数量
        "pageLength": -1,
        "bAutoWidth": false //自动宽度
    });
}
function restoreRow(oTable, nRow) {
    var aData = oTable.fnGetData(nRow);
    var jqTds = $('>td', nRow);

    for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
        oTable.fnUpdate(aData[i], nRow, i, false);
    }

    oTable.fnDraw();
}

var RowNum = 2;

function editRow(oTable, nRow) {
    var aData = oTable.fnGetData(nRow);
    var jqTds = $('>td', nRow);
    jqTds[0].innerHTML = '<input type="text" class="form-control input-sm" name="FId" value="' + aData[0] + '">';
    jqTds[1].innerHTML = '<input type="text" class="form-control input-sm" name="FItemId" value="' + aData[1] + '">';
    jqTds[2].innerHTML = '<span name="FEntryId">' + aData[2] + '</span>';
    //jqTds[3].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FObjectTypeId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    //jqTds[4].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FObjectId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    jqTds[3].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FProjectId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    jqTds[4].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FEquipmentPartsId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    jqTds[5].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FOperationClassId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    jqTds[6].innerHTML = '<div class="control-group  form-group-sm">' + $("select[name='FOperatorId']", oTable[0].rows[1]).prop('outerHTML') + '</div>';
    jqTds[7].innerHTML = '<input type="text" class="form-control input-sm dt-center table-td-vertical-align" name="FDescription" />';

    $("select", nRow).select2({
        placeholder: '请选择',
        minimumResultsForSearch: 20,
        language: "zh-CN",//汉化
    });

    $("select[name='FOperationClassId']", nRow).on("change", function () {
        var FOperationClassId = $(this).val();
        $("select[name='FOperatorId']", nRow).empty().prepend("<option value=''>==请选择==</option>");
        if (!!FOperationClassId) {
            $("select[name='FOperatorId']", nRow).bindSelect({
                url: "/SystemDocument/OperationClassMember/GetSelectJson",
                id: "FId",
                text: "FRealName",
                param: { keyValue: FOperationClassId }
            });
        }
    });

    $("select[name='FProjectId']", nRow).val(aData[5]).trigger("change");
    $("select[name='FEquipmentPartsId']", nRow).val(aData[6]).trigger("change");
    $("select[name='FOperationClassId']", nRow).val(aData[7]).trigger("change");
    $("select[name='FOperatorId']", nRow).val(aData[8]).trigger("change");
    $("input[name='FDescription']", nRow).val(aData[9]);

    RowNum++;
}

function editPartRow(pTable, nRow) {
    var aData = pTable.fnGetData(nRow);
    var jqTds = $('>td', nRow);
    jqTds[0].innerHTML = '<span name="FId">' + aData[0] + '<\span>';
    jqTds[1].innerHTML = '<span name="FEntryId">' + aData[1] + '<\span>';
    jqTds[2].innerHTML = '<span name="FEntryId">' + aData[2] + '<\span>';
    jqTds[3].innerHTML = '<span name="FPartsId">' + aData[3] + '<\span>';
    jqTds[4].innerHTML = '<span name="FNumber">' + aData[4] + '<\span>';
    jqTds[5].innerHTML = '<span name="FFullName">' + aData[5] + '<\span>';
    jqTds[6].innerHTML = '<span name="FModel">' + aData[6] + '<\span>';
    jqTds[7].innerHTML = '<span name="FUnitId">' + aData[7] + '<\span>';
    jqTds[8].innerHTML = '<div class="control-group  form-group-sm"><input type="text" class="form-control input-small" value="' + aData[8] + '" style="width: 100%!important" name="FQty"></div>';
    jqTds[9].innerHTML = '<span name="FStock">' + aData[9] + '<\span>';
    jqTds[10].innerHTML = '<a href="javascript:;" class="btn btn-sm blue" name="delPartRow"><i class="fa fa-times"></i> 删除</a>';

    $("input[name='FQty']", nRow).inputmask('decimal', {
        //rightAlignNumerics: false,
        rightAlign: false,
        nullable: false
    });

    $("a[name='delPartRow']", nRow).on("click", function () {
        if (confirm("确认删除这行吗？") == false) {
            return;
        }
        pTable.fnDeleteRow(nRow);
    });
}

function btn_addRow() {
    var aiNew = oTable.fnAddData(['', '', RowNum, '', '', '', '', '', '', '']);
    var nRow = oTable.fnGetNodes(aiNew[0]);
    editRow(oTable, nRow);
    nEditing = aiNew;
    nNew = true;
};

function btn_deleteRow() {
    if (confirm("确认删除这行吗？") == false) {
        return;
    }
    if (oTable[0].rows.length === 2) {
        $.modalAlert("项目不能为空！", "warning");
        return;
    }
    var nRow = currentRow;
    oTable.fnDeleteRow(nRow);
};

$("#SelectSubmit").on('click', function (e) {
    e.preventDefault();
    if (arrayData.length > 0) {
        $(arrayData).each(function (i, rows) {
            var aData = sTable.fnGetData(rows);
            var aiNew = pTable.fnAddData(['', '', i + 1, aData.FId, aData.FNumber, aData.FFullName, aData.FModel, aData.FUnit, '0', aData.FWarehouse, '']);
            var nRow = pTable.fnGetNodes(aiNew[0]);
            editPartRow(pTable, nRow);
        });
    }
});

function btn_save() {
    if (!$('#form').formValid()) {
        return false;
    }

    var dataHead = new Array();
    var dataEquipEntry = new Array();
    var dataPartsEntry = new Array();
    dataHead = [];
    dataEquipEntry = [];
    dataPartsEntry = [];

    var EquiptableObj = oTable[0];
    var PartstableObj = pTable[0];

    var FId = $("#FId").val();
    var FNumber = $("#FNumber").val();
    var FName = $("#FName").val();
    var FOrganizeId = $("#FOrganizeId").val();
    var FOperationTypeId = $("#FOperationTypeId").val();
    var FOperationLevelId = $("#FOperationLevelId").val();
    var FCyclicTypeId = $("#FCyclicTypeId").val();
    var FStartDate = $("#FStartDate").val();
    var FEndDate = $("#FEndDate").val();
    var FInterval = $("#FInterval").val();
    var FIsCreateTask = $("#FIsCreateTask").prop('checked');
    var FDescription = $("#FDescription").val();

    dataHead.push({
        "FId": FId, "FNumber": FNumber, "FName": FName, "FOrganizeId": FOrganizeId, "FOperationTypeId": FOperationTypeId
        , "FOperationLevelId": FOperationLevelId, "FCyclicTypeId": FCyclicTypeId, "FStartDate": FStartDate
        , "FEndDate": FEndDate, "FInterval": FInterval, "FIsCreateTask": FIsCreateTask, "FDescription": FDescription
    });

    dataHead = JSON.stringify(dataHead[0]);

    var Equip_FId, Equip_FItemId, Equip_FEntryId, Equip_FObjectTypeId, Equip_FObjectId, Equip_FProjectId
        , Equip_FOperationProjectId, Equip_FOperationClassId, Equip_FOperatorId, Equip_FDescription;

    for (var i = 1; i < EquiptableObj.rows.length; i++) {
        Equip_FId = $("input[name='FId']", EquiptableObj.rows[i]).val().trim();
        //Equip_FItemId = $("input[name='FItemId']", EquiptableObj.rows[i]).val().trim();
        Equip_FEntryId = $("span[name='FEntryId']", EquiptableObj.rows[i]).text().trim();
        Equip_FObjectTypeId = $("select[name='FObjectTypeId']", EquiptableObj.rows[i]).val().trim();
        Equip_FObjectId = $("select[name='FObjectId']", EquiptableObj.rows[i]).val().trim();
        Equip_FProjectId = $("select[name='FProjectId']", EquiptableObj.rows[i]).val().trim();
        Equip_FOperationProjectId = $("select[name='FOperationProjectId']", EquiptableObj.rows[i]).val().trim();
        Equip_FOperationClassId = $("select[name='FOperationClassId']", EquiptableObj.rows[i]).val().trim();
        Equip_FOperatorId = $("select[name='FOperatorId']", EquiptableObj.rows[i]).val().trim();
        Equip_FDescription = $("input[name='FDescription']", EquiptableObj.rows[i]).val().trim();

        dataEquipEntry.push({
            "FId": Equip_FId, "FItemId": FId, "FEntryId": Equip_FEntryId, "FObjectTypeId": Equip_FObjectTypeId
            , "FObjectId": Equip_FObjectId, "FProjectId": Equip_FProjectId, "FOperationProjectId": Equip_FOperationProjectId
            , "FOperationClassId": Equip_FOperationClassId, "FOperatorId": Equip_FOperatorId, "FDescription": Equip_FDescription
        });
    }

    dataEquipEntry = JSON.stringify(dataEquipEntry);

    if ((PartstableObj.rows.length > 1) && (PartstableObj.rows[1].childElementCount > 1)) {

        var Parts_FId, Parts_FItemId, Parts_FEntryId, Parts_FPartsId, Parts_FNumber
            , Parts_FFullName, Parts_FModel, Parts_FUnitId, Parts_FQty, Parts_FStock

        for (var i = 1; i < PartstableObj.rows.length; i++) {
            Parts_FId = $("span[name='FId']", PartstableObj.rows[i]).text().trim();
            //Parts_FItemId = $("input[name='FItemId']", PartstableObj.rows[i]).val().trim();
            Parts_FEntryId = $("span[name='FEntryId']", PartstableObj.rows[i]).text().trim();
            Parts_FPartsId = $("span[name='FPartsId']", PartstableObj.rows[i]).text().trim();
            Parts_FNumber = $("span[name='FNumber']", PartstableObj.rows[i]).text().trim();
            Parts_FFullName = $("span[name='FFullName']", PartstableObj.rows[i]).text().trim();
            Parts_FModel = $("span[name='FModel']", PartstableObj.rows[i]).text().trim();
            Parts_FUnitId = $("span[name='FUnitId']", PartstableObj.rows[i]).text().trim();
            Parts_FQty = $("input[name='FQty']", PartstableObj.rows[i]).val().trim();
            Parts_FStock = $("span[name='FStock']", PartstableObj.rows[i]).text().trim();

            dataPartsEntry.push({
                "FId": Parts_FId, "FItemId": FId, "FEntryId": Parts_FEntryId, "FPartsId": Parts_FPartsId
                , "FNumber": Parts_FNumber, "FFullName": Parts_FFullName, "FModel": Parts_FModel
                , "FUnitId": Parts_FUnitId, "FQty": Parts_FQty, "FStock": Parts_FStock
            });
        }

        dataPartsEntry = JSON.stringify(dataPartsEntry);
    }
    $.ajax({
        type: "POST",
        url: "/SystemBusiness/OperationalPlan/SubmitForm",
        contentType: "application/json",
        dataType: "json", //表示返回值类型，不必须
        data: JSON.stringify({
            "dataHead": dataHead,
            "dataEquipEntry": dataEquipEntry,
            "dataPartsEntry": dataPartsEntry
        }),
        //dataType: "json",
        success: function () {
            saved = true;
            $.modalMsg("保存成功！", "success");
            dataHead = [];
            dataEntry = [];
            GoodsArray = [];
        },
        error: function (data) {
            $.modalMsg("保存失败！", "error");
            //window.location.reload();
        }
    });
}

// validation using icons
var handleValidation = function () {

    var form = $('#form');

    form.validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block help-block-error', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "", // validate all fields including form hidden input
        rules: {
            //FNumber: {
            //    //minlength: 2,
            //    required: true
            //},
            FName: {
                required: true,
                //email: true
            },
            FOrganizeId: {
                required: true
            },
            FOperationTypeId: {
                required: true
            },
            FOperationLevelId: {
                required: true
            },
            FCyclicTypeId: {
                required: true
            },
            FStartDate: {
                required: true
            },
            FEndDate: {
                required: true
            },
            //number: {
            //    required: true,
            //    number: true
            //},
            FInterval: {
                required: true,
                digits: true
            },
            FObjectTypeId: {
                required: true
            },
            FObjectId: {
                required: true
            },
            FProjectId: {
                required: true
            },
            FOperationProjectId: {
                required: true
            },
            FOperationClassId: {
                required: true
            },
            FOperatorId: {
                required: true
            },
        },

        errorPlacement: function (error, element) { // render error placement for each input type
            if (element.parents('.mt-radio-list') || element.parents('.mt-checkbox-list')) {
                if (element.parents('.mt-radio-list')[0]) {
                    error.appendTo(element.parents('.mt-radio-list')[0]);
                }
                if (element.parents('.mt-checkbox-list')[0]) {
                    error.appendTo(element.parents('.mt-checkbox-list')[0]);
                }
            } else if (element.parents('.mt-radio-inline') || element.parents('.mt-checkbox-inline')) {
                if (element.parents('.mt-radio-inline')[0]) {
                    error.appendTo(element.parents('.mt-radio-inline')[0]);
                }
                if (element.parents('.mt-checkbox-inline')[0]) {
                    error.appendTo(element.parents('.mt-checkbox-inline')[0]);
                }
            } else if (element.parent(".input-group").size() > 0) {
                error.insertAfter(element.parent(".input-group"));
            } else if (element.attr("data-error-container")) {
                error.appendTo(element.attr("data-error-container"));
            } else {
                error.insertAfter(element); // for other inputs, just perform default behavior
            }
        },

        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        unhighlight: function (element) { // revert the change done by hightlight
            $(element)
                .closest('.form-group').removeClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label
                .closest('.form-group').removeClass('has-error'); // set success class to the control group
        },

        submitHandler: function (form) {
            form[0].submit(); // submit the form
        }

    });

    //apply validation on select2 dropdown value change, this only needed for chosen dropdown integration.
    $('.select2me', form).change(function () {
        form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
    });

    $('.form_datetime .form-control').change(function () {
        form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input 
    });
}

//计算天数差的函数，通用  
function DateDiff(sDate1, sDate2) {    //sDate1和sDate2是2002-12-18格式  
    var aDate, oDate1, oDate2, iDays
    aDate = sDate1.split("-")
    oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0])    //转换为12-18-2002格式  
    aDate = sDate2.split("-")
    oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0])
    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24)    //把相差的毫秒数转换为天数  
    return iDays
}

function getdataItems(dataItem) {
    var dataItems = new Array();
    for (var i in top.clients.dataItems[dataItem]) {
        dataItems.push({ "id": i, "text": top.clients.dataItems[dataItem][i] });
    } 
    return dataItems;
}

function getUserItems() {
    var dataItems = new Array();
    for (var i in top.clients.user) {
        dataItems.push({ "id": i, "text": top.clients.user[i].fullname });
    }
    return dataItems;
    
}

//根据作业类型、作业级别、对象类型、对象获取经验库数据
function getExpWare(foperationTypeId, foperationLevelId, fequipTypeId, fitem, fkeyword) {
    if ((!!foperationTypeId) && (!!foperationLevelId) && (!!fequipTypeId) && (!!fitem)) {
        if (fequipTypeId == "Equipment") {
            fequipTypeId = "";
        }
        if (fequipTypeId == "EquipmentType") {
            fequipTypeId = fitem;
            fitem = "";
        }

        $("#FExpWareId").bindSelect({
            url: "/SystemDocument/ExpWare/GetSelectJson",
            id: "FId",
            text: "FShortName",
            param: { FOperationTypeId: foperationTypeId, FOperationLevelId: foperationLevelId, FEquipTypeId: fequipTypeId, itemId: fitem, keyword: fkeyword }
        });
    }
}

function getOperationItem(ItemId, FOrgId) {
    if (!!ItemId) {
        $("#FObjectId").empty().prepend("<option value=''>==请选择==</option>");
        $("#FOperationProjectId").empty().prepend("<option value=''>==请选择==</option>");
        if (ItemId === 'Equipment') {
            $("#FObjectId").bindSelect({
                url: "/SystemDocument/Equipment/GetPermissionGridJson",
                param: { FObjectType: 'Organize', itemId: FOrgId, keyword: '' },
                id: "FId",
                text: "FShortName",
                search: true
            });

        } else if (ItemId === 'EquipmentType') {
            $("#FObjectId").bindSelect({
                url: "/SystemDocument/EquipmentType/GetTreeSelectJson",
                search: true
            });
        }
    }
}