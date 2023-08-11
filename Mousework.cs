using System;
using System.Runtime.InteropServices;
using System.Threading;

public static class Mousework
{
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    // имитировать левую кнопку мыши, чтобы нажать 
    const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    // имитировать левую мышь, чтобы поднять 
    const int MOUSEEVENTF_LEFTUP = 0x0004;
    // Моделируйте правильную кнопку мыши и нажмите 
    const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    // имитировать правильную кнопку мыши для подъема 
    const int MOUSEEVENTF_RIGHTUP = 0x0010;
    // имитировать клавишу в нажатии мыши 
    const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    // смоделировать ключ, чтобы поднять клавишу в мышью 
    const int MOUSEEVENTF_MIDDLEUP = 0x0040;
    // имитирует работу мыши и должна соответствовать параметру DWDATA
    const int MOUSEEVENTF_WHEEL = 0x0800;

    // получить координаты курсора
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT point);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
    // установить курсор в позицию
    [DllImport("User32.dll")]
    public static extern bool SetCursorPos(int x, int y);
    // переместить курсор относительно
    public static void MoveCursorOn(int x, int y)
    {
        GetCursorPos(out POINT point);
        Thread.Sleep(200);
        SetCursorPos(point.X + x, point.Y + y);
    }
    // перетащить из ... в ....
    public static void DragCursorFromIn(int x1, int y1, int x2, int y2)
    {
        SetCursorPos(x1, y1);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(200);
        SetCursorPos(x2, y2);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
    // перетащить в ....
    public static void DragCursorIn(int x, int y)
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(200);
        SetCursorPos(x, y);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
    // перетащить из ... на ...
    public static void DragCursorFromOn(int x1, int y1, int x2, int y2)
    {
        SetCursorPos(x1, y1);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(200);
        SetCursorPos(x1 + x2, y1 + y2);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
    // перетащить на ...
    public static void DragCursorOn(int x, int y)
    {
        GetCursorPos(out POINT point);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(200);
        SetCursorPos(point.X + x, point.Y + y);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }
    // обработка нажатий Left по координатам
    public static void ClickLeft(string str, int x, int y)
    {
        SetCursorPos(x, y);
        Thread.Sleep(200);
        switch (str)
        {
            case "down": mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); break;
            case "up": mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); break;
        }
    }
    public static void ClickLeft(int click, int x, int y)
    {
        SetCursorPos(x, y);
        Thread.Sleep(200);
        switch (click)
        {
            case 1: mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); break;
            case 2:
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                }
        }
    }
    // обработка нажатий Left на месте
    public static void ClickLeft(string str)
    {
        Thread.Sleep(200);
        switch (str)
        {
            case "down": mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); break;
            case "up": mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); break;
        }
    }
    public static void ClickLeft(int click)
    {
        Thread.Sleep(200);
        switch (click)
        {
            case 1: mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); break;
            case 2:
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                }
        }
    }
    // обработка нажатий Right по координатам
    public static void ClickRight(int x, int y)
    {
        SetCursorPos(x, y);
        Thread.Sleep(200);
        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }
    // обработка нажатий Right на месте
    public static void ClickRight()
    {
        Thread.Sleep(100);
        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }
    // обработка прокрутки колеса
    public static void MoveWheel(int a)
    {
        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, a, 0);
    }
}
