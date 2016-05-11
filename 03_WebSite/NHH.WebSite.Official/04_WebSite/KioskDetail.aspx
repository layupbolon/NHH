<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KioskDetail.aspx.cs" Inherits="NHH.WebSite.Official.KioskDetail" %>

<%@ Import Namespace="NHH.Framework.Utility" %>
<%@ Import Namespace="NHH.Models.Official" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-专业的商业地产运营管理公司,涵盖商业规划,定位,招商,信息系统,工程,物管,商业内容研发等,通过智能化的运营管理平台，为顾客、商家、业主、经营者、供应商提供着高效而便捷的服务,核心产品为大型的超级城市中心中型的创新生活中心,小型的社区商业中心" />
    <title>上海立天唐人商业集团有限公司 - 多经点位 <%=kioskInfo.KioskNumber %></title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/select2.css" type="text/css" rel="stylesheet" />
    <link href="css/business.css" type="text/css" rel="stylesheet" />
    <link href="css/shop_detail.css" type="text/css" rel="stylesheet" />
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
            <section class="topban topban-fixed">
                <div class="selector selector-project">
                    <select class="select2" name="sel_Project" id="sel_Project"></select>
                    <select class="select2" name="sel_Floor" id="sel_Floor"></select>
                    <select class="select2" name="sel_Region" id="sel_Region"></select>
                    <select class="select2" name="sel_BusinessScope" id="sel_BusinessScope"></select>
                    <select class="select2" name="sel_AreaScope" id="sel_AreaScope"></select>
                </div>
            </section>
            <div class="topbanPlaceHolder"></div>

            <div trigger="click" class="tab tabA tab-pj">
                <a href="UnitList.aspx">商铺信息</a>
                <a class="now">场地租借及多种经营</a>
                <a href="ADPositionList.aspx">广告位招租</a>
            </div>

            <div class="proinfo cls mb50">
                <div class="prodetail fr">
                    <div class="pro-main">
                        <div class="prodetailtitle">
                            <h1><%=string.Format("{0} - {1}{2}楼",kioskInfo.ProjectName,kioskInfo.Building,kioskInfo.FloorID) %> <span class="tag-pro tag-green">待出租</span></h1>
                        </div>
                        <div class="prodetailinner">
                            <div class="info mb10">
                                <ul class="cls">
                                    <li>
                                        <label class="label"><span class="size2t4">编号</span>：</label>
                                        <%=kioskInfo.KioskNumber %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size2t4">区域</span>：</label>
                                        <%=kioskInfo.RegionName %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size4t4">经营范围</span>：</label>
                                        <%=kioskInfo.BusinessScopesName %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size4t4">面积范围</span>：</label>
                                        <%=kioskInfo.AreaScopeName %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size2t4">租金</span>：</label>
                                        <%=string.IsNullOrEmpty(kioskInfo.RentRemark)?"暂无": kioskInfo.RentRemark %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size4t4">位置描述</span>：</label>
                                        <%=kioskInfo.Position %>
                                    </li>
                                    <li class="highlight-item">
                                        <label class="label"><span>租赁人员</span>：</label>
                                        <%=kioskInfo.Contacts %>
                                    </li>
                                </ul>
                            </div>

                            <div class="info">
                                <ul>
                                    <li class="action-line cls">

                                        <div class="probtn fl cls">
                                            <a class="btn-primary mr20 fl" href="Intention.aspx?type=2&pid=<%=projectID %>">提交意向表单</a>
                                            <a class="btn-secondary mr20 fl" href="Project.aspx?projectID=<%=kioskInfo.ProjectID %>">项目资料</a>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <!--pro-main End-->
                </div>

                <div class="prozoom fl">
                    <div class="bigimg">
                        <a href="<%=GetFirstPic() %>" class="jqzoom">
                            <img src="<%=GetFirstPic() %>" alt="" />
                        </a>
                    </div>

                    <div class="imgzoomlist mt10">
                        <div class="inner">
                            <ul class="cls">
                                <%=GetPicList() %>
                            </ul>
                        </div>
                        <a href="###" class="abtn aleft">左移</a>
                        <a href="###" class="abtn aright">右移</a>
                    </div>
                </div>
                <!--Product Zoom End-->
            </div>
            <!--Proinfo End-->

            <section class="wraper-1200 mb30">
                <h3 class="section-tit">相关铺位推荐</h3>
                <div class="shoplist">
                    <ul class="cls" id="kioskList">
                    </ul>
                </div>
            </section>

            <%--<section class="wraper-1200 mb30">
                <h3 class="section-tit">室内导航</h3>

                <div class="shop-map">
                    <div id="shop-map"></div>
                </div>
            </section>--%>
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
    <%--<script type="text/javascript" src="js/select2.min.js"></script>--%>
    <script type="text/javascript" src="js/jqzoom.js"></script>
    <script type="text/javascript" src="js/shop_detail.js"></script>

    <script type="text/javascript">
        //百度地图API功能
        function loadJScript() {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=pkWO8KT09r9XVv1BCEKpnblT&callback=init";
            document.body.appendChild(script);
        }
        function init() {
            var map = new BMap.Map("shop-map");            // 创建Map实例
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


        $(function () {
            projectInit("<%=projectID%>");

            floorInit("<%=floorID%>");

            regionInit("<%=regionID%>");

            businessScopeInit("<%=businessScopeID%>");

            areaInit("<%=areaID%>");

            KioskListInit("<%=projectID%>", "<%=floorID%>", "<%=regionID%>", "<%=businessScopeID%>", "<%=areaID%>");

        });

        function projectInit(projectID) {
            var project = $("#sel_Project");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: { action: "getProject" },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    project.empty();
                    var optionValue = "<option value='0'>请选择项目</option>";
                    if (projectID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择项目</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === projectID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    project.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function floorInit(floorID) {
            var floor = $("#sel_Floor");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getFloor",
                    type: "<%=(int)NHHCGTypeEnum.Kiosk%>"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    floor.empty();
                    var optionValue = "<option value='0'>请选择楼层</option>";
                    if (floorID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择楼层</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === floorID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    floor.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function regionInit(regionID) {
            var region = $("#sel_Region");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getRegion"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    region.empty();

                    var optionValue = "<option value='0'>请选择区域</option>";
                    if (regionID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择区域</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === regionID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    region.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function businessScopeInit(businessScopeID) {
            var businessScope = $("#sel_BusinessScope");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getBusinessScope"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    businessScope.empty();
                    var optionValue = "<option value='0'>请选择区域</option>";
                    if (businessScopeID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择区域</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === businessScopeID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    businessScope.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function areaInit(areaID) {
            var area = $("#sel_AreaScope");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getArea"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    area.empty();

                    var optionValue = "<option value='0'>请选择面积范围</option>";
                    if (areaID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择面积范围</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === areaID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    area.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function KioskListInit(projectID, floorID, region, businessScope, area) {
            var query = {
                ProjectID: projectID,
                Floor: floorID,
                Region: region,
                BusinessScope: businessScope,
                Area: area
            }
            var kioskList = $("#kioskList");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getKioskList",
                    query: JSON.stringify(query),
                    kid: "<%=kioskID%>"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    kioskList.empty();

                    var length = 4;
                    if (data.length <= 4)
                        length = data.length;

                    var optionValue = "";
                    for (var i = 0; i < length; i++) {
                        var item = data[i];
                        optionValue += "<li><a target=\"_blank\" href=\"" + "KioskDetail.aspx?busScope=" + businessScope + "&area=" + area + "&rid=" + region + "&fid=" + floorID + "&pid=" + projectID + "&kid=" + item.KioskID + "\">";
                        optionValue += "<img src=\"<%=ParamManager.GetStringValue("ImageSite")%>" + item.Pic + "\">";
                        optionValue += "<span class=\"default-content\">多经点位编号：" + item.KioskNumber + "</span>";
                        optionValue += "<span class=\"mask-content\">";
                        optionValue += "<h4 class=\"n-tit\">" + item.KioskNumber + "</h4>";
                        optionValue += "<em>位置描述：" + item.Position + "</em>";
                        optionValue += "<em>计租面积范围：" + item.AreaScopeName + " </em>";
                        optionValue += "<em>租金价格：" + item.RentRemark + "  </em>";
                        optionValue += "</span><i class=\"mask-bg\"></i></a></li>";
                    }

                    kioskList.append(optionValue);
                },
                complete: function () {
                    var oHead = document.getElementsByTagName('HEAD').item(0);
                    var oScript = document.createElement("script");
                    oScript.type = "text/javascript";
                    oScript.src = "js/business.js";

                    var select = document.createElement("script");
                    select.type = "text/javascript";
                    select.src = "js/select2.min.js";

                    oHead.appendChild(oScript);
                    oHead.appendChild(select);
                },
                error: function (xh) {
                }
            });
        }

    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
