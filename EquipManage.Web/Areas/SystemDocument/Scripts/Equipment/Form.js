var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
$(function () {
    initControl();
    FormFileUpload.init();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/Equipment/GetFormJson",
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
    $("#FBelongOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FUseOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FManuOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FUnit").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "Unit" }
    });
    $("#FCategory").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "Category" }
    });
    $("#FStatus").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "EquipmentStatus" }
    });
    $("#FABC").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "ABCType" }
    });
    $("#FAccuracy").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "AccuracyStatus" }
    });
    $("#FUseOrgID").on("change", function () {
        var UseOrgID = $(this).val();
        if (!!UseOrgID) {
            $("#FPositionID").bindSelect({
                url: "/SystemDocument/Position/GetSelectJson",
                id: "FId",
                text: "FShortName",
                param: { keyValue: UseOrgID }
            });
            $("#FOperatorID").bindSelect({
                url: "/SystemDocument/User/GetGridJsonByOrg",
                id: "FId",
                text: "FRealName",
                param: { keyword: UseOrgID }
            });
        }
    });
    $("#FManuOrgID").on("change", function () {
        var ManuOrgID = $(this).val();
        if (!!ManuOrgID) {
            $("#FManuClassID").bindSelect({
                url: "/SystemDocument/OperationClass/GetSelectJson",
                id: "FId",
                text: "FShortName",
                param: { keyValue: ManuOrgID }
            });
        }
    });
    $("#FManuClassID").on("change", function () {
        var ManuClassID = $(this).val();
        if (!!ManuClassID) {
            $("#FManuPrincipalID").bindSelect({
                url: "/SystemDocument/OperationClassMember/GetSelectJson",
                id: "FId",
                text: "FRealName",
                param: { keyValue: ManuClassID }
            });
        }
    });


}

function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    var postData = $("#form1").formSerialize();
    postData["FEquipmentTypeId"] = itemId;
    $.submitForm({
        url: "/SystemDocument/Equipment/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}

var FormFileUpload = function () {
    return {
        //main function to initiate the module
        init: function () {

            // Initialize the jQuery File Upload widget:
            $('#form1').fileupload({
                disableImageResize: false,
                autoUpload: false,
                disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
                maxFileSize: 5000000,
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
                // Uncomment the following to send cross-domain cookies:
                //xhrFields: {withCredentials: true},                
            });

            // Enable iframe cross-domain access via redirect option:
            $('#form1').fileupload(
                'option',
                'redirect',
                window.location.href.replace(
                    /\/[^\/]*$/,
                    '/cors/result.html?%s'
                )
            );

            // Upload server status check for browsers with CORS support:
            if ($.support.cors) {
                $.ajax({
                    type: 'HEAD'
                }).fail(function () {
                    $('<div class="alert alert-danger"/>')
                        .text('Upload server currently unavailable - ' +
                                new Date())
                        .appendTo('#fileupload');
                });
            }

            // Load & display existing files:
            $('#form1').addClass('fileupload-processing');
            $.ajax({
                // Uncomment the following to send cross-domain cookies:
                //xhrFields: {withCredentials: true},
                url: $('#fileupload').attr("action"),
                dataType: 'json',
                context: $('#fileupload')[0]
            }).always(function () {
                $(this).removeClass('fileupload-processing');
            }).done(function (result) {
                $(this).fileupload('option', 'done')
                .call(this, $.Event('done'), { result: result });
            });
        }

    };

}();