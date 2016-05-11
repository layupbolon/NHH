<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="NHH.WebSite.Official.About" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-公司成立于2013年,是一家专业的商业地产运营管理公司,总部位于上海浦东陆家嘴,公司现服务的商业项目近20个,总建筑面积200余万平方米,公司的服务内容包括商业定位_建筑规划_招商_开业策划与日常运营管理,公司通过智能化的运营管理平台,为顾客_商家_业主_经营者_供应商提供着高效而便捷的服务,因为智能系统_自动控制和节能降耗技术的应用,公司超大型购物中心管理团队配置为35人,仅为同行的四分之一" />
    <title>上海立天唐人商业集团有限公司 - 集团介绍</title>
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
            <section class="aboutBanner">
                <em class="txtabout"></em>
                <div class="aboutcmub">
                    <ul>
                        <li class="current">
                            <a>
                                <h4>集团介绍<em>ABOUT US</em></h4>
                            </a>
                        </li>
                        <li>
                            <a href="NewsList.aspx">
                                <h4>新闻中心<em>NEWS CENTER</em></h4>
                            </a>
                        </li>
                        <li>
                            <a href="Tang.aspx">
                                <h4>唐小二平台<em>TANGXIAOER</em></h4>
                            </a>
                        </li>
                    </ul>
                </div>
            </section>
            <!-- topbanner end-->
            <section id="profile-section" class="column">
                <div class="coment">
                    <h3 class="mt30">关于我们</h3>
                    <em>ABOUT US</em>
                    <div class="comentA" style="margin: 50px 0 0;">
                        <p class="topabout">
                            <strong style="color: #11316b;">立天唐人商业集团 - 只做最好的商业平台</strong>
                        </p>
                        <p style="padding-top: 10px;">
                            立天唐人商业集团创立于2013年，核心业务包括购物中心平台的定位、规划、招商与运营管理，以及创新商业内容 的研发与经营。立天唐人商业集团现有员工近300<br>
                            人，具备了专业的定位、规划、招商、企划推广和运营管理能力。
                        </p>
                        <p>
                            <strong style="color: #11316b; font-size: 18px;">菁英团队</strong><br>
                            立天唐人商业集团打造了一支实力精湛、开创性强、执行力强的精锐团队，夯实了立天唐人稳步拓展的基础！<br>
                            <strong class="nameTeam">丁遥&nbsp;&nbsp;&nbsp;&nbsp;总裁</strong>
                            2012年中国零售业年度人物、国家储值卡专业委员会副主任委员、中欧商学院EMBA。历任苏宁电器集团董事、连锁发展中心总监、集团营销副总，任职期间完成了<br>
                            苏宁前100家连锁店的拓展，构建了覆盖全国的网络基础，实现了苏宁的上市计划。2005年加盟万达集团，历任万达集团商务部总经理、商业管理公司总经理、万达<br>
                            百货集团总经理、集团高级总裁助理，任职期间领导开设了57家百货店，使万达百货成为中国最大的百货连锁企业。<br>
                            <strong class="nameTeam">刘琦&nbsp;&nbsp;&nbsp;&nbsp;副总裁</strong>
                            国内最早将娱乐，休闲业态引进购物中心的创造者和 实践者，具有多年体验型多元商业业态操盘经验。曾服务辽宁兴隆商业集团、吉林中东商业集团，被誉为行业奇<br>
                            葩。未来将致力于商业内容的创新，商业模式的颠覆。<br>
                            <strong class="nameTeam">钟浩&nbsp;&nbsp;&nbsp;&nbsp;互联网副总裁兼CTO</strong>
                            拥有 15 年中美电子商务领域技术及运营管理经验。曾在美国第二大电子商务公司新蛋网任职 12 年，担任技术总监， 架构师。2011 年后历任新蛋网中国区支持中心<br>
                            总经理，中国新蛋网执行副总裁兼CTO。
                        </p>
                        <p>
                            <img src="img/ad-about.jpg">
                        </p>
                    </div>
                </div>

            </section>
            <section id="profile-comnent" class="profile-content" style="padding-top: 30px;">
                <div class="coment">
                    <h3>大事件</h3>
                    <em>NEWS</em>
                    <div class="comentA news mt10">
                        <%=service.GetStickNews() %>
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
