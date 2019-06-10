using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OptBase
{
    /// <summary>
    /// 非wpf相关的工具类
    /// </summary>
    public class OptBaseX
    {
        public struct POINT
    {
       public int x;
       public int y;
    }
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT Point);
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(int xPoint, int yPoint);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(POINT lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(IntPtr hWnd,StringBuilder lpString,int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "GetClassName")]
        public static extern int GetClassName(IntPtr hWnd,StringBuilder lpString,int nMaxCont);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);


        private static POINT GetCursorPos()
        {
            POINT p;
            if (GetCursorPos(out p))
            {
                return p;
            }
            throw new Exception();
        }
        public static IntPtr WindowFromPoint()
        {
            POINT p = GetCursorPos();
            return WindowFromPoint(p);
        }
        public static string GetWindowNameFromHWND(IntPtr hwnd)
        {
            StringBuilder name = new StringBuilder(256);
            GetWindowText(hwnd, name, 256);
            return name.ToString();
        }
        public static string GetWindowClassNameFromHWND(IntPtr hwnd)
        {
            StringBuilder name = new StringBuilder(256);
            GetClassName(hwnd, name, 256);
            return name.ToString();
        }
        public static Rectangle GetWindRect(IntPtr hWnd)
        {
            Rectangle windowRect = new Rectangle();
            GetWindowRect(hWnd, ref windowRect);
            return windowRect;
        }
        public static Point GetMouseCurPosition()
        {
            POINT p;
            if (GetCursorPos(out p))
            {
                return new Point(p.x,p.y);
            }
            throw new Exception();
        }

        public static void SetMouseClick(Point point)
        {
            OptBaseX.SetCursorPos(new POINT() { x = point.X, y = point.Y });
            MouseKeyboardLibrary.MouseSimulator.Click(MouseKeyboardLibrary.MouseButton.Left);
        }
        /// <summary>
        /// 点击矩形中间部分
        /// </summary>
        /// <param name="rectangle"></param>
        public static void SetMouseClick(Rectangle rectangle)
        {
            OptBaseX.SetCursorPos(new POINT() { x = (int)Math.Abs(rectangle.X + rectangle.Width / 2.0), y = (int)Math.Abs(rectangle.Y + rectangle.Height / 2.0) });
            MouseKeyboardLibrary.MouseSimulator.Click(MouseKeyboardLibrary.MouseButton.Left
                );
        }
    }
}
