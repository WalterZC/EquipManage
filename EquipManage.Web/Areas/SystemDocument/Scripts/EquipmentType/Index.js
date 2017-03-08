$(function () {
    gridList();
})
function gridList() {
    $("#gridList").dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        ExpandColumn: "FNumber",
        url: "/SystemDocument/EquipmentType/GetTreeGridJson",
        height: $(window).height() - 90,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '名称', name: 'FFullName', width: 200, align: 'left' },
            { label: '编号', name: 'FNumber', width: 100, align: 'left' },
            { label: '设备规格', name: 'FStandard', width: 80, align: 'center' },
            { label: '设备型号', name: 'FModel', width: 80, align: 'center' },
            { label: '机械系数', name: 'FMachineCoefficient', width: 80, align: 'center' },
            { label: '电子系数', name: 'FElectricCoefficient', width: 80, align: 'center' },
            {
                label: "有效", name: "FEnabledMark", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            {
                label: "变更单", name: "FIfUseChange", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            {
                label: "精密", name: "FIfPrecision", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            {
                label: "稀有", name: "FIfRare", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            {
                label: "大型", name: "FIfLarge", width: 60, align: "center",
                formatter: function (cellvalue) {
                    return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                }
            },
            { label: "备注", name: "FDescription", index: "FDescription", width: 200, align: "left" }
        ]
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增分类",
        url: "/SystemDocument/EquipmentType/Form",
        width: "700px",
        height: "400px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改分类",
        url: "/SystemDocument/EquipmentType/Form?keyValue=" + keyValue,
        width: "700px",
        height: "400px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/EquipmentType/DeleteForm",
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
        title: "查看分类",
        url: "/SystemDocument/EquipmentType/Details?keyValue=" + keyValue,
        width: "700px",
        height: "400px",
        btn: null,
    });
}