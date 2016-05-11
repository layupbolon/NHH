// JavaScript Document
/**
	Last Update:	2013.05.17
	Update Logs:
		1¡¢Fix no image bugs in FF & Webkit;
		2¡¢Fix Zoom Window appears at left side
**/

(function(A){
	A.fn.jqzoom=function(B){
		var C={
			zoomWidth:200,
			zoomHeight:200,
			xOffset:10,
			yOffset:0,
			showEffect:"show",
			hideEffect:"hide",
			fadeinSpeed:"fast",
			fadeoutSpeed:"slow"
		};
		B=B||{};
		A.extend(C,B);
		return this.each(function(){
			var U=A(this);
			var X=A("img",this);
			var L=new Q(X);//Mid Img
			var S={};
			L.loadimage();
			var I=0;
			var R=0;
			var E=new P(U[0].href);//Big Img
			var V=new J();//Lens
			var M={};
			var K=false;
			var W={};
			var T=null;
			var F=false;
			var O={};
			var N=0;
			A(this).click(function(){return false});
			U.css({cursor:"crosshair",display:"block"});
			
			if(U.css("position")!="absolute"&&U.parent().css("position")){
				U.css({cursor:"crosshair",position:"relative",display:"block"})
			}
			
			if(U.parent().css("position")!="absolute"){
				U.parent().css("position","relative")
			}
			
			if(A.browser.safari||A.browser.opera){
				A(X).css({position:"absolute",top:"0px",left:"0px"})
			}
			
			A(this).hover(
				function(Y){
					X=A("img",this);
					L=new Q(X);//Mid Img
					E=new P(U[0].href);//Big Img
					L.loadimage();
					O.x=Y.pageX;
					O.y=Y.pageY;
					if(typeof(S.w)=="undefined"){return}
					H()
				},
				function(){
					G()
				}
			);
			
			//Hover In Function
			function H(){
				if(!F){
					F=true;
					if(!E||A.browser.safari){
						E=new P(U[0].href)
					}
					if(!K||A.browser.safari){
						E.loadimage()
					}
					else{
						T=new D();//Zoom Window
						T.activate();
						V=new J;//Lens
						V.activate()
					}
					U[0].blur();
					return false
				}
			}
			
			//Hover Out Function
			function G(){
				F=false;
				K=false;
				A(V.node).unbind("mousemove");
				V.remove();
				if(A("div.jqZoomWindow").length>0){
					T.remove()
				}
				A().unbind();
				U.unbind("mousemove");
				N=0;
				if(A(".zoom_ieframe").length>0){
					A(".zoom_ieframe").remove()
				}
			}
			
			//Mid Img
			function Q(Y){
				this.node=Y[0];
				this.loadimage=function(){
					this.node.src=Y[0].src;
					//trigger onload() for Webkit
					A.browser.webkit && this.node.onload();
				};
				this.node.onload=function(){
					//Webkit won't do this, so just trigger onload() at loadimage function
					S.w=A(this).width();
					S.h=A(this).height();
					S.pos=A(this).offset();
					S.pos.l=A(this).offset().left;
					S.pos.t=A(this).offset().top;
					S.pos.r=S.w+S.pos.l;
					S.pos.b=S.h+S.pos.t;
				};
				
				return this
			}
			
			//Lens
			function J(){
				this.node=document.createElement("div");
				A(this.node).addClass("jqZoomPup");
				this.node.onerror=function(){
					A(V.node).remove();
					V=new J();
					V.activate()
				};
				this.loadlens=function(){
					M.w=(C.zoomWidth)/W.x;
					M.h=(C.zoomHeight)/W.y;
					A(this.node).css({
						width:M.w+"px",
						height:M.h+"px",
						position:"absolute",
						display:"none",
						borderWidth:1+"px"
					});
					U.append(this.node)
				};
				return this
			}
			
			J.prototype.activate=function(){
				this.loadlens();
				V.setposition(null);
				A(U).bind("mousemove",function(Y){
					O.x=Y.pageX;
					O.y=Y.pageY;
					V.setposition(Y)
				});
				return this
			};
			
			J.prototype.setposition=function(c){
				if(c){
					O.x=c.pageX;
					O.y=c.pageY
				}
				
				if(N==0){
					var f=(S.w)/2-(M.w)/2;
					var Z=(S.h)/2-(M.h)/2;
					A("div.jqZoomPup").show();
					this.node.style.visibility="visible";
					N=1
				}
				else{
					var f=O.x-S.pos.l-(M.w)/2;
					var Z=O.y-S.pos.t-(M.h)/2
				}
				
				if(a()){
					f=0+R
				}
				else{
					if(Y()){
						if(A.browser.msie){
							f=S.w-M.w+R+1
						}
						else{
							f=S.w-M.w+R-1
						}
					}
				}
				
				if(b()){
					Z=0+I
				}
				else{
					if(d()){
						if(A.browser.msie){Z=S.h-M.h+I+1}
						else{Z=S.h-M.h-1+I}
					}
				}
				
				f=parseInt(f);
				Z=parseInt(Z);
				A("div.jqZoomPup",U).css({top:Z,left:f});
				this.node.style.left=f+"px";
				this.node.style.top=Z+"px";
				E.setposition();
				function a(){return O.x-(M.w+2*1)/2-R<S.pos.l}
				function Y(){return O.x+(M.w+2*1)/2>S.pos.r+R}
				function b(){return O.y-(M.h+2*1)/2-I<S.pos.t}
				function d(){return O.y+(M.h+2*1)/2>S.pos.b+I}
				return this
			};
			
			J.prototype.getoffset=function(){
				var Y={};
				Y.left=parseInt(this.node.style.left);
				Y.top=parseInt(this.node.style.top);
				return Y
			};
			
			J.prototype.remove=function(){
				A("div.jqZoomPup",U).remove()
			};
			
			//Big Img 
			function P(Y){
				this.url=Y;
				this.node=new Image();
				this.node.onload=function(){
					var Z=Math.round(A(this).width());
					var a=Math.round(A(this).height());
					W.x=(Z/S.w);
					W.y=(a/S.h);
					K=true;
					if(F){V=new J();V.activate(),T=new D();T.activate()}
				};
				
				this.loadimage=function(){
					if(!this.node){
						this.node=new Image()
					}
					this.node.style.position="absolute";
					this.node.style.display="none";
					this.node.style.left="-5000px";
					this.node.style.top="10px";
					document.body.appendChild(this.node);
					this.node.src=this.url;
					//if(F){T=new D();T.activate()}
				};
				
				return this
			}
			
			//Update Big img moving position
			P.prototype.setposition=function(){
				this.node.style.left=Math.ceil(-W.x*parseInt(V.getoffset().left)+R)+"px";
				this.node.style.top=Math.ceil(-W.y*parseInt(V.getoffset().top)+I)+"px"
			};
			
			//Zoom Window
			function D(){
				var Z=S.pos.l;
				var Y=S.pos.t;
				this.node=document.createElement("div");
				A(this.node).addClass("jqZoomWindow");
				A(this.node).css({
					position:"absolute",
					width:Math.round(C.zoomWidth)+"px",
					height:Math.round(C.zoomHeight)+"px",
					display:"none",
					zIndex:10000,
					overflow:"hidden"
				});
				
				/*Z=(S.pos.r+Math.abs(C.xOffset)+C.zoomWidth<screen.width)?(S.pos.l+S.w+Math.abs(C.xOffset)):(S.pos.l-C.zoomWidth-Math.abs(C.xOffset));*/
				Z=(C.position=="right")?(S.pos.l+S.w+Math.abs(C.xOffset)):(S.pos.l-C.zoomWidth-Math.abs(C.xOffset));
				topwindow=S.pos.t+C.yOffset+C.zoomHeight;
				Y=(topwindow<screen.height&&topwindow>0)?S.pos.t+C.yOffset:S.pos.t;
				this.node.style.left=Z+"px";
				this.node.style.top=Y+"px";
				return this
			}
				
			D.prototype.activate=function(){
				if(!this.node.firstChild){
					this.node.appendChild(E.node)
				}
				document.body.appendChild(this.node);
				switch(C.showEffect){
					case"show":
						A(this.node).show();
						break;
					case"fadein":
						A(this.node).fadeIn(C.fadeinSpeed);
						break;
					default:
						A(this.node).show();
						break
				}
				
				A(this.node).show();
				if(A.browser.msie&&A.browser.version<7){
					this.ieframe=A('<iframe class="zoom_ieframe" frameborder="0" src="#"></iframe>').css({
						position:"absolute",
						left:this.node.style.left,
						top:this.node.style.top,
						zIndex:99,
						width:C.zoomWidth,
						height:C.zoomHeight
					}).insertBefore(this.node)
				}
				
				E.node.style.display="block"
			};
				
			D.prototype.remove=function(){
				switch(C.hideEffect){
					case"hide":
						A(".jqZoomWindow").remove();
						break;
					case"fadeout":
						A(".jqZoomWindow").fadeOut(C.fadeoutSpeed,function(){$(this).remove()});
						break;
					default:
						A(".jqZoomWindow").remove();
						break
				}
			};
				
		})//end return this.each
	}//end fn.jqzoom
})(jQuery);
