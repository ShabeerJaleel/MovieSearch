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
using MovieTube.Viewer.Properties;
using MovieTube.Viewer.Data;
using MovieTube.Client.Scraper;
using DZ.MediaPlayer.Vlc;
using System.IO;

namespace MovieTube.Viewer
{
    public partial class VlcVideoPlayerWidget : BasePlayerWidget, IFavouriteEnabledView, IPlayerWidget
    {

        #region Fields

        private bool init;
        private List<KryptonCheckButton> playerButtons = new List<KryptonCheckButton>();
        
        private bool mute;
        private int lastVolume;
        
        public event EventHandler<PlayEventArgs> PlayNext;
        public event EventHandler<PlayEventArgs> PlayPrevious;
        private DateTime lastPositionChangedTime;
        private volatile bool showBuffering;

        #endregion

        #region Contructor

        public VlcVideoPlayerWidget()
        {
            InitializeComponent();
            
            playerButtons.Add(this.buttonNext);
            playerButtons.Add(this.buttonPrevious);
            playerButtons.Add(this.buttonPlayPause);
            playerButtons.Add(this.buttonStop);
           
        }

        #endregion

        #region Public

        public bool PlayStart(MovieLink link, bool start = true)
        {
            this.headerGroupVideoPlayer.ValuesPrimary.Heading = String.Format("{0} - {1}", link.Parent.Name, link.Parent.ReleaseDate.Year);
            this.currentPlayingVideo = link;
            UpdateFavouriteButton(dataService.IsFavourite(this.currentPlayingVideo.Parent));
            return start ? PlayStart(link.PlayUrl) : true;
        }
         
        private void UpdateFavouriteButton(bool fav)
        {
            this.buttonSpecHeaderFavourite.Checked = fav  ?
                ButtonCheckState.Checked : ButtonCheckState.Unchecked;
        }


     
        private bool PlayStart(string url)
        {
            try
            {
                Init();
              
                this.vlcPlayerControl.Play(new MediaInput(MediaInputType.NetworkStream, url));
                this.lastPositionChangedTime = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                Program.SetIdle();
                KryptonMessageBox.Show("Unable to play the video. Please try again", 
                    Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public void ShowBuffering()
        {
            this.showBuffering = true;
            timerBufferCheck_Tick(this, null);
        }
        public void HideBuffering()
        {
            this.showBuffering = false;
        }


        #endregion

        #region Private

        private void Init()
        {
            if (!init)
            {
                this.vlcPlayerControl.Initialize(this,
                   new string[] {
				"--reset-config",
				"--no-snapshot-preview",
				"--ignore-config",
				"--intf", "rc",
				"--no-osd",
                "--dshow-caching=20000",
                "--http-caching=20000",
                "--network-caching=20000",
                "--drop-late-frames",
				"--plugin-path", Path.Combine(Application.StartupPath, "plugins")});
                InitializeProgress();
                InitializeVolume();
                this.vlcPlayerControl.StateChanged += vlc_onStateChanged;
                this.vlcPlayerControl.PositionChanged += vlc_onPositionChanged;
                this.vlcPlayerControl.EndReached += vlc_onEndReached;
                this.vlcPlayerControl.Stopped += vlc_onStopped;
                this.vlcPlayerControl.EncounteredError += vlc_onEncounteredError;
                this.vlcPlayerControl.TimeChanged += vlc_onTimeChanged;

                
                this.vlcPlayerControl.Player.IsFullScreen = true;
                ((VlcSinglePlayer)this.vlcPlayerControl.Player).WaitingRequiredStateTimeout = new TimeSpan(0,1,0);
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
                int newTrackbarPositionValue = (int)(this.vlcPlayerControl.Position * 1000);

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

                KryptonMessageBox.Show(String.Format("Cannot stop player : {0}", exc));
            }
        }
        
        #endregion

        #region Events
        private void vlc_onEncounteredError(object sender, EventArgs args)
        {
            VlcPlayerControlState currentState = this.vlcPlayerControl.State;
            //
            switch (currentState)
            {
                case VlcPlayerControlState.Idle:
                    {
                        Debug.WriteLine("Idle");
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Paused:
                    {
                        Debug.WriteLine("Paused");
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Playing:
                    {
                        Debug.WriteLine("Playing");
                        this.buttonPlayPause.Values.Image = Resources.pause;
                        break;
                    }
            }
        }
        private void vlc_onStateChanged(object sender, EventArgs args)
        {
            VlcPlayerControlState currentState = this.vlcPlayerControl.State;
            //
            switch (currentState)
            {
                case VlcPlayerControlState.Idle:
                    {
                        Debug.WriteLine("Idle");
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Paused:
                    {
                        Debug.WriteLine("Paused");
                        this.buttonPlayPause.Values.Image = Resources.play;
                        break;
                    }
                case VlcPlayerControlState.Playing:
                    {
                        Debug.WriteLine("Playing");
                        this.buttonPlayPause.Values.Image = Resources.pause;
                        break;
                    }
            }
        }
        private void vlc_onEndReached(object sender, EventArgs e)
        {

        }
        private void vlc_onStopped(object sender, EventArgs e)
        {

        }
        private void vlc_onTimeChanged(object sender, EventArgs e)
        {

        }
        private void vlc_onPositionChanged(object sender, EventArgs e)
        {
            this.lastPositionChangedTime = DateTime.Now;
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
                KryptonMessageBox.Show(String.Format("Cannot change volume level: {0}", exc));
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
                KryptonMessageBox.Show(String.Format("Cannot change position : {0}", exc));
            }
            isMouseClick = false;
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
           UpdateFavouriteButton( ToggleFavourite());
                    
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (PlayNext != null)
                PlayNext(this, new PlayEventArgs { Current =  this.currentPlayingVideo != null ? this.currentPlayingVideo.Parent : null });
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (PlayPrevious != null)
                PlayPrevious(this, new PlayEventArgs { Current = this.currentPlayingVideo != null ? this.currentPlayingVideo.Parent : null });
        }

        private void timerBufferCheck_Tick(object sender, EventArgs e)
        {
           

            if ( (this.vlcPlayerControl.State == VlcPlayerControlState.Playing &&
                this.lastPositionChangedTime.AddSeconds(2) < DateTime.Now) ||
                this.showBuffering)
            {
                this.kryptonLabelBuffering.Visible = true;
            }
            else
                this.kryptonLabelBuffering.Visible = false;

        }

      
        private void kryptonTrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            return;
            if (e.Button != System.Windows.Forms.MouseButtons.Left || !isMouseClick)
                return;

            try
            {

                if (this.vlcPlayerControl.State == VlcPlayerControlState.Idle)
                {
                    this.kryptonTrackBar1.Value = 0;
                    return;
                }
                var a = (float)1000 / (float)this.kryptonTrackBar1.ClientRectangle.Width;
                var p = a * e.X;
                this.kryptonTrackBar1.Value = (int)p;
                //
                float val = 1f * this.kryptonTrackBar1.Value / this.kryptonTrackBar1.Maximum;
                if (val != this.vlcPlayerControl.Position)
                    this.vlcPlayerControl.Position = val;

            }
            catch (Exception exc)
            {
                //
                KryptonMessageBox.Show(String.Format("Cannot change position : {0}", exc));
            }

        }

        private bool isMouseClick;
        private void kryptonTrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseClick = true;
        }
        public PlayerType PlayerType
        {
            get { return Viewer.PlayerType.VLC; }
        }

        #endregion



       
    }

    
}
