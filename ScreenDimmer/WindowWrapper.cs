using System;
using System.Runtime.InteropServices;

namespace ScreenDimmer
{
    public class WindowWrapper
    {
        //[DllImport("user32")]
        //public static extern int GetWindowLong(IntPtr hWnd, WindowWrapper.GWL nIndex);

        [DllImport("user32")]
        public static extern int SetWindowLong(IntPtr hWnd, WindowWrapper.GWL nIndex, WindowWrapper.WS_EX dsNewLong);

        //[DllImport("user32.dll")]
        //public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, WindowWrapper.LWA dwFlags);

        public enum GWL
        {
            ExStyle = -20,
        }

        public enum WS_EX
        {
            Transparent = 32,
            Layered = 524288,
        }

        //public enum LWA
        //{
        //    ColorKey = 1,
        //    Alpha = 2,
        //}
    }
}
