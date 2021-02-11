using RWLT_Host.RWLT;
using System;
using System.IO.Ports;
using System.Windows.Forms;
using UCNLUI.Dialogs;

namespace RWLT_Host
{
    public partial class SettingsEditor : Form
    {
        #region Properties

        string inportName
        {
            get { return UIUtils.TryGetCbxItem(inportNameCbx); }
            set { UIUtils.TrySetCbxItem(inportNameCbx, value); }
        }

        UCNLDrivers.BaudRate inportBaudrate
        {
            get { return (UCNLDrivers.BaudRate)Enum.Parse(typeof(UCNLDrivers.BaudRate), UIUtils.TryGetCbxItem(inportBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(inportBaudrateCbx, value.ToString()); }
        }


        bool isUseOutPort
        {
            get { return isUseOutputPortChb.Checked; }
            set { isUseOutputPortChb.Checked = value; }
        }

        string outportName
        {
            get { return UIUtils.TryGetCbxItem(outportNameCbx); }
            set { UIUtils.TrySetCbxItem(outportNameCbx, value); }
        }

        UCNLDrivers.BaudRate outportBaudrate
        {
            get { return (UCNLDrivers.BaudRate)Enum.Parse(typeof(UCNLDrivers.BaudRate), UIUtils.TryGetCbxItem(outportBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(outportBaudrateCbx, value.ToString()); }
        }


        bool isUseAUXGNSSPort
        {
            get { return isUseAUXGNSSPortChb.Checked; }
            set { isUseAUXGNSSPortChb.Checked = value; }
        }

        string auxGNSSPortName
        {
            get { return UIUtils.TryGetCbxItem(auxGNSSPortNameCbx); }
            set { UIUtils.TrySetCbxItem(auxGNSSPortNameCbx, value); }
        }

        UCNLDrivers.BaudRate auxGNSSPortBaudrate
        {
            get { return (UCNLDrivers.BaudRate)Enum.Parse(typeof(UCNLDrivers.BaudRate), UIUtils.TryGetCbxItem(auxGNSSPortBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(auxGNSSPortBaudrateCbx, value.ToString()); }
        }            

        double salinityPSU
        {
            get { return Convert.ToDouble(salinityEdit.Value); }
            set { UIUtils.TrySetNEditValue(salinityEdit, value); }
        }

        bool isAutoSoundSpeed
        {
            get { return isAutoSoundSpeedChb.Checked; }
            set { isAutoSoundSpeedChb.Checked = value; }
        }

        double soundSpeedMps
        {
            get { return Convert.ToDouble(soundSpeedEdit.Value); }
            set { UIUtils.TrySetNEditValue(soundSpeedEdit, value); }
        }

        int trackFIFOSize
        {
            get { return Convert.ToInt32(trackFIFOSizeEdit.Value); }
            set { UIUtils.TrySetNEditValue(trackFIFOSizeEdit, value); }
        }

        double rerrThreshold
        {
            get { return Convert.ToDouble(rerrEdit.Value); }
            set { UIUtils.TrySetNEditValue(rerrEdit, value); }
        }

        bool isBuoyAsAuxGNSS
        {
            get { return isBuoyAsAuxGNSSChb.Checked; }
            set { isBuoyAsAuxGNSSChb.Checked = value; }
        }

        BaseIDs auxGnssBuoyID
        {
            get { return (BaseIDs)Enum.Parse(typeof(BaseIDs), UIUtils.TryGetCbxItem(auxGnssBuoyIDCbx)); }
            set { UIUtils.TrySetCbxItem(auxGnssBuoyIDCbx, value.ToString()); }
        }

        public SettingsContainer Value
        {
            get
            {
                SettingsContainer result = new SettingsContainer();
                result.InPortName = inportName;
                result.InPortBaudrate = inportBaudrate;
                result.IsUseOutPort = isUseOutPort;
                result.OutPortName = outportName;
                result.OutPortBaudrate = outportBaudrate;
                result.IsUseAUXGNSSPort = isUseAUXGNSSPort;
                result.AUXGNSSPortName = auxGNSSPortName;
                result.AUXGNSSPortBaudrate = auxGNSSPortBaudrate;
                result.SalinityPSU = salinityPSU;
                result.IsSoundSpeedAuto = isAutoSoundSpeed;
                result.SoundSpeedMPS = soundSpeedMps;
                result.TrackPointsToShow = trackFIFOSize;
                result.RadialErrorThrehsold = rerrThreshold;
                result.IsUseBuoyAsAUXGNSS = isBuoyAsAuxGNSS;
                result.AuxGNSSBuoyID = auxGnssBuoyID;
                return result;                
            }
            set
            {
                inportName = value.InPortName;
                inportBaudrate = value.InPortBaudrate;
                isUseOutPort = value.IsUseOutPort;
                outportName = value.OutPortName;
                outportBaudrate = value.OutPortBaudrate;
                isUseAUXGNSSPort = value.IsUseAUXGNSSPort;
                auxGNSSPortName = value.AUXGNSSPortName;
                auxGNSSPortBaudrate = value.AUXGNSSPortBaudrate;
                salinityPSU = value.SalinityPSU;
                isAutoSoundSpeed = value.IsSoundSpeedAuto;
                soundSpeedMps = value.SoundSpeedMPS;
                trackFIFOSize = value.TrackPointsToShow;
                rerrThreshold = value.RadialErrorThrehsold;
                isBuoyAsAuxGNSS = value.IsUseBuoyAsAUXGNSS;
                auxGnssBuoyID = value.AuxGNSSBuoyID;
            }
        }

        #endregion

        #region Constructor

        public SettingsEditor()
        {
            InitializeComponent();

            var portNames = SerialPort.GetPortNames();
            if (portNames.Length > 0)
            {
                inportNameCbx.Items.AddRange(portNames);
                outportNameCbx.Items.AddRange(portNames);
                auxGNSSPortNameCbx.Items.AddRange(portNames);

                inportNameCbx.SelectedIndex = 0;
                outportNameCbx.SelectedIndex = 0;
                auxGNSSPortNameCbx.SelectedIndex = 0;

                var baudRates = Enum.GetNames(typeof(UCNLDrivers.BaudRate));
                inportBaudrateCbx.Items.AddRange(baudRates);
                outportBaudrateCbx.Items.AddRange(baudRates);
                auxGNSSPortBaudrateCbx.Items.AddRange(baudRates);

                inportBaudrate = UCNLDrivers.BaudRate.baudRate9600;
                outportBaudrate = UCNLDrivers.BaudRate.baudRate9600;
                auxGNSSPortBaudrate = UCNLDrivers.BaudRate.baudRate9600;

                auxGnssBuoyIDCbx.Items.AddRange(new string[] { BaseIDs.BASE_1.ToString(), BaseIDs.BASE_2.ToString(), BaseIDs.BASE_3.ToString(), BaseIDs.BASE_4.ToString() });
                auxGnssBuoyID = BaseIDs.BASE_1;
            }
        }

        #endregion

        #region Methods

        private void CheckValidity()
        {
            okBtn.Enabled = !string.IsNullOrEmpty(inportName) &&
                ((inportName != outportName) || !isUseOutPort) &&
                ((inportName != auxGNSSPortName) || !isUseAUXGNSSPort) &&
                ((outportName != auxGNSSPortName) || (!isUseOutPort || !isUseAUXGNSSPort));
        }

        #endregion

        #region Handlers

        private void isUseOutputPortChb_CheckedChanged(object sender, EventArgs e)
        {
            outputPortGroup.Enabled = isUseOutPort;
            CheckValidity();
        }

        private void inportNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValidity();
        }

        private void outportNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValidity();
        }

        private void isUseAUXGNSSPortChb_CheckedChanged(object sender, EventArgs e)
        {
            auxGNSSPortGroup.Enabled = isUseAUXGNSSPort;
            
            if (isUseAUXGNSSPort)
            {
                isBuoyAsAuxGNSS = false;
            }

            CheckValidity();
        }

        private void auxGNSSPortNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValidity();
        }        

        private void isAutoSoundSpeedChb_CheckedChanged(object sender, EventArgs e)
        {
            soundSpeedEdit.Enabled = !isAutoSoundSpeed;
            soundSpeedEditTitleLbl.Enabled = !isAutoSoundSpeed;
        }

        private void styDialogLnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SalinityDialog sDialog = new SalinityDialog())
            {
                sDialog.Text = "RWLT Host - Salinity";
                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    salinityPSU = sDialog.Salinity;
                }
            }
        }

        private void isBuoyAsAuxGNSSChb_CheckedChanged(object sender, EventArgs e)
        {
            auxGnssBuoysGroup.Enabled = isBuoyAsAuxGNSSChb.Checked;
            if (isBuoyAsAuxGNSS)
            {
                isUseAUXGNSSPortChb.Checked = false;
            }
        }
        
        #endregion        
    }
}
