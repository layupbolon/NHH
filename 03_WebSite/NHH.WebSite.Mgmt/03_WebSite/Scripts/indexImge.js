$(function () {
    var container = $('#container');
    var list = $('#list');
    var prev = $('#prev');
    var next = $('#next');
    var index = 1;
    var len = 3;
    var interval = 2000;
    var timer;
    function animate (offset) {
        var left = parseInt(list.css('left')) + offset;

        if (offset>0) {
            offset = '+=' + offset;
        }
        else {
            offset = '-=' + Math.abs(offset);
        }
        list.animate({'left': offset}, 300, function () {
            if(left > -200){
                list.css('left', -624 * len);
            }
            if(left < (-624 * len)) {
                list.css('left', -624);
            }
        });
    }
    function play() {
        timer = setTimeout(function () {
            next.trigger('click');
            play();
        }, interval);
    }
	
    function stop() {
        clearTimeout(timer);
    }

    next.bind('click', function () {
        if (list.is(':animated')) {
            return;
        }
        if (index == 3) {
            index = 1;
            $("#prev span").html("3/1");
        }
        else {
            index += 1;
            $("#prev span").html("3/"+index);
        }
        animate(-624);
    });

    prev.bind('click', function () {
        if (list.is(':animated')) {
            return;
        }
        if (index == 1) {
            index = 3;
            $("#prev span").html("3/3");
        }
        else {
            index -= 1;
            $("#prev span").html("3/"+index);
        }
        animate(624);
    });
	//hover() 方法规定当鼠标指针悬停在被选元素上时要运行的两个函数。该方法触发 mouseenter 和 mouseleave 事件。注意：如果只规定了一个函数，则它将会在 mouseenter 和 mouseleave 事件上运行。

    container.hover(stop, play);
    play();

});