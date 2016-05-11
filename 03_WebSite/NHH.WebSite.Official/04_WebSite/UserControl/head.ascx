<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="head.ascx.cs" Inherits="NHH.WebSite.Official.UserControl.head" %>
<%@ Import Namespace="NHH.Models.Official" %>

<div id="header" class="ie6png">
    <div class="wraper">
        <div class="logoWrap">
            <a href="index.aspx" class="logo ie6png inblock">立天唐人商业集团</a>
        </div>
        <div class="menuWrap">
            <ul class="menu">
                <li <%=CurrentMenu== MenuEnum.首页? "class=\"current\"":"" %>><a href="Index.aspx" class="item"><span>首页</span></a></li>
                <li <%=CurrentMenu== MenuEnum.活动中心? "class=\"current\"":"" %>><a href="CampaignList.aspx" class="item"><span>活动中心</span></a></li>
                <li <%=CurrentMenu== MenuEnum.商机提供? "class=\"current\"":"" %>><a href="Business.aspx" class="item"><span>商机提供</span></a></li>
                <li <%=CurrentMenu== MenuEnum.集团介绍? "class=\"current\"":"" %>><a href="About.aspx" class="item"><span>集团介绍</span></a></li>
                <li <%=CurrentMenu== MenuEnum.联系我们? "class=\"current\"":"" %>><a href="Contacts.aspx" class="item"><span>联系我们</span></a></li>
            </ul>
            <div class="hoverSkin"><tt class="ie6png"><s class="ie6png"></s></tt></div>
        </div>
        <!--<div class="searchbox top fr">
        	<input type="search" id="search_text" tip="搜索的商场名称，或您的当前位置" placeholder="搜索的商场名称，或您的当前位置" class="search searchtop">
        </div> -->
    </div>
</div>
