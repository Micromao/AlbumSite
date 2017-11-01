using ASPNETAJAXWeb.AjaxAlbum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private int PageCount = 12;
    /// <summary>
    /// 当前页码
    /// </summary>
    private int CurrentPageIndex = -1;
    /// <summary>
    /// 总页码数量
    /// </summary>
    private int TotalPageIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {   ///获取总页码数量
		if (ViewState[AjaxAlbumSystem.TOTALPAGEINDEX] != null)
        {
            TotalPageIndex = Int32.Parse(ViewState[AjaxAlbumSystem.TOTALPAGEINDEX].ToString());
        }
        ///获取当前页码
        if (ViewState[AjaxAlbumSystem.CURRENTPAGEINDEX] != null)
        {
            CurrentPageIndex = Int32.Parse(ViewState[AjaxAlbumSystem.CURRENTPAGEINDEX].ToString());
        }
        ///初始化页面的数据，并设置为第一页。
        if (!Page.IsPostBack)
        {
            PagingDataInit();
            BindCurrentPageData();
        }
    }

    private void PagingDataInit()
    {   ///获取数据
		Album album = new Album();
        DataSet ds = album.GetFenleiAndPhoto();

        if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
        if (PageCount == 0) return;
        ///计算总页码数量
        TotalPageIndex = ds.Tables[0].Rows.Count / PageCount;
        if (TotalPageIndex * PageCount < ds.Tables[0].Rows.Count)
        {
            TotalPageIndex++;
        }
        ///保存总页码数量
        ViewState[AjaxAlbumSystem.TOTALPAGEINDEX] = TotalPageIndex;
        ///如果总页码大于0，则设置当前页码为第一页
        if (TotalPageIndex > 0)
        {
            CurrentPageIndex = 0;
            ///保存当前页码
            ViewState[AjaxAlbumSystem.CURRENTPAGEINDEX] = CurrentPageIndex;
        }
    }
    private void BindCurrentPageData()
    {
        if (PageCount <= 0 && CurrentPageIndex < 0) return;
        ///获取数据
        Album album = new Album();
        DataSet dsCurrent = album.GetFenleiAndPhoto(PageCount * CurrentPageIndex, PageCount);

        ///显示数据
        dlCategory.DataSource = dsCurrent;
        dlCategory.DataBind();
        ///控制分页按钮的可用性
        SetPageButtonEnable();
        ShowCurrentIndex();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {   ///重定向到添加相册分类页面
		Response.Redirect("~/AddFenlei.aspx");
    }

    /// <summary>
    /// 分页函数，执行【上一页】、【下一页】和直接跳转到指定页码的操作。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Command(object sender, CommandEventArgs e)
    {
        string commandName = e.CommandName.ToLower();
        if (string.IsNullOrEmpty(commandName) == true) return;

        ///判断总页码数量是否合法
        if (TotalPageIndex <= 0) return;

        switch (commandName)
        {
            case "first":   ///首页
				{
                    CurrentPageIndex = 0;
                    break;
                }
            case "prev":   ///上一页
				{
                    CurrentPageIndex = Math.Max(0, CurrentPageIndex - 1);
                    break;
                }
            case "next":   ///下一页
				{
                    CurrentPageIndex = Math.Min(TotalPageIndex - 1, CurrentPageIndex + 1);
                    break;
                }
            case "last":   ///末页
				{
                    CurrentPageIndex = TotalPageIndex - 1;
                    break;
                }
            case "move":   ///直接跳转到指定的页码
				{
                    int page = Int32.Parse(tbMove.Text.Trim());
                    if (page > 0 && page <= PageCount)
                    {
                        CurrentPageIndex = page - 1;
                    }
                    break;
                }
            default: break;
        }
        ///保存当前页码
        ViewState[AjaxAlbumSystem.CURRENTPAGEINDEX] = CurrentPageIndex;
        ///重新绑定控件的数据
        BindCurrentPageData();
    }

    /// <summary>
    /// 设置分页按钮（【首页】、【上一页】、【下一页】、【末页】、【执行】）的可用性
    /// </summary>
    private void SetPageButtonEnable()
    {
        ///判断总页码数量是否合法
        if (TotalPageIndex <= 0) return;

        ///如果页数量小于1，则所有分页按钮不可用
        if (TotalPageIndex <= 1)
        {
            ibtFirst.Enabled = ibtPrev.Enabled = ibtNext.Enabled = ibtLast.Enabled = ibtMove.Enabled = false;
            return;
        }
        ///如果是第一页，【上一页】按钮不可用
        ibtPrev.Enabled = CurrentPageIndex == 0 ? false : true;
        ///如果是最后一页，【下一页】按钮不可用
        ibtNext.Enabled = CurrentPageIndex == TotalPageIndex - 1 ? false : true;
        tbMove.Enabled = true;
    }

    /// <summary>
    /// 设置当前页码和所有页码
    /// </summary>
    private void ShowCurrentIndex()
    {
        ///判断总页码数量是否合法
        if (TotalPageIndex <= 0) return;
        ///显示页码信息
        lbCurrentIndex.Text = "当前第" + (CurrentPageIndex + 1).ToString()
            + "页，共" + TotalPageIndex.ToString() + "页";
    }
}