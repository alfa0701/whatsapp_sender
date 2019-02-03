using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; // эту строку добавил стало меньше ошибок
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.IO;



namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("USER32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(MouseEvent dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        
        [DllImport("user32.dll")]
        public static extern int ActivateKeyboardLayout(int HKL, int flags);
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint thid);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint Flags);
        
        const int BM_SETSTATE = 243;
        const int WM_LBUTTONDOWN = 513;
        const int WM_LBUTTONUP = 514;
        const int WM_KEYDOWN = 256;
        const int WM_CHAR = 258;
        const int WM_KEYUP = 257;
        const int WM_SETFOCUS = 7;
        const int WM_SYSCOMMAND = 274;
        const int SC_MINIMIZE = 32;


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd,
            IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            SetWindowPosFlags uFlags);

        // Определим перечисление SetWindowPosFlags.
        [Flags()]
        private enum SetWindowPosFlags : uint
        {
            SynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText("1.txt", Encoding.Default);

        }
         
        private void button3_Click(object sender, EventArgs e)
        {
               Process[] anotherApps = Process.GetProcessesByName("MEmu");           
               SetForegroundWindow(anotherApps[0].MainWindowHandle);
               SendKeys.SendWait(textBox1.Text);
        }

        public enum MouseEvent
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
            MOUSEEVENTF_ABSOLUTE = 0x8000,
            MOUSEEVENTF_WHEEL = 0x0800
        }

        private void ClickXY(int x, int y)
        {
            Process[] anotherApps = Process.GetProcessesByName("MEmu");
            SetForegroundWindow(anotherApps[0].MainWindowHandle);

            // Установите позицию окна.
            SetWindowPos(anotherApps[0].MainWindowHandle, IntPtr.Zero, 0, 0, 300, 500, 0);

            //Координаты указаны относительно текущего положения курсора
            mouse_event(MouseEvent.MOUSEEVENTF_MOVE, -Cursor.Position.X, -Cursor.Position.Y, 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_MOVE, x, y, 0, 0);

            mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP, x, y, 0, 0);   
        }

        private void WhellXY(int x, int y)
        {
            Process[] anotherApps = Process.GetProcessesByName("MEmu");
            SetForegroundWindow(anotherApps[0].MainWindowHandle);

            // Установите позицию окна.
            SetWindowPos(anotherApps[0].MainWindowHandle, IntPtr.Zero, 0, 0, 300, 500, 0);

            //Координаты указаны относительно текущего положения курсора
            mouse_event(MouseEvent.MOUSEEVENTF_MOVE, -Cursor.Position.X, -Cursor.Position.Y, 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_MOVE, x, y, 0, 0);

            mouse_event(MouseEvent.MOUSEEVENTF_WHEEL, x, y, -120, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClickXY(90,80);         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClickXY(95,185);         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClickXY(90, 60);         
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClickXY(95, 185);         
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClickXY(5, 25);         
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button5_Click(null, null);
            Thread.Sleep(1000);

            button11_Click(null, null);
            Thread.Sleep(1000);

            button6_Click(null, null);
            Thread.Sleep(1000);
            
            button12_Click(null, null);
            Thread.Sleep(1000);

            button7_Click(null, null);
            Thread.Sleep(1000);

            button8_Click(null, null);
            numericUpDown1.Value++;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //    button11_Click(null, null);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                WhellXY(5, 25);
                Thread.Sleep(100);
            }
        }
        
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);
        
        const uint MAPVK_VK_TO_VSC = 0x00;
        const uint MAPVK_VSC_TO_VK = 0x01;
        const uint MAPVK_VK_TO_CHAR = 0x02;
        const uint MAPVK_VSC_TO_VK_EX = 0x03;
        const uint MAPVK_VK_TO_VSC_EX = 0x04;

        class extraKeyInfo
        {
            public ushort repeatCount;
            public char scanCode;
            public ushort extendedKey, prevKeyState, transitionState;

            public int getint()
            {
                return repeatCount | (scanCode << 16) | (extendedKey << 24) |
                    (prevKeyState << 30) | (transitionState << 31);
            }
        };


        [DllImport("user32.dll")]
        static extern int MapVirtualKey(int uCode, uint uMapType);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScan(char ch);

        public static void SendK(IntPtr wnd, Char ch, Int32 del)
        {
            uint vkCode = (byte)(VkKeyScan(ch));
            extraKeyInfo lParam = new extraKeyInfo();
            lParam.scanCode = (char)MapVirtualKey((int)vkCode, MAPVK_VK_TO_VSC);
            PostMessage(wnd, WM_KEYDOWN, 'ы', lParam.getint()); //'ы'
            lParam.repeatCount = 1;
            lParam.prevKeyState = 1;
            lParam.transitionState = 1;
            System.Threading.Thread.Sleep(del);
            //PostMessage(wnd, WM_CHAR, (IntPtr)251, new IntPtr(0));
            PostMessage(wnd, WM_KEYUP, 'ы', lParam.getint());
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private static void SendCtrlhotKey(char key)
        {
            keybd_event(0x11, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 0x2, 0);
            keybd_event(0x11, 0, 0x2, 0);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process[] anotherApps = Process.GetProcessesByName("MEmu");
            SetForegroundWindow(anotherApps[0].MainWindowHandle);
            string s = textBox1.Text; // (win1251.GetString(win1251Bytes));
            Clipboard.SetText(s, TextDataFormat.Text);
            Clipboard.SetText(s, TextDataFormat.UnicodeText);
            SendCtrlhotKey('V');

        }
    }
}
