<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tang.aspx.cs" Inherits="NHH.WebSite.Official.Tang" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    <meta name="description" content="立天唐人商业NHHCG.COM-唐小二商户平台,为商户提供专业便捷的服务平台, 用户中心-随时管理店铺各类事项,店铺信息管理,经营数据管理,合约管理,用户管理等让工作更为高效便捷,物业管理-随时提报维修,全程监控维修进度及质量,评价服务质量,投诉管理-即时反馈信息,随时掌握处理信息,全程高效透明,还可评价处理结果满意度,企划活动-精彩活动倾情呈现,随时随地获悉活动一手资讯" />
    <title>上海立天唐人商业集团有限公司 - 唐小二平台</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="js/killercarousel/killercarousel.css" type="text/css" rel="stylesheet" />
    <link href="css/TXR.css" type="text/css" rel="stylesheet" />
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
            <section id="first" class="txeWrap bg01">
                <div class="wrpaOne">
                    <div class="cnt">
                        <div class="title-txe">
                            <h3>商户管家</h3>
                            <h5>唐小二智能商业平台</h5>
                        </div>

                        <div class="dlwrap">
                            <div class="txe_code">
                                <img src="img/code_big.gif">
                                <em>关注唐小二</em>
                            </div>
                            <div class="bgtxr"></div>
                        </div>
                    </div>
                    <div class="mobile-first">
                        <img src="img/phonelogin.png" class="mobile">
                    </div>
                    <div class="scrollDown animated infinite hinge delay bounce"></div>
                </div>
            </section>
            <!-- 首屏 end-->
            <div class="firstScreenPlaceholder screen-panel"></div>

            <section id="wrapTwo" class="txeWrap screen-panel">
                <div class="wrpaOne flmobile">
                    <div class="title-blod">
                        <p class="title"><span>用户</span>中心</p>
                        <p class="des">便捷管理各类事项、让您的工作更为高效</p>
                    </div>
                    <div class="cnt txtfour">
                        <dl>
                            <dt>
                                <img src="img/u01.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u01_white.png"></dt>
                            <dd>
                                <h5>店铺信息</h5>
                                <p>商户可快速查看自有店铺信息、编辑店铺图片</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u02.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u02_white.png"></dt>
                            <dd>
                                <h5>经营数据</h5>
                                <p>商户营业额数据获取、进行大数据分析</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u03.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u03_white.png"></dt>
                            <dd>
                                <h5>合约管理</h5>
                                <p>商户可快速查看自有店铺信息、编辑店铺图片</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u04.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u04_white.png"></dt>
                            <dd>
                                <h5>店铺信息</h5>
                                <p>商户可快速查看自有店铺信息、编辑店铺图片</p>
                            </dd>
                        </dl>

                    </div>

                    <div class="mobile">
                        <div class="screen-area">
                            <div class="movearea">
                                <ul>
                                    <li>
                                        <img src="img/txe_page1_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page1_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page1_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page1_item_1.jpg"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="navDot">
                            <div class="navInner">
                                <a href="javascript:"><span class="ie6png">1</span></a>
                                <a href="javascript:"><span class="ie6png">2</span></a>
                                <a href="javascript:"><span class="ie6png">3</span></a>
                                <a href="javascript:"><span class="ie6png">4</span></a>
                            </div>
                        </div>
                        <img src="img/phone_frame_bg.png" class="phonebg">
                    </div>

                </div>

            </section>
            <!-- 二屏 end-->

            <section id="wrapThr" class="txeWrap screen-panel bgWhite ">
                <div class="wrpaOne flmobile">
                    <div class="title-blod">
                        <p class="title"><span>物业</span>管理</p>
                        <p class="des">提报维修、全程监控、并对维修人员服务进行评价</p>
                    </div>

                    <div class="cnt txtfourl txtB" style="left: 0;">
                        <dl>
                            <dt>
                                <img src="img/u05.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u05_white.png"></dt>
                            <dd>
                                <h5>我要报修</h5>
                                <p>提报维修更加快速便捷、效率更高效</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u06.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u06_white.png"></dt>
                            <dd>
                                <h5>维修过程监控</h5>
                                <p>维修过程进度随时更新、让你随时获悉维修情况</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u07.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u07_white.png"></dt>
                            <dd>
                                <h5>报修评价</h5>
                                <p>对维修人员进行评价，一起提升服务品质</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u08.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u08_white.png"></dt>
                            <dd>
                                <h5>记录查询</h5>
                                <p>储存所有报修记录，方便随时查询</p>
                            </dd>
                        </dl>
                    </div>

                    <div class="mobile mobiler">
                        <div class="screen-area">
                            <div class="movearea">
                                <ul>
                                    <li>
                                        <img src="img/txe_page2_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page2_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page2_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page2_item_1.jpg"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="navDot">
                            <div class="navInner">
                                <a href="javascript:"><span class="ie6png">1</span></a>
                                <a href="javascript:"><span class="ie6png">2</span></a>
                                <a href="javascript:"><span class="ie6png">3</span></a>
                                <a href="javascript:"><span class="ie6png">4</span></a>
                            </div>
                        </div>
                        <img src="img/phone_frame_bg.png" class="phonebg">
                        <!-- <img src="img/phone02.png" class="mobiler"> -->
                    </div>
                </div>
            </section>
            <!-- 三屏 end-->

            <section id="wrapFor" class="txeWrap screen-panel">
                <div class="wrpaOne flmobile">
                    <div class="title-blod">
                        <p class="title"><span>投诉</span>管理</p>
                        <p class="des">即时处理、全程高效透明，为您提供更优质的体验</p>
                    </div>
                    <div class="cnt txtfourr txtB">
                        <dl class="mb50">
                            <dt>
                                <img src="img/u09.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u09_white.png"></dt>
                            <dd>
                                <h5>我要投诉</h5>
                                <p>快速反馈处理、为您提供优质服务</p>
                            </dd>
                        </dl>
                        <dl class="mb50">
                            <dt>
                                <img src="img/u06.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u06_white.png"></dt>
                            <dd>
                                <h5>投诉处理全程掌握</h5>
                                <p>投诉反馈进度随时更新、全程高度透明</p>
                            </dd>
                        </dl>
                        <dl class="mb50">
                            <dt>
                                <img src="img/u07.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u07_white.png"></dt>
                            <dd>
                                <h5>投诉处理评价</h5>
                                <p>对投诉处理人员的速度、态度全方位进行评价</p>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <img src="img/u08.png"></dt>
                            <dt class="highlight-icon">
                                <img src="img/u08_white.png"></dt>
                            <dd>
                                <h5>记录查询</h5>
                                <p>储存所有投诉记录，方便随时查询</p>
                            </dd>
                        </dl>
                    </div>
                    <div class="mobile">
                        <div class="screen-area">
                            <div class="movearea">
                                <ul>
                                    <li>
                                        <img src="img/txe_page3_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page3_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page3_item_1.jpg"></li>
                                    <li>
                                        <img src="img/txe_page3_item_1.jpg"></li>
                                </ul>
                            </div>
                        </div>
                        <div class="navDot">
                            <div class="navInner">
                                <a href="javascript:"><span class="ie6png">1</span></a>
                                <a href="javascript:"><span class="ie6png">2</span></a>
                                <a href="javascript:"><span class="ie6png">3</span></a>
                                <a href="javascript:"><span class="ie6png">4</span></a>
                            </div>
                        </div>
                        <img src="img/phone_frame_bg.png" class="phonebg">
                        <!-- <img src="img/phone02.png" class="mobiler"> -->
                    </div>
                </div>
            </section>
            <!-- 四屏 end-->

            <section id="wrapFri" class="txeWrap screen-panel bgindex">
                <div class="activ-allery">
                    <div class="title-blod">
                        <p class="title"><span>企划</span>活动</p>
                        <p class="des">精彩活动 倾情呈现 随时随地获悉活动资讯</p>
                    </div>
                    <div class="imgactiv">
                        <div class="kc-wrap">
                            <!-- Carousel items follow -->
                            <!--  如果图片数量少于7张，请将图片多做一次输出 -->
                            <div class="kc-item">
                                <img src="img/txe_activity_1.jpg">
                            </div>
                            <div class="kc-item">
                                <img src="img/txe_activity_2.jpg">
                            </div>
                            <div class="kc-item">
                                <img src="img/txe_activity_3.jpg">
                            </div>
                            <div class="kc-item">
                                <img src="img/txe_activity_4.jpg">
                            </div>
                            <div class="kc-item">
                                <img src="img/txe_activity_5.jpg">
                            </div>
                        </div>

                        <div class="phonebg-wrap phone-frame">
                            <div class="rel">
                                <img src="img/phone_frame_activity.png">
                            </div>
                        </div>
                        <div class="phonebg-wrap phone-content-holder">
                            <div class="rel">
                                <img src="img/phone_content_placeholder.png">
                            </div>
                        </div>
                    </div>

                </div>
            </section>
            <!-- 五屏 end-->

            <!--这个空的screen-panel不能删除，用来滚动到footer时发挥作用-->
            <section class="screen-panel screen-footer"></section>
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

    <!--特有JS引用-->
    <script type="text/javascript" src="js/jquery.scrollTo.min.js"></script>
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>
    <script type="text/javascript" src="js/killercarousel/killercarousel.patched.js"></script>
    <script type="text/javascript" src="js/txe.js"></script>

    <script:script ID="script" runat="server" />
</body>
</html>
