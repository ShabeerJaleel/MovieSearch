using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using DZ.MediaPlayer.Vlc.WindowsForms;
using DZ.MediaPlayer.Vlc.Io;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;
using BlueTube.Viewer.Properties;
using Client.Scraper;
using BlueTube.Viewer.Data;

namespace BlueTube.Viewer
{
    public partial class VideoPlayerWidget : UserControl, IFavouriteEnabledView
    {

        #region Fields

        private bool init;
        private List<KryptonCheckButton> playerButtons = new List<KryptonCheckButton>();
        private ScrapedVideo currentPlayingVideo;
        private DataService dataService = DataService.Create();
        private bool mute;
        private int lastVolume;
        public event EventHandler<ToggleFavouriteEventArgs> ToggelFavourite;
        public event EventHandler<PlayEventArgs> PlayNext;
        public event EventHandler<PlayEventArgs> PlayPrevious;

        #endregion

        #region Contructor

        public VideoPlayerWidget()
        {
            InitializeComponent();
            
            playerButtons.Add(this.buttonNext);
            playerButtons.Add(this.buttonPrevious);
            playerButtons.Add(this.buttonPlayPause);
            playerButtons.Add(this.buttonStop);
        }

        #endregion

        #region Public

        public bool PlayStart(ScrapedVideo video)
        {
            this.headerGroupVideoPlayer.ValuesPrimary.Heading = video.Title;
            this.currentPlayingVideo = video;
            UpdateFavouriteButton(DataService.Create().IsFavourite(this.currentPlayingVideo));
            return PlayStart(video.PlayUrl);
        }
         
        private void UpdateFavouriteButton(bool fav)
        {
            this.buttonSpecHeaderFavourite.Checked = fav  ?
                ButtonCheckState.Checked : ButtonCheckState.Unchecked;
        }


     
        public bool PlayStart(string url)
        {
            try
            {
                Init();

                this.vlcPlayerControl.Play(new MediaInput(MediaInputType.NetworkStream, url));
                return true;
            }
            catch (Exception ex)
            {
                Program.SetIdle();
                KryptonMessageBox.Show("Unable to play the video. Please try again", Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return false;
        }
        public void ActivateView()
        {
            if (this.vlcPlayerControl.State == VlcPlayerControlState.Paused)
                this.vlcPlayerControl.PauseOrResume();
        }
        public void DeactivateView()
        {
            if(this.vlcPlayerControl.State == VlcPlayerControlState.Playing)
                this.vlcPlayerControl.PauseOrResume();
        }


        #endregion

        #region Private

        private void Init()
        {
            if (!init)
            {
                this.vlcPlayerControl.Initialize(this);
                InitializeProgress();
                InitializeVolume();
                this.vlcPlayerControl.StateChanged += vlc_onStateChanged;
                this.vlcPlayerControl.PositionChanged += vlc_onPositionChanged;
                this.vlcPlayerControl.EndReached += vlc_onEndReached;
                this.vlcPlayerControl.Player.IsFullScreen = true;
                init = true;
            }

        }
        private void InitializeProgress()
        {
            //statusStrip.Items["playerStatus"].Text = Convert.ToString(videoWindow.VlcPlayerControl.State);
            this.labelTimeProgress.Text = String.Format("{0:D2}:{1:D2}:{2:D2}",
                this.vlcPlayerControl.Time.Hours, this.vlcPlayerControl.Time.Minutes,
                this.vlcPlayerControl.Time.Seconds);
        }
        private void InitializeVolume()
        {
            this.kryptonTrackBarVolume.Value = (int)((1.0 * this.vlcPlayerControl.Volume / 100) *
                this.kryptonTrackBarVolume.Maximum);
        }
        private void PlayerButtonsUncheck()
        {
            foreach (var b in this.playerButtons)
                b.Checked = false;
        }
        private void UpdatePlayProgress()
        {

            try
            {
                int newTrackbarPositionValue = (int)(this.vlcPlayerControl.Position * 100);

                if (newTrackbarPositionValue != this.kryptonTrackBar1.Value)
                {
                    this.kryptonTrackBar1.Value = newTrackbarPositionValue > this.kryptonTrackBar1.Maximum
                                                ? this.kryptonTrackBar1.Maximum
                                                : newTrackbarPositionValue;
                }

                var time = this.vlcPlayerControl.Time;
                this.labelTimeProgress.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
                if (this.vlcPlayerControl.State != VlcPlayerControlState.Idle)
                {
                    var total = TimeSpan.FromMilliseconds((1 / this.vlcPlayerControl.Position) * this.vlcPlayerControl.Time.TotalMilliseconds);
                    this.labelTotalTime.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", total.Hours, total.Minutes, total.Seconds);
                }
            }
            catch { }
            finally
            {

            }

        }
        public void PlayStop()
        {
            PlayerButtonsUncheck();
            try
            {
                if (this.vlcPlayerControl.State != VlcPlayerControlState.Idle)
                {
                    this.vlcPlayerControl.Stop();
                    UpdatePlayProgress();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show(String.Format("Cannot stop player : {0}", exc));
            }
        }
        

        #endregion

        #region Events

        private void vlc_onStateChanged(object sender, EventArgs args)
        {
            VlcPlayerControlState currentState = this.vlcPlayerControl.State;
            //
            switch (currentState)
            {
                case VlcPlayerControlState.Idle:
                    {
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Paused:
                    {
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Playing:
                    {
                        this.buttonPlayPause.Values.Image = Resources.pause;
                        break;
                    }
            }
        }
        private void vlc_onEndReached(object sender, EventArgs e)
        {

        }
        private void vlc_onPositionChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new ThreadStart(UpdatePlayProgress));
            else
                UpdatePlayProgress();
        }

        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            PlayerButtonsUncheck();
            switch (this.vlcPlayerControl.State)
            {
                case VlcPlayerControlState.Idle:
                    {
                        if (currentPlayingVideo != null)
                            PlayStart(currentPlayingVideo);
                        break;
                    }
                case VlcPlayerControlState.Paused:
                    {
                        this.vlcPlayerControl.PauseOrResume();
                        break;
                    }
                case VlcPlayerControlState.Playing:
                    {
                        this.vlcPlayerControl.PauseOrResume();
                        break;
                    }
            }
        }

        private void kryptonCheckButton1_Click(object sender, EventArgs e)
        {
            //this.vlcControl1.AudioProperties.IsMute = !this.vlcControl1.AudioProperties.IsMute;
        }

        private void kryptonTrackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.vlcPlayerControl.Volume = (int)((1.0 *
                    this.kryptonTrackBarVolume.Value / this.kryptonTrackBarVolume.Maximum) * 100);
                if (this.vlcPlayerControl.Volume > 0)
                {
                    mute = false;
                    this.buttonVolume.Values.Image = Resources.volume;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(String.Format("Cannot change volume level: {0}", exc));
            }
        }

        private void kryptonTrackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {

                if (this.vlcPlayerControl.State == VlcPlayerControlState.Idle)
                {
                    this.kryptonTrackBar1.Value = 0;
                    return;
                }
                //
                float val = 1f * this.kryptonTrackBar1.Value / this.kryptonTrackBar1.Maximum;
                if (val != this.vlcPlayerControl.Position)
                    this.vlcPlayerControl.Position = val;

            }
            catch (Exception exc)
            {
                //
                MessageBox.Show(String.Format("Cannot change position : {0}", exc));
            }

        }

        private void VideoPlayerWidget_Load(object sender, EventArgs e)
        {
            //Init();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            PlayStop();
        }

        private void kryptonCheckButtonVolume_Click(object sender, EventArgs e)
        {
            Init();
            if (!mute)
                this.lastVolume = this.vlcPlayerControl.Volume;

            this.vlcPlayerControl.Volume = mute ? this.lastVolume : 0;
            mute = !mute;
            this.buttonVolume.Values.Image = mute ? Resources.mute : Resources.volume;
            this.kryptonTrackBarVolume.Value = this.vlcPlayerControl.Volume;
        }

        public void TriggerSearch(SearchParameters param)
        {
            throw new NotImplementedException();
        }

        private void buttonSpecHeaderGroupFullscreen_Click(object sender, EventArgs e)
        {
            new FullscreenPlayer(this.vlcPlayerControl).ShowDialog();
        }

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            if(this.currentPlayingVideo != null)
            {
                var fav = dataService.IsFavourite(this.currentPlayingVideo);
                if (!fav)
                    dataService.AddToFavourite(this.currentPlayingVideo);
                else
                    dataService.DeleteFromFavourite(this.currentPlayingVideo);
                    
                UpdateFavouriteButton(!fav);
                if (this.ToggelFavourite != null)
                    this.ToggelFavourite(this, new ToggleFavouriteEventArgs
                    {
                        IsFavourite = !fav,
                        Video = this.currentPlayingVideo
                    });

                
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (PlayNext != null)
                PlayNext(this, new PlayEventArgs { Current = this.currentPlayingVideo });
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (PlayPrevious != null)
                PlayPrevious(this, new PlayEventArgs { Current = this.currentPlayingVideo });
        }

        #endregion

       

    }

    
}
