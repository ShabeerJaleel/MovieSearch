using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MovieTube.Viewer
{
    public static class Tracer
    {
        public static event EventHandler<TraceEventArgs> Trace;
        public static void WriteLine(string text)
        {
            Debug.WriteLine(text);
            if (Trace != null)
                Trace(text, new TraceEventArgs(text));
        }
    }

    public class TraceEventArgs : EventArgs
    {
        public TraceEventArgs(string text)
        {
            Text = text;
        }
        public string Text { get; private set; }
    } 
}
