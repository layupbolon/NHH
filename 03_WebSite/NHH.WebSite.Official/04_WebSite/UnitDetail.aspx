<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitDetail.aspx.cs" Inherits="NHH.WebSite.Official.UnitDetail" %>

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
    <title>上海立天唐人商业集团有限公司 - 铺位 <%=unitInfo.UnitNumber %></title>
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
                    <select class="select2" name="pjName" id="sel_pjName">
                    </select>
                    <select class="select2" name="pjBuilding" id="sel_pjBuilding">
                    </select>
                    <select class="select2" name="pjType" id="sel_pjType">
                    </select>
                    <select class="select2" name="pjArea" id="sel_pjArea">
                    </select>
                </div>
            </section>
            <div class="topbanPlaceHolder"></div>

            <div trigger="click" class="tab tabA tab-pj">
                <a class="now">商铺信息</a>
                <a href="KioskList.aspx">场地租借及多种经营</a>
                <a href="ADPositionList.aspx">广告位招租</a>
            </div>

            <div class="proinfo cls mb50">
                <div class="prodetail fr">
                    <div class="pro-main">
                        <div class="prodetailtitle">
                            <h1><%=unitInfo.UnitPosition %> <span class="tag-pro tag-green">待出租</span></h1>
                        </div>
                        <div class="prodetailinner">
                            <div class="info mb10">
                                <ul class="cls">
                                    <li>
                                        <label class="label"><span class="size3t4">商铺号</span>：</label>
                                        <%=unitInfo.UnitNumber %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size2t4">位置</span>：</label>
                                        <%=unitInfo.Position %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size2t4">面积</span>：</label>
                                        <%=unitInfo.Area %>平米
                                    </li>
                                    <li>
                                        <label class="label"><span class="size2t4">租金</span>：</label>
                                        <%=unitInfo.RentRemark %>
                                    </li>
                                    <li>
                                        <label class="label"><span class="size3t4">物业费</span>：</label>
                                        <%= unitInfo.ProperyCosts == 0 ? "暂无" : unitInfo.ProperyCosts + "元/月/平米（按建筑面积）" %> 
                                    </li>
                                    <li>
                                        <label class="label"><span>业态类型</span>：</label>
                                        <%=unitInfo.bizTypesName %>
                                    </li>
                                    <li class="highlight-item">
                                        <label class="label"><span>租赁人员</span>：</label>
                                        <%=unitInfo.Contacts %>
                                    </li>
                                </ul>
                            </div>

                            <div class="info">
                                <ul>
                                    <li class="action-line cls">

                                        <div class="probtn fl cls">
                                            <a class="btn-primary mr20 fl" href="Intention.aspx?type=1&pid=<%=projectID %>">提交意向表单</a>
                                            <a class="btn-secondary mr20 fl" href="Project.aspx?projectID=<%=unitInfo.projectID %>">项目资料</a>
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
                    <ul class="cls" id="unitList">
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

            buildingInit("<%=building%>");

            bizTypeInit("<%=bizType%>");

            areaInit("<%=unitArea%>");

            UnitListInit("<%=projectID%>", "<%=building%>", "<%=bizType%>", "<%=unitArea%>");

        });

        function projectInit(projectID) {
            var project = $("#sel_pjName");
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

        function buildingInit(building) {
            var floor = $("#sel_pjBuilding");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getBuilding",
                    type: "<%=(int)NHHCGTypeEnum.Unit%>"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    floor.empty();
                    var optionValue = "<option value=''>请选择楼层</option>";
                    if (building === "") {
                        optionValue = "<option selected=\"selected\" value=''>请选择楼层</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === building) {
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

        function bizTypeInit(bizTypeID) {
            var bizType = $("#sel_pjType");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getBizType"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    bizType.empty();
                    var optionValue = "<option value='0'>请选择业态类型</option>";
                    if (bizTypeID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择业态类型</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === bizTypeID) {
                            optionValue += "<option selected=\"selected\" value='" + item.Value + "'>" + item.Text + "</option>";
                        } else
                            optionValue += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    bizType.append(optionValue);
                },
                complete: function () {
                },
                error: function (xh) {
                }
            });
        }

        function areaInit(unitAreaID) {
            var area = $("#sel_pjArea");
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
                    if (unitAreaID === 0) {
                        optionValue = "<option selected=\"selected\" value='0'>请选择面积范围</option>";
                    }
                    data.forEach(function (item) {
                        if (item.Value === unitAreaID) {
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

        function UnitListInit(projectID, building, bizType, unitArea) {
            var query = {
                projectID: projectID,
                building: building,
                bizType: bizType,
                unitArea: unitArea
            }
            var unitList = $("#unitList");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getUnitList",
                    query: JSON.stringify(query),
                    uid: "<%=unitID%>"
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    unitList.empty();

                    var length = 4;
                    if (data.length <= 4)
                        length = data.length;

                    var optionValue = "";
                    for (var i = 0; i < length; i++) {
                        var item = data[i];
                        optionValue += "<li><a target=\"_blank\" href=\"" + "UnitDetail.aspx?area=" + unitArea + "&biz=" + bizType + "&bid=" + building + "&pid=" + projectID + "&unitID=" + item.UnitID + "\">";
                        optionValue += "<img src=\"<%=ParamManager.GetStringValue("ImageSite")%>" + item.Pic + "\">";
                        optionValue += "<span class=\"default-content\">铺位号：" + item.UnitNumber + "</span>";
                        optionValue += "<span class=\"mask-content\">";
                        optionValue += "<h4 class=\"n-tit\">" + item.UnitNumber + "</h4>";
                        optionValue += "<em>商铺位置：" + item.UnitPosition + "</em>";
                        optionValue += "<em>计租面积：" + item.Area + " 平米 </em>";
                        optionValue += "<em>租金价格：" + item.RentRemark + " </em>";
                        optionValue += "</span><i class=\"mask-bg\"></i></a></li>";
                    }

                    unitList.append(optionValue);
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
