$(function(){
	//Global Object stack for UI.laterEvent sleepid
	var sleepidObj = {
		windowScroll : {},
		windowResize : {}
	};

	//Fix home topbanner height for ie6\7\8
	function fixOldIETopbanner(){
		//if((isIE && 9 > $.browser.version) || !$.support.leadingWhitespace){
			$(".txeWrap").each(function(){
				var mt = 80;
				if($(this).is('#first')) {mt = 0;}
				$(this).height($(window).height()-mt);
			});
			$('.firstScreenPlaceholder').height($(window).height());
		//}
	}
	fixOldIETopbanner();
		
	//page scroll
	var curScreenIndex = 0,
		upDestiIndex = 0,
		downDestiIndex = 1,
		isScrolling = false,
		scrollPageItem = $('#main .screen-panel'),
		pageTimeNum = scrollPageItem.length,
		offsetY = [0, -80, -80, -80, -80];
	$(document).mousewheel(function(e){
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
				isScrolling = false; 
				onEnterScreenEvent(curScreenIndex);
				runScreenPanelEvent(curScreenIndex);
			}
		});		
	}

	//初始化每一屏的事件
	function runScreenPanelEvent(index){
		var text = [
				'.txtfour',
				'.txtfourl',
				'.txtfourr'
			],
			highlight = [
				'pinkHighlight',
				'blueHighlight',
				'blueHighlight'
			];

		if(index < 0 || index > 3){
			return;
		}

		if(scrollPageItem.eq(index).attr("data-ready") == "true" ) {
			scrollPageItem.eq(index)[0].slider.go(1);
			return;
		}
		
		//手机 + 右边文字联动
		scrollPageItem.eq(index).attr("data-ready","true");
		scrollPageItem.eq(index).find(".movearea").find("ul").find("li").width(scrollPageItem.eq(index).find(".movearea").width());
		scrollPageItem.eq(index).find(".movearea").find("ul").width("1000%");

		scrollPageItem.eq(index)[0].slider = UI.Xslider(scrollPageItem.eq(index),{				
			autoScroll:3800,
			speed:800,
			loop: "cycle",
			showNav:".navDot a",
			beforeStart: function(e){	
											
				$(e.container).find(text[index-1]).find("dl").eq($(e.next).attr("index")).addClass(highlight[index-1]).siblings().removeClass(highlight[index-1]);			
			}
		});	

		scrollPageItem.eq(index).find(text[index-1]).find("dl").eq(0).addClass(highlight[index-1]);	
		scrollPageItem.eq(index).find(text[index-1]).find("dl").on("mouseenter",
			function(){
				var thisSlide = scrollPageItem.eq(index)[0].slider;
				thisSlide.pause();				
				thisSlide.go($(this).index()+1);

			}).
		on("mouseleave",
			function(){
				var thisSlide = scrollPageItem.eq(index)[0].slider;
				thisSlide.resume();				
			});
		
	}

	//第一次进入每一屏内容时的入场动画
	function onEnterScreenEvent(index){
		var mobile = [
				'.mobile',
				'.mobiler',
				'.mobile'
			], 
			text = [
				'.txtfour',
				'.txtfourl',
				'.txtfourr'
			];
		if(scrollPageItem.eq(index).attr("data-enter") == "true" ) {

			if(index == 4) {				
				kc.bringToFront(2);
			}

			return;
		}

		scrollPageItem.eq(index).attr("data-enter","true");

		if(index < 1 || index > 4){	
			return;
		}
		//企划活动入场
		if(index == 4){

			adjustActivPhone();
			setTimeout(function(){					
				scrollPageItem.eq(4).find('.imgactiv').animate({
					bottom: 0
				},{
					duration: 600,
					queue: false,
					complete: function(){
						adjustActivPhone();
					}
				});
			},10);

			return;
		}

		//其他手机文字内容的入场
		scrollPageItem.eq(index).find(mobile[index-1]).animate({
				bottom: 0
			},{
				duration: 800,
				queue: false
			});
			setTimeout(function(){
				scrollPageItem.eq(index).find(text[index-1]).animate({
					bottom: 0
				},{
					duration: 800,
					queue: false
				});				
			},300);	
	}

	function onLeaveScreenEvent(index){}



	/* Window Resize Event */
	$(window).bind("resize",function(){
		sleepidObj.windowResize.sleepid && clearTimeout(sleepidObj.windowResize.sleepid);
		UI.laterEvent(function(){
			fixOldIETopbanner();			
			adjustWindow();				
			fireScreenScroll(curScreenIndex,true);
			adjustActivPhone();
		},200,sleepidObj.windowResize);	
	});
	$(window).resize();


	var zoomObj = $('.title-blod, .cnt, .wrpaOne > .mobile, .imgactiv, .scrollDown'),
		supporCSSZoom = UI.cssSupports("zoom"),		
		screenPanels = $('.screen-panel:not(.firstScreenPlaceholder, .screen-footer)'),
		screenPanelInnerWrap = $(".txeWrap .wrpaOne");
	function adjustWindow(){
		//setTimeout(function(){			
			/*var winW = $(window).width();
			var baseW = 1920;
			var posRatio = winW/baseW;*/
			var winW = $(window).width(), 
				winH = $(window).height(), 
				baseH = 1080,
				posRatio = winH/baseH;
				//wrapSideMarginRatio,				
				//wrapSideMarginRatioBase = 1.5,
				//wrapSideMarginRatioMaxLimit = 2;

			posRatio = posRatio > 1 ? 1 : posRatio;			

			screenPanels.height($(window).height()-80);
			
			//为小屏幕做适配，用加大两边边距来减小内容中间距离
			//wrapSideMarginRatio = posRatio < 1 ? wrapSideMarginRatioBase/posRatio : 1;
			//wrapSideMarginRatio = wrapSideMarginRatio > wrapSideMarginRatioMaxLimit ? wrapSideMarginRatioMaxLimit : wrapSideMarginRatio;
			
			//为小屏幕做适配，根据内容原始的宽度比来计算出当前合适的宽度
			screenPanelInnerWrap.each(function(i){
				var $this = $(this),
					thisH = $this.height(),
					standardRatio = 1260 / (1000 + offsetY[i]),
					tw = (winW / (winH - 80)) > standardRatio ? thisH * standardRatio : '80%';
				$this.css({
					//left: (10 * wrapSideMarginRatio) + "%",
					//right: (10 * wrapSideMarginRatio) + "%"
					width: tw
				});
			});			

			if(supporCSSZoom){	
				zoomObj.css("zoom",posRatio);/* IE6-7 Safari Chrome */				
			}
			else{					
				zoomObj.css("-moz-transform","scale("+posRatio+")");
				zoomObj.css("-webkit-transform","scale("+posRatio+")");
				zoomObj.css("-o-transform","scale("+posRatio+")");
				zoomObj.css("-ms-transform","scale("+posRatio+")");
				zoomObj.css("transform","scale("+posRatio+")");	

				$('.wrpaOne > .mobile').css({
					marginBottom: -48 * posRatio
				});
				$('#first .cnt').css({
					top: (27 * posRatio) + '%'
				});								
			}

			//set First Screen iPhone mobile img dimension
			setTimeout(function(){
				$('#first .mobile-first').each(function(){
					$(this).find('img').width($(this).find('img').height()*459/859);
				});	
			},0);
				
			//set Activity carousel left
			$(".kc-horizon").css({
				left: ($(".kc-wrap").width() - $(".kc-horizon").width())/2				
			});				

		//},0);
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

	
	//企业活动展示
	var kcHTML = $('.kc-wrap').html(),
		isloop = false;
	if($('.kc-wrap').find('.kc-item').length < 8 && isloop==true){
			$('.kc-wrap').append(kcHTML).append(kcHTML);
	}

	var lastFontItem = null;

	$('.kc-wrap').KillerCarousel({
            // Width of carousel.
            width: 1600,
            frontItemIndex: 2,
            itemAlign: "middle",
            renderer3d: null,
            // Item spacing in 3d (CSS3 3d) mode.
            //spacing3d: 220,
            // Item spacing in 2d (no CSS3 3d) mode. 
            spacing2d: 270,
            // Path to images for shadows etc.
            imagePath: "js/killercarousel/",
            showShadow: true,
            showReflection: false,
            // Looping mode.
            infiniteLoop: isloop,
            // Scale at 75% of parent element.
            scale: 100,
            //autoScale: 85,
            fadeEdgeItems: true,
            autoChangeDelay: 0,
            animStartCallback:function(e){
            	/*var fontItem = $(e.frontItem.b);   	
            	fontItem.find("img.cc-decoration").animate({
            		opacity: 1
            	},{duration: 400, queue:false});

            	lastFontItem.find("img.cc-decoration").animate({
            		opacity: 1
            	},{duration: 400, queue:false}); */           	
            },
            animStopCallback:function(e){            	
            	/*var fontItem = $(e.frontItem.b);            	

            	lastFontItem = fontItem.find("img.cc-decoration").animate({
            		opacity: 0
            	},{duration: 400, queue:false}); 
            	fontItem.siblings().find("img.cc-decoration").animate({
            		opacity: 1
            	},{duration: 400, queue:false}); */         	
            },
            itemOnCallback: function(e){
            	/*var thisItem = e.item.b; 
            	thisItem.css('visibility','visible');*/
            	
            	/*if(thisItem.hasClass('kc-front-item')){
            		//根据图片设置iphone大小
		        	 adjustActivPhone();
            	}    */ 	
            }

        });

       var kc = $('.kc-wrap').data('KillerCarousel');       

        /*var dir = 0;
        
        setInterval(function() {
            kc.position(kc.position() + dir * 50);
        }, 100)
         $(window).bind('mouseup touchend', function() {
            dir = 0;
            kc.lineUp();
        })*/

        var activityPhoneBg =  $(".imgactiv").find(".phone-frame"),
        	activityPhoneContentHolder =  $(".imgactiv").find(".phone-content-holder"),
        	frontItemIndex = kc.getFrontItemIndex(),
        	frontItem = kc.getItemElement(frontItemIndex);
        	lastFontItem = frontItem;        	
        
       function adjustActivPhone(){
       		setTimeout(function(){
       			frontItemIndex = kc.getFrontItemIndex(),
        		frontItem = kc.getItemElement(frontItemIndex);
	        	//根据图片设置iphone大小
	        	 activityPhoneBg.find('img').css({
			        width: frontItem.width() * 1.22
			     });
			     activityPhoneContentHolder.find('img').css({
			        width: frontItem.width() * 1.22
			     });		    

			     //将跑马灯置于底部
			     $('.kc-wrap .kc-horizon').css({
			     	top:"100%",
			     	marginTop: 0 - frontItem.height() / 2
			     });

	        },500);
       }

        adjustActivPhone();
       
	

	
});

