using System.Runtime.InteropServices;

namespace Macro.Services
{
    public static class MouseService
    {
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void MoveAndLeftClick(Point pt)
        {
            Cursor.Position = pt;

            mouse_event((int)MouseEventFlags.LeftDown, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlags.LeftUp, 0, 0, 0, 0);
        }

        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x0002,
            LeftUp = 0x0004
        }
    }
}
