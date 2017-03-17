var TableDatatablesManaged = function () {

    var initTable = function () {

        var table = $('#EnergyItemTable');

        table.dataTable({
            "processing": true,
            //"serverSide": true,
            "bAutoWidth": false,//自动宽度
            "ajax": {
                "url": "/SystemDocument/EnergyItem/GetGridJson?keyword=",
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
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"/><span></span></label>';
                    },
                    "bSortable": false,
                    "sWidth": "20px;"
                },
                { "data": "FId", "bSortable": false, "sTitle": "FId", "sClass": "hiddenCol" },
                { "data": "FNumber", "sTitle": "能源编码", "sWidth": "25%;" },
                { "data": "FFullName", "sTitle": "能源名称", "sWidth": "25%;" },
                { "data": "FUnit", "bSortable": false, "sTitle": "计量单位", "sWidth": "20%;" },
                { "data": "FDescription", "bSortable": false, "sTitle": "能源备注" }
            ],
            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],
            "pageLength": 5,
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

        var tableWrapper = jQuery('#EnergyItemTable_wrapper');

        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).prop("checked", true);
                    $(this).parents('tr').addClass("active");
                    AddFId($(this).parents('tr').children('td.hiddenCol').text());
                } else {
                    $(this).prop("checked", false);
                    $(this).parents('tr').removeClass("active");
                    RemodeFId($(this).parents('tr').children('td.hiddenCol').text());
                }
            });
        });

        table.on('change', 'tbody tr .checkboxes', function () {
            $(this).parents('tr').toggleClass("active");
            if ($(this).prop('checked')) {
                AddFId($(this).parents('tr').children('td.hiddenCol').text());
            } else {
                RemodeFId($(this).parents('tr').children('td.hiddenCol').text());
            }
        });

        function AddFId(FID) {
            if ((FID == "") || (FID == null) || (FID == undefined)) {
                return;
            }
            var orgVal = $("#FEnergys").val();
            var currentVal;
            if ((orgVal == "") || (orgVal == null) || (orgVal == undefined)) {
                currentVal = FID;
            } else {
                currentVal = orgVal + "," + FID;
            }

            $("#FEnergys").val(currentVal);
        }

        function RemodeFId(FID) {
            if ((FID == "") || (FID == null) || (FID == undefined)) {
                return;
            }
            var orgVal = $("#FEnergys").val();
            var currentVal;
            if (orgVal.indexOf(',')==-1) {
                currentVal = orgVal.replace((FID), "");;
            } else {
                currentVal = orgVal.replace((',' + FID), "");
            }

            $("#FEnergys").val(currentVal);
        }
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
    TableDatatablesManaged.init();
});
