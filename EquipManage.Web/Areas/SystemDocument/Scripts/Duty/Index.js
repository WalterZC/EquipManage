$(function () {
    $('#layout').layout();
    treeView();
    gridList();
});
function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/UserItemAuthorize/GetUseItemPermissionTree",
        param: { FObjectType: "Organize" },
        onnodeclick: function (item) {
            $("#txt_keyword").val('');
            $('#btn_search').trigger("click");
        }
    });
}

function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/Duty/GetPermissionGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '岗位名称', name: 'FFullName', width: 150, align: 'left' },
            { label: '岗位编号', name: 'FEnCode', width: 150, align: 'left' },
            {
                label: '归属机构', name: 'FOrganizeId', width: 150, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.organize[cellvalue] == null ? "" : top.clients.organize[cellvalue].fullname;
                }
            },
            {
                label: '创建时间', name: 'FCreatorTime', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: "有效", name: "FEnabledMark", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            { label: '备注', name: 'FDescription', width: 300, align: 'left' }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { itemId: $("#itemTree").getCurrentNode().id, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增岗位",
        url: "/SystemDocument/Duty/Form",
        width: "450px",
        height: "380px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改岗位",
        url: "/SystemDocument/Duty/Form?keyValue=" + keyValue,
        width: "450px",
        height: "380px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/Duty/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().FId },
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Details",
        title: "查看岗位",
        url: "/SystemDocument/Duty/Details?keyValue=" + keyValue,
        width: "450px",
        height: "500px",
        btn: null,
    });
}