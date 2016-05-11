/*
	Demo:
	$(".productshow").Xslider({
            unitDisplayed: 1,//可视的单位个数
            numtoMove: 1,
            dir: "H",//水平移动还是垂直移动       默认H为水平移动，传入V则表示垂直移动,传入F则表示渐变切换（注意是大写字母,F时还有限制）;
            viewedSize:120,//可视宽度或高度     不传入则查找要移动对象外层的宽或高度;
            unitLen:20,//移动的单位宽或高度（像素）      不传入则查找第一个li的尺寸;
            speed: 500,//滚动速度（毫秒）     默认为500;
            autoScroll:2000,//自动移动间隔时间（毫秒）     不传入则不会自动移动;
            loop:"cycle",//循环滚动 
            scrollObj:"ul",//要移动的对象    不传入则查找productshow下的ul;		    
		    scrollObjSize://移动最长宽或高（像素）（要移动对象的宽度或高度）   不传入则由li个数乘以unitLen计算;
            scrollunits: "li",//移动单位的对象数组，默认的是查找li
            curStyle: "current",//当前移动对象的样式，默认是"current"
            eventType: "click",//触发出点的事件类型
            imgInit: 1,//当前初始化的位置
            stepOne: false,//移动到每一个移动点。默认情况下滚动到最后能显示的一屏触点失效
            btnNext: "a.aright",//移动到上一屏的触电选择器 默认为"a.aright"
            btnPrevious: "a.aleft",//移动到下一屏的触电选择器 默认为"a.aleft"
            start:1,//设置开始位置 默认是将第一个当做开始位置。
            beforeStart:function(e){//移动之前出发的事件
    		    //before code;
                //e:{
                    goIndex: 当前移动到,
                    container: slider容器,
                    current: 当前单位,
                    next: 移动后单位,
                    eventTrigger: 出发的触点,
                    EOF: 是否是结尾,
                    BOF: 是否是开始
                 }
		    },
            afterEnd:function(e){//移动之后出发的事件
                //end code;
                //e 同上
            },
            disableStyle: { previous: "agrayleft", next: "agrayright" },//触电失效时的样式。
            navCurStyle: "current",//当前nav触点的样式 默认是"current"
            showNav: null,//是否需要nav触点的选择器 默认为null
            navEventType: "mouseenter"//nav触点的事件类型 默认为click

   });
   事件
   go: function (n, eventTrigger, callback);//移动单位方法，{n:移动到的序号,eventTrigger:触点对象,移动动画完成后触发的事件}
   prev: function ();//移动到上一个单位
   next: function ();//移动到下一个单位
   getSet: function ();//获取的配置项对象
   init: function (settings);//重新初始化对象 settings配置项对象
   getThis: function ();//获取对象内部元素
   eq: function (n);//当容器为一个数组的时候，选择第n个容器，返回值对象本身。
*/

(function ($) {
    $.extend($.easing, {
        easeOutQuint: function (x, t, b, c, d) {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
    });

    var init = function (container, settings) {
        var self = this;
        self.settings = settings;
        self.container = container;
        self.selector = container.selector;
        self.container.each(function () {
            var me = $(this);
            var scrollObj = me.find(settings.scrollObj),
                scrollunits = scrollObj.find(settings.scrollunits),
                unitLen = settings.unitLen || (settings.dir == "H" ? scrollunits.eq(0).outerWidth() : scrollunits.eq(0).outerHeight()),
                unitDisplayed = settings.unitDisplayed,
                numtoMove = settings.numtoMove > unitDisplayed ? unitDisplayed : settings.numtoMove;
            scrollObj.parent().css({ position: "relative", overflow: "hidden" });
            var runenv = {
                scrollObj: scrollObj,
                viewedSize: settings.viewedSize || (settings.dir == "H" ? scrollObj.parent().width() : scrollObj.parent().height()), //length of the wrapper visible;
                scrollunits: scrollunits, //units to move;
                unitLen: unitLen,
                unitDisplayed: unitDisplayed, //units num displayed;
                numtoMove: settings.numtoMove > unitDisplayed ? unitDisplayed : settings.numtoMove,
                scrollObjSize: settings.scrollObjSize || scrollunits.length * unitLen, //length of the scrollObj;
                offset: 0, //max width to move;
                offsetnow: 0, //scrollObj now offset;
                movelength: unitLen * numtoMove,
                pos: settings.dir == "H" ? "left" : "top",
                moving: false, //moving now?;
                start: settings.start,
                btnright: me.find(settings.btnNext),
                btnleft: me.find(settings.btnPrevious)
            }
			
			if((runenv.viewedSize/runenv.unitLen)>runenv.scrollunits.length){
				settings.loop = "";
			}
			runenv.scrollObjSize = Math.max(runenv.scrollObjSize,runenv.viewedSize);

            if (settings.loop == "cycle") {
                runenv.canMoveLength = runenv.scrollunits.length;
            } else if (settings.stepOne) {
                runenv.canMoveLength = runenv.scrollunits.length;
                runenv.canMoveSize = runenv.scrollObjSize - runenv.viewedSize;
            } else {
                runenv.canMoveSize = runenv.scrollObjSize - runenv.viewedSize;
                runenv.canMoveLength = Math.ceil(runenv.canMoveSize / runenv.unitLen) + 1;
            }
            runenv.scrollunits.each(function (k, i) {
                $(this).attr("index", k);
            })

            //initialization css

            if (settings.dir == "F2") {
                var len = runenv.scrollunits.length;
                runenv.scrollunits.each(function (i, v) {
                    $(this).css("z-index", len - i);
                })
            }
            settings.dir == "H" ? runenv.scrollObj.css("left", "0px") : runenv.scrollObj.css("top", "0px");

            //cycle tyle initialization dom
            if (settings.loop == "cycle") {
                var frontobj = [], backobj = [];
                for (var i = 0; i < runenv.unitDisplayed; i++) {
                    var beforeobj = runenv.scrollunits.eq(runenv.scrollunits.length - runenv.unitDisplayed + i).clone();
                    var afterobj = runenv.scrollunits.eq(runenv.unitDisplayed - i - 1).clone();
                    beforeobj.attr("index", ~ ~beforeobj.attr("index") - runenv.unitDisplayed - 1);
                    afterobj.attr("index", ~ ~afterobj.attr("index") + runenv.unitDisplayed + 1);
                    runenv.scrollunits.eq(0).before(beforeobj);
                    runenv.scrollunits.eq(runenv.scrollunits.length - 1).after(afterobj);

                }
                var offset_c = 0 - runenv.unitLen * (runenv.unitDisplayed - runenv.start + 1);
                settings.dir == "H" ? runenv.scrollObj.css("left", offset_c) : runenv.scrollObj.css("top", offset_c);
            }


            //save environment variable
            var current = scrollunits.eq(0);
            me.data("runenv", runenv);
            me.data("curobj", current);
            me.data("curindex", 1);
            self.settings.canMoveSize = runenv.canMoveSize;
            self.settings.canMoveLength = runenv.canMoveLength;

            //setting current
            runenv.scrollunits.removeClass(settings.curStyle);
            runenv.scrollunits.eq(settings.imgInit - 1).addClass(settings.curStyle);

            //move to starting position
            fnmove.call(me, me.current, settings.imgInit, settings, true);
            if (settings.showNav) {
				if(typeof settings.showNav === 'boolean'){
					settings.showNav=".nav";	
				}
                me.find(settings.showNav).removeClass(settings.navCurStyle);
                me.find(settings.showNav).eq(settings.imgInit - 1).addClass(settings.navCurStyle);
                me.find(settings.showNav).bind(settings.navEventType, function (e) {
                    fnmove.call(me, current, $(this).index() + 1, settings);
                    return false;
                })
            }
        });

    }
    var init3D = function (container, settings) {
        
    }
    var fnmove = function (cur, goIndex, settings, flg, eventTrigger, callback) {
        var self = $(this), offset_0 = 0, gotoIndex = goIndex;
        var curindex = self.data("curindex");

        var runenv = self.data("runenv");
        if (settings.loop == "cycle") {

            if (goIndex <= 0) {
                offset_0 = (curindex + runenv.scrollunits.length + runenv.unitDisplayed - runenv.start) * runenv.unitLen;

                settings.dir == "H" ? runenv.scrollObj.css("left", -offset_0) : runenv.scrollObj.css("top", -offset_0);
                goIndex = runenv.scrollunits.length + goIndex;

            } else {
                if (curindex > runenv.scrollunits.length - runenv.unitDisplayed && goIndex > runenv.scrollunits.length) {
                    offset_0 = (curindex - runenv.scrollunits.length + runenv.unitDisplayed - runenv.start) * runenv.unitLen;
                    settings.dir == "H" ? runenv.scrollObj.css("left", -offset_0) : runenv.scrollObj.css("top", -offset_0);
                    goIndex = ((goIndex - 1) % runenv.scrollunits.length) + 1;
                }
            }
        }

        goIndex = goIndex >= runenv.canMoveLength ? runenv.canMoveLength : goIndex;

        if (goIndex <= 0) {
            goIndex = 1;
        }
        if (goIndex > 0) {
            var offset = runenv.unitLen * (goIndex - runenv.start);
            if (offset < 0) offset = 0;
            self.data("curindex", goIndex);
            var currentobj = self.data("curobj");
            var EOF = false;
            var BOF = false;
            self.data("curobj", runenv.scrollunits.eq(goIndex - 1));
            runenv.scrollunits.removeClass(settings.curStyle);
            runenv.scrollunits.eq(goIndex - 1).addClass(settings.curStyle);
            if (settings.showNav) {
                self.find(settings.showNav).removeClass(settings.navCurStyle);
                self.find(settings.showNav).eq(goIndex - 1).addClass(settings.navCurStyle);
            }
            if (settings.loop == "cycle") {
                offset = runenv.unitLen * (goIndex - runenv.start + runenv.unitDisplayed);
            }
            if (curindex > goIndex) {
                if (settings.loop == "cycle") { 
                
                }
                else {
                    offset = offset <= runenv.scrollObjSize - runenv.viewedSize ? offset : runenv.scrollObjSize - runenv.viewedSize;
                }
                //console.log("<-", runenv.scrollObjSize - runenv.viewedSize - runenv.unitLen, offset);
                if (offset <= runenv.scrollObjSize - runenv.viewedSize || settings.loop == "cycle") {
                    if (goIndex <= 1 && settings.loop != "cycle") {
                        BOF = true;
                    }
                    settings.beforeStart.call(self, {
                        goIndex: gotoIndex,
                        container: self,
                        current: currentobj,
                        next: runenv.scrollunits.eq(goIndex - 1),
                        eventTrigger: eventTrigger,
                        EOF: EOF,
                        BOF: BOF
                    });
                    $.fn.Xslider.sn.animate.call(self, runenv.scrollObj, -offset, settings.dir, runenv.unitLen, settings.speed, function () {

                        settings.afterEnd.call(self, {
                            goIndex: gotoIndex,
                            container: self,
                            current: currentobj,
                            next: runenv.scrollunits.eq(goIndex - 1),
                            eventTrigger: eventTrigger,
                            EOF: EOF,
                            BOF: BOF
                        });
                        if (callback) {
                            callback.call(self);
                        }
                    }, flg);
                }

            } else if (curindex == goIndex) {
                //console.log("==");
                if (callback) {
                    callback.call(self);
                }
            } else {
                //console.log("->");

                if (settings.loop == "cycle") {

                } else {
                    runenv.scrollObjSize = runenv.scrollObjSize < runenv.viewedSize ? runenv.viewedSize : runenv.scrollObjSize;
                    offset = runenv.viewedSize > runenv.scrollObjSize - offset ? runenv.scrollObjSize - runenv.viewedSize : offset;
                }
                if (goIndex >= runenv.canMoveLength && settings.loop != "cycle") {
                    EOF = true;
                }
                settings.beforeStart.call(self, {
                    goIndex: gotoIndex,
                    container: self,
                    current: currentobj,
                    next: runenv.scrollunits.eq(goIndex - 1),
                    eventTrigger: eventTrigger,
                    EOF: EOF,
                    BOF: BOF
                });

                $.fn.Xslider.sn.animate.call(self, runenv.scrollObj, -offset, settings.dir, runenv.unitLen, settings.speed, function () {
                    settings.afterEnd.call(self, {
                        goIndex: gotoIndex,
                        container: self,
                        current: currentobj,
                        next: runenv.scrollunits.eq(goIndex - 1),
                        eventTrigger: eventTrigger,
                        EOF: EOF,
                        BOF: BOF
                    });
                    if (callback) {
                        callback.call(self);
                    }
                }, flg);
            }
            runenv.btnleft.removeClass(settings.disableStyle.previous);
            runenv.btnright.removeClass(settings.disableStyle.next);

            if (goIndex <= 1 && settings.loop != "cycle") {
                runenv.btnleft.addClass(settings.disableStyle.previous);
            }
            if (goIndex >= runenv.canMoveLength && settings.loop != "cycle") {
                runenv.btnright.addClass(settings.disableStyle.next);
            }

        } else {
            if (settings.loop != "cycle") {
                runenv.btnleft.addClass(settings.disableStyle.previous);
            }
        }
    }
    var fnmuve3D = function (cur, goIndex, settings, flg, eventTrigger, callback) {
        var self = $(this), offset_0 = 0, gotoIndex = goIndex;
        var curindex = self.data("curindex");

        var runenv = self.data("runenv");

    }
    var nextEvent = function (settings, eventTrigger) {
        var me = this;
        fnmove.call(me, me.data("curobj"), me.data("curindex") + settings.numtoMove, settings, false, eventTrigger);
    }
    var previousEvent = function (settings, eventTrigger) {
        var me = this;
        fnmove.call(me, me.data("curobj"), me.data("curindex") - settings.numtoMove, settings, false, eventTrigger);
    }
    var ClassSlider = function (container, settings) {
        var self = this;
        // factory or constructor
        if (!(self instanceof ClassSlider)) {
            return new ClassSlider(container, $.extend({}, $.fn.Xslider.sn.defaults, settings));
        }
        self.settings = settings;
        init.call(this, container, self.settings);

        self.container.each(function () {
            var me = $(this);
            var runenv = $(this).data("runenv");
            var btnright = $(this).find(settings.btnNext),
			   	btnleft = $(this).find(settings.btnPrevious);
            btnleft.unbind(settings.eventType).bind(settings.eventType, function () {
                previousEvent.call(me, settings, this);
                return false;
            });
            btnright.unbind(settings.eventType).bind(settings.eventType, function () {
                nextEvent.call(me, settings, this);
                return false;
            });

            if (settings.autoScroll && runenv.scrollunits.length > runenv.unitDisplayed) {
                //$.fn.Xslider.sn.autoScroll.call(self,me, settings.autoScroll);
                var autoScrolling = function () {							
                    if (runenv.scrollunits.length<=~~me.data("curindex") && settings.loop!="cycle") {
                        fnmove.call(me, me.data("curobj"), 1, settings);
                    } else {
                        nextEvent.call(me, settings);
                    }
					scrollTimmer = setTimeout(autoScrolling, settings.autoScroll);
                };
                var scrollTimmer = setTimeout(autoScrolling, settings.autoScroll);

                me.hover(function () {
                    clearTimeout(scrollTimmer);
                }, function () {
                    scrollTimmer = setTimeout(autoScrolling, settings.autoScroll);
                });

            }
        });
    }
    ClassSlider.prototype = {
        go: function (n, eventTrigger, callback) {
            var self = this;
            //self.container = $(self.selector);
            self.container.each(function () {
                var me = $(this);
                fnmove.call(me, me.data("curobj"), n, self.settings, false, eventTrigger, callback);

            });

        },
        prev: function () {
            var self = this;
            self.container.each(function () {
                var me = $(this);
                previousEvent.call(me, self.settings);

            });
        },
        next: function () {
            var self = this;
            self.container.each(function () {
                var me = $(this);
                nextEvent.call(me, self.settings);

            });
        },
        getSet: function () {
            return this.settings;
        },
        init: function (settings) {
            if (settings) {
                this.settings = settings;
            }
            init.call(this, this.container, this.settings);
        },
        getThis: function () {
            return this;
        },
        eq: function (n) {
            this.container = $(this.selector).eq(n);
            return this;
        }
    }

    $.fn.Xslider = function (settings) {
        return ClassSlider(this, settings);
    }

    $.fn.Xslider.sn = {
        defaults: {
            unitDisplayed: 1,
            dir: "H",
            showNav: null,
            navEventType: "mouseenter",
            speed: 500,
            scrollObj: "ul",
            scrollunits: "li",
            curStyle: "current",
            eventType: "click",
            imgInit: 1,
            start: 1,
            numtoMove: 1,
            stepOne: false,
            btnNext: "a.aright",
            btnPrevious: "a.aleft",
            afterEnd: function (e) { },
            beforeStart: function (e) { },
            disableStyle: { previous: "agrayleft", next: "agrayright" },
            navCurStyle: "current"

        },
        animate: function (obj, w, dir, unitLen, speed, callback, noAnimate) {
            if (dir == "H") {
                if (noAnimate) {
                    obj.css({ left: w });
                    callback.call(this);
                } else {
                    obj.stop().animate({
                        left: w
                    }, speed, "easeOutQuint", callback);
                }
            } else if (dir == "V") {
                if (noAnimate) {
                    obj.css({ top: w });
                    callback.call(this);
                } else {
                    obj.stop().animate({
                        top: w
                    }, speed, "easeOutQuint", callback);
                }
            } else if (dir == "F") {
				var curindex=$(this).data("curindex");
                if (noAnimate) {
                    obj.children(":visible").hide();
                    obj.children().eq(curindex-1).show();
                } else {
                    obj.children(":visible").fadeOut(120);
					
                    obj.children().eq(curindex-1).fadeIn(speed, callback);
                }
            }
            else if (dir == "F2") {
				var curindex=$(this).data("curindex");
                if (noAnimate) {
                    obj.children(":visible").hide();
                    obj.children().eq(curindex-1).show();
                } else {
                    var runenv = $(this).data("runenv");
                    var curobj = $(this).data("curobj");
                    runenv.scrollObj.prepend(curobj);
                    var len = runenv.scrollunits.length;
                    $(runenv.scrollunits.selector).each(function (i, v) {
                        $(this).css("z-index", len - i);
                    })
                    curobj.hide().fadeIn(speed, callback);
                    /*obj.children(":visible").css({zIndex:0}).show();
                    obj.children().eq(-w / unitLen).hide().css({zIndex:1}).fadeIn(speed, callback);*/
                }
            } else if (dir == "noanim") {
                callback();
            }
        }
    }
})(jQuery);

window["UI"] = window["UI"] || {};
UI["Xslider"]=function(s,op){
	return $(s).Xslider(op);
}