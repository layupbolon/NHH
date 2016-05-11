<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="NHH.WebSite.Official.News" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="Description" content="<%=currentNews.NewsBrief %>" />
    <title>上海立天唐人商业集团有限公司 - <%=currentNews.NewsTitle %></title>
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
    <head:head ID="head" runat="server" CurrentMenu="集团介绍" />

    <div id="main">
        <div class="wraper">
            <section class="topban">
                <div class="container">
                    <h3>新闻中心<em>NEWS CENTER</em></h3>
                </div>
            </section>
            <!-- topbanner end-->
            <section id="profile-comnent" class="profile-content bgWhite" style="padding-bottom: 10px;">
                <div class="coment details">
                    <h1><%=currentNews.NewsTitle %></h1>
                    <p class="tips"><em>发布日期：<%=currentNews.PublishTime.ToString("yyyy.MM.dd") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发布者：立天唐人商业集团</em></p>
                    <%=currentNews.NewsContent %>
                </div>
                <div class="coment details mt30">
                    <p class="tips hei30">
                        <% if (previousNews != null)
                           { %>
                        <a href="News.aspx?newsID=<%=previousNews.NewsID %>" class="newsPre">上一条：<%=previousNews.NewsTitle %></a>
                        <% }
                           if (nextNews != null)
                           { %>
                        <a href="News.aspx?newsID=<%=nextNews.NewsID %>" class="newsNext">下一条：<%=nextNews.NewsTitle %></a>
                        <% } %>
                        <div class="clear"></div>
                        <p></p>
                    <div class="clear"></div>
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
