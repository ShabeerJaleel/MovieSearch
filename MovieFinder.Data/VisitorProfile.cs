//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieFinder.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisitorProfile
    {
        public System.Guid ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public System.DateTime LastAccessedTime { get; set; }
        public string LastAccessedIP { get; set; }
        public string LastAccessedLanguage { get; set; }
        public string LastAccessedUrl { get; set; }
        public int HitCount { get; set; }
    }
}
