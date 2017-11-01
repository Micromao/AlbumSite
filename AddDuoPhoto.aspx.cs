using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ASPNETAJAXWeb.AjaxAlbum;
using System.IO;

public partial class AddDuoPhoto : System.Web.UI.Page
{
    protected int MAXPHOTOCOUNT = AjaxAlbumSystem.MAXPHOTOCOUNT;
    int categoryID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初始化时获取相册分类的ID
        if (Request.Params["CategoryID"] != null)
        {
            categoryID = Int32.Parse(Request.Params["CategoryID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindPageData();   //如果页面是第一次访问，则获取相册分类信息并绑定
        }

        btnCommit.Enabled = ddlCategory.Items.Count > 0 ? true : false; //三元运算符，如果下拉框已选择，提交按钮为可用状态，如果未选择，提交按钮为不可用状态

                
    }

    private void BindPageData()
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
        //判断是否选择相册分类,若未选择，跳出该方法，不再向下执行
        if (ddlCategory.SelectedIndex <= 0) return;

        HttpFileCollection fileList = HttpContext.Current.Request.Files;    //获取页面所请求的文件列表，存储到HttpFileCollection对象 fileList中
        if (fileList == null) return;
        Album album = new Album();
        try
        {
            //判断每一个<input type="file"/>中的文件有效性
            for (int i = 0; i < fileList.Count; i++)
            {
                //获取上传的文件，并判断是否为空
                HttpPostedFile postedFile = fileList[i];    //异常----------
                if (postedFile == null) continue;
                //获取文件名称，并判断是否为空
                string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                if (string.IsNullOrEmpty(extension) == true) continue;
                bool isAllow = false;
                foreach (string ext in AjaxAlbumSystem.ALLOWPHOTOFILELIST)
                {
                    if (ext == extension.ToLower())
                    {
                        isAllow = true;
                        break;
                    }
                }
                if (isAllow == false) continue;
                string timeFilename = AjaxAlbumSystem.CreateDateTimeString();
                string url = "Photoes/" + timeFilename + extension;
                //获取文件完整路径
                string fullPath = Server.MapPath(url);
                postedFile.SaveAs(fullPath);

                //文件输入到数据库中
                album.AddPhoto(fileName, url, postedFile.ContentType, postedFile.ContentLength, Int32.Parse(ddlCategory.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            //显示错误原因
            lbMessage.Text = "上传文件错误，错误原因为：" + ex.Message;
            return;
        }
        Response.Redirect("~/Fenlei.aspx?Category!ID=" + ddlCategory.SelectedValue);
    }
}