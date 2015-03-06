﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace MovieFinder.Web.Data
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MovieFinderEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MovieFinderEntities object using the connection string found in the 'MovieFinderEntities' section of the application configuration file.
        /// </summary>
        public MovieFinderEntities() : base("name=MovieFinderEntities", "MovieFinderEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MovieFinderEntities object.
        /// </summary>
        public MovieFinderEntities(string connectionString) : base(connectionString, "MovieFinderEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MovieFinderEntities object.
        /// </summary>
        public MovieFinderEntities(EntityConnection connection) : base(connection, "MovieFinderEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<AccessLog> AccessLogs
        {
            get
            {
                if ((_AccessLogs == null))
                {
                    _AccessLogs = base.CreateObjectSet<AccessLog>("AccessLogs");
                }
                return _AccessLogs;
            }
        }
        private ObjectSet<AccessLog> _AccessLogs;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<MovieLink> MovieLinks
        {
            get
            {
                if ((_MovieLinks == null))
                {
                    _MovieLinks = base.CreateObjectSet<MovieLink>("MovieLinks");
                }
                return _MovieLinks;
            }
        }
        private ObjectSet<MovieLink> _MovieLinks;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the AccessLogs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAccessLogs(AccessLog accessLog)
        {
            base.AddObject("AccessLogs", accessLog);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the MovieLinks EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMovieLinks(MovieLink movieLink)
        {
            base.AddObject("MovieLinks", movieLink);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MovieFinderModel", Name="AccessLog")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class AccessLog : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new AccessLog object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="uniqueID">Initial value of the UniqueID property.</param>
        /// <param name="timestamp">Initial value of the Timestamp property.</param>
        /// <param name="accessCount">Initial value of the AccessCount property.</param>
        public static AccessLog CreateAccessLog(global::System.Int32 id, global::System.Guid uniqueID, global::System.DateTime timestamp, global::System.Int32 accessCount)
        {
            AccessLog accessLog = new AccessLog();
            accessLog.ID = id;
            accessLog.UniqueID = uniqueID;
            accessLog.Timestamp = timestamp;
            accessLog.AccessCount = accessCount;
            return accessLog;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid UniqueID
        {
            get
            {
                return _UniqueID;
            }
            set
            {
                OnUniqueIDChanging(value);
                ReportPropertyChanging("UniqueID");
                _UniqueID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("UniqueID");
                OnUniqueIDChanged();
            }
        }
        private global::System.Guid _UniqueID;
        partial void OnUniqueIDChanging(global::System.Guid value);
        partial void OnUniqueIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                OnIPAddressChanging(value);
                ReportPropertyChanging("IPAddress");
                _IPAddress = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("IPAddress");
                OnIPAddressChanged();
            }
        }
        private global::System.String _IPAddress;
        partial void OnIPAddressChanging(global::System.String value);
        partial void OnIPAddressChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                OnTimestampChanging(value);
                ReportPropertyChanging("Timestamp");
                _Timestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Timestamp");
                OnTimestampChanged();
            }
        }
        private global::System.DateTime _Timestamp;
        partial void OnTimestampChanging(global::System.DateTime value);
        partial void OnTimestampChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 AccessCount
        {
            get
            {
                return _AccessCount;
            }
            set
            {
                OnAccessCountChanging(value);
                ReportPropertyChanging("AccessCount");
                _AccessCount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AccessCount");
                OnAccessCountChanged();
            }
        }
        private global::System.Int32 _AccessCount;
        partial void OnAccessCountChanging(global::System.Int32 value);
        partial void OnAccessCountChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                OnCountryCodeChanging(value);
                ReportPropertyChanging("CountryCode");
                _CountryCode = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CountryCode");
                OnCountryCodeChanged();
            }
        }
        private global::System.String _CountryCode;
        partial void OnCountryCodeChanging(global::System.String value);
        partial void OnCountryCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ClientTime
        {
            get
            {
                return _ClientTime;
            }
            set
            {
                OnClientTimeChanging(value);
                ReportPropertyChanging("ClientTime");
                _ClientTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ClientTime");
                OnClientTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _ClientTime;
        partial void OnClientTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnClientTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PCName
        {
            get
            {
                return _PCName;
            }
            set
            {
                OnPCNameChanging(value);
                ReportPropertyChanging("PCName");
                _PCName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PCName");
                OnPCNameChanged();
            }
        }
        private global::System.String _PCName;
        partial void OnPCNameChanging(global::System.String value);
        partial void OnPCNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Version
        {
            get
            {
                return _Version;
            }
            set
            {
                OnVersionChanging(value);
                ReportPropertyChanging("Version");
                _Version = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Version");
                OnVersionChanged();
            }
        }
        private global::System.String _Version;
        partial void OnVersionChanging(global::System.String value);
        partial void OnVersionChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MovieFinderModel", Name="MovieLink")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class MovieLink : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new MovieLink object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="movieID">Initial value of the MovieID property.</param>
        /// <param name="linkTitle">Initial value of the LinkTitle property.</param>
        /// <param name="siteTitle">Initial value of the SiteTitle property.</param>
        /// <param name="pageUrl">Initial value of the PageUrl property.</param>
        /// <param name="pageSiteID">Initial value of the PageSiteID property.</param>
        /// <param name="downloadSiteID">Initial value of the DownloadSiteID property.</param>
        /// <param name="dowloadUrl">Initial value of the DowloadUrl property.</param>
        /// <param name="failedAttempts">Initial value of the FailedAttempts property.</param>
        /// <param name="version">Initial value of the Version property.</param>
        /// <param name="hasSubtitle">Initial value of the HasSubtitle property.</param>
        public static MovieLink CreateMovieLink(global::System.Int32 id, global::System.Int32 movieID, global::System.String linkTitle, global::System.String siteTitle, global::System.String pageUrl, global::System.String pageSiteID, global::System.String downloadSiteID, global::System.String dowloadUrl, global::System.Int32 failedAttempts, global::System.Int32 version, global::System.Boolean hasSubtitle)
        {
            MovieLink movieLink = new MovieLink();
            movieLink.ID = id;
            movieLink.MovieID = movieID;
            movieLink.LinkTitle = linkTitle;
            movieLink.SiteTitle = siteTitle;
            movieLink.PageUrl = pageUrl;
            movieLink.PageSiteID = pageSiteID;
            movieLink.DownloadSiteID = downloadSiteID;
            movieLink.DowloadUrl = dowloadUrl;
            movieLink.FailedAttempts = failedAttempts;
            movieLink.Version = version;
            movieLink.HasSubtitle = hasSubtitle;
            return movieLink;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 MovieID
        {
            get
            {
                return _MovieID;
            }
            set
            {
                OnMovieIDChanging(value);
                ReportPropertyChanging("MovieID");
                _MovieID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MovieID");
                OnMovieIDChanged();
            }
        }
        private global::System.Int32 _MovieID;
        partial void OnMovieIDChanging(global::System.Int32 value);
        partial void OnMovieIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LinkTitle
        {
            get
            {
                return _LinkTitle;
            }
            set
            {
                OnLinkTitleChanging(value);
                ReportPropertyChanging("LinkTitle");
                _LinkTitle = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LinkTitle");
                OnLinkTitleChanged();
            }
        }
        private global::System.String _LinkTitle;
        partial void OnLinkTitleChanging(global::System.String value);
        partial void OnLinkTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String SiteTitle
        {
            get
            {
                return _SiteTitle;
            }
            set
            {
                OnSiteTitleChanging(value);
                ReportPropertyChanging("SiteTitle");
                _SiteTitle = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("SiteTitle");
                OnSiteTitleChanged();
            }
        }
        private global::System.String _SiteTitle;
        partial void OnSiteTitleChanging(global::System.String value);
        partial void OnSiteTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String PageUrl
        {
            get
            {
                return _PageUrl;
            }
            set
            {
                OnPageUrlChanging(value);
                ReportPropertyChanging("PageUrl");
                _PageUrl = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("PageUrl");
                OnPageUrlChanged();
            }
        }
        private global::System.String _PageUrl;
        partial void OnPageUrlChanging(global::System.String value);
        partial void OnPageUrlChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String PageSiteID
        {
            get
            {
                return _PageSiteID;
            }
            set
            {
                OnPageSiteIDChanging(value);
                ReportPropertyChanging("PageSiteID");
                _PageSiteID = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("PageSiteID");
                OnPageSiteIDChanged();
            }
        }
        private global::System.String _PageSiteID;
        partial void OnPageSiteIDChanging(global::System.String value);
        partial void OnPageSiteIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String DownloadSiteID
        {
            get
            {
                return _DownloadSiteID;
            }
            set
            {
                OnDownloadSiteIDChanging(value);
                ReportPropertyChanging("DownloadSiteID");
                _DownloadSiteID = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DownloadSiteID");
                OnDownloadSiteIDChanged();
            }
        }
        private global::System.String _DownloadSiteID;
        partial void OnDownloadSiteIDChanging(global::System.String value);
        partial void OnDownloadSiteIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String DowloadUrl
        {
            get
            {
                return _DowloadUrl;
            }
            set
            {
                OnDowloadUrlChanging(value);
                ReportPropertyChanging("DowloadUrl");
                _DowloadUrl = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DowloadUrl");
                OnDowloadUrlChanged();
            }
        }
        private global::System.String _DowloadUrl;
        partial void OnDowloadUrlChanging(global::System.String value);
        partial void OnDowloadUrlChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 FailedAttempts
        {
            get
            {
                return _FailedAttempts;
            }
            set
            {
                OnFailedAttemptsChanging(value);
                ReportPropertyChanging("FailedAttempts");
                _FailedAttempts = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FailedAttempts");
                OnFailedAttemptsChanged();
            }
        }
        private global::System.Int32 _FailedAttempts;
        partial void OnFailedAttemptsChanging(global::System.Int32 value);
        partial void OnFailedAttemptsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> LastValidatedWhen
        {
            get
            {
                return _LastValidatedWhen;
            }
            set
            {
                OnLastValidatedWhenChanging(value);
                ReportPropertyChanging("LastValidatedWhen");
                _LastValidatedWhen = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LastValidatedWhen");
                OnLastValidatedWhenChanged();
            }
        }
        private Nullable<global::System.DateTime> _LastValidatedWhen;
        partial void OnLastValidatedWhenChanging(Nullable<global::System.DateTime> value);
        partial void OnLastValidatedWhenChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Version
        {
            get
            {
                return _Version;
            }
            set
            {
                OnVersionChanging(value);
                ReportPropertyChanging("Version");
                _Version = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Version");
                OnVersionChanged();
            }
        }
        private global::System.Int32 _Version;
        partial void OnVersionChanging(global::System.Int32 value);
        partial void OnVersionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> LastValidatedBy
        {
            get
            {
                return _LastValidatedBy;
            }
            set
            {
                OnLastValidatedByChanging(value);
                ReportPropertyChanging("LastValidatedBy");
                _LastValidatedBy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LastValidatedBy");
                OnLastValidatedByChanged();
            }
        }
        private Nullable<global::System.Guid> _LastValidatedBy;
        partial void OnLastValidatedByChanging(Nullable<global::System.Guid> value);
        partial void OnLastValidatedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean HasSubtitle
        {
            get
            {
                return _HasSubtitle;
            }
            set
            {
                OnHasSubtitleChanging(value);
                ReportPropertyChanging("HasSubtitle");
                _HasSubtitle = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("HasSubtitle");
                OnHasSubtitleChanged();
            }
        }
        private global::System.Boolean _HasSubtitle;
        partial void OnHasSubtitleChanging(global::System.Boolean value);
        partial void OnHasSubtitleChanged();

        #endregion

    
    }

    #endregion

    
}
