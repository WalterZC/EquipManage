var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
$(function () {
    gridList();
});

function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/OperationItem/GetPartGridJson?itemId=" + itemId,
        height: $(window).height() - 96,
        sync: false,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true, classes: 'table-td-vertical-align' },
            {
                label: '缩略图', name: 'FFileName', width: 150, align: 'center', classes: 'table-td-vertical-align',
                formatter: function (cellvalue, options, rowObject) {
                    return cellvalue == null ? '<img style="width: 150px; height: 90px;" class="img-responsive" src="/Content/img/no-image330x250.png" alt="无照片" />' : '<img style="width: 150px; height: 90px;" class="img-responsive" src="/Files/PartsImg/' + cellvalue + '.jpg" />';
                }
            },
            {
                label: '系统', name: 'FSystemId', width: 150, align: 'center', classes: 'table-td-vertical-align',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.dataItems["EquipmentSystem"][cellvalue] == null ? "" : top.clients.dataItems["EquipmentSystem"][cellvalue];
                }
            },
            { label: '名称', name: 'FName', width: 150, align: 'center', classes: 'table-td-vertical-align' }
        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            url: "/SystemDocument/OperationItem/GetPartGridJson",
            postData: { itemId: itemId, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    if (!itemId) {
        return false;
    }
    $.modalOpen({
        id: "ItemAdd",
        title: "新增项目",
        url: "/SystemDocument/OperationProject/PartForm?itemId=" + itemId,
        width: "600px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var itemName = $("#gridList").jqGridRowValue().FName;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "PartDetail",
        title: itemName + " 》修改项目",
        url: "/SystemDocument/OperationProject/PartForm?keyValue=" + keyValue,
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
        id: "Details",
        title: "查看项目",
        url: "/SystemDocument/OperationProject/PartFormDetails?keyValue=" + keyValue,
        width: "450px",
        height: "490px",
        btn: null,
    });
}