var currentRow = null; 
var TableDatatablesScroller = function () {
    var initTable = function () {
        var table = $('#DataList');
        var btn_delete = $("#EM-delete");
        var btn_detail = $("#EM-detail");
        var btn_edit = $("#EM-edit");
        var btn_check = $("#EM-check");


        var oTable = table.dataTable({
            "processing": true,
            "serverSide": true,
            "bAutoWidth": true,//自动宽度
            "ajax": {
                "type": "POST",
                "url": "/SystemBusiness/OperationalPlan/GetGridJson",
                "dataType": "json",
                "cache": false,  //禁用缓存
                "data": function (data) {//添加额外的数据给服务器
                    data.page = (data.start / data.length) + 1;
                    data.rows = data.length;
                    data.sord = 'asc';
                    data.sidx = 'FNumber asc';
                },
                "dataSrc": function (data) {
                    data.recordsTotal = data.records;
                    data.recordsFiltered = data.records;
                    return data.rows;
                }
            },
            "columns": [
                { "data": "FId", "bSortable": false, "sTitle": "FId", "sClass": "hiddenCol" },
                { "data": "FNumber", "sTitle": "计划编号"},
                { "data": "FName", "sTitle": "计划名称"},
                {
                    "data": "FOrganizeId", "sTitle": "组织部门",
                    "render": function (data, type, full, meta) {
                        return top.clients.organize[data]["fullname"] == null ? "" : top.clients.organize[data]["fullname"];
                    }
                },
                {
                    "data": "FOperationTypeId", "sTitle": "作业类型",
                    "render": function (data, type, full, meta) {
                        return top.clients.dataItemsFid[data] == null ? "" : top.clients.dataItemsFid[data];
                    }
                },
                {
                    "data": "FOperationLevelId", "sTitle": "作业级别",
                    "render": function (data, type, full, meta) {
                        return top.clients.dataItemsFid[data] == null ? "" : top.clients.dataItemsFid[data];
                    }
                },
                {
                    "data": "FCyclicTypeId",
                    "sTitle": "时间单位",
                    "render": function (data, type, full, meta) {
                        var result;
                        switch (data){
                            case 1:
                                result = '天'
                                break;
                            case 2:
                                result = '周'
                                break;
                            case 3:
                                result = '月'
                                break;
                            case 4:
                                result = '季度'
                                break;
                            case 5:
                                result = '年'
                                break;
                            default:
                                result = ''
                                break;    
                        }
                        return result;
                    }
                },
                { "data": "FStartDate", "sTitle": "开始日期" },
                { "data": "FEndDate", "sTitle": "截至日期"},
                { "data": "FInterval", "sTitle": "日期间隔"},
                {
                    "data": "FIsCreateTask",
                    "sTitle": "有效",
                    "bSortable": false, 
                    "sClass": "text-center",
                    "render": function (data, type, full, meta) {
                        return data == 1 ? "<i class=\"fa fa-toggle-on  font-green\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                    }
                },
                { "data": "FCreatorTime", "sTitle": "制作日期" },
                {
                    "data": "FCreatorUserId", "sTitle": "制作人",
                    "render": function (data, type, full, meta) {
                        return top.clients.user[data] == null ? "" : top.clients.user[data]["fullname"];
                    }
                },
                { "data": "FCheckTime", "sTitle": "审核日期" },
                {
                    "data": "FCheckerId", "sTitle": "审核人",
                    "render": function (data, type, full, meta) {
                        return top.clients.user[data]== null ? "" : top.clients.user[data]["fullname"];
                    }
                },
                {
                    "data": "FCheckMark", "sTitle": "审核状态",
                    "bSortable": false, 
                    "render": function (data, type, full, meta) {
                        if (data == 0) {
                            return '<td><span style="color:red;" name="FCheckMark">未审核</span></td>'
                        }
                        else if (data == 1) {
                            return '<td><span style="color:green;" name="FCheckMark">已审核</span></td>'
                        }
                    }
                },
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
            "bFilter": true, //过滤功能 
            "bSort": true, //排序功能 
            "bInfo": true,//页脚信息 
            "bPaginate": true, //翻页功能
            "bAutoWidth": true, //自动宽度
            stateSave: true,
            "pagingType": "bootstrap_full_number",
            "order": [
                [1, 'asc']
            ],
            "lengthMenu": [
                [10, 20,100, 500,1000],
                [10, 20,100, 500,1000] // change per page values here
            ],
            "pageLength": 10,
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
                //var aData = oTable.fnGetData(currentRow);
            }
        });

        //删除按钮事件
        btn_delete.on("click", function (e) {
            e.preventDefault();
            if (currentRow == null) {
                $.modalMsg("请先选择一条需要删除的单据～", 'warning');
                return;
            }
            var aData = oTable.fnGetData(currentRow);
            if ($('span[name="FCheckMark"]', currentRow).text().trim()=='已审核') {
                $.modalMsg("该单据已经审核,请先反审核~", 'warning');
                return;
            }
            var aData = oTable.fnGetData(currentRow);

            $.deleteForm({
                url: "/SystemBusiness/OperationalPlan/Delete",
                param: { FId: aData.FId },
                success: function () {
                    oTable.fnDeleteRow(currentRow);
                    currentRow = null;
                }
            });
        });
    }
    return {

        //main function to initiate the module
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }
            initTable();
        }

    };

}();

jQuery(document).ready(function () {
    TableDatatablesScroller.init();
});