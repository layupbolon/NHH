<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="NHH.WebSite.Official.Index" %>

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
    <title>立天唐人商业(NHHCG.COM) - 科技改变商业-专业涵盖商业规划,定位,招商,信息系统,工程,物管,商业内容研发等,核心产品为大型的超级城市中心中型的创新生活中心,小型的社区商业中心,智能化管理平台运营管理</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/home.css" type="text/css" rel="stylesheet" />
</head>
<!--[if lt IE 7]><body class="ie6 ielt7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 7]><body class="ie7 ielt8 ielt9 ielt10 ie"><![endif]-->
<!--[if IE 8]><body class="ie8 ie"><![endif]-->
<!--[if IE 9]><body class="ie9 ie"><![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body>
    <!--<![endif]-->
    <!--[if lt IE 9]><script src="js/html5shiv.js"></script><![endif]-->
    <head:head ID="head" runat="server" CurrentMenu="首页" />

    <div id="main">
        <div class="wraper">
            <div class="firstScreenFixed">
                <section id="firstOne" class="topbanner">
                    <ul>
                        <% =service.GetTopBanner() %>
                    </ul>
                    <div class="navDot">
                        <div class="navInner">
                            <% for (int i = 0; i < service.GetTopBannerCount(); i++)
                               {%>
                            <a href="###"><span class="ie6png">i+1</span></a>
                            <%  }%>
                        </div>
                    </div>
                    <div class="scrollDown animated infinite hinge delay bounce"></div>
                </section>
                <!-- topbanner end-->
                <!--<div class="m-space"></div> -->
            </div>
            <div class="firstScreenPlaceholder panel"></div>

            <section id="adA-section" class="panel screen-panel">
                <%=service.GetBannerByLocationID(LocationEnum.官网首页第二屏) %>
            </section>

            <section id="adB-section" class="panel screen-panel">
                <%=service.GetBannerByLocationID(LocationEnum.官网首页第三屏) %>
            </section>

            <section class="panel screen-panel">
                <%=service.GetLastBanner() %>
            </section>

            <!--这个空的panel不能删除，用来滚动到footer时发挥作用-->
            <section class="panel"></section>
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
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/global.js"></script>

    <!--首页特有JS引用-->
    <script type="text/javascript" src="js/jquery.scrollTo.min.js"></script>
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>
    <script type="text/javascript" src="js/home.js"></script>

    <script:script ID="script" runat="server" />
</body>
</html>
