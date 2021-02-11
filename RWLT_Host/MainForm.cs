using RWLT_Host.RWLT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLKML;
using UCNLNav;
using UCNLNMEA;
using UCNLUI.Dialogs;
using uOSM;

namespace RWLT_Host
{   
    public partial class MainForm : Form
    {
        #region Properties

        uOSMTileProvider tProvider;

        TSLogProvider logger;
        SimpleSettingsProviderXML<SettingsContainer> settingsProvider;       

        string settingsFileName;
        string logPath;
        string logFileName;
        string snapshotsPath;
        string tileDBPath;

        bool isRestart = false;
        bool isAutoscreenshot = false;

        RWLT_Core core;

        bool tracksChanged = true;
        bool TracksChanged
        {
            get { return tracksChanged; }
            set
            {
                if (value != tracksChanged)
                {
                    tracksChanged = value;
                    InvokeSetEnabled(mainToolStrip, trackExportBtn, tracksChanged);
                    InvokeSetEnabled(mainToolStrip, trackClearBtn, tracksChanged);
                }
            }
        }


        Dictionary<string, List<GeoPoint3DTm>> tracks = new Dictionary<string, List<GeoPoint3DTm>>();

        Array basesIDs = Enum.GetValues(typeof(BaseIDs));

        RWLT_Emulator emulator;
        bool emulatorEnabled = false;

        Dictionary<TBAQuality, Color> tbaTextColors = new Dictionary<TBAQuality, Color>()
        {
            { TBAQuality.Good, Color.Green },
            { TBAQuality.Fair, Color.DarkGoldenrod },
            { TBAQuality.Poor, Color.Orange },
            { TBAQuality.Out_of_base, Color.Red },
            { TBAQuality.Invalid, Color.Black }
        };

        Dictionary<DOPState, Color> dopTextColors = new Dictionary<DOPState, Color>()
        {
            { DOPState.Ideal, Color.LimeGreen },
            { DOPState.Excellent, Color.Green },
            { DOPState.Good, Color.Olive },
            { DOPState.Moderate, Color.DarkGoldenrod },
            { DOPState.Fair, Color.Orange },
            { DOPState.Poor, Color.Red },
            { DOPState.Invalid, Color.Black }
        };

        #endregion

        #region Constructor

        public MainForm()
        {            
            InitializeComponent();

            #region paths & filenames

            DateTime startTime = DateTime.Now;
            settingsFileName = Path.ChangeExtension(Application.ExecutablePath, "settings");
            logPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
            logFileName = StrUtils.GetTimeDirTreeFileName(startTime, Application.ExecutablePath, "LOG", "log", true);
            snapshotsPath = StrUtils.GetTimeDirTree(startTime, Application.ExecutablePath, "SNAPSHOTS", false);

            tileDBPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Cache\\Tiles\\");

            #endregion

            #region logger

            logger = new TSLogProvider(logFileName);
            logger.WriteStart();
            logger.Write(string.Format("{0} v{1}", Application.ProductName, Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            logger.TextAddedEvent += (o, e) => InvokeAppendHisotryLine(e.Text);

            #endregion            

            #region settings

            settingsProvider = new SimpleSettingsProviderXML<SettingsContainer>();
            settingsProvider.isSwallowExceptions = false;

            logger.Write(string.Format("Loading settings from {0}", settingsFileName));

            try
            {
                settingsProvider.Load(settingsFileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            logger.Write("Current application settings:");
            logger.Write(settingsProvider.Data.ToString());

            #endregion                        

            #region core

            core = new RWLT_Core(new SerialPortSettings(settingsProvider.Data.InPortName, settingsProvider.Data.InPortBaudrate,
                System.IO.Ports.Parity.None, DataBits.dataBits8, System.IO.Ports.StopBits.One, System.IO.Ports.Handshake.None),
                settingsProvider.Data.RadialErrorThrehsold, 10.0, settingsProvider.Data.CourseEstimatorFIFOSize, settingsProvider.Data.TrackFilterFIFOSize);

            core.Salinity = settingsProvider.Data.SalinityPSU;
            if (!settingsProvider.Data.IsSoundSpeedAuto)
                core.Soundspeed = settingsProvider.Data.SoundSpeedMPS;

            core.LogEvent += (o, e) => logger.Write(string.Format("{0}: {1}", e.EventType, e.LogString));

            core.SystemUpdateEvent += new EventHandler(core_SystemUpdateEventHandler);
            core.LocationUpdatedEvent += new EventHandler<LocationUpdatedEventArgs>(core_LocationUpdatedEventHandler);

            if (settingsProvider.Data.IsUseOutPort)
                core.InitOutputPort(new SerialPortSettings(settingsProvider.Data.OutPortName,
                    settingsProvider.Data.OutPortBaudrate,
                    System.IO.Ports.Parity.None,
                    DataBits.dataBits8,
                    System.IO.Ports.StopBits.One,
                    System.IO.Ports.Handshake.None));

            if (settingsProvider.Data.IsUseAUXGNSSPort)
            {
                core.InitAUXGNSSPort(new SerialPortSettings(settingsProvider.Data.AUXGNSSPortName,
                    settingsProvider.Data.AUXGNSSPortBaudrate,
                    System.IO.Ports.Parity.None,
                    DataBits.dataBits8,
                    System.IO.Ports.StopBits.One,
                    System.IO.Ports.Handshake.None));
            }
            else
            {
                if (settingsProvider.Data.IsUseBuoyAsAUXGNSS && (settingsProvider.Data.AuxGNSSBuoyID != BaseIDs.BASE_INVALID))
                {
                    core.AuxGNSSBuoyID = settingsProvider.Data.AuxGNSSBuoyID;
                }
            }
            
            #endregion

            #region Misc. UI

            auxCrsLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;
            auxCapLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;
            auxGNSSStatusLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;
            auxGNSSStatusCaptionLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;
            auxGNSSStatusCaptionLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;
            auxGNSSStatusLbl.Visible = settingsProvider.Data.IsUseAUXGNSSPort || settingsProvider.Data.IsUseBuoyAsAUXGNSS;

            List<string> fitTracksNames = new List<string>();
            fitTracksNames.Add("ALL");
            fitTracksNames.Add("RWLT (FLT)");
            fitTracksNames.Add("Marked");
            fitTracksNames.Add("RWLT (FLT)+Marked");
            fitTracksNames.Add("RWLT (FLT)+RWLT (RAW)");

            geoPlot.InitTrack("RWLT (RAW)", 64, Color.Yellow, 1, 4, false, Color.Yellow, 1, 200);
            geoPlot.InitTrack("RWLT (FLT)", settingsProvider.Data.TrackPointsToShow, Color.Red, 1, 4, true, Color.Red, 1, 200);

            if (settingsProvider.Data.IsUseAUXGNSSPort)
            {
                geoPlot.InitTrack("AUX GNSS", 64, Color.Blue, 1, 4, true, Color.Blue, 1, 200);
                fitTracksNames.Add("RWLT (FLT)+AUX GNSS");
                fitTracksNames.Add("RWLT (FLT)+AUX GNSS+Marked");
            }

            fitTracksNames.Add("BASE 1");
            fitTracksNames.Add("BASE 2");
            fitTracksNames.Add("BASE 3");
            fitTracksNames.Add("BASE 4");

            geoPlot.InitTrack("Marked", 256, Color.Black, 4, 4, false, Color.Black, 1, 200);

            geoPlot.InitTrack("BASE 1", 4, Color.DarkRed, 2, 4, false, Color.Salmon, 1, 200);
            geoPlot.InitTrack("BASE 2", 4, Color.DarkOrange, 2, 4, false, Color.Gold, 1, 200);
            geoPlot.InitTrack("BASE 3", 4, Color.Green, 2, 4, false, Color.MediumSpringGreen, 1, 200);
            geoPlot.InitTrack("BASE 4", 4, Color.Purple, 2, 4, false, Color.SkyBlue, 1, 200);
            

            TracksChanged = false;

            tracksToFitCbx.Items.Clear();
            tracksToFitCbx.Items.AddRange(fitTracksNames.ToArray());
            tracksToFitCbx.SelectedIndex = 0;

            geoPlot.SetTracksVisibility(true);

            geoPlot.TextBackgroundColor = Color.FromArgb(127, Color.White);

            #endregion

            #region emulator

            emulatorEnabled = settingsProvider.Data.IsEmulatorButtonVisible;
            emulatorBtn.Visible = emulatorEnabled;
            if (emulatorEnabled)
            {
                emulator = new RWLT_Emulator(48 + 30 / 60.0 + 16.03 / 3600.0, 44 + 33 / 60.0 + 6.01 / 3600.0, 30, 10, 7);
                emulator.NewEmuStringEvent += (o, e) => core.Emulate(e.EmuString);
            }

            #endregion

            #region tProvider

            tProvider = new uOSMTileProvider(256, 19, new Size(256, 256), tileDBPath, settingsProvider.Data.TileServers);
            geoPlot.ConnectTileProvider(tProvider);

            #endregion
        }

        #endregion

        #region Methods
        
        private void SaveFullScreenshot()
        {
            Bitmap target = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(target, this.DisplayRectangle);

            try
            {
                if (!Directory.Exists(snapshotsPath))
                    Directory.CreateDirectory(snapshotsPath);

                target.Save(Path.Combine(snapshotsPath, string.Format("{0}.{1}", StrUtils.GetHMSString(), ImageFormat.Png)));
            }
            catch
            {
                //
            }
        }

        private void InvokeSaveFullScreenshot()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SaveFullScreenshot(); });
            else
                SaveFullScreenshot();
        }

        private void ProcessException(Exception ex, bool isShowMsgBox)
        {
            logger.Write(ex);

            if (isShowMsgBox)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InvokeAppendHisotryLine(string line)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AppendHistory(line); });
            else
                geoPlot.AppendHistory(line);
        }

        private void InvokeSetLeftTopCornerText(string line)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.LeftUpperText = line; });
            else
                geoPlot.LeftUpperText = line;
        }

        private void InvokeUpdateTrack(string id, double lat, double lon, double course)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AddPoint(id, lat, lon, course); });
            else
                geoPlot.AddPoint(id, lat, lon, course);
        }

        private void InvokeUpdateTrack(string id, double lat, double lon)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AddPoint(id, lat, lon); });
            else
                geoPlot.AddPoint(id, lat, lon);
        }

        private void InvokeUpdateView()
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.Invalidate(); });
            else
                geoPlot.Invalidate();
        }

        private void InvokeSetText(StatusStrip strip, ToolStripStatusLabel lbl, string text)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { lbl.Text = text; });
            else
                lbl.Text = text;
        }

        private void InvokeSetText(ToolStrip strip, ToolStripLabel lbl, string text)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { lbl.Text = text; });
            else
                lbl.Text = text;
        }

        private void InvokeSetColorMode(ToolStrip strip, ToolStripLabel lbl, Color foreColor)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { lbl.ForeColor = foreColor; });
            else
                lbl.ForeColor = foreColor;
        }

        private void InvokeSetEnabled(ToolStrip strip, ToolStripItem btn, bool enabled)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { btn.Enabled = enabled; });
            else
                btn.Enabled = enabled;
        }        


        private bool TracksExportToKML(string fileName)
        {
            KMLData data = new KMLData(fileName, "Generated by RWLT Host application");
            List<KMLLocation> kmlTrack;

            foreach (var item in tracks)
            {
                kmlTrack = new List<KMLLocation>();
                foreach (var point in item.Value)
                    kmlTrack.Add(new KMLLocation(point.Longitude, point.Latitude, -point.Depth));

                data.Add(new KMLPlacemark(string.Format("{0} track", item.Key), "", kmlTrack.ToArray()));
            }

            bool isOk = false;
            try
            {
                TinyKML.Write(data, fileName);
                isOk = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            return isOk;
        }

        private bool TracksExportToCSV(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var track in tracks)
            {
                sb.AppendFormat("\r\nTrack name: {0}\r\n", track.Key);
                sb.Append("HH:MM:SS;LAT;LON;DPT;\r\n");

                foreach (var point in track.Value)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "{0:00};{1:00};{2:00};{3:F06};{4:F06};{5:F03}\r\n",
                        point.TimeStamp.Hour,
                        point.TimeStamp.Minute,
                        point.TimeStamp.Second,
                        point.Latitude,
                        point.Longitude,
                        point.Depth);
                }
            }

            bool isOk = false;
            try
            {
                File.WriteAllText(fileName, sb.ToString());
                isOk = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            return isOk;
        }

        private void AddTrackPoint(string key, double lat, double lon, double dpt, DateTime tStamp, double course_deg)
        {
            if (!tracks.ContainsKey(key))
                tracks.Add(key, new List<GeoPoint3DTm>());

            tracks[key].Add(new GeoPoint3DTm(lat, lon, dpt, tStamp));

            #region Emulator related
            if ((emulatorEnabled) && (emulator.IsRunning))
            {
                if (!tracks.ContainsKey("target_actual_emu"))
                    tracks.Add("target_actual_emu", new List<GeoPoint3DTm>());

                tracks["target_actual_emu"].Add(new GeoPoint3DTm(emulator.TargetLatitude, emulator.TargetLongitude, dpt, tStamp));
            }
            #endregion

            TracksChanged = true;

            if (!double.IsNaN(course_deg))
                InvokeUpdateTrack(key, lat, lon, course_deg);
            else
                InvokeUpdateTrack(key, lat, lon);

            InvokeUpdateView();
        }


        private void ProcessAnalyzeLog(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = string.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        int idx = s.IndexOf(NMEAParser.SentenceStartDelimiter);
                        if (idx >= 0)
                        {
                            core.Emulate(s.Substring(idx) + "\r\n");                            
                            Application.DoEvents();
                        }
                    }
                }

                MessageBox.Show(string.Format("Analysis of '{0}' is complete", Path.GetFileNameWithoutExtension(fileName)),
                    "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        #endregion

        #region Handlers

        #region UI

        #region mainToolStrip

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (core.IsOpen)
            {
                logger.Write("Closing connection(s)...");
                try
                {
                    core.Stop();                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }

                settingsBtn.Enabled = true;
                logAnalyzeBtn.Enabled = true;
                logAnalyzeCurrentBtn.Enabled = true;
                connectionBtn.Checked = false;

            }
            else
            {
                logger.Write("Opening connection(s)...");

                try
                {
                    core.Start();

                    settingsBtn.Enabled = false;
                    logAnalyzeBtn.Enabled = false;
                    logAnalyzeCurrentBtn.Enabled = false;
                    connectionBtn.Checked = true;
                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }
        }

        #region TRACK

        private void trackExportBtn_Click(object sender, EventArgs e)
        {
            bool isSaved = false;

            using (SaveFileDialog sDilog = new SaveFileDialog())
            {
                sDilog.Title = "Exporting tracks...";
                sDilog.Filter = "Google KML (*.kml)|*.kml|CSV (*.csv)|*.csv";
                sDilog.FileName = string.Format("RWLT_Tracks_{0}", StrUtils.GetHMSString());

                if (sDilog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (sDilog.FilterIndex == 1)
                        isSaved = TracksExportToKML(sDilog.FileName);
                    else if (sDilog.FilterIndex == 2)
                        isSaved = TracksExportToCSV(sDilog.FileName);
                }
            }

            if (isSaved &&
                MessageBox.Show("Tracks saved, do you want to clear all tracks data?",
                "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                tracks.Clear();
                TracksChanged = false;
            }
        }

        private void trackClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear all tracks data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                tracks.Clear();
                TracksChanged = false;
            }
        }

        #endregion

        #region LOG

        private void logViewCurrentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(logger.FileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void logAnalyzeCurrentBtn_Click(object sender, EventArgs e)
        {
            ProcessAnalyzeLog(logFileName);
        }

        private void logAnalyzeBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Select a LOG file to analyze...";
                oDialog.DefaultExt = "log";
                oDialog.Filter = "LOG files (*.log)|*.log";
                oDialog.InitialDirectory = logPath;

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProcessAnalyzeLog(oDialog.FileName);
                }
            }
        }

        private void logClearAllEntriesBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete all log entries?",
                                "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                string logRootPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
                var dirs = Directory.GetDirectories(logRootPath);
                int dirNum = 0;
                foreach (var item in dirs)
                {
                    try
                    {
                        Directory.Delete(item, true);
                        dirNum++;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }

                MessageBox.Show(string.Format("{0} entries was/were deleted.", dirNum),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        #endregion        

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            using (SettingsEditor sEditor = new SettingsEditor())
            {
                sEditor.Value = settingsProvider.Data;

                if (sEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool isSaved = false;
                    settingsProvider.Data = sEditor.Value;

                    try
                    {
                        settingsProvider.Save(settingsFileName);
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }

                    if ((isSaved) && (MessageBox.Show("Settings saved. Restart application to apply new settings?",
                        "Question",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        isRestart = true;
                        Application.Restart();
                    }
                }
            }
        }

        private void isScreenShotsBtn_Click(object sender, EventArgs e)
        {
            isAutoscreenshot = isScreenShotsBtn.Checked;
        }

        private void markCurrentBtn_Click(object sender, EventArgs e)
        {
            core.MarkCurrentLocation();
        }

        private void emulatorBtn_Click(object sender, EventArgs e)
        {
            if (emulator.IsRunning)
            {
                emulator.Stop();
                emulatorBtn.Checked = false;
                connectionBtn.Enabled = true;
                settingsBtn.Enabled = true;
            }
            else
            {
                emulator.Start();
                emulatorBtn.Checked = true;
                connectionBtn.Enabled = false;
                settingsBtn.Enabled = false;
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aDialog = new AboutBox())
            {                
                aDialog.ApplyAssembly(Assembly.GetExecutingAssembly());
                aDialog.Weblink = "www.unavlab.com";
                aDialog.ShowDialog();
            }
        }

        #endregion        

        #region plotStrip

        private void tracksToFitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fTrackNames = tracksToFitCbx.SelectedItem.ToString();
            if (fTrackNames == "ALL")
                geoPlot.SetTracksVisibility(true);
            else
            {
                var splits = fTrackNames.Split(new char[] { '+' });
                geoPlot.SetTracksVisibility(splits, true);
            }

            geoPlot.Invalidate();
        }

        private void isStatisticsBtn_Click(object sender, EventArgs e)
        {
            core.IsStatistics = !core.IsStatistics;
            isStatisticsBtn.Checked = core.IsStatistics;
        }

        #endregion

        #region mainForm

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tracksChanged)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.Yes;
                while (tracksChanged && (result == System.Windows.Forms.DialogResult.Yes))
                {
                    result = MessageBox.Show("Tracks are not saved. Save them before exit?", 
                        "Warning", 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                        trackExportBtn_Click(sender, null);
                }

                e.Cancel = (result == System.Windows.Forms.DialogResult.Cancel);
            }
            else
            {
                e.Cancel = !isRestart && (MessageBox.Show(string.Format("Close {0}?", Application.ProductName),
                                                          "Question",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (core.IsOpen)
            {
                try
                {
                    core.Stop();
                }
                catch (Exception ex)
                {
                    ProcessException(ex, false);
                }
            }

            core.Dispose();

            logger.Write("Closing application...");
            logger.FinishLog();
            logger.Flush();
        }

        #endregion

        #endregion

        #region core

        private void core_SystemUpdateEventHandler(object sender, EventArgs e)
        {
            #region LeftTopCornerText

            StringBuilder sb = new StringBuilder();

            sb.Append("Target\r\n");

            InvokeSetEnabled(mainToolStrip, markCurrentBtn, core.TargetLatitude.IsInitializedAndNotObsolete);

            if (core.TargetLatitude.IsInitialized && core.TargetLongitude.IsInitialized)
            {
                sb.AppendFormat("LAT: {0}\r\nLON: {1}\r\n", core.TargetLatitude, core.TargetLongitude);

                if (core.IsRadialErrorExeedsThreshold)
                {
                    sb.AppendFormat("RER: > {0} m\r\n", settingsProvider.Data.RadialErrorThrehsold);
                }
                else
                {
                    sb.AppendFormat("RER: {0}\r\n", core.TargetLocationRadialError);
                }
                
                #region emultion related things
                if ((emulatorEnabled) && (emulator.IsRunning))
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture, "ELT: {0:F06}\r\nELN: {1:F06}\r\n", emulator.TargetLatitude, emulator.TargetLongitude);

                    //
                    double dst_m = 0;
                    double fwd_az_rad = 0;
                    double rev_az_rad = 0;
                    int its = 0;
                    Algorithms.VincentyInverse(Algorithms.Deg2Rad(emulator.TargetLatitude), Algorithms.Deg2Rad(emulator.TargetLongitude),
                        Algorithms.Deg2Rad(core.TargetLatitude.Value), Algorithms.Deg2Rad(core.TargetLongitude.Value), Algorithms.WGS84Ellipsoid,
                        Algorithms.VNC_DEF_EPSILON, Algorithms.VNC_DEF_IT_LIMIT, out dst_m, out fwd_az_rad, out rev_az_rad, out its);

                    sb.AppendFormat(CultureInfo.InvariantCulture, "AER: {0:F03}\r\n", dst_m);
                    //
                }
                #endregion                
            }                            

            if (core.TargetDepth.IsInitialized)
                sb.AppendFormat("DPT: {0}\r\n", core.TargetDepth);

            if (core.TargetBatVoltage.IsInitialized)
                sb.AppendFormat("BAT: {0}\r\n", core.TargetBatVoltage);

            if (core.TargetTemperature.IsInitialized)
                sb.AppendFormat("TMP: {0}\r\n", core.TargetTemperature);

            if (core.TargetPressure.IsInitialized)
                sb.AppendFormat("PRS: {0}\r\n", core.TargetPressure);

            if (core.TargetAlarm.IsInitialized)
                sb.AppendFormat("ALM: {0}\r\n", core.TargetAlarm);

            if (core.CEP.IsInitialized)
                sb.AppendFormat("CEP: {0}\r\n", core.CEP);

            if (core.DRMS.IsInitialized)
                sb.AppendFormat("DRMS: {0}\r\n", core.DRMS);

            if (core.DRMS2.IsInitialized)
                sb.AppendFormat("2DRMS: {0}\r\n", core.DRMS2);

            if (core.DRMS3.IsInitialized)
                sb.AppendFormat("3DRMS: {0}\r\n", core.DRMS3);


            sb.Append("\r\nBases");
            foreach (BaseIDs baseID in basesIDs)
            {
                if (baseID != BaseIDs.BASE_INVALID)
                {
                    sb.AppendFormat("\r\n{0}: ", ((int)baseID) + 1);

                    if (core.BaseBatVoltages.ContainsKey(baseID))
                        if (core.BaseBatVoltages[baseID].IsInitialized)
                            sb.AppendFormat("⚡ {0}, ", core.BaseBatVoltages[baseID]);

                    if (core.BaseMSRs.ContainsKey(baseID))
                        if (core.BaseMSRs[baseID].IsInitialized)
                            sb.AppendFormat("ᛉ {0}", core.BaseMSRs[baseID]);
                }
            }


            if (core.AUXGNSSUsed)
            {
                sb.AppendFormat("\r\n\r\nAUX GNSS\r\n");
                if (core.AUXLatitude.IsInitialized && core.AUXLongitude.IsInitialized)
                    sb.AppendFormat("LAT: {0}\r\nLON: {1}\r\n", core.AUXLatitude, core.AUXLongitude);

                if (core.AUXSpeed.IsInitialized)
                    sb.AppendFormat("SPD: {0}\r\n", core.AUXSpeed);

                if (core.AUXTrack.IsInitialized)
                    sb.AppendFormat("TRK: {0}\r\n", core.AUXTrack);
            }

            InvokeSetLeftTopCornerText(sb.ToString());
            InvokeUpdateView();

            #endregion

            #region port state status labels

            if (core.IsOpen)
            {
                string inPortStateStr = "OK";
                if (core.InPortTimeout)
                    inPortStateStr = "TIMEOUT";

                InvokeSetText(mainStatusStrip, mainPortStatusLbl, inPortStateStr);

                if (core.AUXGNSSUsed)
                {
                    string auxGNSSPortStateStr = "OK";
                    if (core.AUXGNSSTimeout)
                        auxGNSSPortStateStr = "TIMEOUT";

                    InvokeSetText(mainStatusStrip, auxGNSSStatusLbl, auxGNSSPortStateStr);
                }
            }

            #endregion

            #region target status labels

            sb.Clear();

            if (core.TargetCourse.IsInitializedAndNotObsolete)
                sb.AppendFormat("⎈{0} ", core.TargetCourse);

            if (core.ReverseAzimuthToTarget.IsInitialized)
                sb.AppendFormat("🡿{0} ", core.ReverseAzimuthToTarget);

            if (sb.Length > 0)
                InvokeSetText(targetToolStrip, targetCrsLbl, sb.ToString());

            sb.Clear();

            if (core.TargetDepth.IsInitialized)
                sb.AppendFormat("🡻{0} ", core.TargetDepth);

            if (core.TargetBatVoltage.IsInitialized)
                sb.AppendFormat("⚡{0} ", core.TargetBatVoltage);

            if (core.TargetTemperature.IsInitialized)
                sb.AppendFormat("🌡{0} ", core.TargetTemperature);
            
            if (sb.Length > 0)
                InvokeSetText(targetToolStrip, targetMiscLbl, sb.ToString());

            #endregion

            #region Target-to-base arrangement quality & GDOP labels

            if (core.TBAState.IsInitialized)
            {
                InvokeSetText(targetToolStrip, tbaLbl, core.TBAState.ToString());
                InvokeSetColorMode(targetToolStrip, tbaLbl, tbaTextColors[core.TBAState.Value]);
            }

            if (core.HDOPState.IsInitialized)
            {
                InvokeSetText(targetToolStrip, hdopLbl, core.HDOPState.ToString());
                InvokeSetColorMode(targetToolStrip, hdopLbl, dopTextColors[core.HDOPState.Value]);
            }

            #endregion

            #region AUX/Target

            if (core.AUXGNSSUsed || (core.AuxGNSSBuoyID != BaseIDs.BASE_INVALID))
            {
                sb.Clear();

                if (core.AUXTrack.IsInitialized)
                    sb.AppendFormat("⎈{0} ", core.AUXTrack);

                if (core.ForwardAzimuthToTarget.IsInitialized)
                    sb.AppendFormat("🡽{0} ", core.ForwardAzimuthToTarget);

                if (sb.Length > 0)
                    InvokeSetText(auxToolStrip, auxCrsLbl, sb.ToString());


                sb.Clear();                

                if (core.DistanceToTarget.IsInitialized)
                    sb.AppendFormat("🡺{0} ", core.DistanceToTarget);

                if (core.AUXSpeed.IsInitialized)
                    sb.AppendFormat("🐌{0} ", core.AUXSpeed);

                if (sb.Length > 0)
                    InvokeSetText(auxToolStrip, auxMiscLbl, sb.ToString());
            }

            #endregion
        }

        private void core_LocationUpdatedEventHandler(object sender, LocationUpdatedEventArgs e)
        {            
            if (e.IsValid)
            {              
                double course_deg = double.NaN;

                if (e.ID == "RWLT (FLT)")
                {
                    if (core.TargetCourse.IsInitialized)
                        course_deg = core.TargetCourse.Value;

                    logger.Write(string.Format(CultureInfo.InvariantCulture, "TLOC: LAT={0:F06}°, LON={1:F06}°, DPT={2}, Valid={3}",
                        e.Latitude, e.Longitude, 
                        double.IsNaN(e.Depth) ? "undefined" : string.Format(CultureInfo.InvariantCulture, "{0:F03} m", e.Depth), 
                        e.IsValid));
                }
                else if (e.ID == "AUX GNSS")
                {
                    if (core.AUXTrack.IsInitialized)
                        course_deg = core.AUXTrack.Value;
                }

                AddTrackPoint(e.ID, e.Latitude, e.Longitude, e.Depth, e.TimeStamp, course_deg);
            }

            if (isAutoscreenshot)
                InvokeSaveFullScreenshot();
        }

        #endregion        

        #endregion                
    }
}
