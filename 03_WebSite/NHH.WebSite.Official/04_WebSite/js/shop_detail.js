$(function(){
	//ÉÌÆ·ËõÂÔÍ¼¹ö¶¯
	var slider=UI.Xslider(".imgzoomlist",{
		numtoMove:1,
		stepOne:true,	
		start:2,
		viewedSize:355,
		unitLen:71,
		unitDisplayed:1,
		beforeStart:function(e){
			$(e.next).find("a").trigger("mouseenter");
		}
	});
	var bigimg=$(".bigimg").find("img");
	$(".imgzoomlist").find("li a").mouseenter(function(){
		var $this=$(this);
		bigimg.attr("src",$this.attr("href"));
		bigimg.parent("a").attr("href",$this.attr("bigimg"));
		$this.parent().addClass("current").siblings().removeClass("current");
		$(".imgzoomlist").data("curindex",~~$(this).parent().attr("index")+1);
		slider.go(~~$(this).parent().attr("index")+1);
	}).click(function(){	
		return false;	
	});	
	
	//to remove exist jqZoomWindow
	$(".jqzoom").bind("mouseenter",function(){
		var jqZoomWin = $(".jqZoomWindow");
		if(jqZoomWin.length!=0) {
			jqZoomWin.remove(); 
		}
	});
	var options = {
		zoomWidth: 380,
		zoomHeight: 380,
		
		xOffset: 18,
		yOffset: 0,
		title : false,
		showEffect:"fadein",
		hideEffect:"fadeout",
		position: "right" 
	}
	$(".jqzoom").jqzoom(options);
	
	
	
})