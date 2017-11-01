using ASPNETAJAXWeb.AjaxAlbum;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddPhoto : System.Web.UI.Page
{
    int categoryID = -1; //相册分类ID
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初始化时获取相册分类的ID
        if (Request.Params["CategoryID"] != null)
        {
            categoryID = Int32.Parse(Request.Params["CategoryID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindPageData(categoryID);   //如果页面是第一次访问，则获取相册分类信息并绑定
        }
        btnCommit.Enabled = ddlCategory.Items.Count > 0 ? true : false; //三元运算符，如果下拉框已选择，提交按钮为可用状态，如果未选择，提交按钮为不可用状态
           
    }

  
    private void BindPageData(int categoryID)
    {
        //获取相册分类信息
        Album album = new Album();
        DataSet ds = album.GetFenlei();
        //显示相册分类信息到下拉框中
        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataBind();

        // 设置被选择的分类, 把列表控件的选择项设置为指定项-Value属性
        AjaxAlbumSystem.ListSelectedItemByValue(ddlCategory, categoryID.ToString());
        if (ddlCategory.SelectedIndex == -1 && ddlCategory.Items.Count > 0)
        {
            ddlCategory.SelectedIndex = 0;
        }
    }

    
    
    protected void btnCommit_Click(object sender, EventArgs e)
    {

    
        //判断文件上传内容是否为空
        if (fuPhoto.HasFile == false || fuPhoto.PostedFile.ContentLength <=0)
        {
            lbMessage.Text = "上传文件为空，请重新选择文件！";
            return;
        }
        //获取上传文件的值
        string type = fuPhoto.PostedFile.ContentType;
        int size = fuPhoto.PostedFile.ContentLength;

        //创建时间序列组合的文件名称
        string fileName = AjaxAlbumSystem.CreateDateTimeString();
        string extension = Path.GetExtension(fuPhoto.PostedFile.FileName);
        //判断文件名合法
        bool isAllow = false;
        foreach(string ext in AjaxAlbumSystem.ALLOWPHOTOFILELIST)
        {
            if (ext == extension.ToLower())
            {
                isAllow = true;
                break;
            }
        }
        if (isAllow == false) return;
        string url = "Photoes/" + fileName + extension;
        string fullPath = Server.MapPath(url);     //获取虚拟服务器上指定的物理路径


       

        if (File.Exists(fullPath)==true)
        {
            lbMessage.Text = "文件已存在，请重新选择！";
            return;
        }
        try
        {
            //asp:FileUpload空间功能，上传文件，并将文件输入到数据库中
            fuPhoto.SaveAs(fullPath);
            Album album = new Album();
            if (album.AddPhoto(tbName.Text, url, type, size, Int32.Parse(ddlCategory.SelectedValue)) > 0)
            {
               

                Response.Redirect("~/Fenlei.aspx?CategoryID = " + ddlCategory.SelectedValue);

            }
        }
        catch (Exception ex)
        {
            //显示错误信息
            lbMessage.Text = "上传文件错误，错误原因为:" + ex.Message;
            return;
        }
    }
        

}