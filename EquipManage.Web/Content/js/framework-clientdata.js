var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        dataItemsFid:[],
        organize: [],
        role: [],
        duty: [],
        user: [],
        authorizeMenu: [],
        authorizeButton: [],
        equipment:[]
    };
    var init = function () {
        $.ajax({
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.dataItemsFid = data.dataItemsFid;
                dataJson.organize = data.organize;
                dataJson.role = data.role;
                dataJson.duty = data.duty;
                dataJson.user = data.user;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
                dataJson.authorizeButton = data.authorizeButton;
                dataJson.equipment = data.equipment;
                dataJson.parts = data.parts;
            }
        });
    }
    init();
    return dataJson;
}