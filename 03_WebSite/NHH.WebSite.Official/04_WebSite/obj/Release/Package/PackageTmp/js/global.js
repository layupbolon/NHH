var isIE = /msie|trident/.test(navigator.userAgent.toLowerCase()) || !!window.ActiveXObject;
var ie6 = isIE6 = isIE && !window.XMLHttpRequest;
var gtIE8 = isIE && !!document.documentMode;
var isIE7 = isIE && !isIE6 && !gtIE8 || (document.documentMode==7);
var gteIE8 = isIE && !(isIE6 || isIE7);	
//Reset body class for IE8+ CSS hack
if(gtIE8){
    window.onload = function(){
        if( document.getElementsByTagName("body")[0] ){
            if( !document.documentMode ) return;
            var classStr = document.getElementsByTagName("body")[0].className;
            classStr = classStr.replace(/\sielt\d+/g, "");
            classStr = classStr.replace(/ie\d+/g, "ie" + document.documentMode);
            for(var i = document.documentMode+1, j = 10; i <= j; i++ ){
                classStr += (" ielt"+i);    
            }
            document.getElementsByTagName("body")[0].className = classStr;
        }
    }
}

//UI Namespace
(function (win, UI, undefined) {
    if (win[UI] === undefined) {
        win[UI] = {};
    } else {
        return;
    }
    var mix = function (r, s, ov) {
        if (!s || !r) return r;
        if (ov === undefined) ov = true;
        for (var p in s) {
            if (ov || !(p in r)) {
                r[p] = s[p];
            }
        }
        return r;
    };
    UI = win[UI];
    mix(UI, {
        laterEvent: function (fn, times, me) {
            if (me.sleepid) {
                clearTimeout(me.sleepid);
            }
            me.sleepid = setTimeout(fn, times);
        },
		popWinC: function (s, op) {
            op = $.extend({
                opener: ".opener",
                openerC: ".openerC",
                addClass: "now",
				funcIn : function(){},
				funcOut: function(){}
            },op || {});
			
			$(s).each(function(){
				var $this=$(this),
					objOpner = $(this).find(op.opener),
					objC = $(this).find(op.openerC);
	
				$(this).hover(function () {
					$this.addClass(op.addClass);
					op.funcIn.call(this,objC,op);
				}, function () {
					$this.removeClass(op.removeClass || op.addClass);
					op.funcOut.call(this,objC,op);
				});
			});
        },
		childUntil:function(expr,obj){
			if(obj.length==0) return null;
			var child=obj.children(expr);
			if(child.length==0){
				return arguments.callee(expr,obj.children());
			}else{
				return child;	
			}
		},
		/**
		 * 判断浏览器是否，如: cssSupports('borderRadius')
		 * @return {Boolean} 返回true或false
		 */
		cssSupports: (function() {
			var div = document.createElement('div'),
				vendors = 'Khtml O Moz Webkit'.split(' '),
				len = vendors.length;
			return function(prop) {
				if ( prop in div.style ) return true;
				if ('-ms-' + prop in div.style) return true;
				
				prop = prop.replace(/^[a-z]/, function(val) {
					return val.toUpperCase();
				});
		
				while(len--) {
					if ( vendors[len] + prop in div.style ) {
					return true;
				}
			}
				return false;
			};
		})()
    });
})(window, "UI");


var tabPanel=function(obj){
	obj.each(function(){
		var $this=$(this),
			tabc=UI.childUntil(".tabc",$this.parent());
		$this.children("a:not([rel*='link'])").add($this.find(".tabitem")).each(function(n){
			$(this).attr("rel",n);	
		}).mouseenter(function(event){
			if($this.attr("trigger")=="click"){return false;}
			$(this).addClass("now").siblings().removeClass("now");
			$(this).siblings("b[class*=hide]").removeClass("hide");
			$(this).prev("b").addClass("hide").andSelf().next("b").addClass("hide");
			tabc && tabc.hide().eq(parseInt($(this).attr("rel"))).show();
			if($(this).attr("command")){
				eval($(this).attr('command')+"(this,event)");
			}
		}).click(function(event){			
			if($this.attr("enableLink")!="true"){
				if($this.attr("trigger")!="click"){return false;}	
			}
			$(this).addClass("now").siblings().removeClass("now");
			tabc && tabc.hide().eq(parseInt($(this).attr("rel"))).show();			
			if($(this).attr("command")){
				eval($(this).attr('command')+"(this,event)");
			}			
		});
	});
}


$(function () {	

	//bind mousewheel event
	if(!$.event.special.mousewheel){	
		$.event.special.mousewheel = {
			setup: function() {
				var handler = $.event.special.mousewheel.handler;
				
				// Fix pageX, pageY, clientX and clientY for mozilla
				if ( $.browser.mozilla )
					$(this).bind('mousemove.mousewheel', function(event) {
						$.data(this, 'mwcursorposdata', {
							pageX: event.pageX,
							pageY: event.pageY,
							clientX: event.clientX,
							clientY: event.clientY
						});
					});
			
				if ( this.addEventListener )
					this.addEventListener( ($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false);
				else
					this.onmousewheel = handler;
			},
			
			teardown: function() {
				var handler = $.event.special.mousewheel.handler;
				
				$(this).unbind('mousemove.mousewheel');
				
				if ( this.removeEventListener )
					this.removeEventListener( ($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false);
				else
					this.onmousewheel = function(){};
				
				$.removeData(this, 'mwcursorposdata');
			},
			
			handler: function(event) {
				var args = Array.prototype.slice.call( arguments, 1 );
				
				event = $.event.fix(event || window.event);
				// Get correct pageX, pageY, clientX and clientY for mozilla
				$.extend( event, $.data(this, 'mwcursorposdata') || {} );
				var delta = 0, returnValue = true;
				
				if ( event.wheelDelta ) delta = event.wheelDelta/120;
				if ( event.detail     ) delta = -event.detail/3;
				//if ( $.browser.opera  ) delta = -event.wheelDelta;
				
				event.data  = event.data || {};
				event.type  = "mousewheel";
				
				// Add delta to the front of the arguments
				args.unshift(delta);
				// Add event to the front of the arguments
				args.unshift(event);

				return $.event.handle.apply(this, args);
			}
		};

		$.fn.extend({
			mousewheel: function(fn) {
				return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
			},
			
			unmousewheel: function(fn) {
				return this.unbind("mousewheel", fn);
			}
		});
	}

	//Init all the tab in page
	tabPanel($("[data-tab='enable'][trigger]"));

	
	//Menu Hover Effect
	var hoverMenuSkin = $("#header .hoverSkin");
	var currentHighlightMenu = $("#header .menu > li.current");
	if(currentHighlightMenu.length==0){currentHighlightMenu = $("#header .menu > li").first();}
	hoverMenuSkin.css({marginTop:0,left:-2000}).animate({left:currentHighlightMenu.position().left+23,width:currentHighlightMenu.outerWidth(true)+((currentHighlightMenu.find(".openerC").length==0)?4:12)},{queue:false,duration:400});
	
	UI.popWinC(".menu > li",{
		addClass: "over",
		funcIn: function(o){
			var $this = $(this);
			var offsetX = $this.position().left;
			hoverMenuSkin[0].showedSubMenu = o;
			 UI.laterEvent(function () {
				hoverMenuSkin.stop().animate({ left: offsetX + 23,width:$this.outerWidth(true)+(($this.find(".openerC").length==0)?4:12) }, { queue: false, duration: 200 });
			}, 80, this);
		},
		funcOut: function(o){
            this.sleepid && clearTimeout(this.sleepid);
		}
	});
	
	$("#header .menu").bind("mouseleave",function(){
		hoverMenuSkin.animate({left:currentHighlightMenu.position().left+23,width:currentHighlightMenu.outerWidth(true)+((currentHighlightMenu.find(".openerC").length==0)?4:12)},{queue:false,duration:400});
	});
	
	////Toolbox:
	var toolbox = $(".toolbox");
	var toolboxDefaultTop = parseInt(toolbox.css("top"));
	var toolboxStickContentSide = true;
	var wraperDom,floatSpace,toRight;
	if(toolboxStickContentSide){
		wraperDom = $(".wraper").first();
		floatSpace  = 70;
		toolboxStickToContent();
	}	
    toolbox.find(".tool_top").click(function () {
        $($.browser.safari || document.compatMode == 'BackCompat' ? document.body : document.documentElement).animate({
            scrollTop: 0
        }, 200);
        return false;
    });
	
	$(window).bind("scroll resize",function(){
		var sctop=getSt();
		toolboxStickContentSide && toolboxStickToContent();
		if(sctop>250){
			toolbox.find(".goTop").slideDown();		
		}else{
			toolbox.find(".goTop").slideUp();	
		}
		if($(this).height() - toolbox.height() - toolboxDefaultTop < 0){
			toolbox.addClass("toolbox-stick-bottom");
		}
		else{
			toolbox.removeClass("toolbox-stick-bottom");
		}
	});
	//IE6
	if(!-[1, ] && !window.XMLHttpRequest){
		$(window).bind("resize",function(){
			document.documentElement.scrollLeft = 0;
		});	
	}
	$(window).resize();
	
	//Toolbox hover effect
	$(".toolbox .tool").hover(function(){
		$(this).parent().addClass("hover");
	},function(){
		$(this).parent().removeClass("hover");
	});
	
	//Defined Functions for ToolBox
	function toolboxStickToContent(){
		toRight=($(window).width()-wraperDom.width())/2-floatSpace;		
		toRight=toRight>0 ? toRight :0;
		toolbox.css({
			right:toRight	
		});	
	}	
	function getSt(){
		if(navigator.userAgent.toLowerCase().indexOf('webkit/') != -1) {
			return document.documentElement.scrollTop + document.body.scrollTop;	
		}
		else return document.documentElement ? document.documentElement.scrollTop : document.body.scrollTop;
	}	
	

	//Init select2
	if($.fn.select2) {
		$("select.select2").each(function(){
			$(this).select2({
				minimumResultsForSearch: Infinity				
			});
			
			if($(this).hasClass("select2-16")){
				$(this).on("select2:open", function (e) { 
					console.log("select2:open", e);
					$(".select2-container--open").addClass("font16");
			 });
			}
		});
	}
	
	//hover wx icon
	var $qrcodeWX = $('<div class="tip-qrcode"><img src="img/codenhh.jpg"></div>');
		$("body").append($qrcodeWX);
	$(".icon-wx").hover(function(){		
		$qrcodeWX.show().css({
			top: $(this).offset().top,
			left:  $(this).position().left,
			marginLeft: -81 + $(this).width()/2,
			marginTop: -175
		});
	},function(){
		$qrcodeWX.hide();
	});
	
});