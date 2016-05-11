<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapTest.aspx.cs" Inherits="NHH.WebSite.Official.MapTest" %>

<!DOCTYPE html>

<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<style type="text/css">
	body, html,#allmap {width: 500px;height: 500px;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
	<title>MapTest</title>
</head>
<body>
	<div id="allmap" ></div>
</body>
</html>
<script type="text/javascript">
    //百度地图API功能
    function loadJScript() {
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "http://api.map.baidu.com/api?v=2.0&ak=l5tGpgKT4dObeGMe4MGEeqmA&callback=init";
        document.body.appendChild(script);
    }
    function init() {
        var map = new BMap.Map("allmap");            // 创建Map实例
        var point = new BMap.Point(121.506382, 31.240036); // 创建点坐标

        var vectorMarker = new BMap.Marker(new BMap.Point(121.506382, 31.240036), {
            // 指定Marker的icon属性为Symbol
            icon: new BMap.Symbol(BMap_Symbol_SHAPE_POINT, {
                scale: 1.5,//图标缩放大小
                fillColor: "orange",//填充颜色
                fillOpacity: 0.8//填充透明度
            })
        });
        
        map.enableScrollWheelZoom();                 //启用滚轮放大缩小

        var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 左上角，添加比例尺
        var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
        var top_right_navigation = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL }); //右上角，仅包含平移和缩放按钮
        map.addControl(top_left_control);
        map.addControl(top_left_navigation);
        map.addControl(top_right_navigation);

        map.addOverlay(vectorMarker);

        //var marker = new BMap.Marker(point);  // 创建标注
        //map.addOverlay(marker);               // 将标注添加到地图中

        //marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画

        vectorMarker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
        
        map.centerAndZoom(point, 18);
    }

    window.onload = loadJScript;  //异步加载地图
</script>
