<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Business.aspx.cs" Inherits="NHH.WebSite.Official.Business" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-专业的商业地产运营管理公司,涵盖商业规划,定位,招商,信息系统,工程,物管,商业内容研发等,通过智能化的运营管理平台，为顾客、商家、业主、经营者、供应商提供着高效而便捷的服务,核心产品为大型的超级城市中心中型的创新生活中心,小型的社区商业中心" />
    <title>上海立天唐人商业集团有限公司 - 商机提供</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/select2.css" type="text/css" rel="stylesheet" />
    <link href="js/swiper/swiper.css" type="text/css" rel="stylesheet" />
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
            <section class="topbanner banner1200 swiper-container">
                <div class="poj_search">
                    <script>
                        function MM_o(selObj) {
                            if (selObj.selectedIndex === 0 || !selObj.options[selObj.selectedIndex].value) return;
                            window.open(selObj.options[selObj.selectedIndex].value);
                        }
                    </script>

                    <select id="txt_Project" onchange="MM_o(this)" name="select" class="busLink select2">
                        <%=service.GetProjectList(null) %>
                    </select>
                    <a href="ProjectList.aspx" class="cmnBtn btnA mt50">查看全部项目列表</a>
                </div>

                <ul class="swiper-wrapper">
                    <%=service.GetTopBanner() %>
                </ul>
                <div class="navDot">
                    <div class="navInner swiper-pagination">
                        <% for (int i = 0; i < service.GetBannerCount(); i++)
                           { %>
                        <a href="###"><span class="ie6png">i+1</span></a>
                        <% } %>
                    </div>
                </div>
            </section>
            <!-- topbanner end-->

            <section id="adA-section" class="column">
                <div class="th3-column fl htSection">
                    <img src="img/temp/join_pic.jpg">
                    <div class="txt-ad">
                        <h3>我要入驻</h3>
                        <p>
                            快速提交您的意向<br>
                            我们为您甄别筛选合适的铺位
                        </p>
                        <a href="UnitList.aspx" class="cmnBtn btnB mt50">我要入驻</a>
                    </div>
                </div>
                <div class="th3-column fl htSection ml20">
                    <img src="img/temp/adposition_pic.jpg">
                    <div class="txt-ad">
                        <h3>广告位招租</h3>
                        <p>
                            跳出迷茫  瞻望远方在这里<br>
                            有属于您的一席之地！
                        </p>
                        <a href="ADPositionList.aspx" class="cmnBtn btnB mt50">我要租赁</a>
                    </div>
                </div>
                <div class="th3-column fl htSection ml20">
                    <img src="img/temp/Multi_pic.jpg">
                    <div class="txt-ad">
                        <h3>场地租借及多种经营</h3>
                        <p>
                            提供多种经营方式租赁服务<br>
                            选择更多、更方便
                        </p>
                        <a href="KioskList.aspx" class="cmnBtn btnB mt50">我要租借</a>
                    </div>
                </div>
                <div class="th3-column fl htSection txewrap mt20">
                    <div class="qrcode">
                        <img src="img/temp/code.png">
                        <p>关注唐小二平台</p>
                    </div>
                    <div class="txt-ad txe-txt ml150">
                        <h3>唐小二商户服务平台</h3>
                        <p class="mt10">
                            现在，只要打开手机，关注唐小二平台<br>
                            你就能随时随地，查看商铺、物业报修、投诉建议。
                        </p>
                        <a href="Tang.aspx" class="cmnBtn btnB mt50">查看详情</a>
                    </div>
                    <div class="txepic"></div>
                </div>
                <div class="th3-column fl htSection ml20 mt20">
                    <img src="img/temp/property_service.gif">
                    <div class="txt-ad" style="padding: 30px 0 10px;">
                        <h3>物业服务</h3>
                        <p style="line-height: 2.5em;">
                            维修服务、安保服务、保洁服务<br>
                            服务时间：09:00 ~ 20:00
                        </p>
                    </div>
                </div>
                <div class="clear"></div>
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
    <script type="text/javascript" src="js/swiper/swiper-3.0.4.min.js"></script>
    <script type="text/javascript" src="js/business.js"></script>

    <script:script ID="script" runat="server" />
</body>
</html>
