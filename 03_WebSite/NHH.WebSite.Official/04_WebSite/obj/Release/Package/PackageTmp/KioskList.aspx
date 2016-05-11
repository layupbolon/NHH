<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KioskList.aspx.cs" Inherits="NHH.WebSite.Official.KioskList" %>

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
    <title>上海立天唐人商业集团有限公司 - 商机提供 多经点位出租</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/select2.css" type="text/css" rel="stylesheet" />
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

            <section class="topban topban-fixed">
                <div class="selector selector-project">
                    <select class="select2" name="sel_Project" id="sel_Project">
                        <option value="0">选择项目</option>
                    </select>
                    <select class="select2" name="sel_Floor" id="sel_Floor">
                        <option selected="selected" value="0">选择楼层</option>
                    </select>
                    <select class="select2" name="sel_Region" id="sel_Region">
                        <option selected="selected" value="0">选择区域</option>
                    </select>
                    <select class="select2" name="sel_BusinessScope" id="sel_BusinessScope">
                        <option selected="selected" value="0">选择经营范围</option>
                    </select>
                    <select class="select2" name="sel_AreaScope" id="sel_AreaScope">
                        <option selected="selected" value="0">选择面积范围</option>
                    </select>
                </div>
            </section>
            <div class="topbanPlaceHolder"></div>

            <div trigger="click" class="tab tabA tab-pj">
                <a href="UnitList.aspx">商铺信息</a>
                <a class="now">场地租借及多种经营</a>
                <a class="none" href="ADPositionList.aspx">广告位招租</a>
            </div>

            <section class="wraper-1200 mb30 mt30">
                <div class="shoplist">
                    <ul class="cls" id="kioskList">
                    </ul>

                    <div class="btnMore tc mt20"><a class="cmnBtn btnE" href="###">SHOW MORE</a></div>
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
    <script type="text/javascript" src="js/select2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            projectInit();
            $("#sel_Project").change(function () {
                KioskListInit($(this).val(), $("#sel_Floor").val(), $("#sel_Region").val(), $("#sel_BusinessScope").val(), $("#sel_AreaScope").val());
            });

            floorInit();
            $("#sel_Floor").change(function () {
                KioskListInit($("#sel_Project").val(), $(this).val(), $("#sel_Region").val(), $("#sel_BusinessScope").val(), $("#sel_AreaScope").val());
            });

            regionInit();
            $("#sel_Region").change(function () {
                KioskListInit($("#sel_Project").val(), $("#sel_Floor").val(), $(this).val(), $("#sel_BusinessScope").val(), $("#sel_AreaScope").val());
            });

            businessScopeInit();
            $("#sel_BusinessScope").change(function () {
                KioskListInit($("#sel_Project").val(), $("#sel_Floor").val(), $("#sel_Region").val(), $(this).val(), $("#sel_AreaScope").val());
            });

            areaInit();
            $("#sel_AreaScope").change(function () {
                KioskListInit($("#sel_Project").val(), $("#sel_Floor").val(), $("#sel_Region").val(), $("#sel_BusinessScope").val(), $(this).val());
            });

            KioskListInit($("#sel_Project").val(), $("#sel_Floor").val(), $("#sel_Region").val(), $("#sel_BusinessScope").val(), $("#sel_AreaScope").val());
        });

        function projectInit() {
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
                    data.forEach(function (item) {
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

        function floorInit() {
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
                    data.forEach(function (item) {
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

        function regionInit() {
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
                    data.forEach(function (item) {
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

        function businessScopeInit() {
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
                    data.forEach(function (item) {
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

        function areaInit() {
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
                    data.forEach(function (item) {
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
                AreaScope: area
            }
            var kioskList = $("#kioskList");
            $.ajax({
                url: "Handler/NHHCGHandler.ashx",
                data: {
                    action: "getKioskList",
                    query: JSON.stringify(query)
                },
                type: "get",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                cache: false,
                success: function (data) {
                    kioskList.empty();

                    var optionValue = "";
                    data.forEach(function (item) {
                        optionValue += "<li><a target=\"_blank\" href=\"" + "KioskDetail.aspx?busScope=" + businessScope + "&area=" + area + "&rid=" + region + "&fid=" + floorID + "&pid=" + projectID + "&kid=" + item.KioskID + "\">";
                        optionValue += "<img src=\"<%=ParamManager.GetStringValue("ImageSite")%>" + item.Pic + "\">";
                        optionValue += "<span class=\"default-content\">多经点位编号：" + item.KioskNumber + "</span>";
                        optionValue += "<span class=\"mask-content\">";
                        optionValue += "<h4 class=\"n-tit\">" + item.KioskNumber + "</h4>";
                        optionValue += "<em>位置描述：" + item.Position + "</em>";
                        optionValue += "<em>计租面积范围：" + item.AreaScopeName + " </em>";
                        optionValue += "<em>租金价格：" + item.RentRemark + "  </em>";
                        optionValue += "</span><i class=\"mask-bg\"></i></a></li>";
                    });

                    kioskList.append(optionValue);
                },
                complete: function () {
                    var oHead = document.getElementsByTagName('HEAD').item(0);
                    var oScript = document.createElement("script");
                    oScript.type = "text/javascript";
                    oScript.src = "js/business.js";
                    oHead.appendChild(oScript);
                },
                error: function (xh) {
                }
            });
        }
    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
