<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="NHH.WebSite.Official.Contacts" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-专业的商业地产运营管理公司,涵盖商业规划,定位,招商,信息系统,工程,物管,商业内容研发等,通过智能化的运营管理平台，为顾客、商家、业主、经营者、供应商提供着高效而便捷的服务,核心产品为大型的超级城市中心中型的创新生活中心,小型的社区商业中心" />
    <title>上海立天唐人商业集团有限公司 - 联系我们</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/about.css" type="text/css" rel="stylesheet" />
</head>
<!--[if lt IE 7]><body class="ie6 ielt7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 7]><body class="ie7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 8]><body class="ie8 ie"><![endif]-->
<!--[if IE 9]><body class="ie9 ie"><![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body>
    <!--<![endif]-->
    <!--[if lt IE 9]><script src="js/html5shiv.js"></script><![endif]-->
    <head:head ID="head" runat="server" CurrentMenu="联系我们" />

    <div id="main">
        <div class="wraper">
            <section class="topban topban-contact">
           <div class="container">
               <h3>联系我们<em>CONTACT US</em></h3>
               <div class="tab banner-tab">
                    <a class="now">联系方式</a><b>|</b><a href="Complain.aspx">投诉建议</a>
               </div>
           </div>
        </section>
            <!-- topbanner end-->
            <section id="profile-comnent" class="profile-content">
                <div class="coment">
                    <div class="comentA contents mt10">
                        <!--<img src="img/map.png">-->
                        <div id="allmap"></div>
                        <p>
                            <strong>立天唐人商业集团有限公司   总部</strong>
                            地址： 上海浦东新区富城路99号震旦国际大厦32F （陆家嘴CBD）<br>
                            电话：021-31352309
                            <br>
                            传真：021-31352338
                            <br>
                            邮编：200120
                            <br>
                            网址：<a href="http://www.nhhcg.com" target="_blank">www.nhhcg.com</a>
                        </p>
                        <div class="clear"></div>
                    </div>
                </div>
            </section>
        </div>
    </div>

    <foot:foot ID="foot" runat="server" />

    <toolbox:toolbox ID="toolbox" runat="server" />

    <!--整站共用JS引用-->
    <!--[if IE 6]>
<script type="text/javascript" src="js/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>
    DD_belatedPNG.fix('.ie6png');
</script>
<![endif]-->
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/global.js"></script>

    <!--首页特有JS引用-->
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>

    <script type="text/javascript">
        //百度地图API功能
        function loadJScript() {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=pkWO8KT09r9XVv1BCEKpnblT&callback=init";
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

            vectorMarker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画

            map.centerAndZoom(point, 18);
        }

        window.onload = loadJScript;  //异步加载地图
    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
