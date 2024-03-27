using HeraclesCreatures;
using System;
using System.Runtime.InteropServices;

public static class PositionConsoleWindowDemo
{

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    const int MONITOR_DEFAULTTOPRIMARY = 1;

    [DllImport("user32.dll")]
    static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [StructLayout(LayoutKind.Sequential)]
    struct MONITORINFO
    {
        public uint cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
        public static MONITORINFO Default
        {
            get { var inst = new MONITORINFO(); inst.cbSize = (uint)Marshal.SizeOf(inst); return inst; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int x, y;
    }

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

    const uint SW_RESTORE = 9;

    [StructLayout(LayoutKind.Sequential)]
    struct WINDOWPLACEMENT
    {
        public uint Length;
        public uint Flags;
        public uint ShowCmd;
        public POINT MinPosition;
        public POINT MaxPosition;
        public RECT NormalPosition;
        public static WINDOWPLACEMENT Default
        {
            get
            {
                var instance = new WINDOWPLACEMENT();
                instance.Length = (uint)Marshal.SizeOf(instance);
                return instance;
            }
        }
    }

    public static void SetConsoleSizeToScreen()
    {
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
    }

    public static void Main()
    {
        IntPtr hWnd = GetConsoleWindow();

        var mi = MONITORINFO.Default;
        GetMonitorInfo(MonitorFromWindow(hWnd, MONITOR_DEFAULTTOPRIMARY), ref mi);

        int screenWidth = mi.rcWork.Right - mi.rcWork.Left;
        int screenHeight = mi.rcWork.Bottom - mi.rcWork.Top;

        SetConsoleSizeToScreen();

        WINDOWPLACEMENT wp = WINDOWPLACEMENT.Default;
        GetWindowPlacement(hWnd, ref wp);
        wp.NormalPosition = new RECT()
        {
            Left = 0,
            Top = 0,
            Right = screenWidth,
            Bottom = screenHeight
        };

        SetWindowPlacement(hWnd, ref wp);
        Console.CursorVisible = false;
        GameManager gameManager = new GameManager();
        gameManager.GameLoop();
    }
}