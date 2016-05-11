<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="NHH.WebSite.Official.Project" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="<%=projectInfo.Description %>" />
    <title>上海立天唐人商业集团有限公司 - <%=projectInfo.ProjectName %></title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/business.css" type="text/css" rel="stylesheet" />
</head>
<!--[if lt IE 7]><body class="ie6 ielt7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 7]><body class="ie7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 8]><body class="ie8 ie"><![endif]-->
<!--[if IE 9]><body class="ie9 ie"><![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body>
    <!--<![endif]-->
    <!--[if lt IE 9]><script src="js/html5shiv.js"></script><![endif]-->

    <head:head ID="head" runat="server" CurrentMenu="商机提供" />

    <div id="main">
        <div class="wraper">
            <section class="topbanner pj_banner mt80">
                <ul>
                    <%=service.GetProjectBanner(projectID) %>
                </ul>
                <div class="navDot">
                    <div class="navInner">
                        <% for(int i = 0; i < service.GetProjectBannerCount(projectID); i++)
                           { %>
                        <a href="###"><span class="ie6png">i+1</span></a>
                        <% } %>
                    </div>
                </div>
                <div class="scrolldown ie6png"></div>

            </section>
            <!-- topbanner end-->
            <div class="pj_info">
                <div class="inner">
                    <ul>
                        <li>
                            <p>商业运营面积</p>
                            <p class="numFont"><%=projectInfo.OperatingArea %></p>
                            <p>万平方米</p>
                        </li>
                        <li class="noneLine">
                            <p>商业建筑面积</p>
                            <p class="numFont"><%=projectInfo.GrossArea %></p>
                            <p>万平方米</p>
                        </li>
                        <li class="middle_pjname noneLine">
                            <p class="pj"><%=projectInfo.ProjectBriefName %></p>
                            <p><%=projectInfo.Feature %></p>
                        </li>
                        <li>
                            <p>开业时间</p>
                            <p class="numFont"><%=projectInfo.OpeningDate.ToString("yyyy.MM") %></p>
                            <% if (projectInfo.OpeningDate > DateTime.Now)
                               {%>
                            <p>敬请期待</p>
                            <%} %>
                            
                        </li>
                        <li class="noneLine">
                            <p>商圈人口</p>
                            <p class="numFont"><%=projectInfo.Population %></p>
                            <p>万人</p>
                        </li>
                    </ul>
                </div>
            </div>

            <section id="profile-section" class="column">
                <div class="coment profile cls">
                    <h3>项目概况</h3>
                    <em>Project profile</em>
                    <dl class="lineBot mt30">
                        <dd>楼盘位置：<%=projectInfo.ProjectBriefName %></dd>
                        <dd>开业时间：<%=projectInfo.OpeningDate.ToString("yyyy.MM") %></dd>
                        <dd>商业建筑面积：<%=projectInfo.GrossArea %>万平方米</dd>
                        <dd>商业运营面积：<%=projectInfo.OperatingArea %>万平方米</dd>
                        <dd>商业类型：<%=projectInfo.BusinessType %></dd>
                        <dd>主力品牌：大地影院</dd>
                        <dd>商圈人口：<%=projectInfo.Population %>万人</dd>
                        <dd>月均人流：<%=projectInfo.MonthlyHumanTraffic.ToString()!="0" ? projectInfo.MonthlyHumanTraffic.ToString() : "--"%>万人次</dd>
                    </dl>
                    <div class="clear"></div>
                    <div class="project-map mt30 mr10 mb30 fr">
                        <div id="project-map"></div>
                    </div>
                    <dl class="contentPj mt30">
                        <dt>地理位置</dt>
                        <dd><%=projectInfo.Position %></dd>
                    </dl>
                    <dl class="contentPj mt30">
                        <dt>交通状况</dt>
                        <dd><%=projectInfo.PublicTransport %></dd>
                    </dl>
                    <dl class="contentPj mt30">
                        <dt>咨询热线：<%=projectInfo.Tel %></dt>
                    </dl>
                </div>

            </section>
            <section id="profile-comnent" class="profile-content">
                <div class="coment" style="margin-top: 0;">
                    <div class="tab_inner">
                        <div class="tab tabA" data-tab="enable" trigger="click">
                            <a href="javascript:" class="now">项目概况</a>
                            <a href="javascript:">图片展示</a>
                            <a href="KioskList.aspx" rel="link">场地租借及多种经营</a>
                            <a href="ADPositionList.aspx" rel="link">广告位招租</a>
                            <a href="UnitList.aspx" class="none" rel="link">我要入驻</a>
                        </div>
                        <div class="innerwrap mt20">
                            <div class="tabc">
                                <dl class="textinner">
                                    <dd>
                                        <%=projectInfo.Introduce %>
                                    </dd>
                                </dl>
                            </div>

                            <div class="tabc" style="display: none;">
                                <div class="pic-gallery">

                                    <ul class="cls">
                                        <%=service.GetProjectPic(projectID) %>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="box-slide mb50">

                <div class="coment">
                    <h3>商场活动</h3>
                    <em>MARKETING Activity</em>
                </div>

                <div class="wraper-1200 rel">
                    <div class="slide-contain mt20">
                        <div class="slide-area">
                            <ul class="mover cls">
                                <%=service.GetProjectCampaign(projectID) %>
                            </ul>
                        </div>
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

    <!--特有JS引用-->
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>
    <script type="text/javascript" src="js/business.js"></script>
    <script type="text/javascript">
        //百度地图API功能
        function loadJScript() {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=pkWO8KT09r9XVv1BCEKpnblT&callback=init";
            document.body.appendChild(script);
        }
        function init() {
            var longitude = <%=projectInfo.Longitude%>;
            var latitude = <%=projectInfo.Latitude%>;
            if (longitude == 0) longitude = 121.506382;
            if (latitude == 0) latitude = 31.240036;
            var map = new BMap.Map("project-map");            // 创建Map实例
            var point = new BMap.Point(longitude, latitude); // 创建点坐标

            var vectorMarker = new BMap.Marker(new BMap.Point(longitude, latitude), {
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

    <!-- Add fancyBox -->
    <link rel="stylesheet" href="js/fancybox/jquery.fancybox.css?v=2.1.5" type="text/css" media="screen" />
    <script type="text/javascript" src="js/fancybox/jquery.fancybox.pack.js?v=2.1.5"></script>
    <script>
        $(".fancybox").fancybox({
            nextEffect: null,
            prevEffect: null,
            loop: false,
            padding: 10
        });
    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
