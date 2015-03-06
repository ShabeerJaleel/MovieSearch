using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace BlueTube.Viewer
{
    public static class ExtensionMethods
    {
        public static void InvokeEx<TControl>(this TControl control, Action action)
   where TControl : Control
        {
            if (control.InvokeRequired)
                control.BeginInvoke(action);
            else
                action();
        }

    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class |
        AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}
