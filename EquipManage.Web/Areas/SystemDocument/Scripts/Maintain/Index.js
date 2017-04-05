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
        //treeGrid: true,
        //treeGridModel: "adjacency",
        ExpandColumn: "FNumber",
        url: "/SystemDocument/Maintain/GetPermissionGridJson",
        height: $(window).height() - 96,
        colModel: [
            { label: "主键", name: "FId", hidden: true, key: true },
            { label: '简称', name: 'FShortName', width: 80, align: 'left' },
            { label: '名称', name: 'FFullName', width: 200, align: 'left' },
            { label: '编号', name: 'FNumber', width: 80, align: 'left' },
            { label: '联系人', name: 'FLinkMan', width: 80, align: 'left' },
            { label: '手机', name: 'FMobilePhone', width: 100, align: 'left' },
            { label: '微信', name: 'FWeChat', width: 100, align: 'left' },
            { label: '扣率', name: 'FRate', width: 80, align: 'left' },
            {
                label: '所属部门', name: 'FOrganizeId', width: 150, align: 'left',
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
        title: "新增机构",
        url: "/SystemDocument/Maintain/Form",
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改机构",
        url: "/SystemDocument/Maintain/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/Maintain/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().FId },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Details",
        title: "查看机构",
        url: "/SystemDocument/Maintain/Details?keyValue=" + keyValue,
        width: "700px",
        height: "560px",
        btn: null,
    });
}