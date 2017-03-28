$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemDocument/User/GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'FId', hidden: true },
            { label: '账户', name: 'FAccount', width: 80, align: 'left' },
            { label: '姓名', name: 'FRealName', width: 80, align: 'left' },
            { label: '工号', name: 'FMemberNo', width: 80, align: 'left' },
            {
                label: '性别', name: 'FGender', width: 60, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == true) {
                        return '男';
                    } else {
                        return '女';
                    }
                }
            },
            { label: '手机', name: 'FMobilePhone', width: 100, align: 'left' },
            {
                label: '公司', name: 'FOrganizeId', width: 150, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.organize[cellvalue] == null ? "" : top.clients.organize[cellvalue].fullname;
                }
            },
            {
                label: '部门', name: 'FDepartmentId', width: 80, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.organize[cellvalue] == null ? "" : top.clients.organize[cellvalue].fullname;
                }
            },
            {
                label: '岗位', name: 'FDutyId', width: 80, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return top.clients.duty[cellvalue] == null ? "" : top.clients.duty[cellvalue].fullname;
                }
            },
            {
                label: '创建时间', name: 'FCreatorTime', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            {
                label: "允许登录", name: "FEnabledMark", width: 60, align: "center",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 0) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            },
            { label: '备注', name: 'FDescription', width: 200, align: 'left' }
        ],
        pager: "#gridPager",
        sortname: 'FDepartmentId asc,FCreatorTime desc',
        viewrecords: true
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
        title: "新增用户",
        url: "/SystemDocument/User/Form",
        width: "700px",
        height: "560px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: "修改用户",
        url: "/SystemDocument/User/Form?keyValue=" + keyValue,
        width: "700px",
        height: "560px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/User/DeleteForm",
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
        title: "查看用户",
        url: "/SystemDocument/User/Details?keyValue=" + keyValue,
        width: "720px",
        height: "560px",
        btn: null,
    });
}
function btn_revisepassword() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    var Account = $("#gridList").jqGridRowValue().FAccount;
    var RealName = $("#gridList").jqGridRowValue().FRealName;
    $.modalOpen({
        id: "RevisePassword",
        title: '重置密码',
        url: '/SystemDocument/User/RevisePassword?keyValue=' + keyValue + "&account=" + escape(Account) + '&realName=' + escape(RealName),
        width: "450px",
        height: "260px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalConfirm("注：您确定要【禁用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/SystemDocument/User/DisabledAccount",
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                }
            })
        }
    });
}
function btn_enabled() {
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalConfirm("注：您确定要【启用】该项账户吗？", function (r) {
        if (r) {
            $.submitForm({
                url: "/SystemDocument/User/EnabledAccount",
                param: { keyValue: keyValue },
                success: function () {
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                }
            })
        }
    });
}