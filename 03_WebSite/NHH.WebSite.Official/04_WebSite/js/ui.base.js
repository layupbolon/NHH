// JavaScript Document

function startTabA(w) { //开启指定容器内的TAB功能，参数为包含TAB组件的外部容器
	var sWrap = "." + w;
	var wrap = $(sWrap); //包含TAB的外部容器
	if (wrap.length) { //判断容器是否存在
		$(sWrap + " .tabs a").click(function(){ //当窗口中tabs下面的链接点击时
			var contentID = $(this).parents(sWrap).get(0).id; 
			var tab = $(this).parents(".tabs").children("*"); 
			var tabNum = tab.length; //获取TAB个数
			tab.removeClass("currentBtn");
			if ($(this).parents(".tabs").children("a").length > 0)
				$(this).addClass("currentBtn");
			else
				$(this).parent().addClass("currentBtn");
			for (var i = 1; i <= tabNum; i++) {
				$("#" + contentID + "_" + i).hide(); //先将所有tabContent隐藏
			}
			$("#" + contentID + "_" + this.rel).show(); //将所点击链接所对应的tabContent显示
			return false;
		});
		
		if($(sWrap + " .prevView").length) { //如果存在向前按钮则执行
			$(sWrap + " .prevView").click(function(){
				var contentID = $(this).parents(sWrap).get(0).id;
				var curNum = parseInt($("#" + contentID + " .tabs .currentBtn").attr("rel")) - 1;
				if (curNum < 1) curNum = $("#" + contentID + " .tabs a").length;
				$("#" + contentID + " .tabs a").eq(curNum-1).click();
			});
		}
		
		if($(sWrap + " .nextView").length) { //如果存在向后按钮则执行
			$(sWrap + " .nextView").click(function(){
				var contentID = $(this).parents(sWrap).get(0).id;
				var curNum = parseInt($("#" + contentID + " .tabs .currentBtn").attr("rel")) + 1;
				if (curNum > $("#" + contentID + " .tabs a").length) curNum = 1;
				$("#" + contentID + " .tabs a").eq(curNum-1).click();
			});
		}
	}
}

$(function(){
	$(".sliderWarp").Xslider({
		showNav:".navB a",
		scrollObj:".mover",
		loop:"cycle",
		autoScroll:5000
	});
	$(".mainMenu>li").hover(function(){
		var me=$(this);
		me.find(".menu").show();
		me.find(".topLine").css("display","inline");
	},function(){
		var me=$(this);
		me.find(".menu").hide();
		me.find(".topLine").hide();
	});
	$(".conMenu>li").hover(function(){
		var me=$(this);		
		me.find(".desc").stop().animate({top:-2},100);
	},function(){
		var me=$(this);		
		me.find(".desc").stop().animate({top:106},200);
	});
	$(".sideMenu li").click(function(){
		var $this=$(this);
		$this.siblings().filter(".curr").removeClass("curr").find("dl").slideUp(500);
		$this.find("dl").slideDown(400).end().addClass("curr");
	});
	(function (op) {
           op=$.extend(op || {},{
                hasDefaultText: ".hasDefaultText",
                removeClass: "blur",
                addClass: "blur"
            });
            var obj = $(op.hasDefaultText);
            var tmpText = new Array();
            var objIndex = 0;
            for (i = 1; i <= obj.length; i++) {
                tmpText[i - 1] = obj.eq(i - 1).val();
            }
            obj.focus(function () {
                objIndex = obj.index($(this));
                if ($(this).val() == tmpText[objIndex]) {
                    $(this).val("");
                    $(this).addClass(op.addClass);
                }
            });
            obj.blur(function () {
                objIndex = obj.index($(this));
                if ($(this).val() == "") {
                    $(this).val(tmpText[objIndex]);
                    $(this).removeClass(op.removeClass);
                }
            });
        })();
		
});
$(function(){
	$(".list_hotNews").Xslider({
		loop:"cycle",
		dir:"V",
		autoScroll:3000
	});
	
})
