
var keyValue = $.request("keyValue");//设备方案ID
var itemId = $.request("itemId");//设备类型ID
var oTable = null;
var currentRow = null;

var equipTable = null;
var EquipmentCurrentRow = null;

$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/OperationProject/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    } else {
        $("#FEquipmentTypeId").val(itemId);
    }
    initTable();
    top.frames["ProjectForm"].$('#Equipmentlayout').layout();
    treeView();
    InitEquipmentTable();
})
function initControl() {
    var FParentNo = "OperationType";

    $("#FOperationTypeId").bindSelect({
        url: "/SystemDocument/ItemsType/GetGridSelectJson",
        id: "FId",
        text: "FFullName",
        param: { itemId: FParentNo, keyword: "" }
    });
    $("#FOperationTypeId").on("change", function () {
        var ItemId = $(this).val();
        if (!!ItemId) {
            $("#FOperationLevelId").bindSelect({
                url: "/SystemDocument/ItemsData/GetGridJson",
                id: "FId",
                text: "FItemName",
                param: { itemId: ItemId, keyword: "" }
            });
        }
    });

    $('#wizard').wizard().on('change', function (e, data) {
        var $finish = $("#btn_finish");
        var $next = $("#btn_next");
        if (data.direction == "next") {
            switch (data.step) {
                case 1:
                    if (!$('#form1').formValid()) {
                        return false;
                    }
                    submitFirstStepForm();

                    break;
                case 2:
                    if (!$('#form1').formValid()) {
                        return false;
                    }
                    $finish.show();
                    $next.hide();

                    break;
                default:
                    break;
            }
        } else {
            $finish.hide();
            $next.show();
        }
    });
}

function initTable() {
    if (!jQuery().dataTable) {
        return;
    }

    var table = $('#PartItemTable');
    //var itemId = $.request("itemId");//设备类型ID

    oTable = table.dataTable({
        "processing": true,
        "bAutoWidth": false,//自动宽度
        "ajax": {
            "url": "/SystemDocument/OperationItem/GetPartGridJson",
            "dataType": "json",
            "data":{"itemId":keyValue},
            "dataSrc": function (data) {
                var json = new Array();
                json["data"] = data;
                return json.data;
            }
        },
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
            {
                "sClass": "text-center",
                "data": "FFileName",
                "render": function (data, type, full, meta) {
                    return data == null ? '<img style="width: 120px; height: 80px;" class="img-responsive" src="/Content/img/no-image330x250.png" alt="无照片" />' : '<img style="width: 150px; height: 90px;" class="img-responsive" src="/Files/PartsImg/' + data + '.jpg" />';
                },
                "bSortable": false,
                "sWidth": "180px;",
                "sTitle": "部位照片"
            },
            { "data": "FId", "bSortable": false, "sTitle": "FId", "sClass": "hiddenCol" },
            { "data": "FItemId", "bSortable": false, "sTitle": "设备类型Id", "sClass": "hiddenCol" },
            //{ "data": "FNumber", "sTitle": "编码" },
            { "data": "FFullName", "sTitle": "全称", "sClass": "hiddenCol" },
            { "data": "FShortName", "bSortable": false, "sTitle": "名称", "sClass": "dt-center table-td-vertical-align" },
            {
                "data": "FSystemId",
                "bSortable": false,
                "sTitle": "所属系统",
                "render": function (data, type, full, meta) {
                    return top.clients.dataItems["EquipmentSystem"][data] == null ? "" : top.clients.dataItems["EquipmentSystem"][data];
                },
                "sClass": "dt-center table-td-vertical-align"
            },
            { "data": "FValType", "bSortable": false, "sTitle": "值类型", "sClass": "dt-center table-td-vertical-align" },
            { "data": "FMaxVal", "bSortable": false, "sTitle": "最大值", "sClass": "dt-center table-td-vertical-align" },
            { "data": "FMinVal", "bSortable": false, "sTitle": "最小值", "sClass": "dt-center table-td-vertical-align" },
            //{ "data": "FContent", "bSortable": false, "sTitle": "选项" },
        ],
        "lengthMenu": [
            [3],
            [3] // change per page values here
        ],
        "bFilter": false, //过滤功能 
        "bSort": false, //排序功能 
        //"bInfo": false,//页脚信息 
        //"bPaginate": false, //翻页功能
        "bLengthChange": false, //改变每页显示数据数量
        "pageLength": 3,
        "bAutoWidth": false,//自动宽度

    });

    var tableWrapper = jQuery('#EnergyItemTable_wrapper');

    oTable.on('click', 'tbody tr', function (e) {
        if ($(this).hasClass('selected')) {
            currentRow = null;//清楚选中行
            $(this).removeClass('selected');
            $(".operate").animate({ "left": '-100.1%' }, 200);
        }
        else {
            currentRow = this;//记录选中行
            oTable.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            $(".operate").animate({ "left": 0 }, 200);
        }
    });
    $(".operate").find('.close').click(function () {
        currentRow = null;//清楚选中行
        oTable.$('tr.selected').removeClass('selected');
        $(".operate").animate({ "left": '-100.1%' }, 200);
    })
}

function submitFirstStepForm() {
    var postData = $("#form1").formSerialize();
    var ItemId = $("#FId").val();
    $.ajax({
        url: "/SystemDocument/OperationProject/SubmitForm?keyValue=" + keyValue,
        type: 'POST',
        data: postData,
        sync: false,
        dataType: "text",
        success: function (data) {
            $("#step-1").find("#FId").val(data);
        }
    });
}
function btn_add() {
    var FEquipTypeID = $("#step-1").find("#FId").val();
    if (!FEquipTypeID) {
        return false;
    }
    $.modalOpen({
        id: "ProjectPartAdd",
        title: "新增项目",
        url: "/SystemDocument/OperationProject/PartForm?itemId=" + FEquipTypeID,
        width: "600px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    //var itemName = $("#PartItemTable").jqGridRowValue().FName;
    //var keyValue = $("#gridList").jqGridRowValue().FId;
    var aData = oTable.fnGetData(currentRow);
    var jqTds = $('>td', currentRow);
    $.modalOpen({
        id: "ProjectPartMod",
        title: aData.FShortName + " 》修改项目",
        url: "/SystemDocument/OperationProject/PartForm?keyValue=" + aData.FId,
        width: "600px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/OperationItem/DeletePartForm",
        param: { keyValue: $("#gridList").jqGridRowValue().FId },
        success: function () {
            $("#gridList").resetSelection();
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "ProjectPartDetail",
        title: "查看项目",
        url: "/SystemDocument/OperationProject/PartFormDetails?keyValue=" + keyValue,
        width: "450px",
        height: "490px",
        btn: null,
    });
}

function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/EquipmentType/GetTreeJson",
        onnodeclick: function (item) {
            equipTable.fnReloadAjax('/SystemDocument/Equipment/GetGridJson?itemId='+item.id);
        }
    });
}

function btn_cloneProjectItem() {
    $.modalOpen({
        id: "ProjectItemClone",
        title: "克隆项目",
        url: "/SystemDocument/OperationItem/ProjectItemClone?FEquipmentTypeId=" + itemId + "&FOperationProjectId=" + keyValue,
        width: "400px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function InitEquipmentTable() {
    if (!jQuery().dataTable) {
        return;
    }

    var table = $('#EquipmentTable');
    var itemId = $.request("itemId");//设备类型ID
    var CheckBoxContent = $("#FItemIds").val();

    equipTable = table.dataTable({
        "processing": true,
        "bAutoWidth": false,//自动宽度
        "ajax": {
            "url": "/SystemDocument/Equipment/GetGridJson?itemId=" + itemId,
            "dataType": "json",
            "dataSrc": function (data) {
                var json = new Array();
                json["data"] = data;
                return json.data;
            }
        },
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
            {
                "sClass": "text-center",
                "data": "FId",
                "render": function (data, type, full, meta) {
                    if (CheckBoxContent.indexOf(data) >= 0) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes" checked="checked"/><span></span></label>';
                    } else {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"/><span></span></label>';

                    }
                },
                "bSortable": false,
                "sWidth": "20px;"
            },
            { "data": "FId", "bSortable": false, "sTitle": "FId", "sClass": "hiddenCol" },
            { "data": "FEquipmentTypeId", "bSortable": false, "sTitle": "设备类型Id", "sClass": "hiddenCol" },
            { "data": "FNumber", "sTitle": "编码", "sWidth": "80px;" },
            //{ "data": "FFullName", "sTitle": "全称", "sClass": "hiddenCol" },
            { "data": "FShortName", "sTitle": "名称", "sClass": "dt-center table-td-vertical-align", "sWidth": "150px;" },
            {"data": "FModel","sTitle": "规格","sClass": "dt-center table-td-vertical-align"},
            //{ "data": "FValType", "bSortable": false, "sTitle": "值类型", "sClass": "dt-center table-td-vertical-align" },
            //{ "data": "FMaxVal", "bSortable": false, "sTitle": "最大值", "sClass": "dt-center table-td-vertical-align" },
            //{ "data": "FMinVal", "bSortable": false, "sTitle": "最小值", "sClass": "dt-center table-td-vertical-align" },
            //{ "data": "FContent", "bSortable": false, "sTitle": "选项" },
        ],
        //"lengthMenu": [
        //    [3],
        //    [3] // change per page values here
        //],
        "bFilter": true, //过滤功能 
        "bSort": true, //排序功能 
        //"bInfo": false,//页脚信息 
        //"bPaginate": false, //翻页功能
        "bLengthChange": false, //改变每页显示数据数量
        "pageLength": 7,
        "bAutoWidth": false,//自动宽度
        "columnDefs": [
               {  // set default column settings
                   'orderable': false,
                   'targets': [0]
               }
        ],

        "order": [
            [3, "asc"]
        ], // set first column as a default sort by asc
    });

    var tableWrapper = jQuery('#EnergyItemTable_wrapper');

    table.on('change', 'tbody tr .checkboxes', function () {
        $(this).parents('tr').toggleClass("selected");
        var aData = equipTable.fnGetData($(this).parents('tr'));
        if ($(this).prop('checked')) {
            EquipmentCurrentRow = $(this).parents('tr');//记录选中行
            AddFId(aData.FId);
        } else {
            EquipmentCurrentRow = null;//清楚选中行
            RemodeFId(aData.FId);
        }
    });

    function AddFId(FID) {
        if ((FID == "") || (FID == null) || (FID == undefined)) {
            return;
        }
        var checkitemlist = new Array();
        var orgVal = $("#FItemIds").val();
        checkitemlist = orgVal.split(",");
        if (!contains(checkitemlist, FID)) {
            checkitemlist.push(FID);
        }

        $("#FItemIds").val(checkitemlist.toString());
    }

    function RemodeFId(FID) {
        if ((FID == "") || (FID == null) || (FID == undefined)) {
            return;
        }
        var checkitemlist = new Array();
        var orgVal = $("#FItemIds").val();
        checkitemlist = orgVal.split(",");
        removeByValue(checkitemlist, FID);

        $("#FItemIds").val(checkitemlist.toString());
    }
    function removeByValue(arr, val) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == val) {
                arr.splice(i, 1);
                break;
            }
        }
    }
    function contains(arr, obj) {
        var i = arr.length;
        while (i--) {
            if (arr[i] === obj) {
                return true;
            }
        }
        return false;
    }
}

function submitForm() {
    var postData = $("#FItemIds").val();

    $.submitForm({
        url: "/SystemDocument/OperationProject/SubmitEquipRelateForm?keyValue=" + keyValue,
        type: 'POST',
        param: { "FItemIds": postData },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
            $.currentWindow().$(".operate").animate({ "left": '-100.1%' }, 50);
        }
    });
}

$.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
    // DataTables 1.10 compatibility - if 1.10 then versionCheck exists.  
    // 1.10s API has ajax reloading built in, so we use those abilities  
    // directly.  
    if ($.fn.dataTable.versionCheck) {
        var api = new $.fn.dataTable.Api(oSettings);

        if (sNewSource) {
            api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
        }
        else {
            api.ajax.reload(fnCallback, !bStandingRedraw);
        }
        return;
    }

    if (sNewSource !== undefined && sNewSource !== null) {
        oSettings.sAjaxSource = sNewSource;
    }

    // Server-side processing should just call fnDraw  
    if (oSettings.oFeatures.bServerSide) {
        this.fnDraw();
        return;
    }

    this.oApi._fnProcessingDisplay(oSettings, true);
    var that = this;
    var iStart = oSettings._iDisplayStart;
    var aData = [];

    this.oApi._fnServerParams(oSettings, aData);

    oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
        /* Clear the old information from the table */
        that.oApi._fnClearTable(oSettings);

        /* Got the data - add it to the table */
        var aData = (oSettings.sAjaxDataProp !== "") ?
            that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

        for (var i = 0 ; i < aData.length ; i++) {
            that.oApi._fnAddData(oSettings, aData[i]);
        }

        oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

        that.fnDraw();

        if (bStandingRedraw === true) {
            oSettings._iDisplayStart = iStart;
            that.oApi._fnCalculateEnd(oSettings);
            that.fnDraw(false);
        }

        that.oApi._fnProcessingDisplay(oSettings, false);

        /* Callback user function - for event handlers etc */
        if (typeof fnCallback == 'function' && fnCallback !== null) {
            fnCallback(oSettings);
        }
    }, oSettings);
};