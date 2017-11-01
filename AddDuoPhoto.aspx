<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddDuoPhoto.aspx.cs" Inherits="AddDuoPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 433px;
            
        }
        .auto-style2 {
            height: 35px;
            width: 427px;
        }
        .auto-style3 {
            width: 122px;
            height: 25px;
        }
        .auto-style4 {
            width:90%;
        }
        .auto-style5 {
            width: 139px;
        }
        .auto-style6 {
            width: 139px;
            height: 40px;
        }
        .auto-style7 {
            width: 90%;
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContant" Runat="Server">
 <script lang="javascript" type="text/javascript">
	function addFile(max)
	{		
		var file = document.getElementsByName("File");	
		if(file.length == 1 && file[0].disabled == true)
		{
			file[0].disabled = false;
			return;
		}
		if(file.length < max)
		{
			var filebutton = '<br /><input type="file" size="50" name="File" class="Button" />';
			document.getElementById('FileList').insertAdjacentHTML("beforeEnd",filebutton);
		}
	}
</script>

<table class="Text" border="1" cellpadding="3" bgcolor="green" cellspacing="1">
	<tr bgcolor="white">
		<td class="auto-style6">所属分类：</td>
		<td class="auto-style7">
			<asp:DropDownList ID="ddlCategory" runat="server" SkinID="nn" Width="330px" Height="40px" CssClass="auto-style7"></asp:DropDownList>
		</td>
	</tr>
	<tr bgcolor="white">
		<td valign="top" class="auto-style5">选择照片：　　　　</td>
		<td class="auto-style4">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>			
					<td valign="top" class="auto-style1" >
						<p id="FileList"><input type="file" disabled="disabled" size="50" name="File" class="auto-style2" /></p>
					</td>
					<td valign="top">&nbsp; <input type="button" value='再传一张' class="auto-style3" onclick="addFile(<%= MAXPHOTOCOUNT %>)" /><font color="red">（最多上传 <%=MAXPHOTOCOUNT%> 张照片）</font><br />　单击按钮增加一个上传照片。如果文件的名称或者内容为空，则不上传该图片。</td>
				</tr>	
			</table>
		</td>
	</tr>					
	<tr bgcolor="white">
		<td colspan="2" align="center">
			<asp:Button ID="btnCommit" runat="server" Text="提交" SkinID="anniu" Width="100px" OnClick="btnCommit_Click" />&nbsp;<asp:Label ID="lbMessage" runat="server" CssClass="Text" ForeColor="Red"></asp:Label>
		</td>
	</tr>
</table>



</asp:Content>

