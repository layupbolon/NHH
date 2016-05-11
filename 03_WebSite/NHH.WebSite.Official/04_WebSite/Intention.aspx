<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Intention.aspx.cs" Inherits="NHH.WebSite.Official.Intention" %>
<%@ Import Namespace="NHH.Framework.Utility" %>

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
    <link href="css/business.css" type="text/css" rel="stylesheet" />
    <link href="css/intention.css" type="text/css" rel="stylesheet" />
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
            <section class="topban">
                <div class="container">
                    <h3>提交意向<em>INTENTION</em></h3>
                </div>
            </section>

            <%--<div trigger="click" class="tab tabA tab-pj">
                <% if (type == 1)
                   { %>
                <a class="now none">商铺租赁</a>
                <% } %>
                <% if (type == 2)
                   { %>
                <a class="now none">场地租借及多种经营</a>
                <% } %>
                <% if (type == 3)
                   { %>
                <a class="now none">广告位招租</a>
                <% } %>
            </div>--%>

            <section id="intForm" class="formWrap">
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
                                <label>租赁类型：</label>

                                <select id="sel_Type" class="select2 select2-16" name="pjNHH">
                                    <option value="1" <%=type==1? "selected=\"selected\"":"" %>>商铺租赁</option>
                                    <option value="2" <%=type==2? "selected=\"selected\"":"" %>>场地租借及多种经营</option>
                                    <option value="3" <%=type==3? "selected=\"selected\"":"" %>>广告位招租</option>
                                </select>
                            </dd>
                            <dd class="ml100">
                                <label>所属项目：</label>
                                <select id="txt_Project" class="select2 select2-16" name="pjNHH">
                                    <%=projectService.GetProjectList(projectID) %>
                                </select>
                            </dd>
                            <dd class="tips">
                                <label>备注信息：</label>
                                <textarea id="txt_Remark" placeholder="可以填写您的更多要求，方便我们为您提供更为精确的资讯和更周到的服务。"></textarea>
                            </dd>
                        </dl>
                        <div class="clear"></div>
                        <p class="btnT"><a href="###" id="btn_Add" class="cmnBtn btnD">提交意向</a></p>
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

    <!--首页特有JS引用-->
    <script type="text/javascript" src="js/jquery.Xslider.js"></script>
    <script type="text/javascript" src="js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btn_Add").click(function () {
                var item = {
                    "IntentionType": $("#sel_Type").val(),
                    "ContactName": $("#txt_UserName").val(),
                    "ContactPhone": $("#txt_Phone").val(),
                    "ProjectName": $("#txt_Project").val(),
                    "Remark": $("#txt_Remark").val(),
                    "Source": 1
                };

                $.post("<%=ParamManager.GetStringValue("APISite")%>" + "operations/intention/add",
                    {
                        model: item
                    },
                    "json"
                );
                alert("提交成功！我们会尽快联系您，请耐心等待，谢谢！");
            });
        });
    </script>

    <script language="javascript" type="text/javascript">
        /***
        *功能：隐藏和显示div  
        *参数divDisplay：html标签id  
        ***/
        function click_a(divDisplay) {
            if (document.getElementById(divDisplay).style.display !== "block") {
                document.getElementById(divDisplay).style.display = "block";
            }
            else {
                document.getElementById(divDisplay).style.display = "none";
            }
        }
    </script>

    <script:script ID="script" runat="server" />
</body>
</html>
