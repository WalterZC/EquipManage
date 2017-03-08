var keyValue = $.request("keyValue");
$(function () {
    $.ajax({
        url: "/SystemDocument/Equipment/GetFormJson",
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