$(function(){
	//Global Object stack for UI.laterEvent sleepid
	var sleepidObj = {
		windowScroll : {},
		windowResize : {}
	};

	//Fix home topbanner height for ie6\7\8
	function fixOldIETopbanner(){
		if((isIE && 9 > $.browser.version) || !$.support.leadingWhitespace){
			//$('.topbanner, .firstScreenFixed, .firstScreenPlaceholder').height($(window).height());
			$('.firstScreenFixed, .screen-panel').height($(window).height()-80);
			$('.firstScreenPlaceholder').height($(window).height());
			return;
		}		
	}
	fixOldIETopbanner();

	var supporCSS3Transform = UI.cssSupports("transform");
	//var supporCSS3BgSize = UI.cssSupports("backgroundSize");

	if(!supporCSS3Transform){
		$('.screen-panel .adbg').each(function(){		
			var img = new Image();
			img.src = $(this).css('backgroundImage').replace(/^url\("|"\)$/ig,'');
			$(this).html(img);
		});
	}
	
	$('#firstOne .bgImg').each(function(){		
		$(this).html('');
		var img;
		if(!supporCSS3Transform) {
			img = new Image();
			img.src = $(this).css('backgroundImage').replace(/^url\("|"\)$/ig,'');
			$(this).html(img);
		}		
	});

	
	//Banner滚动图片
	var bannerSlide = UI.Xslider(".topbanner",{
		dir: "F",
		autoScroll:8500,
		speed:500,
		showNav:".navDot a",
		beforeStart: function(e){			
			bannerContentAnim(e.next,e.current);
		}
	});
	

	function bannerContentAnim(item, lastItem){
		var nextItem = $(item),	
		 	showedItem = $(lastItem),		
			topText = nextItem.find('.top-text'),
			h3 = topText.find('h3'),
			p = topText.find('p');			
		
		showedItem.find('.top-text').hide();
		topText.hide().show().css({opacity:0}).animate({opacity:1},1000);
		h3.css('top',50).animate({top:0},800);
		p.css('top',50).animate({top:0},1200);

		//alert(supporCSS3aBgSize);

		if(isIE && $.browser.version == 9) supporCSS3Transform = false;
		if(supporCSS3Transform) return;	
		nextItem.css('zIndex',100).find(".bgImg").css({opacity:0}).animate({opacity:1},600);
		nextItem.siblings().css('zIndex',0);
		showedItem.css('zIndex',99);
	}

	function bannerAnimPause(){
		bannerSlide.pause();
		//$(".topbanner").trigger("mouseenter");
	}

	function bannerAnimResume(){
		bannerSlide.resume();
		//$(".topbanner").trigger("mouseleave");
	}

	setTimeout(function(){
		bannerContentAnim($(".topbanner").find("li").eq(0));
	},210);
		

	//Resize Window adjust events
	var bannerArrowHoler = $(".topbanner .arrowsInner");
	$(window).bind("resize",function(){
		sleepidObj.windowResize.sleepid && clearTimeout(sleepidObj.windowResize.sleepid);
		UI.laterEvent(function(){
			fixOldIETopbanner();
			bannerArrowHoler.css({width:$(window).width()});
			adjustWindow();
			fireScreenScroll(curScreenIndex,true);
		},200,sleepidObj.windowResize);	
	});
	$(window).resize();	


	var zoomObj = $('.top-text, .panel-content'),
		supporCSSZoom = UI.cssSupports("zoom"),
		screenPanels = $('.screen-panel');
	function adjustWindow(){
		//setTimeout(function(){			
			var winW = $(window).width();
			var baseW = 1920;
			var posRatio = winW/baseW;

			screenPanels.height($(window).height()-80);
			//if(winH<900){										
				if(supporCSSZoom){	
					zoomObj.css("zoom",posRatio);/* IE6-7 Safari Chrome */					
					if(!isIE) return;
					//为IE设置缩放后DOM的位置
					zoomObj.each(function(){
						var $this = $(this);
						//如果是第一屏中的文字
						/*if($this.is('.top-text')) {
							if($this.attr('data-orginleft') && posRatio < 1){
								$this.css('left',$this.attr('data-orginleft')*posRatio + '%');	
								return;					
							} 

							if($this.attr('data-orginright') && posRatio < 1){
								$this.css('right',$this.attr('data-orginright')*posRatio + '%');	
								return;					
							} 

							if($this.css('left') != 'auto'){
								$this.attr('data-orginleft',parseInt($this.css("left")));						
							} 
							else if($this.css('right') != 'auto'){
								$this.attr('data-orginright',parseInt($this.css("right")));			
							}
							return;
						}	*/
						//如果是第一屏中的文字
						if($this.is('.top-text')) {
							$this.css("zoom",1);
							$this.find('h3').css('fontSize',80*posRatio);
							$this.find('sup').css('fontSize',100*posRatio);
							$this.find('p').css('fontSize',36*posRatio);
							return;
						}

						//如果是第二、三、四屏的内容
						if(!$this.attr('data-orginheight')){
							$this.attr('data-orginheight',$this.outerHeight(true));						
						} 
						$this.css('width',winW*0.5/posRatio);	
						if(posRatio>1) return;
						$this.css('marginBottom',$this.attr('data-orginheight') * (posRatio - 1));
					});								
				}
				else{					
					zoomObj.css("-moz-transform","scale("+posRatio+")");
					zoomObj.css("-webkit-transform","scale("+posRatio+")");
					zoomObj.css("-o-transform","scale("+posRatio+")");
					zoomObj.css("-ms-transform","scale("+posRatio+")");
					zoomObj.css("transform","scale("+posRatio+")");	
					zoomObj.css('width',winW*0.5/posRatio);					
				}	
			//}
			
		//},0);
	}

	
	//Cases box hover
	$(".case-items li").hover(function(){
		$(this).addClass("item-hover");	
	},function(){
		$(this).removeClass("item-hover");	
	});

	//page scroll
	var curScreenIndex = 0,
		upDestiIndex = 0,
		downDestiIndex = 1,
		isScrolling = false,
		scrollPageItem = $('#main .panel'),
		pageTimeNum = scrollPageItem.length,
		offsetY = [0, -80, -80, -80];
	$(document).bind("mousewheel",function(e){
		e.preventDefault();
		var eventDelta = e.originalEvent.wheelDelta || e.originalEvent.detail*-1;
		var dir = eventDelta > 0 ? 'wheelup' : 'wheeldown';
		
		if(isScrolling == true) return;		
		downDestiIndex = curScreenIndex + 1;
		upDestiIndex = curScreenIndex - 1;

		if(dir == 'wheeldown' && downDestiIndex > scrollPageItem.length -1) return;
		if(dir == 'wheelup' && upDestiIndex < 0) {upDestiIndex = 0;}
		
		isScrolling = true;	
		curScreenIndex = dir == 'wheeldown' ? downDestiIndex : upDestiIndex;
		
		fireScreenScroll(curScreenIndex);
		return false;
	});

	function fireScreenScroll(index,noAnim){
		$(window).scrollTo(scrollPageItem.eq(index),{
			duration: noAnim ? 0 : 480,
			axis: "y",	
			easing: "easeInOutCirc",			
			offset: offsetY[index],			
			onAfter:function(target){
				if(index!=0){
					bannerAnimPause();
				}
				else {
					bannerAnimResume();
				}
						
				isScrolling = false; 				
			}
		});		
	}

	
	//用户拉右侧的浏览器滚动条后判断停在第几屏
	/*var scrollEventID;
	$(window).scroll(function(){		
		scrollEventID && clearTimeout(scrollEventID);
		scrollEventID = setTimeout(function(){
			var wh = $(window).height(),			
			st = $(window).scrollTop();	
			
			for(var i = 0; i < pageTimeNum; i++){
				var itemH = scrollPageItem.eq(i).height();
				wh = itemH < wh ? itemH : wh; 
				
				if(scrollPageItem.eq(i).position().top + offsetY[i+1] > st - wh){
					curScreenIndex = i;					
					break;
				}
			}
		},200);	

	}); */

	//点击返回顶阅按钮
	$(".toolbox .tool_top").click(function () {
		isScrolling = true;
		desScreenIndex = 0;
		$(window).scrollTo(0,800,{
			offset:0,
			onAfter:function(){
				isScrolling = false;
				curScreenIndex = 0;
			}
		});

	}).click();

	//$('.scrollDown').animate({top:});


	//Hover Item Slide Bg
	$('.hoverArea').hover(function(){
		//$(this).addClass("ul-half-hover");
		var mask = $(this).find('.panel-mask'),
			content = $(this).find('.panel-content'),
			arrow = content.find('.arrow');
		mask.animate({			
			top: 0
		},{queue:false, duration: 300, easing: "easeOutSine"});
		content.animate({bottom: 35},{queue:false, duration: 250});
		arrow.animate({width: 43},{queue:false, duration: 300,easing: "easeOutQuad"});
	},function(){
		var mask = $(this).find('.panel-mask'),
			content = $(this).find('.panel-content'),
			arrow = content.find('.arrow');
		mask.animate({			
			top: "100%"
		},{queue:false,duration: 300 });
		content.animate({bottom: 0},{queue:false,duration: 250});
		arrow.animate({width: 0},{queue:false,duration: 250});



	});
	
	
});

