(function () {
    window.beforeUnload = {
        config: {
            enable : false,
        },
        init: function () {
            $("input[type='text'],textarea").change(function () {
                var tag = $("#beforeUnload");
                if (tag == null || tag == undefined || tag.length == 0) {
                    $(document.body).append("<input type='hidden' id='beforeUnload' value='0'/>");
                    tag = $("#beforeUnload");
                }
                tag.val("1");
            });

            $(".formsubmit").click(function () {
                $(window).unbind("beforeunload");
                return true;
            });

            $(window).bind("beforeunload", function () {
                var tag = $("#beforeUnload");
                if (tag != null && tag != undefined) {
                    if (tag.val() == "1") {
                        window.event.returnValue = "你编辑的信息还未保存，是否离开此页面？";
                    }
                }
            });
        }
    };
})();
$(function () {
    if (beforeUnload.config.enable) {
        beforeUnload.init();
    }
});