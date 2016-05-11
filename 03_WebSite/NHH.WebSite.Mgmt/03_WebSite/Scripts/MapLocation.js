window.MapLocation = {
    getOffsetTop: function (obj) {
        var tmp = obj.offsetTop;
        var val = obj.offsetParent;
        while (val != null) {
            tmp += val.offsetTop;
            val = val.offsetParent;
        }
        return tmp;
    },
    getOffsetLeft: function (obj) {
        var tmp = obj.offsetLeft;
        var val = obj.offsetParent;
        while (val != null) {
            tmp += val.offsetLeft;
            val = val.offsetParent;
        }
        return tmp;
    },
};

MapLocation.init = function (smallMapId) {

    var html = "<div id=\"modalMapLocation\" class=\"modal fade\" tabindex=\"-1\" aria-hidden=\"true\" style=\"display: none;\">";
    html += "<div class=\"modal-dialog\" style=\"width:822px;\">";
    html += "<div class=\"modal-content\">";
    html += "<div class=\"modal-header no-padding\">";
    html += "<div class=\"table-header\">平面图位置</div>";
    html += "</div>";
    html += "<div class=\"modal-body no-padding nothing\">";
    html += "<div class=\"popWinStyle\">";
    html += "<img id=\"bigMapFile\" />";
    html += "<span id=\"divPoint\" style=\"background-color:red; width:8px; height:8px;position:absolute; display:none; z-index:99999;\"></span>";
    html += "</div>";
    html += "<div class=\"ui-btngroup\">";
    html += "<a href=\"###\" class=\"btn btn-xs btn-success btn-font mr20\" id=\"lbtnConfirmMapFile\">确定</a>";
    html += "<a href=\"###\" class=\"btn btn-xs btn-font btn-cancel\" id=\"lbtnCloseMapFile\">取消</a>";
    html += "</div></div></div></div></div>";
    $(document.body).append(html);

    //小图
    $(".lnkMapFile").click(function () {
        var src = $("#" + smallMapId).attr("data-src");
        $("#bigMapFile").attr("src", src);
        $("#modalMapLocation").modal();

        var floorMapX = $("#FloorMapX").val();
        var floorMapY = $("#FloorMapY").val();
        if (floorMapX == '' || floorMapY == '') {
            $("#divPoint").hide();
        }
        else {
            var x = parseInt(floorMapX);
            var y = parseInt(floorMapY);
            var obj = document.getElementById("bigMapFile");
            var parentX = MapLocation.getOffsetLeft(obj);
            var parentY = MapLocation.getOffsetTop(obj);
            x += parentX;
            y += parentY;
            $("#divPoint").css({ "left": x + "px", "top": y + "px" }).show();
        }
    });
    //大图
    $("#bigMapFile").click(function () {
        $("#FloorMapLocation").val("bigMapFile");
        var obj = document.getElementById("bigMapFile");
        var thisX = MapLocation.getOffsetLeft(obj);
        var thisY = MapLocation.getOffsetTop(obj);

        var mouseX = event.clientX + document.body.scrollLeft; //鼠标X位置
        var mouseY = event.clientY + document.body.scrollTop; // 顺便Y位置

        //相对位置
        var x = mouseX - thisX + 20;
        var y = mouseY - thisY + 20;
        $("#FloorMapX").val(x);
        $("#FloorMapY").val(y);
        $("#divPoint").css({ "left": x + "px", "top": y + "px" }).show();
    });
    //确定平面图
    $("#lbtnConfirmMapFile").click(function () {
        $("#modalMapLocation").modal('hide');
        $("#divPoint").hide();
    });
    //关闭平面图
    $("#lbtnCloseMapFile").click(function () {
        $("#modalMapLocation").modal('hide');
        $("#divPoint").hide();
    });
};

MapLocation.floorChange = function (smallMapId, ajaxUrl) {
    $("#FloorId").change(function () {
        var floorId = $(this).val();
        if (floorId == undefined) {
            return false;
        }
        $("#FloorMapLocation").val("");
        $("#divPoint").hide();
        $.ajax({
            type: "GET",
            url: ajaxUrl,
            data: "floorId=" + floorId,
            dataType: "JSON",
            success: function (result, status) {
                $("#" + smallMapId).attr("src", result.smallMapFile).attr("data-src", result.bigMapFile);
            }
        });
    });
};

MapLocation.showBigMap = function () {
    var html = "<div id=\"modalMapLocation\" class=\"modal fade\" tabindex=\"-1\" aria-hidden=\"true\" style=\"display: none;\">";
    html += "<div class=\"modal-dialog\" style=\"width:822px;\">";
    html += "<div class=\"modal-content\">";
    html += "<div class=\"modal-header no-padding\">";
    html += "<div class=\"table-header\">平面图位置</div>";
    html += "</div>";
    html += "<div class=\"modal-body no-padding nothing\">";
    html += "<div class=\"popWinStyle\">";
    html += "<img id=\"bigMapFile\" />";
    html += "<span id=\"divPoint\" style=\"background-color:red; width:8px; height:8px;position:absolute; display:none; z-index:99999;\"></span>";
    html += "</div></div></div></div></div>";
    $(document.body).append(html);

    //图片
    $(".imgPopUp").click(function () {
        var src = $(this).attr("data-src");
        $("#bigMapFile").attr("src", src);
        $("#modalMapLocation").modal();

        var floorMapX = $(this).attr("data-x");
        var floorMapY = $(this).attr("data-y");
        if (floorMapX == '' || floorMapY == '') {
            $("#divPoint").hide();
        }
        else {
            var x = parseInt(floorMapX);
            var y = parseInt(floorMapY);
            var obj = document.getElementById("bigMapFile");
            var parentX = MapLocation.getOffsetLeft(obj);
            var parentY = MapLocation.getOffsetTop(obj);
            x += parentX;
            y += parentY;
            $("#divPoint").css({ "left": x + "px", "top": y + "px" }).show();
        }
    });
};