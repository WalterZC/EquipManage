var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/Organize/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
});
function initControl() {
    $("#FCategoryId").select2({ minimumResultsForSearch: -1 })
    $("#FParentId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson",
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }

    $.submitForm({
        url: "/SystemDocument/Organize/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}
//function getFullName(FFullNameHead, CurrentNode) {
//    if (typeof (treenode) == 'undefined') {
//        return FFullNameHead;
//    } else {
//        var rowNodes = $("#gridList").jqGrid("getRowData");
//        rowNodes.forEach(function (value, index, array) {
//            if (value.FId == CurrentNode.parent) {
//                FFullNameHead = FFullNameHead + '_' + $(val.FShortName).find("span").text();
//                getParentNode(FFullNameHead, value)
//            }
//        });
//    }
//}
