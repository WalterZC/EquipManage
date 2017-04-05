var FUserId = $.request("FUserId");
$(function () {
    $("#itemTree").treeview({
        showcheck: true,
        url: "/SystemDocument/UserItemAuthorize/GetUseItemPermissionSelectTree",
        param: { FUserId: FUserId,FObjectType: "Organize" }
    });
});
function submitForm() {
    var Ids = $("#itemTree").getCheckedNodes();
    $.submitForm({
        url: "/SystemDocument/User/SubmitAuthorizedOrg",
        param: { FUserId: FUserId, FOrgIds: String(Ids), FObjectType: "Organize" },
        success: function () {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}