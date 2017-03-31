var keyValue = $.request("keyValue");
$(function () {
    initControl();
    $.ajax({
        url: "/SystemDocument/Organize/GetFormJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            $("#form1").find('.form-control,select,input').attr('readonly', 'readonly');
            $("#form1").find('div.ckbox label').attr('for', '');
        }
    });
});
function initControl() {
    $("#FCategoryId").select2({ minimumResultsForSearch: -1 })
    $("#FParentId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson",
    });
}