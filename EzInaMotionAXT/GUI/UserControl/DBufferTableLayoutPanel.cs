using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace EzIna.Motion.AXT.GUI
{

    // Table LayoutPanel 깜박임 
    //https://m.blog.naver.com/PostView.nhn?blogId=tnrud3579&logNo=221521715504&proxyReferer=https:%2F%2Fwww.google.co.kr%2F
    //https://blog.naver.com/PostView.nhn?blogId=bhshop&logNo=221705281992&categoryNo=2&parentCategoryNo=0&viewDate=&currentPage=1&postListTopCurrentPage=1&from=search
    //https://alwaysnr.tistory.com/15

    public partial class DBufferTableLayoutPanel : System.Windows.Forms.TableLayoutPanel
    {
        public DBufferTableLayoutPanel()
        {
          
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        public DBufferTableLayoutPanel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.CacheText, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_COMPOSITED;
                return cp;
            }
        }
        public void BeginUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }
        public void EndUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Parent.Invalidate(true);
        }
    }
    public static class NativeMethods
    {
        public static int WM_SETREDRAW = 0x000B; //uint WM_SETREDRAW
        public static int WS_EX_COMPOSITED = 0x02000000;
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam); //UInt32 Msg
    }
}
