$(function () {
    $('#layout').layout();
    treeView();
    gridList();
});
function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/ExpWare/GetTreeJson",
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
        var itemId = '';
        var itemName = '';
        var FEquipTypeId = '';
        if ($("#itemTree").getCurrentNode().img == "fa fa-file-text-o") {
            itemId = $("#itemTree").getCurrentNode().id;
            itemName = $("#itemTree").getCurrentNode().text;
            FEquipTypeId = $("#itemTree").getCurrentNode().parent.id;
        } else if ($("#itemTree").getCurrentNode().img == "fa fa-folder-open") {
            //itemId = $("#itemTree").getCurrentNode().id;
            itemName = $("#itemTree").getCurrentNode().text;
            FEquipTypeId = $("#itemTree").getCurrentNode().id;
        }

        $gridList.jqGrid('setGridParam', {
            url: "/SystemDocument/ExpWare/GetGridJson",
            postData: { FEquipTypeId: FEquipTypeId, itemId: itemId, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    var itemId = '';
    var itemName = '';
    var FEquipTypeId = '';
    if ($("#itemTree").getCurrentNode().img == "fa fa-file-text-o") {
        itemId = $("#itemTree").getCurrentNode().id;
        itemName = $("#itemTree").getCurrentNode().text;
        FEquipTypeId = $("#itemTree").getCurrentNode().parent.id;
    } else if ($("#itemTree").getCurrentNode().img == "fa fa-folder-open") {
        //itemId = $("#itemTree").getCurrentNode().id;
        itemName = $("#itemTree").getCurrentNode().text;
        FEquipTypeId = $("#itemTree").getCurrentNode().id;
    }

    if (!FEquipTypeId) {
        return false;
    }
    $.modalOpen({
        id: "Form",
        title: itemName + " 》新增经验",
        url: "/SystemDocument/ExpWare/Form?itemId=" + itemId + "&FEquipTypeId=" + FEquipTypeId,
        width: "900px",
        height: "650px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var itemName = $("#gridList").jqGridRowValue().FShortName;
    var treeName = $("#itemTree").getCurrentNode().text;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: treeName + " 》" + itemName + " 》修改经验",
        url: "/SystemDocument/ExpWare/Form?keyValue=" + keyValue,
        width: "900px",
        height: "650px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/ExpWare/DeleteForm",
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
        title: "查看经验",
        url: "/SystemDocument/ExpWare/Details?keyValue=" + keyValue,
        width: "900px",
        height: "650px",
        btn: null,
    });
}