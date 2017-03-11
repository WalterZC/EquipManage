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
            { label: '名称', name: 'FShortName', width: 150, align: 'left' },
            { label: '编号', name: 'FNumber', width: 150, align: 'left' },

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
            url: "/SystemDocument/Equipment/GetGridJson",
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
        title: itemName + " 》新增设备",
        url: "/SystemDocument/Equipment/Form?itemId=" + itemId,
        width: "900px",
        height: "650px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var itemName = $("#itemTree").getCurrentNode().text;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: itemName + " 》修改类型",
        url: "/SystemDocument/Equipment/Form?keyValue=" + keyValue,
        width: "900px",
        height: "650px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/Equipment/DeleteForm",
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
        title: "查看设备",
        url: "/SystemDocument/Equipment/Details?keyValue=" + keyValue,
        width: "900px",
        height: "650px",
        btn: null,
    });
}
function btn_files() {
    var itemId = $("#itemTree").getCurrentNode().id;
    var itemName = $("#itemTree").getCurrentNode().text;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    if (!itemId) {
        return false;
    }
    $.modalOpen({
        id: "Files",
        title: itemName + " 》设备文件",
        url: "/SystemDocument/Equipment/Files?itemId=" + itemId,
        width: "900px",
        height: "650px",
        btn: null
    });
}
function btn_equipmentstype() {
    $.modalOpen({
        id: "EquipmentType",
        title: "设备类型",
        url: "/SystemDocument/EquipmentType/Index",
        width: "800px",
        height: "550px",
        btn: null,
    });
}