﻿var menu = {
    init: function () {
        menu.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Menu/ChangeHot",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.hot == true) {
                        btn.text('Còn');
                    }
                    else {
                        btn.text('Hết');
                    }
                }
            });
        });
    }
}
menu.init();