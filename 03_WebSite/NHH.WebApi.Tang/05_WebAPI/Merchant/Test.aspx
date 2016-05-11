<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="NHH.WebAPI.Merchant.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>WebApiTestPage</title>
    <script src="JS/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="JS/jquery-2.1.4.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        URL:<asp:TextBox ID="txtUrl" runat="server" Width="922px"></asp:TextBox>
        <br/>
        Token:<asp:TextBox ID="txtToken" runat="server" Width="1053px"></asp:TextBox>
        <br/>
        PostData:<asp:TextBox ID="txtPostData" runat="server" Height="244px" Width="1130px" TextMode="MultiLine"></asp:TextBox>
        <br/>
        ReturnData:<asp:TextBox ID="txtReturnData" runat="server" Height="303px" Width="1120px" TextMode="MultiLine"></asp:TextBox>
        <br/>
        <input id="btnPost" type="button" value="Post" />
        &nbsp;&nbsp;
        <input id="btnGet" type="button" value="Get" />
    </div>
     <script type="text/javascript">
         $(function () {
             //Post
             $("#btnPost").click(function () {
                 var Url = $("#txtUrl").val();
                 var PostData = $("#txtPostData").val();
                 var token = $("#txtToken").val();
                 if (token != "") {
                     $.ajax({
                         url: Url,
                         type: "POST",
                         headers: { "X-NHH-Header": token },
                         data: JSON.parse(PostData),
                         dataType: "JSON",
                         cache: false,
                         success: function(result) {
                             $("#txtReturnData").val(JSON.stringify(result));
                         },
                         error: function(result) {
                             $("#txtReturnData").val("请求失败");
                         }
                     });
                 } else {
                     $.ajax({
                         url: Url,
                         type: "POST",
                         data: JSON.parse(PostData),
                         dataType: "JSON",
                         cache: false,
                         success: function(result) {
                             $("#txtReturnData").val(JSON.stringify(result));
                         },
                         error: function(result) {
                             $("#txtReturnData").val("请求失败");
                         }
                     });
                 }
             });
             //Get
             $("#btnGet").click(function () {
                 var Url = $("#txtUrl").val();
                 var PostData = $("#txtPostData").val();
                 var token = $("#txtToken").val();
                 if (token != "") {
                     $.ajax({
                         url: Url,
                         type: "GET",
                         headers: { "X-NHH-Header": token },
                         data: PostData,
                         dataType: "JSON",
                         success: function(result) {
                             $("#txtReturnData").val(JSON.stringify(result));
                         },
                         error: function(result) {
                             $("#txtReturnData").val("请求失败");
                         }
                     });
                 } else {
                     $.ajax({
                         url: Url,
                         type: "GET",
                         data: PostData,
                         dataType: "JSON",
                         success: function (result) {
                             $("#txtReturnData").val(JSON.stringify(result));
                         },
                         error: function (result) {
                             $("#txtReturnData").val("请求失败");
                         }
                     });
                 }
             });
         });
     </script>   

    </form>
</body>
</html>
