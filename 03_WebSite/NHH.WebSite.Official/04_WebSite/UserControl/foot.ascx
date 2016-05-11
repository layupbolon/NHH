<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="foot.ascx.cs" Inherits="NHH.WebSite.Official.UserControl.foot" %>

<div id="footer">
	<div class="footerInner">
    	<div class="show mb30">
        	<div class="showwrap">
                 <div class="share cls fl">
                    <a class="item1 icon-wx" style="cursor:pointer;" title="微信">微信</a>
                	<a class="item3" href="http://weibo.com/nhhcg" target="_blank" title="新浪微博">新浪微博</a>
                </div>
                <div class="searchbox fr">
                    <!--<input type="search" id="search_text" tip="搜索的商场名称，或您的当前位置" placeholder="搜索的商场名称，或您的当前位置" class="search searchBot"> -->
                    <img src="img/slogan.png" style="padding-top: 12px;">
                </div>
             </div>
        </div>
	    <div class="wraper">
			<div class="cont2 cls">
				<div class="linklist fl cls">
					<dl class="fl">
						<dt>活动中心</dt>
						<dd><a href="CampaignList.aspx">热门活动</a></dd>
						<%--<dd><a href="acti10.html">唐贩系列</a></dd>
						<dd><a href="acti11.html">昆仑开业庆典</a></dd>--%>
					</dl>
				
					<dl class="fl">
						<dt>商机提供</dt>
						<dd><a href="ProjectList.aspx">唐人项目</a></dd>
						<dd><a href="Intention.aspx?type=1">我要入驻</a></dd>                        
						<dd><a href="Intention.aspx?type=3">广告位招租</a></dd>
						<dd><a href="Intention.aspx?type=2">场地租借及多种经营</a></dd>
					</dl>
					<dl class="fl">
						<dt>关于我们</dt>
						<dd><a href="About.aspx">集团介绍</a></dd>
						<dd><a href="NewsList.aspx">新闻资讯</a></dd>    
						<dd><a href="Tang.aspx">唐小二平台</a></dd>                        
					</dl>
					<dl class="fl">
						<dt>服务中心</dt>
                        <dd><a href="Intention.aspx?type=1">入驻意向</a></dd>
						<dd><a href="Contacts.aspx">联系我们</a></dd>
					</dl>
				</div>
			</div>
	    	<div class="copy cls">
	            <p class="fl">版权所有 &copy; 2015 上海立天唐人商业集团有限公司&nbsp;&nbsp;&nbsp;&nbsp; 沪ICP备 10013227号</p>
				<p class="fr">
                    <a href="http://mail.nhhchina.com/">企业邮箱</a>
                </p>
	        </div>
	    </div>
	</div>
</div>