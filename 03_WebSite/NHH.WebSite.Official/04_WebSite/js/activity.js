$(function(){
	//Global Object stack for UI.laterEvent sleepid
	var sleepidObj = {
		windowScroll : {},
		windowResize : {}
	};			

	//Resize Window adjust events	
	$(window).bind("resize",function(){
		sleepidObj.windowResize.sleepid && clearTimeout(sleepidObj.windowResize.sleepid);
		UI.laterEvent(function(){					
			adjustWindow();			
		},200,sleepidObj.windowResize);	
	});
	$(window).resize();	


	var zoomObj = $('.panel-content'),
		supporCSSZoom = UI.cssSupports("zoom"),
		screenPanels = $('.panel');
	function adjustWindow(){
			
			var winW = $(window).width();
			var baseW = 1920;
			var posRatio = winW/baseW;

			screenPanels.height($(window).height()-80);
										
			if(supporCSSZoom){	
				zoomObj.css("zoom",posRatio);//IE6-7 Safari Chrome
				if(!isIE) return;
				//为IE设置缩放后DOM的位置
				zoomObj.each(function(){
					var $this = $(this);
					if(!$this.attr('data-orginheight')){
						$this.attr('data-orginheight',$this.outerHeight(true));
					} 
					$this.css('width',winW*0.5/posRatio);	
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
			
			
		
	}	
	
	
});

