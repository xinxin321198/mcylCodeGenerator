using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class GeneratorInitPageControl
    {
        /// <summary>
        /// 生成初始化分页控件的方法
        /// </summary>
        /// <returns></returns>
        public static string generatorInitPageControl()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
        /// <summary>
        /// 分页对象，初始化列表数据之前置为null可查出所有的数据，否则就根据page当前的所有数据量、当前页计算出数据的范围去查询，并且返回这一个page
        /// </summary>
        private PageInfo page = null;
        

        /// <summary>
        /// 初始化分页控件
        /// </summary>
        private void initPageControl()
        {
            toolStripLabel_currentPage.Text = this.page.CurrentPage.ToString();//当前页码
            toolStripLabel_pageCount.Text = this.page.PageCount.ToString();//总页数
            toolStripLabel_pageSize.Text = this.page.PageSize.ToString();//每页显示多少条
            toolStripLabel_allCount.Text = this.page.AllCount.ToString();//数据总数
            toolStrip_Page.Dock = DockStyle.Bottom;//让控件置于底部
        }


         //首页按钮的点击事件
        private void toolStripButton_startPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.page.CurrentPage = 1;//当前页设为第一页
                this.toolStripLabel_currentPage.Text = this.page.CurrentPage.ToString();//当前所在页
                this.initDataGridView();//构建好page对象后刷新列表
            }
            catch (Exception ex)
            {

                MyMessageBox.Show(ex.Message);
            }
        }

        //上一页按钮的点击事件
        private void toolStripButton_prevPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.page.PrevPage == 0)
                {
                    return;
                }
                this.page.CurrentPage = this.page.PrevPage;//当前页设为上一页
                this.toolStripLabel_currentPage.Text = this.page.CurrentPage.ToString();//当前所在页
                this.initDataGridView();//构建好page对象后刷新列表
            }
            catch (Exception ex)
            {

                MyMessageBox.Show(ex.Message);
            }
        }

        //下一页按钮的点击事件
        private void toolStripButton_nextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.page.NextPage == 0)
                {
                    return;
                }
                this.page.CurrentPage = this.page.NextPage;//当前页设为下一页
                this.toolStripLabel_currentPage.Text = this.page.CurrentPage.ToString();//当前所在页
                this.initDataGridView();//构建好page对象后刷新列表
            }
            catch (Exception ex)
            {

                MyMessageBox.Show(ex.Message);
            }
        }

        //末尾页按钮的点击事件
        private void toolStripButton_endPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.page.CurrentPage = this.page.PageCount;//当前页设为末尾页页
                this.toolStripLabel_currentPage.Text = this.page.CurrentPage.ToString();//当前所在页
                this.initDataGridView();//构建好page对象后刷新列表
            }
            catch (Exception ex)
            {

                MyMessageBox.Show(ex.Message);
            }
        }
");
            return sb.ToString();
        }

    }
}
