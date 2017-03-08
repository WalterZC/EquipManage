$(function () {
    $('#layout').layout();
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/ChangeType/GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'FId', hidden: true },
            { label: '变更类型编码', name: 'FNumber', width: 120, align: 'left' },
            { label: '变更类型名称', name: 'FFullName', width: 120, align: 'left' },
            { label: '序号', name: 'FSortCode', width: 100, align: 'left' },
            {
                label: '创建时间', name: 'FCreatorTime', width: 100, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: "有效", name: "FEnabledMark", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            { label: '备注', name: 'FDescription', width: 200, align: 'left' }
        ],
        pager: "#gridPager",
        sortname: 'FDepartmentId asc,FCreatorTime desc',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            url: "/SystemDocument/ChangeType/GetGridJson",
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增类型",
        url: "/SystemDocument/ChangeType/Form",
        width: "600px",
        height: "530px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改变更类型",
        url: "/SystemDocument/ChangeType/Form?keyValue=" + keyValue,
        width: "600px",
        height: "530px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/ChangeType/DeleteForm",
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
        id: "Details",
        title: "查看变更类型",
        url: "/SystemDocument/ChangeType/Details?keyValue=" + keyValue,
        width: "600px",
        height: "530px",
        btn: null,
    });
}