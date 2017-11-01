<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


.Text
{
	font:Tahoma;
	font-size:9pt;
}

        .auto-style1 {
            width: 1072px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContant" Runat="Server" >

    <table class="Table" border="0" cellpadding="5" cellspacing="0" style="width:100%">
		<tr>
			<td colspan="2" class="auto-style1">
				<asp:UpdatePanel runat="server" ID="up" >
					<ContentTemplate>
						<asp:DataList ID="dlCategory" runat="server"  SkinID="dd" HorizontalAlign="Center" RepeatColumns="4" RepeatDirection="Horizontal" GridLines="Both" ShowFooter="False" ShowHeader="False">
						  <ItemTemplate>
								<table class="Table" border="0" cellpadding="5" cellspacing="0"  >
									<tr>
										<td>
											<a href='Fenlei.aspx?CategoryID=<%# Eval("ID") %>' target="_blank"><img alt='<%# Eval("PhotoTitle") %>' src='<%# Eval("PhotoUrl") %>' border="0" width="200" height="160" /></a>
										</td>
									</tr>
									<tr>
										<td align="center">
											<a href='Fenlei.aspx?CategoryID=<%# Eval("ID") %>' target="_blank"><%# Eval("Name") %>(<%# Eval("PhotoCount") %>张)</a>
										</td>
									</tr>
								</table>
							</ItemTemplate>							
						</asp:DataList>

						<table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#009DE9">
							<tr>
								<td height="40" align="center" valign="middle" bgcolor="#FFFFFF" class="Text">
									<asp:Label ID="lbCurrentIndex" runat="server" CssClass="Text"></asp:Label>&nbsp;
									<asp:ImageButton ID="ibtFirst" runat="server" ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/blfirst.gif" CommandName="first" OnCommand="Page_Command" CausesValidation="False" />&nbsp;
									<asp:ImageButton ID="ibtPrev" runat="server" ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/blprev.gif" CommandName="prev" OnCommand="Page_Command" CausesValidation="False" />&nbsp;
									<asp:ImageButton ID="ibtNext" runat="server" ImageUrl='~/App_Themes/ASPNETAjaxWeb/Images/blnext.gif' CommandName="next" OnCommand="Page_Command" CausesValidation="False" />&nbsp;
									<asp:ImageButton ID="ibtLast" runat="server" ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/bltail.gif" CommandName="last" OnCommand="Page_Command" CausesValidation="False" />
									&nbsp;转移到<asp:TextBox ID="tbMove" runat="server" Width="30px" CssClass="TextBox" SkinID="mm">1</asp:TextBox>页<asp:RangeValidator ID="rvPage" runat="server" ControlToValidate="tbMove"
										Display="Dynamic" ErrorMessage="页码输入错误" MinimumValue="1" Type="Integer" MaximumValue="10000"></asp:RangeValidator><asp:RequiredFieldValidator ID="rfPage" runat="server" ControlToValidate="tbMove"
										Display="Dynamic" ErrorMessage="请输入页码"></asp:RequiredFieldValidator>
									<asp:ImageButton ID="ibtMove" runat="server" CommandName="move" ImageUrl='~/App_Themes/ASPNETAjaxWeb/Images/bldo.gif' OnCommand="Page_Command" />			
								</td>
							</tr>
						</table>
					</ContentTemplate>
				</asp:UpdatePanel>				
			</td>
		</tr>
		<tr>
			<td class="auto-style1">
				<br />
				<asp:Button ID="btnAdd" runat="server" Text="添加新分类" OnClick="btnAdd_Click" SkinID="anniu" />
			</td>
		</tr>
	</table>

</asp:Content>

