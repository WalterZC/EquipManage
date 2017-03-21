$(function () {
    $('#layout').layout();
    treeView();
    gridList();
});
function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/EquipmentType/GetTreeJson",
        onnodeclick: function (item) {
            $("#txt_keyword").val('');
            $('#btn_search').trigger("click");
        }
    });
}
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '名称', name: 'FItemName', width: 150, align: 'left' },
            { label: '编号', name: 'FItemCode', width: 150, align: 'left' },
            { label: '排序', name: 'FSortCode', width: 80, align: 'center' },
            {
                label: "默认", name: "FIsDefault", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            {
                label: '创建时间', name: 'FCreatorTime', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: "有效", name: "FEnabledMark", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            { label: "备注", name: "FDescription", index: "FDescription", width: 200, align: "left", sortable: false }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            url: "/SystemDocument/OperationProject/GetGridJson",
            postData: { itemId: $("#itemTree").getCurrentNode().id, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    var itemId = $("#itemTree").getCurrentNode().id;
    var itemName = $("#itemTree").getCurrentNode().text;
    if (!itemId) {
        return false;
    }
    $.modalOpen({
        id: "Form",
        title: itemName + " 》新增方案",
        url: "/SystemDocument/OperationProject/Form?itemId=" + itemId,
        width: "650px",
        height: "650px",
        btn: null
    });
}
function btn_edit() {
    var itemName = $("#itemTree").getCurrentNode().text;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: itemName + " 》修改方案",
        url: "/SystemDocument/OperationProject/Form?keyValue=" + keyValue,
        width: "650px",
        height: "650px",
        btn: null
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/OperationProject/DeleteForm",
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
        title: "查看方案",
        url: "/SystemDocument/OperationProject/Details?keyValue=" + keyValue,
        width: "450px",
        height: "470px",
        btn: null,
    });
}