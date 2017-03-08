
$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/Parts/GetGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '类别', name: 'FCategory', width: 50, align: 'left' },
            { label: '编号', name: 'FNumber', width: 150, align: 'left' },
            { label: '名称', name: 'FFullName', width: 150, align: 'left' },
            { label: '图号', name: 'FFigure', width: 80, align: 'left' },
            { label: '规格型号', name: 'FModel', width: 80, align: 'left' },
            { label: '单位', name: 'FUnit', width: 50, align: 'left' },
            { label: '成本价格', name: 'FCost', width: 80, align: 'left' },
            {
                label: '归属机构', name: 'FOrganizeId', width: 150, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.organize[cellvalue] == null ? "" : top.clients.organize[cellvalue].fullname;
                }
            },
            { label: '仓库', name: 'FWarehouse', width: 80, align: 'left' },
            { label: '仓位', name: 'FWarehousePlace', width: 80, align: 'left' },
            { label: '存放位置', name: 'FPlace', width: 80, align: 'left' },
            { label: '保管员', name: 'FStorekeeper', width: 80, align: 'left' },
            { label: '零件供应商', name: 'FSupply', width: 100, align: 'left' },
            { label: '生产厂商', name: 'FManufacturer', width: 100, align: 'left' },
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
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增零件",
        url: "/SystemDocument/Parts/Form",
        width: "650px",
        height: "550px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改零件",
        url: "/SystemDocument/Parts/Form?keyValue=" + keyValue,
        width: "650px",
        height: "550px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/Parts/DeleteForm",
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
        title: "查看零件",
        url: "/SystemDocument/Parts/Details?keyValue=" + keyValue,
        width: "650px",
        height: "550px",
        btn: null,
    });
}