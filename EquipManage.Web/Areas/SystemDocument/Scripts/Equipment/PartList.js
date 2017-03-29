var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
$(function () {
    $('#layout').layout();
    treeView();
    gridList();
});
function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/OperationItem/GetEquipmentProjectItemTreeJson?FEquipmentId=" + keyValue,
        onnodeclick: function (item) {
            $("#txt_keyword").val('');
            $('#btn_search').trigger("click");
        }
    });
}
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/OperationItem/GetPartGridJson?itemId=" + keyValue,
        height: $(window).height() - 96,
        sync: false,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true, classes: 'table-td-vertical-align' },
            { label: "设备类型Id", name: "FItemId", hidden: true, key: true, classes: 'table-td-vertical-align' },
            { label: "全称", name: "FFullName", hidden: true, key: true, classes: 'table-td-vertical-align' },
            {
                label: '缩略图', name: 'FFileName', width: 150, align: 'center', classes: 'table-td-vertical-align',
                formatter: function (cellvalue, options, rowObject) {
                    return cellvalue == null ? '<img style="width: 150px; height: 90px;" class="img-responsive" src="/Content/img/no-image330x250.png" alt="无照片" />' : '<img style="width: 150px; height: 90px;" class="img-responsive" src="/Files/PartsImg/' + cellvalue + '.jpg" />';
                }
            },
            { label: "名称", name: "FShortName", width: 100, key: true, classes: 'table-td-vertical-align' },
            {
                label: '系统', name: 'FSystemId', width: 80, align: 'center', classes: 'table-td-vertical-align',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.dataItems["EquipmentSystem"][cellvalue] == null ? "" : top.clients.dataItems["EquipmentSystem"][cellvalue];
                }
            },
            {
                label: '值类型',
                name: 'FValType',
                width: 80,
                align: 'center',
                classes: 'table-td-vertical-align',
                formatter: function (data, options, rowObject) {
                    var returnVal;
                    switch (data) {
                        case "FCheck":
                            returnVal = "单选"
                            break;
                        case "FDecimal":
                            returnVal = "数值"
                            break;
                        case "FString":
                            returnVal = "文字"
                            break;
                        case "FMultiCheck":
                            returnVal = "多选"
                            break;
                        default:
                            returnVal = "其他"
                    }
                    return returnVal;
                }
            },
            { label: '最大值', name: 'FMaxVal', width: 80, align: 'center', classes: 'table-td-vertical-align' },
            { label: '最小值', name: 'FMinVal', width: 80, align: 'center', classes: 'table-td-vertical-align' },
            { label: '类型', name: 'FItemType',hidden:true, width: 80, align: 'center', classes: 'table-td-vertical-align' }

        ]
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            //url: "/SystemDocument/Equipment/GetPartGridJson",
            url: "/SystemDocument/OperationItem/GetPartGridJson",
            postData: { itemId: $("#itemTree").getCurrentNode().id, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    if (!$("#itemTree").getCurrentNode().id) {
        return false;
    }
    $.modalOpen({
        id: "Form",
        title: "新增部位",
        //url: "/SystemDocument/Equipment/PartForm?itemId=" + itemId,
        url: "/SystemDocument/OperationProject/PartForm?itemId=" + $("#itemTree").getCurrentNode().id + "&FItemType=2",
        //width: "500px",
        //height: "500px",
        width: "600px",
        height: "700px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var itemName = $("#gridList").jqGridRowValue().FShortName;
    var keyValue = $("#gridList").jqGridRowValue().FId;
    var FItemType = $("#gridList").jqGridRowValue().FItemType;
    if ($("#gridList").jqGridRowValue().FItemType == '1') {
        alert("该项目为作业方案明细，不允许修改！");
        return;
    }
    $.modalOpen({
        id: "PartDetail",
        title: itemName + " 》修改部位",
        //url: "/SystemDocument/Equipment/PartForm?keyValue=" + keyValue,
        url: "/SystemDocument/OperationProject/PartForm?keyValue=" + keyValue + "&FItemType=" + FItemType,
        width: "600px",
        height: "700px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    if ($("#gridList").jqGridRowValue().FItemType == '1') {
        alert("该项目为作业方案项目，不允许删除！");
        return;
    }
    $.deleteForm({
        //url: "/SystemDocument/Equipment/DeletePartForm",
        //param: { keyValue: $("#gridList").jqGridRowValue().FId },
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
        title: "查看部位",
        //url: "/SystemDocument/Equipment/PartFormDetails?keyValue=" + keyValue,
        url: "/SystemDocument/OperationProject/PartFormDetails?keyValue=" + keyValue,
        width: "600px",
        height: "700px",
        btn: null,
    });
}