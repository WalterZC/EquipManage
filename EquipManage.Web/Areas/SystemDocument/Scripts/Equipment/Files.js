﻿var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
jQuery(document).ready(function () {
    initControl();
    FormDropzone.init();
    
});
var FormDropzone = function () {


    return {
        //main function to initiate the module
        init: function () {

            Dropzone.options.myDropzone = {
                dictDefaultMessage: "",
                init: function () {
                    this.on("addedfile", function (file) {
                        // Create the remove button
                        var removeButton = Dropzone.createElement("<a href='javascript:;'' class='btn red btn-sm btn-block'>Remove</a>");

                        // Capture the Dropzone instance as closure.
                        var _this = this;

                        // Listen to the click event
                        removeButton.addEventListener("click", function (e) {
                            // Make sure the button click doesn't submit the form:
                            e.preventDefault();
                            e.stopPropagation();

                            // Remove the file preview.
                            _this.removeFile(file);

                            // If you want to the delete the file on the server as well,
                            // you can do the AJAX request here.
                            $.ajax({
                                type: 'GET',
                                contentType: "application/json; charset=utf-8",
                                url: '/Equipment/DeleteFile',
                                data: { "file": file.name }
                            });
                        });

                        // Add the button to the file preview element.
                        file.previewElement.appendChild(removeButton);
                    });
                    //this.on("queuecomplete", function (file) {
                    //    //上传完成后触发的方法
                    //    console.log(files.name);
                    //});
                    this.on("removedfile", function (file) {
                        //删除文件时触发的方法
                    });
                    this.on("success", function (file, data) {
                        
                        
                    });
                }
            }
        }
    };
}();

function initControl() {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: '/Equipment/GetFileList',
        success: function (data) {
            var content = "";
            if (data["files"].length == 0) {
                content = '<div class="row"> <div class="col-md-4 text-center"><div class="alert alert-warning">目前还没有一个附件呢，你可以上传试试!</div></div></div>';
            } else {
                var i = 0;
                data["files"].forEach(function (value, index, array) {
                    if (index % 4 == 0 && i != 4) {
                        content += '<div class="row" style="padding-top:8px;padding-bottom:8px;">'
                        content += '<div class="col-md-12 attach">'
                    }
                    content += '<div class="col-sm-3 col-md-2 text-center">'
                    content += '                <a href="' + value.url + '" target="_blank">'
                    content += '                    <img class="attach-img"'
                    content += '                         src="' + value.thumbnailUrl + '"'
                    content += '                         title="' + value.name + '" />'
                    content += '                </a>'
                    content += '                <div class="clearfix m-t-10">'
                    content += '                    <span class="attach-text" data-toggle="tooltip" data-placement="top"'
                    content += '                          data-original-title="' + value.name + '">' + value.name + '</span>'
                    content += '                </div>'
                    content += '               <div class="clearfix">'
                    content += '                    <a href="/Equipment/DownFile?filePath=' + value.url + '&fileName=' + value.name + '" type="text"'
                    content += '                           class="btn btn-warning btn-sm waves-effect waves-light m-t-5 copy">'
                    content += '                        <i class="fa fa-download"></i> <span>下载</span>'
                    content += '                    </a>'
                    content += '                    <button type="button" class="btn btn-danger btn-sm waves-effect waves-light m-t-5"'
                    content += '                            onclick="delFile(\'' + value.name + '\')">'
                    content += '                        <i class="fa fa-trash-o"></i> <span>删除</span>'
                    content += '                    </button>'
                    content += '                </div>'
                    content += '           </div>';
                    i++;
                    if ((index+1) % 4 == 0 && i==4) {
                        content += '       </div></div>';
                        i = 0;
                    }
                });
                $("div[name='attach']").prepend(content);
            }

            //$('#fileupload').fileupload('option', 'done').call($('#fileupload'), $.Event('done'), { result: { files: data.files } })
            //$('#fileupload').removeClass('fileupload-processing');
        }

    });
        $("#FItemID").val(keyValue);
}

function delFile(file) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: '/Equipment/DeleteFile',
        data: { "file": file },
        success: function (result) {
            if (result == "OK")
            {
                $("span[data-original-title='" + file + "']").parent().parent(".text-center").remove();
            }
        }
    });
  }