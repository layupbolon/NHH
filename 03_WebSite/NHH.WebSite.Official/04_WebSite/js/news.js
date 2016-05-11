$(function(){

	var loadResult = function(ms) {
		//A: 模拟Ajax
	    var dtd = $.Deferred();
	    dtd.notify();
	    setTimeout(dtd.resolve, ms);
	    // setTimeout(dtd.reject, ms);
	    // setTimeout(dtd.notify, ms);
	    return dtd.promise(); //此处也可以直接返回dtd

	    //B: 以上是模拟，开发时可以使用以下方式
	   /* return $.ajax({
	    	url: "test.html",
	    	data: "",
	    	timeout: ms
	    });*/
	};

	var renderNewItems = function(){
		var html = $("#item-template").html();
		for(var i = 1, n = 4; i < 4; i++){
			html += html; 
		}
		$('.news .listwrap').last().after(html);
	};

	var showloading = function(ms) {
	    $(".btnMore").addClass('btn-loading').find(".cmnBtn").text('LOADING...');
	};

	var hideloading = function(ms) {
	    $(".btnMore").removeClass('btn-loading').find(".cmnBtn").text('SHOW MORE');
	};

	//模拟AJAX请求异步加载
	function simulateAjax(ms){		
		loadResult(ms).done(function(d) {
			console.log(d);
			hideloading();
			renderNewItems();
		    console.log('Done');
		}).fail(function() {
			hideloading();
		    //console.log('失败了');
		}).progress(function(res) {
			showloading();
		    //console.log('等待中...');
		});
	}

	//Click Show More News items
	$('.btnMore').on("click",function(){
		if($(".btnMore").hasClass('btn-loading')) return;
		var timeout = 2500;//设置超时时间
		simulateAjax(timeout);
	});
	
	
});

