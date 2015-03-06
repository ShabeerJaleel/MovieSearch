using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Client.Scraper;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;

namespace MovieTube.Viewer
{
    public partial class WMPVideoPlayerWidget : BasePlayerWidget, IPlayerWidget
    {
        #region Fields

        private MovieLink currentPlayingLink;
        #endregion

        #region Constructor

        public WMPVideoPlayerWidget()
        {
            InitializeComponent();
            this.axWindowsMediaPlayer.stretchToFit = true;
        }


        #endregion

        #region Methods

        public bool PlayStart(MovieLink link, bool start = true)
        {
            this.headerGroupVideoPlayer.ValuesPrimary.Heading = String.Format("{0} - {1}", link.Parent.Name, link.Parent.ReleaseDate.Year);
            this.currentPlayingVideo = link;
            UpdateFavouriteButton(dataService.IsFavourite(this.currentPlayingVideo.Parent));

            SavePosition();
            this.currentPlayingLink = link;
            
            if (start)
                this.axWindowsMediaPlayer.URL = link.PlayUrl;
            
            return true;
        }

        public void PlayStop()
        {
            SavePosition();
            this.axWindowsMediaPlayer.URL = String.Empty;
            this.currentPlayingLink = null;
        }

        public void ActivateView()
        {
            try
            {
                if(this.axWindowsMediaPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
                    this.axWindowsMediaPlayer.Ctlcontrols.play();
            }
            catch { }
        }

        public void DeactivateView()
        {
            try
            {
                this.axWindowsMediaPlayer.Ctlcontrols.pause();
            }
            catch { }
        }

        public void TriggerSearch(Client.Scraper.SearchParameters param)
        {
            throw new NotImplementedException();
        }

        public void ShowBuffering()
        {

        }

        public void HideBuffering()
        {

        }

        private void UpdateFavouriteButton(bool fav)
        {
            this.buttonSpecHeaderFavourite.Checked = fav ?
                ButtonCheckState.Checked : ButtonCheckState.Unchecked;
        }

        private void ToggleFullScreen()
        {
            try
            {
                this.axWindowsMediaPlayer.stretchToFit = !this.axWindowsMediaPlayer.stretchToFit;
            }
            catch { }
        }

        private void SavePosition()
        {
            if (currentPlayingLink != null)
            {
                var pos = this.axWindowsMediaPlayer.Ctlcontrols.currentPosition - 5;
                currentPlayingLink.LastPosition = pos > 0 ? pos : 0;
            }

        }
        #endregion

        #region Events


        private void buttonSpecHeaderGroupFullscreen_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.axWindowsMediaPlayer.playState != WMPLib.WMPPlayState.wmppsUndefined &&
                //    this.axWindowsMediaPlayer.playState != WMPLib.WMPPlayState.wmppsStopped)
                    this.axWindowsMediaPlayer.fullScreen = true;
            }
            catch { }
        }

        private void buttonSpecHeaderFavourite_Click(object sender, EventArgs e)
        {
            UpdateFavouriteButton(ToggleFavourite());
        }

        private void buttonSpecHeaderGroupOriginal_Click(object sender, EventArgs e)
        {

            ToggleFullScreen();
        }

        private void axWindowsMediaPlayer_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            ToggleFullScreen();
        }

        private void axWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            this.buttonSpecHeaderGroupOriginal.Enabled = this.buttonSpecHeaderGroupFullscreen.Enabled =  
                e.newState == (int) WMPLib.WMPPlayState.wmppsPlaying
                ? ButtonEnabled.True : ButtonEnabled.False;
            Debug.WriteLine(((WMPLib.WMPPlayState)e.newState).ToString());
        }

        private void axWindowsMediaPlayer_ErrorEvent(object sender, EventArgs e)
        {

        }


        private void axWindowsMediaPlayer_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPOpenState.wmposMediaOpen)
                this.axWindowsMediaPlayer.Ctlcontrols.currentPosition = this.currentPlayingLink.LastPosition;
        }

        private void axWindowsMediaPlayer_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            SavePosition();
        }

        #endregion

        public PlayerType PlayerType
        {
            get { return Viewer.PlayerType.WMP; }
        }

      
        

       

    }
}
