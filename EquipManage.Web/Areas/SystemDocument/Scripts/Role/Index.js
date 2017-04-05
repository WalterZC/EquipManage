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
        url: "/SystemDocument/Role/GetPermissionGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '角色名称', name: 'FFullName', width: 150, align: 'left' },
            { label: '角色编号', name: 'FEnCode', width: 150, align: 'left' },
            {
                label: '角色类型', name: 'FType', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    return top.clients.dataItems["RoleType"][cellvalue] == undefined ? "" : top.clients.dataItems["RoleType"][cellvalue]
                }
            },
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
            postData: {itemId: $("#itemTree").getCurrentNode().id, keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增角色",
        url: "/SystemDocument/Role/Form",
        width: "550px",
        height: "570px",
        btn: null
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改角色",
        url: "/SystemDocument/Role/Form?keyValue=" + keyValue,
        width: "550px",
        height: "570px",
        btn: null
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/Role/DeleteForm",
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
        title: "查看角色",
        url: "/SystemDocument/Role/Details?keyValue=" + keyValue,
        width: "550px",
        height: "620px",
        btn: null,
    });
}