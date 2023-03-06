using System;
using System.Runtime.InteropServices;

public static class WinMouse
{
	private const int MOUSEEVENTF_LEFTDOWN = 0x02;
	private const int MOUSEEVENTF_LEFTUP = 0x04;
	private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
	private const int MOUSEEVENTF_RIGHTUP = 0x10;
	
	public enum Button
	{
		Left,
		Right,
	}

	public static (int x, int y) Position
	{
		get
		{
			GetCursorPos(out POINT point);
			return (point.X, point.Y);
		}

		set => SetCursorPos(value.x, value.y);
	}

	public static void Click(Button button = Button.Left)
	{
		int action = button == Button.Left
							 ? MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP
							 : MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
		
		mouse_event(action, 0, 0, 0, 0);
	}

	public static void Press(Button button = Button.Left)
	{
		int action = button == Button.Left ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_RIGHTDOWN;
		mouse_event(action, 0, 0, 0, 0);
	}

	public static void Release(Button button = Button.Left)
	{
		int action = button == Button.Left ? MOUSEEVENTF_LEFTUP : MOUSEEVENTF_RIGHTUP;
		mouse_event(action, 0, 0, 0, 0);
	}
	
	[DllImport("user32.dll")]
	static extern bool SetCursorPos(int X, int Y);
	
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
	
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	static extern bool GetCursorPos(out POINT point);
	
	[StructLayout(LayoutKind.Sequential)]
	struct POINT
	{
		public Int32 X;
		public Int32 Y;
	}
}

