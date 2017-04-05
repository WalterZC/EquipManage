$(document).ready(function () {
    $('#layout').layout();
    treeView();
    //classView();
    gridList();
});
//function classView() {
//    $.ajax({
//        type: "GET",   //传值方式
//        url: "/SystemDocument/OperationClass/GetGridJson",   //后台处理地址
//        contentType: "application/json",
//        dataType: "json", //表示返回值类型，不必须
//        success: function (msg) {    //返回信
//            msg.forEach(function (value, index, array) {
//                $("#classTree").append('<a href="javascript:;" onclick="a_OperationClass(\'' + value.FId + '\')" data-id="' + value.FId + '" class="list-group-item"> ' + value.FNumber + ' - ' + value.FShortName + ' </a>');
//            });
//        },
//        error: function (msg) { //错误信息
//            alert(msg.value);
//        }
//    });
//}
function treeView() {
    $("#itemTree").treeview({
        url: "/SystemDocument/OperationClass/GetTreeJson",
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
                {
                    label: '班组成员', name: 'FMemberID', width: 80, align: 'left',
                    formatter: function (cellvalue, options, rowObject) {
                        return top.clients.user[cellvalue] == null ? "" : top.clients.user[cellvalue].fullname;
                    }
                },
                {
                    label: "班组负责人", name: "FIsPrinciple", width: 80, align: "center",
                    formatter: function (cellvalue) {
                        return cellvalue == true ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                    }
                },
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
            url: "/SystemDocument/OperationClassMember/GetGridJson",
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
        title: itemName + " 》新增人员",
        url: "/SystemDocument/OperationClassMember/Form?itemId=" + itemId,
        width: "450px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var itemId = $("#classTree a:first").attr("data-id");
    var itemName = $("#classTree a:first").text().trim();
    var keyValue = $("#gridList").jqGridRowValue().FId;
    $.modalOpen({
        id: "Form",
        title: itemName + " 》修改人员",
        url: "/SystemDocument/OperationClassMember/Form?keyValue=" + keyValue + "&itemId=" + itemId,
        width: "450px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemDocument/OperationClassMember/DeleteForm",
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
        title: "查看人员",
        url: "/SystemDocument/OperationClassMember/Details?keyValue=" + keyValue,
        width: "450px",
        height: "470px",
        btn: null,
    });
}
function btn_OperationClassMemberApp() {
    $.modalOpen({
        id: "OperationClassMemberApp",
        title: "班组",
        url: "/SystemDocument/OperationClassMemberApp/Index",
        width: "800px",
        height: "550px",
        btn: null,
    });
}

function a_OperationClass(FID) {
    var currentNode = $("#classTree a[data-id='" + FID + "']");
    $("#classTree a.active").removeClass("active");
    $(currentNode).addClass("active");
    $("#classTree a:first").text($(currentNode).text());
    $("#classTree a:first").attr('data-id', $(currentNode).attr('data-id'));
    $("#txt_keyword").val('');
    $('#btn_search').trigger("click");
}
