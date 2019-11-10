using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Runtime.InteropServices;

namespace Video_Conference
{
    public class TimedDialog : IDisposable
    {
        private int mTid;
        private ManualResetEvent mStop = new ManualResetEvent(false);

        public TimedDialog(int msec)
        {
            mTid = GetCurrentThreadId();
            ThreadPool.QueueUserWorkItem(findDialog, msec);
        }

        public void Dispose()
        {
            mStop.Set();
        }

        private void findDialog(object arg)
        {
            if (!mStop.WaitOne((int)arg, false))
                EnumThreadWindows(mTid, new EnumThreadDelegate(enumWindow), IntPtr.Zero);
        }

        private static bool enumWindow(IntPtr hWnd, IntPtr lp)
        {
            // Find a dialog window
            StringBuilder sb = new StringBuilder(256);
            GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;
            // Alrighty, got it, now zap it with WM_CLOSE
            SendMessage(hWnd, 0x10, IntPtr.Zero, IntPtr.Zero);
            return false;
        }

        //--- P/Invoke declarations
        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lp);
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadDelegate callback, IntPtr lp);
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder name, int maxlen);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

    }
}
