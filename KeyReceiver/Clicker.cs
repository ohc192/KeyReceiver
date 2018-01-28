using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyReceiver
{
    public class Clicker
    {
        
        public class PixelOffsets
        {

            public static Point TopCheckBox => new Point(-130, -80);
            public static Point CloseButton => new Point(120, -90);
            public static Point OpenButton => new Point(-100, -30);

            public class CheckBoxes
            {
                public static Point EquipmentLvl2 = TopCheckBox;
                public static Point EquipmentLvl3 = new Point(TopCheckBox.X, TopCheckBox.Y + CheckBoxDistance);
                public static Point Meds = new Point(TopCheckBox.X, TopCheckBox.Y + 2 * CheckBoxDistance);
                public static Point Sniper = new Point(TopCheckBox.X, TopCheckBox.Y + 3 * CheckBoxDistance);
                public static Point AR = new Point(TopCheckBox.X, TopCheckBox.Y + 4 * CheckBoxDistance);
                public static Point SMG = new Point(TopCheckBox.X, TopCheckBox.Y + 5 * CheckBoxDistance); 
                public static Point Scopes = new Point(TopCheckBox.X, TopCheckBox.Y + 6 * CheckBoxDistance);
            }

            public static int CheckBoxDistance = 30;
        }

        private IntPtr handle = IntPtr.Zero;
        private readonly string executable;

        public Clicker(string executable)
        {
            this.executable = executable;
            GetHandle();
        }

        private void GetHandle()
        {
            Process[] p = Process.GetProcessesByName(executable);
            if (p.Length == 1)
            {
                handle = p[0].MainWindowHandle;
            }
        }

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;

        [StructLayout(LayoutKind.Sequential)]
        public struct TPoint
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref TPoint lpPoint);
        
        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs,
            [MarshalAs(UnmanagedType.LPArray),
             In]
            INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public async Task ClickOnPoint(Point p, bool fromCenter = false, bool fromBottomRight = false, bool ReturnCursor = true, bool isRetry = false)
        {
            if (handle == IntPtr.Zero)
            {
                if (!isRetry)
                {
                    GetHandle();
                    await ClickOnPoint(p, fromCenter, fromBottomRight, ReturnCursor, true);
                }

                return;
            }
            
            Point oldPos = Cursor.Position;

            /*TPoint clientPoint;
            clientPoint.X = x;
            clientPoint.Y = y;
            ClientToScreen(handle, ref clientPoint);*/

            RECT rct;

            if (!GetWindowRect(handle, out rct))
            {
                MessageBox.Show("ERROR: Could not get DC client window rectangle");
                return;
            }

            Point newPoint;
            if (fromCenter)
            {
                newPoint = new Point((rct.Left + rct.Right) / 2 + p.X, (rct.Top + rct.Bottom) / 2 + p.Y);
            }
            else if (fromBottomRight)
            {
                newPoint = new Point(rct.Right + p.X, rct.Bottom + p.Y);
            }
            else
            {
                newPoint = new Point(rct.Left + p.X, rct.Top + p.Y);
            }

            Cursor.Position = newPoint;

            INPUT inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; // input type: mouse
            inputMouseDown.Data.Mouse.Flags = MOUSEEVENTF_LEFTDOWN;

            var inputs = new INPUT[] {inputMouseDown};

            SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            INPUT inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; // input type: mouse
            inputMouseUp.Data.Mouse.Flags = MOUSEEVENTF_LEFTUP;

            inputs = new INPUT[] { inputMouseUp };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(50);

            if (ReturnCursor)
            {
                Cursor.Position = oldPos;
            }
        }

        public async Task SetStateDiff(ItemState diff)
        {
            await ClickOnPoint(PixelOffsets.OpenButton, false, true);
            if (diff.EquipmentLvl2)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.EquipmentLvl2, true);
            }
            if (diff.EquipmentLvl3)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.EquipmentLvl3, true);
            }
            if (diff.Meds)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.Meds, true);
            }
            if (diff.Sniper)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.Sniper, true);
            }
            if (diff.AR)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.AR, true);
            }
            if (diff.SMG)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.SMG, true);
            }
            if (diff.Scopes)
            {
                await ClickOnPoint(PixelOffsets.CheckBoxes.Scopes, true);
            }
            await ClickOnPoint(PixelOffsets.CloseButton, true);
        }
    }
}
