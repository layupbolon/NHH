$(function(){
		
	//Banner滚动图片
	$(".topbanner").each(function(){
		if($(this).hasClass('swiper-container')){
			initDragSlide(this);
		}
		else {
			UI.Xslider(this,{
				dir: "F3",
				autoScroll:6500,
				speed:500,
				showNav:".navDot a"
			});
		}
	});	

	//定义支持鼠标drag的滚动初始化
	function initDragSlide(selectorObj){
		return new Swiper(selectorObj,{
			direction: "horizontal",
			speed:500,
			loop : true,						
			//prevButton:".btn-prev",
			//nextButton:".btn-next",
			preventClicks:false,
			touchMoveStopPropagation:false,
			preventClicksPropagation:false,
			resistanceRatio:0.8,
			pagination : '.swiper-pagination',
			paginationClickable :true,			
			paginationBulletRender: function (index, className) {
				  var className = className.replace(/swiper-pagination-bullet-active/i,'current');
			      return '<a class="' + className + '"><span>' + (index + 1) + '</span></a>';
			},
			onInit: function(swiper){				
				swiper.container.on("mouseenter",function(){
					$(this).addClass('grab-cursor');
				}).on("mouseleave",function(){
					$(this).removeClass('grab-cursor');
				}).on("mousedown",function(){
					$(this).addClass('grabbing-cursor');
				}).on("mouseup",function(){
					$(this).removeClass('grabbing-cursor');
				});
			}
		});
	}

	//Shoplist hover效果	
	$('.shoplist li').hover(function(){
		var $this = $(this);
		$this.find('.mask-bg').animate({
			opacity: .9
		},{queue:false});
		$this.find('.mask-content').find('h4,em').animate({
			top: 0,
			opacity: 1
		},{duration: 800, queue:false});
		$this.find('.default-content').animate({
			bottom: -60			
		},{queue:false});
	},function(){
		var $this = $(this);
		$this.find('.mask-bg').stop().css("opacity",0);
		$this.find('.mask-content').find('h4').stop().css({opacity:0,top:5});
		$this.find('.mask-content').find('em').stop().css({opacity:0,top:10});
		$this.find('.default-content').animate({
			bottom: 0			
		},{queue:false});
	});



	//Project list hover效果	
	$('.htSection').hover(function(){
		var $this = $(this);
		$this.find('.mask-bg').animate({
			opacity: .8
		},{queue:false,duration: 400});
		$this.find('.mask-content').find('h3,p,.cmnBtn').animate({
			top: 0,
			opacity: 1
		},{duration: 400, queue:false});
		$this.find('.pjname').animate({
			bottom: -90			
		},{queue:false,duration: 200});
	},function(){
		var $this = $(this);
		$this.find('.mask-bg').stop().css("opacity",0);
		$this.find('.mask-content').find('h3').stop().css({opacity:0,top:5});
		$this.find('.mask-content').find('p').stop().css({opacity:0,top:10});
		$this.find('.mask-content').find('.cmnBtn').stop().css({opacity:0,top:15});
		$this.find('.pjname').animate({
			bottom: 0			
		},{queue:false});
	});


	//项目详情页box-slide	
	/*$(".box-slide").each(function(){
		var $this = $(this);
		var scrollObjSize =  $(".mover li",$this).length * 406;
		$(".mover",$this).width(scrollObjSize);
		UI.Xslider($this,{
			numtoMove:3,
			unitLen:406,
			scrollObjSize:scrollObjSize,
			showNav:".navDot a"
		});	
	});*/

	//项目详情页box-slide	
	$(".box-slide").each(function(){
		var $this = $(this),
			itemNum = $this.find(".mover li").length,
			dotNav = '',
			itemCount = 3,
			abtnTpl = [
				'<div class="abtnp mt10" style="display: none;">',
	            	'<a href="javascript:" class="abtn aleft agrayleft">左移</a>',
	            	'<a href="javascript:" class="abtn aright">右移</a>',
	            '</div>'
			];
		
		if(itemNum>itemCount){
			for(var i=1,j=Math.ceil(itemNum/itemCount);i<=j;i++){
				dotNav+='<a href="javascript:void(0);"><span class="ie6png">'+i+'</span></a>';
			}
			
			$this.find(".slide-contain").before('<div class="dotwrap mt40"><div class="navDot"><div class="navInner">'+dotNav+'</div></div></div>');
			$this.find(".slide-contain").before(abtnTpl.join(''));
			
			UI.Xslider($this,{
				speed: 1600,
				numtoMove:1,
				unitLen:$this.find('.slide-area').width(),
				scrollObjSize:$this.find('.slide-area').width()*Math.ceil(itemNum/itemCount),
				showNav:".navDot a"
			});	

			$this.hover(function(){
				$(".abtnp",this).stop(true,true).fadeIn()
			},function(){
				$(".abtnp",this).stop(true,true).fadeOut()
			});
			
		}	
	});
	

	
});

