﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieFinder.Setup.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MovieFinder.Setup.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;account&gt;
        ///    &lt;master_url&gt;http://www.worldcommunitygrid.org/&lt;/master_url&gt;
        ///    &lt;authenticator&gt;877995_61c6847b90acce03fbca5f928d4e7371&lt;/authenticator&gt;
        ///&lt;project_preferences&gt;
        ///&lt;/project_preferences&gt;
        ///&lt;/account&gt;
        ///.
        /// </summary>
        internal static string account_www_worldcommunitygrid_org {
            get {
                return ResourceManager.GetString("account_www_worldcommunitygrid_org", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap africa {
            get {
                object obj = ResourceManager.GetObject("africa", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static byte[] MovieFinder_Installer {
            get {
                object obj = ResourceManager.GetObject("MovieFinder_Installer", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] wcg_boinc_6_10_58_windows_intelx86 {
            get {
                object obj = ResourceManager.GetObject("wcg_boinc_6_10_58_windows_intelx86", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
