<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="NHH.WebSite.Official.NewsList" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-集团新闻 行业新闻 战略合作 活动" />
    <title>上海立天唐人商业集团有限公司 - 新闻中心</title>
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
    <head:head ID="head" runat="server" CurrentMenu="集团介绍"/>

    <div id="main">
        <div class="wraper">
            <section class="topban">
                <div class="container">
                    <h3>新闻中心<em>NEWS CENTER</em></h3>
                </div>
            </section>
            <!-- topbanner end-->
            <section id="profile-comnent" class="profile-content" style="padding-top: 30px;">
                <div class="coment">
                    <div class="comentA news">
                        <%=service.GetAllNews() %>
                        <div class="clear"></div>
                        <div class="btnMore mt30"><a href="NewsList.aspx" class="cmnBtn btnC">SHOW MORE</a></div>
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

    <script:script ID="script" runat="server" />
</body>
</html>
