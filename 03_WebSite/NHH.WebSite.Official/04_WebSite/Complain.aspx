<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Complain.aspx.cs" Inherits="NHH.WebSite.Official.Complain" %>
<%@ Import Namespace="NHH.Framework.Utility" %>

<%@ Register Src="UserControl/head.ascx" TagName="head" TagPrefix="head" %>
<%@ Register Src="UserControl/foot.ascx" TagName="foot" TagPrefix="foot" %>
<%@ Register Src="UserControl/toolbox.ascx" TagName="toolbox" TagPrefix="toolbox" %>
<%@ Register Src="UserControl/script.ascx" TagName="script" TagPrefix="script" %>

<!doctype html>
<html lang="zh-ch">
<head>
    <meta charset="utf-8" />
    
    <title>上海立天唐人商业集团有限公司</title>
    <link href="css/global.css" type="text/css" rel="stylesheet" />
    <link href="css/select2.css" type="text/css" rel="stylesheet" />
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
    <head:head ID="head" runat="server" CurrentMenu="联系我们" />

    <div id="main">
        <div class="wraper">
            <section class="topban topban-contact">
           <div class="container">
               <h3>联系我们<em>CONTACT US</em></h3>
               <div class="tab banner-tab">
                    <a href="Contacts.aspx">联系方式</a><b>|</b><a class="now">投诉建议</a>
               </div>
           </div>
        </section>
            <!-- topbanner end-->

            <div class="feedback-pageinfo">感谢您对我们的支持，如您有任何意见或建议，可以随时反馈给我们，我们会及时采纳，力求为您提供最优质的服务。</div>
            <section id="complainForm" class="formWrap">
                <form>
                    <div class="formList">
                        <dl>
                            <dd>
                                <label>您的姓名：</label>
                                <input id="txt_UserName" name="username" type="text" placeholder="请填写您的真实姓名">
                            </dd>
                            <dd class="ml100">
                                <label>您的电话：</label>
                                <input id="txt_Phone" name="phoneNum" type="text" placeholder="请填写您的电话">
                            </dd>
                            <dd>
                                <label>反馈类型：</label>
                                <select id="sel_Type" class="select2 select2-16" name="feedbackType">
                                    <option value="1" selected="selected">我要投诉</option>
                                    <option value="2">我要建议</option>
                                </select>
                            </dd>
                            <dd class="ml100">
                                <label>您的角色：</label>
                                <select id="sel_Role" class="select2 select2-16" name="userRole">
                                    <option value="1" selected="selected">我是商户</option>
                                    <option value="2">我是顾客</option>
                                </select>
                            </dd>
                            <dd class="tips">
                                <label>反馈详情：</label>
                                <textarea id="txt_Feedback" placeholder="详细描述您投诉的详情，我们会尽快调查并处理。"></textarea>
                            </dd>
                        </dl>
                        <div class="clear"></div>
                        <p class="btnT"><a href="javascript:void(0);" id="btn_Add" class="cmnBtn btnR">提交信息</a></p>
                    </div>
                </form>
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

    <!--投诉建议页特有JS引用-->
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>
    <script type="text/javascript" src="js/select2.min.js"></script>
    <script type="text/javascript">

        $(function () {
            $("#btn_Add").click(function () {
                var item = {
                    Name: $("#txt_UserName").val(),
                    Tel: $("#txt_Phone").val(),
                    ComplainType: $("#sel_Type").val(),
                    Role: $("#sel_Role").val(),
                    ComplainDetail: $("#txt_Feedback").val()
                };
                if ($.trim(item.Name) === "") {
                    alert("请填写您的真实姓名");
                    return;
                }
                if ($.trim(item.Tel) === "") {
                    alert("请填写您的电话");
                    return;
                }
                $.post("<%=ParamManager.GetStringValue("APISite")%>"+"NHHCG/Complaint/add",
                    {
                        model: item
                    },
                    "json"
                );
                alert("提交成功！我们会尽快联系您，请耐心等待，谢谢！");
            });
        });
    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
