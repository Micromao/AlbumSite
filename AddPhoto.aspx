<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>--%>
<%@ Page Title="添加单张图片" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContant" Runat="Server">
    
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>   --%>
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 0px;
        }
        .auto-style2 {
            height: 30px;
        }
        .auto-style4 {
            height: 40px;
        }
        .auto-style5 {
            height: 40px;
        }
        .auto-style6 {
            height: 40px;
            width: 90%;
        }
        .auto-style7 {
            height: 39px;
            width: 90%;
        }
        .auto-style8 {
            height: 36px;
            width: 90%;
        }
    </style>


   
        <table class="Table" border="1" cellpadding="2" bgcolor="green" cellspacing="1" >
           <tr bgcolor="white">
			<td colspan="2"><hr /><a name="message"></a></td>
		</tr>
		<tr bgcolor="white">
			<td valign="top" class="auto-style5">照片名称：</td>
			<td class="auto-style6"><asp:TextBox ID="tbName" runat="server" SkinID="mm" Width="60%" MaxLength="50"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfNameBlank" runat="server" ControlToValidate="tbName" Display="none" ErrorMessage="名称不能为空！"></asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator ID="rfNameValue" runat="server" ControlToValidate="tbName" Display="none" InitialValue="请输入分类名称" ErrorMessage="名称不能为空！"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="tbName" Display="none" ErrorMessage="分类名称的长度最大为50，请重新输入。" ValidationExpression=".{1,50}"></asp:RegularExpressionValidator>
				<ajaxToolkit:TextBoxWatermarkExtender ID="wmeName" runat="server" TargetControlID="tbName" WatermarkText="请输入分类名称" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceNameBlank" runat="server" TargetControlID="rfNameBlank" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceNameValue" runat="server" TargetControlID="rfNameValue" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceNameRegex" runat="server" TargetControlID="revName" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
			</td>
		</tr>
		<tr bgcolor="white">
			<td class="auto-style4">所属分类：</td>
			<td class="auto-style7">
				<asp:DropDownList ID="ddlCategory" runat="server" SkinID="nn" Width="307px" CssClass="auto-style1" Height="38px"></asp:DropDownList>
			</td>
		</tr>
		<tr bgcolor="white">
			<td>选择照片：</td>
			<td class="auto-style8">
				<asp:FileUpload ID="fuPhoto" runat="server" CssClass="Button" Width="400px" BorderColor="#0099CC" BorderWidth="1" style="margin-top: 0px" />
				<asp:Label ID="lbMessage" runat="server" CssClass="Text" ForeColor="Red"></asp:Label>
			</td>
		</tr>	
             <tr bgcolor="white">
			<td colspan="2" class="auto-style2" align="center">
             <asp:Button ID="btnCommit" runat="server" Text="提交"  OnClick="btnCommit_Click"  style="height: 21px;width:100px;" SkinID="mm" BorderColor="#33CCFF" />

			</td>
		</tr>
		    </table>
        <br />
       


</asp:Content>


