using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    static class Program
    {
        public static SoftKey ytsoftkey = new SoftKey();//引用由域天工具随机生成的加密类模块

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Connection());
        }

        public static void checkKey()
        {
            //在窗体开发装载时，必须首先调用该函数
            ytsoftkey = new SoftKey();
            if (!ytsoftkey.YCheckKey())
            {
                MessageBox.Show(@"
    请插入加密狗！
    玉溪美创科技信息有限公司版权所有！
    电话：0877-2063186
                ");
                Application.Exit();
            }
        }
    }
}
