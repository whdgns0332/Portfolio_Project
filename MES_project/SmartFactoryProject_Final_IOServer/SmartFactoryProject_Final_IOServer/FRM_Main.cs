using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = unvell.ReoGrid;

namespace SmartFactoryProject_Final_IOServer
{
    public partial class FRM_Main : Form
    {
        const int MaxDataRow = 200;
        enum DataMode { PLC, AUTO }
        DataMode mode = DataMode.AUTO;
        bool running = false;

        string plcCode;
        Dictionary<string, QualityData> tempDic = new Dictionary<string, QualityData>();        // '기기코드 품질코드' - 해당 코드들에 대응되는 Config 요소의 List (각각 온도, 생산량을 저장한다)
        Dictionary<string, QualityData> amountDic = new Dictionary<string, QualityData>();

        public FRM_Main()
        {
            InitializeComponent();
        }

        #region ---- Initialize
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeLayout();
            Initialize();
            InitializeList();
            InitializeGridView();
            InitializeOPC();
            InitializeExcel();
        }

        private void Initialize()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection infoSec = ini["Info"];

            ModeSelect(mode);
            Tim_TimeCheck.Interval = 100;
            Tim_TimeCheck.Enabled = true;
            SetRealTime(GetRealTime());

            Tim_Temp.Enabled = true;
            Tim_Temp.Interval = 1000;
            Txt_TempItv.Text = "1";
            Tim_Amount.Enabled = true;
            Tim_Amount.Interval = 1000;
            Txt_AmoItv.Text = "1";

            plcCode = infoSec["PLCCode"].ToString();
            Lbl_IPVal.Text = infoSec["PLCIP"].ToString();
            Lbl_PortVal.Text = infoSec["PLCPort"].ToString();
            
            DivTime_Base = DateTime.Now;

            Excel_Data.Visible = false;
        }

        private void InitializeList()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            DBConnect dbConnect = new DBConnect();

            string plcCode = ini["Info"]["PLCCode"].ToString();
            DataSet configs = dbConnect.ExecuteProcedure("SPIO_GET_CONFIGS", new SqlDbType[] { SqlDbType.VarChar },
                                                                               new object[] { plcCode });
            if (configs == null || configs.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow row in configs.Tables[0].Rows)
            {
                if (row[1].ToString() != "PQ")
                {
                    tempDic[$"{row[0].ToString()} {row[1].ToString()}"] = new QualityData(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                                                                                          double.Parse(row[4].ToString()), double.Parse(row[5].ToString()));
                }
                else
                {
                    amountDic[$"{row[0].ToString()} {row[1].ToString()}"] = new QualityData(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                                                                                            double.Parse(row[4].ToString()), double.Parse(row[5].ToString()));
                }
            }
        }

        private void InitializeGridView()
        {
            Dgv_Data.ColumnCount = 5;
            Dgv_Data.Columns[0].Name = "Date";
            Dgv_Data.Columns[0].HeaderText = "시간";
            Dgv_Data.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Data.Columns[0].Width = 125;
            Dgv_Data.Columns[1].Name = "MachCode";
            Dgv_Data.Columns[1].HeaderText = "기기 코드";
            Dgv_Data.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Data.Columns[1].Width = 80;
            Dgv_Data.Columns[2].Name = "Temp1";
            Dgv_Data.Columns[2].HeaderText = "내경 온도";
            Dgv_Data.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Data.Columns[2].Width = 125;
            Dgv_Data.Columns[3].Name = "Temp2";
            Dgv_Data.Columns[3].HeaderText = "외경 온도";
            Dgv_Data.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Data.Columns[3].Width = 125;
            Dgv_Data.Columns[4].Name = "Amount";
            Dgv_Data.Columns[4].HeaderText = "생산량";
            Dgv_Data.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Data.Columns[4].Width = 80;

            Dgv_Data.RowHeadersWidth = 5;
            Dgv_Data.RowTemplate.Height = TextRenderer.MeasureText("AA", Dgv_Data.DefaultCellStyle.Font).Height + 6;
        }

        private void InitializeOPC()
        {
            _opcServer = new OPCServer();
        }

        private void InitializeExcel()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection dataSect = ini["Data"];
            string dataPath = Directory.GetCurrentDirectory() + $@"{dataSect["DataFolder"]}";
            string directoryPath = dataPath + $@"\{DivTime_Base.Year}-{DivTime_Base.Month}";
            string filePath = directoryPath + $@"\{DivTime_Base.Day}일 {DivTime_Base.Hour}시.xlsx";

            if (File.Exists(filePath))
            {
                Excel_Data.Load(filePath);
                Excel.Worksheet worksheet = Excel_Data.CurrentWorksheet;
                worksheet.SetCols(5);
                
                for (int i = 1; i < worksheet.RowCount; i++)
                {
                    if (worksheet[i, 0] == null)
                    {
                        rowIndex = i - 1;
                        break;
                    }
                }
            }
            else
            {
                InitializeExcelData();
            }
        }
        
        private void InitializeExcelData()
        {
            Excel.Worksheet worksheet = Excel_Data.CurrentWorksheet;
            worksheet.SetCols(5);
            worksheet[0, 0] = "시간";
            worksheet.SetColumnsWidth(0, 1, 150);
            worksheet.Cells[new Excel.CellPosition(0, 0)].Style.HAlign = Excel.ReoGridHorAlign.Center;
            worksheet[0, 1] = "기기 코드";
            worksheet.SetColumnsWidth(1, 1, 70);
            worksheet.Cells[new Excel.CellPosition(0, 1)].Style.HAlign = Excel.ReoGridHorAlign.Center;
            worksheet[0, 2] = "내경 온도";
            worksheet.SetColumnsWidth(2, 1, 120);
            worksheet.Cells[new Excel.CellPosition(0, 2)].Style.HAlign = Excel.ReoGridHorAlign.Center;
            worksheet[0, 3] = "외경 온도";
            worksheet.SetColumnsWidth(3, 1, 120);
            worksheet.Cells[new Excel.CellPosition(0, 3)].Style.HAlign = Excel.ReoGridHorAlign.Center;
            worksheet[0, 4] = "생산량";
            worksheet.SetColumnsWidth(4, 1, 60);
            worksheet.Cells[new Excel.CellPosition(0, 4)].Style.HAlign = Excel.ReoGridHorAlign.Center;

            rowIndex = 0;
        }
        #endregion

        #region ---- Layout
        private void InitializeLayout()
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            ctrlLayout.SetBackgroundImage(this, overallResPath + @"\bg_original2.gif");

            ctrlLayout.Control_Sizing(Lbl_Time, this.Size, 0.155, 0.025);
            ctrlLayout.Control_Positioning(Lbl_Time, this.ClientSize, 0.01, 0.03);
            Lbl_Time.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_Time.Height, 0.9);
            Lbl_Time.ForeColor = Color.White;

            ctrlLayout.Control_Sizing(Lbl_IPName, this.Size, 0.05, 0.025);
            ctrlLayout.Control_Positioning(Lbl_IPName, this.Size, 0.86, 0.01, ControlLayout.HorizontalSiding.Right);
            Lbl_IPName.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_IPName.Height, 1);
            ctrlLayout.Control_Sizing(Lbl_IPVal, this.Size, 0.12, 0.025);
            ctrlLayout.Control_Positioning(Lbl_IPVal, this.Size, 0.86, 0.01);
            Lbl_IPVal.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_IPVal.Height, 0.9);

            ctrlLayout.Control_Sizing(Lbl_PortName, this.Size, 0.05, 0.025);
            ctrlLayout.Control_Positioning(Lbl_PortName, this.Size, 0.86, 0.035, ControlLayout.HorizontalSiding.Right);
            Lbl_PortName.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_PortName.Height, 1);
            ctrlLayout.Control_Sizing(Lbl_PortVal, this.Size, 0.12, 0.025);
            ctrlLayout.Control_Positioning(Lbl_PortVal, this.Size, 0.86, 0.035);
            Lbl_PortVal.Font = ctrlLayout.GetProperFontSize("Tahoma", Lbl_PortVal.Height, 0.9);

            Lbl_Connect.AutoSize = false;
            ctrlLayout.Control_Sizing(Lbl_Connect, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Lbl_Connect, this.ClientSize, 0.01, 0.915);
            ctrlLayout.MakeCurvedBorder(Lbl_Connect, 10, 10);
            Lbl_Connect.TextAlign = ContentAlignment.MiddleCenter;

            ctrlLayout.Control_Sizing(Btn_PLCMode, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_PLCMode, this.ClientSize, 0.125, 0.915);
            Btn_Mode_SetImage(Btn_PLCMode, mode == DataMode.PLC, false);
            Btn_PLCMode.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_PLCMode.Height, 0.5, true);

            ctrlLayout.Control_Sizing(Btn_AutoMode, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_AutoMode, this.ClientSize, 0.24, 0.915);
            Btn_Mode_SetImage(Btn_AutoMode, mode == DataMode.AUTO, false);
            Btn_AutoMode.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_AutoMode.Height, 0.5, true);

            ctrlLayout.Control_Sizing(Btn_Start, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_Start, this.ClientSize, 0.78, 0.915);
            Btn_Start_SetImage(false);
            Btn_Start.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_Start.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_Stop, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_Stop, this.ClientSize, 0.89, 0.915);
            Btn_Stop_SetImage(false);
            Btn_Stop.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_Stop.Height, 0.6, true);
            
            ctrlLayout.Control_Sizing(Dgv_Data, this.ClientSize, 0.9, 0.75);
            ctrlLayout.Control_Positioning(Dgv_Data, this.ClientSize, 0.05, 0.1);
            
            ctrlLayout.Control_Sizing(Lbl_Temp_Itv, this.Size, 0.11, 0.032);
            ctrlLayout.Control_Positioning(Lbl_Temp_Itv, this.ClientSize, 0.525, 0.936, ControlLayout.HorizontalSiding.Right, ControlLayout.VerticalSiding.Center);
            Lbl_Temp_Itv.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Lbl_Temp_Itv.Height, 0.7);
            ctrlLayout.Control_Sizing(Lbl_Amo_Itv, this.Size, 0.11, 0.032);
            ctrlLayout.Control_Positioning(Lbl_Amo_Itv, this.ClientSize, 0.525, 0.974, ControlLayout.HorizontalSiding.Right, ControlLayout.VerticalSiding.Center);
            Lbl_Amo_Itv.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Lbl_Amo_Itv.Height, 0.7);
            ctrlLayout.Control_Sizing(Lbl_Temp_Unit, this.Size, 0.02, 0.032);
            ctrlLayout.Control_Positioning(Lbl_Temp_Unit, this.ClientSize, 0.615, 0.936, ControlLayout.HorizontalSiding.Left, ControlLayout.VerticalSiding.Center);
            Lbl_Temp_Unit.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Lbl_Temp_Unit.Height, 0.7);
            ctrlLayout.Control_Sizing(Lbl_Amo_Unit, this.Size, 0.02, 0.032);
            ctrlLayout.Control_Positioning(Lbl_Amo_Unit, this.ClientSize, 0.615, 0.974, ControlLayout.HorizontalSiding.Left, ControlLayout.VerticalSiding.Center);
            Lbl_Amo_Unit.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Lbl_Amo_Unit.Height, 0.7);

            Txt_TempItv.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_TempItv, this.Size, 0.08, 0.032);
            ctrlLayout.Control_Positioning(Txt_TempItv, this.ClientSize, 0.53, 0.936, ControlLayout.HorizontalSiding.Left, ControlLayout.VerticalSiding.Center);
            Txt_AmoItv.DefaultSetting();
            ctrlLayout.Control_Sizing(Txt_AmoItv, this.Size, 0.08, 0.032);
            ctrlLayout.Control_Positioning(Txt_AmoItv, this.ClientSize, 0.53, 0.974, ControlLayout.HorizontalSiding.Left, ControlLayout.VerticalSiding.Center);
            ctrlLayout.Control_Sizing(Btn_ItvChange, this.Size, 0.11, 0.075);
            ctrlLayout.Control_Positioning(Btn_ItvChange, this.ClientSize, 0.64, 0.915);
            Btn_ItvChange_SetImage(false);
            Btn_ItvChange.Font = ctrlLayout.GetProperFontSize("맑은 고딕", Btn_ItvChange.Height, 0.4, true);
            Btn_ItvChange.ForeColor = Color.White;
            
        }

        private void Btn_Start_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_Start, overallResPath + @"\btn_Bluebase_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_Start, overallResPath + @"\btn_Bluebase.gif");
        }

        private void Btn_Stop_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_Stop, overallResPath + @"\btn_Redbase_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_Stop, overallResPath + @"\btn_Redbase.gif");
        }

        private void Btn_Mode_SetImage(Button btn, bool activated, bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (activated)
            {
                if (pressed)
                    ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_Greenbase_.gif");
                else
                    ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_Greenbase.gif");
            }
            else
            {
                if (pressed)
                    ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_Graybase_.gif");
                else
                    ctrlLayout.SetBackgroundImage(btn, overallResPath + @"\btn_Graybase.gif");
            }
        }

        private void Btn_ItvChange_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

            if (pressed)
                ctrlLayout.SetBackgroundImage(Btn_ItvChange, overallResPath + @"\btn_Graybase_.gif");
            else
                ctrlLayout.SetBackgroundImage(Btn_ItvChange, overallResPath + @"\btn_Graybase.gif");
        }
        #endregion
        #region ---- Button - Mouse Up&Down
        private void Btn_PLCMode_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Mode_SetImage(Btn_PLCMode, mode == DataMode.PLC, true);
        }

        private void Btn_PLCMode_MouseUp(object sender, MouseEventArgs e)
        {
            if (!running)
                ModeSelect(DataMode.PLC);
            Btn_Mode_SetImage(Btn_PLCMode, mode == DataMode.PLC, false);
            Btn_Mode_SetImage(Btn_AutoMode, mode == DataMode.AUTO, false);
        }

        private void Btn_AutoMode_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Mode_SetImage(Btn_AutoMode, mode == DataMode.AUTO, true);
        }

        private void Btn_AutoMode_MouseUp(object sender, MouseEventArgs e)
        {
            if (!running)
                ModeSelect(DataMode.AUTO);
            Btn_Mode_SetImage(Btn_PLCMode, mode == DataMode.PLC, false);
            Btn_Mode_SetImage(Btn_AutoMode, mode == DataMode.AUTO, false);
        }

        private void Btn_Start_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Start_SetImage(true);
        }

        private void Btn_Start_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Start_SetImage(false);
        }

        private void Btn_Stop_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Stop_SetImage(true);
        }

        private void Btn_Stop_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Stop_SetImage(false);
        }

        private void Btn_ItvChange_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_ItvChange_SetImage(true);
        }

        private void Btn_ItvChange_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_ItvChange_SetImage(false);
        }
        #endregion

        #region ---- Button - Click
        private void ModeSelect(DataMode selectMode)
        {
            mode = selectMode;
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            if (running)
                return;

            running = true;

            if (mode == DataMode.PLC)
            {
                Task task = new Task(new Action(ConnectPLC));
                task.Start();
                SetConnectState(PLCState.Connecting);
            }
            else
            {
                SetConnectState(PLCState.Running);
            }
        }

        private void Stop()
        {
            if (!running)
                return;

            if(mode == DataMode.PLC)
            {
                DisconnectPLC();
            }

            running = false;
            SetConnectState(PLCState.Stopped);
        }

        private void Btn_ItvChange_Click(object sender, EventArgs e)
        {
            double interval;
            if (double.TryParse(Txt_TempItv.Text, out interval) && interval >= 0.1)
                Tim_Temp.Interval = (int)(interval * 1000);
            if (double.TryParse(Txt_AmoItv.Text, out interval) && interval >= 0.1)
                Tim_Amount.Interval = (int)(interval * 1000);
        }
        #endregion

        #region ---- Timer
        private void Tim_TimeCheck_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = GetRealTime();
            if (currentTime.Hour != DivTime_Base.Hour)  // 1시간이 지났을 경우 엑셀 파일을 저장하고 새 엑셀을 구성한다
            {
                ResetExcel();
                DivTime_Base = currentTime;
            }
            SetRealTime(currentTime);
        }

        private DateTime GetRealTime()
        {
            if (CanGetTimeFromServer())  /// 서버에서 시간을 가져올 수 있는지를 확인하는 곳
            {
                DateTime serverTime = DateTime.Now;
                return serverTime;
            }
            else
            {
                return DateTime.Now;
            }
        }

        private bool CanGetTimeFromServer()
        {
            return false;
        }

        private void SetRealTime(DateTime time)
        {
            Lbl_Time.Text = time.ToString("yyyy-MM-dd HH:mm:ss.f");
        }

        private void Tim_Temp_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                if (mode == DataMode.PLC)
                    OperatePLCMode(DataType.Temp);
                else
                    OperateRandMode(DataType.Temp);
            }
        }

        private void Tim_Amount_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                if (mode == DataMode.PLC)
                    OperatePLCMode(DataType.Amount);
                else
                    OperateRandMode(DataType.Amount);
            }
        }
        #endregion

        #region ---- Data - Common
        enum DataType { Temp, Amount }

        private bool IsRepData(string time)
        {
            return true;
            /*
            //if (time.Substring(17, 2) == "00")       // 1분에 1회
            if (time.Substring(18, 1) == "0")          // 10초에 1회
                return true;
            else
                return false; 
                */
        }
        #endregion
        #region ---- PLCMode
        const string TargetServer = "Takebishi.Dxp.6";
        OPCServer _opcServer;

        Dictionary<string, OPCGroup> opcGroupDic = new Dictionary<string, OPCGroup>();
        Dictionary<string, OPCItem> opcItemDic = new Dictionary<string, OPCItem>();
        bool connected = false;

        enum PLCState { Stopped, Running, Connecting, ServerNotFound, Error }

        private void ConnectPLC()
        {
            string hostName = System.Net.Dns.GetHostName();
            _opcServer = new OPCServer();
            Array serverList = _opcServer.GetOPCServers(hostName) as Array;

            bool serverFound = false;
            for (int i = 1; i <= serverList.GetLength(0); i++)
            {
                string serverName = serverList.GetValue(i) as string;
                if (serverName == TargetServer)
                {
                    serverFound = true;
                    _opcServer.Connect(serverName, hostName);
                    break;
                }
            }

            if (!serverFound)
            {
                SetConnectState(PLCState.ServerNotFound);
                return;
            }
            else if (_opcServer.ServerState != 1)
            {
                SetConnectState(PLCState.Error);
                return;
            }

            _opcServer.OPCGroups.DefaultGroupDeadband = 0;
            _opcServer.OPCGroups.DefaultGroupIsActive = true;
            _opcServer.OPCGroups.DefaultGroupUpdateRate = 1000;

            InitializePLCItems();

            SetConnectState(PLCState.Running);
            connected = true;
        }

        private void InitializePLCItems()
        {
            foreach (QualityData data in tempDic.Values)
            {
                OPCGroup _opcGroup;
                if (!opcGroupDic.ContainsKey(data.MachCode))
                {
                    _opcGroup = _opcServer.OPCGroups.Add(data.MachCode);    // Groups 내에 해당하는 Name을 가진 Group이 이미 있다면 COMException이 발생함
                    _opcGroup.DeadBand = 0;
                    _opcGroup.IsActive = true;
                    _opcGroup.IsSubscribed = true;
                    opcGroupDic[data.MachCode] = _opcGroup;
                }
                else
                {
                    _opcGroup = opcGroupDic[data.MachCode];
                }

                OPCItem _opcItem;
                if (!opcItemDic.ContainsKey(data.TagAddr))
                {
                    try
                    {
                        _opcItem = _opcGroup.OPCItems.AddItem(data.TagAddr, 1); // Takebishi 프로그램에서 해당하는 명칭의 태그가 존재하지 않으면 COMException이 발생함
                        opcItemDic[data.TagAddr] = _opcItem;
                    }
                    catch
                    {
                        DisconnectPLC();
                        SetConnectState(PLCState.Error);
                        return;
                    }
                }
            }

            foreach (QualityData data in amountDic.Values)
            {
                OPCGroup _opcGroup;
                if (!opcGroupDic.ContainsKey(data.MachCode))
                {
                    _opcGroup = _opcServer.OPCGroups.Add(data.MachCode);    // Groups 내에 해당하는 Name을 가진 Group이 이미 있다면 COMException이 발생함
                    _opcGroup.DeadBand = 0;
                    _opcGroup.IsActive = true;
                    _opcGroup.IsSubscribed = true;
                    opcGroupDic[data.MachCode] = _opcGroup;
                }
                else
                {
                    _opcGroup = opcGroupDic[data.MachCode];
                }

                OPCItem _opcItem;
                if (!opcItemDic.ContainsKey(data.TagAddr))
                {
                    try
                    {
                        _opcItem = _opcGroup.OPCItems.AddItem(data.TagAddr, 1); // Takebishi 프로그램에서 해당하는 명칭의 태그가 존재하지 않으면 COMException이 발생함
                        opcItemDic[data.TagAddr] = _opcItem;
                    }                                                       
                    catch
                    {
                        DisconnectPLC();
                        SetConnectState(PLCState.Error);
                        return;
                    }
                }
            }
        }

        private void DisconnectPLC()
        {
            opcItemDic.Clear();
            opcGroupDic.Clear();
            _opcServer.Disconnect();
            connected = false;
            running = false;
        }

        delegate void ConnectStateDelegate(PLCState state);
        private void SetConnectState(PLCState state)
        {
            if (Lbl_Connect.InvokeRequired)         // OPCServer에 대한 연결에 소모되는 시간이 길어 Task를 통한 비동기를 진행하는데 이 경우 Lbl_Connect 등의 UI 요소에 접근시 에러가 발생함
            {                                       // InvokeRequired를 통해 필요 여부를 확인한 뒤 Invoke를 통해 에러 없이 원래 스레드에 작업을 요청한다
                ConnectStateDelegate del = new ConnectStateDelegate(SetConnectState);
                Invoke(del, state);
            }
            switch (state)
            {
                case PLCState.Stopped:
                    Lbl_Connect.Text = "Stopped";
                    Lbl_Connect.ForeColor = Color.Black;
                    Lbl_Connect.BackColor = SystemColors.Control;
                    break;
                case PLCState.Running:
                    Lbl_Connect.Text = "Running";
                    Lbl_Connect.ForeColor = Color.Black;
                    Lbl_Connect.BackColor = Color.LightGreen;
                    break;
                case PLCState.Connecting:
                    Lbl_Connect.Text = "Connecting";
                    Lbl_Connect.ForeColor = Color.Black;
                    Lbl_Connect.BackColor = Color.Yellow;
                    break;
                case PLCState.ServerNotFound:
                    Lbl_Connect.Text = "Server Not Found";
                    Lbl_Connect.ForeColor = Color.Black;
                    Lbl_Connect.BackColor = Color.Salmon;
                    break;
                case PLCState.Error:
                    Lbl_Connect.Text = "Error";
                    Lbl_Connect.ForeColor = Color.Black;
                    Lbl_Connect.BackColor = Color.Salmon;
                    break;
            }
        }

        private void OperatePLCMode(DataType type)
        {
            if (!connected)
                return;

            DBConnect dbConnect = new DBConnect();
            DataSet confData = dbConnect.ExecuteProcedure("SPIO_GET_CONFIGS", new SqlDbType[] { SqlDbType.VarChar },
                                                                                 new object[] { plcCode });
            if (confData == null || confData.Tables[0].Rows.Count == 0)
                return;

            List<SqlCommand> commandList = new List<SqlCommand>();
            for (int i = 0; i < confData.Tables[0].Rows.Count; i++)
            {
                DataRow row = confData.Tables[0].Rows[i];
                QualityData data = null;
                if (type == DataType.Temp && row[1].ToString().Substring(0, 2) == "TP")
                    data = tempDic[$"{row[0].ToString()} {row[1].ToString()}"];
                else if (type == DataType.Amount && row[1].ToString().Substring(0, 2) == "PQ")
                    data = amountDic[$"{row[0].ToString()} {row[1].ToString()}"];
                else
                    continue;
                string flag = row[6].ToString();

                SqlCommand cmd = ProcessPLCData(data, flag);
                if (cmd != null)
                {
                    commandList.Add(cmd);

                    if (flag == "R")
                    {
                        AddDataInGridView(Lbl_Time.Text, data.MachCode, data.QualCode, data.Value.ToString());
                        WriteLog(Lbl_Time.Text, data);
                    }
                }
            }
            dbConnect.TryTransaction(commandList);
        }

        private SqlCommand ProcessPLCData(QualityData data, string flag)
        {
            DBConnect dbConnect = new DBConnect();
            OPCItem item = opcItemDic[data.TagAddr];

            if (flag == "R")
            {
                try
                {
                    object value = null;
                    object quality = null;
                    object timestamp = null;

                    item.Read(1, out value, out quality, out timestamp);
                    data.Value = double.Parse(value.ToString());
                }
                catch (COMException excep)
                {
                    string className = nameof(FRM_Main);
                    string funcName = nameof(OperatePLCMode);
                    string logText = string.Concat(excep.Message.ToString());
                    Log.WriteLog(Log.LogType.Error, className, funcName, logText);

                    DisconnectPLC();
                    SetConnectState(PLCState.Error);
                    return null;
                }

                bool represent = IsRepData(Lbl_Time.Text);
                SqlCommand cmd = dbConnect.GetSqlCommandForProd("SPIO_SET_Q_CONTROL",
                                                                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,     SqlDbType.VarChar,
                                                                                  SqlDbType.Bit, SqlDbType.NVarChar, SqlDbType.VarChar },
                                                                   new object[] { data.MachCode,     data.QualCode,     Lbl_Time.Text,     data.Value.ToString(), Lbl_Time.Text.Substring(11),
                                                                                  represent,     null,               null});
                return cmd;
            }
            else
            {
                try
                {
                    item.Write(0);
                }
                catch (COMException excep)
                {
                    string className = nameof(FRM_Main);
                    string funcName = nameof(OperatePLCMode);
                    string logText = string.Concat(excep.Message.ToString());
                    Log.WriteLog(Log.LogType.Error, className, funcName, logText);

                    DisconnectPLC();
                    SetConnectState(PLCState.Error);
                }
                return null;
            }
        }
        #endregion
        #region ---- RandMode
        int seed = (int)(DateTime.Now.Ticks << 32 >> 32);

        private void OperateRandMode(DataType type)
        {
            DBConnect dbConnect = new DBConnect();
            DataSet confData = dbConnect.ExecuteProcedure("SPIO_GET_CONFIGS", new SqlDbType[] { SqlDbType.VarChar },
                                                                                 new object[] { plcCode });
            if (confData == null || confData.Tables[0].Rows.Count == 0)
                return;

            List<SqlCommand> commandList = new List<SqlCommand>();
            for (int i = 0; i < confData.Tables[0].Rows.Count; i++)
            {
                DataRow row = confData.Tables[0].Rows[i];
                QualityData data = null;
                if (type == DataType.Temp && row[1].ToString().Substring(0, 2) == "TP")
                    data = tempDic[$"{row[0].ToString()} {row[1].ToString()}"];
                else if (type == DataType.Amount && row[1].ToString().Substring(0, 2) == "PQ")
                    data = amountDic[$"{row[0].ToString()} {row[1].ToString()}"];
                else
                    continue;
                string flag = row[6].ToString();

                SqlCommand cmd = ProcessRandData(data, flag);
                if (cmd != null)
                {
                    commandList.Add(cmd);

                    if (flag == "R")
                    {
                        AddDataInGridView(Lbl_Time.Text, data.MachCode, data.QualCode, data.Value.ToString());
                        WriteLog(Lbl_Time.Text, data);
                    }
                }
            }
            dbConnect.TryTransaction(commandList);
        }

        private SqlCommand ProcessRandData(QualityData data, string flag)
        {
            DBConnect dbConnect = new DBConnect();
            if (flag == "R")
            {
                GenerateRandValue(data);
                bool represent = IsRepData(Lbl_Time.Text);

                SqlCommand cmd = dbConnect.GetSqlCommandForProd("SPIO_SET_Q_CONTROL",
                                                                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,     SqlDbType.VarChar,
                                                                                  SqlDbType.Bit, SqlDbType.NVarChar, SqlDbType.VarChar },
                                                                   new object[] { data.MachCode,     data.QualCode,     Lbl_Time.Text,     data.Value.ToString(), Lbl_Time.Text.Substring(11),
                                                                                  represent,     null,               null});
                return cmd;
            }
            else
            {
                data.Value = 0;
                return null;
            }
        }

        private void GenerateRandValue(QualityData data)
        {
            Random rand = new Random(seed);
            seed += rand.Next(10, 110);         // 시드가 항상 고정된 수치만큼 변동된다면 랜덤의 결과값이 규칙성을 띈다는 문제점이 있어 시드를 무작위로 증가하게 하여 이를 해결한다
            double randMin = data.Minimum - (data.Maximum - data.Minimum) * 0.1;
            double randWidth = (data.Maximum - data.Minimum) * 1.2;

            if (data.QualCode != "PQ")
                data.Value = (rand.NextDouble() * randWidth) + randMin;
            else
                data.Value = (rand.NextDouble() > 0.9) ? data.Value + 1 : data.Value;
        }
        #endregion
        #region -------- DataGridView
        /*  Datagridview.AllowUserToAddRows가 true라면 Datagridview의 맨 아래 Row에 사용자가 데이터를 입력하기 위한 빈 Row가 생성된다
         *      이 Row는 삭제가 불가능(삭제 시도시 Exception이 발생)하고 비어있지만 실제로 존재하는 Row로서 취급(이 Row가 존재하는 한 Datagridview.Count는 0이 되지 못한다)되므로
         *          사용자의 입력을 필요로 하지 않는 본 프로그램에 있어서는 장애물에 지나지 않음
         *      따라서 해당 속성을 False로 해 빈 Row를 생성하지 않게 하고 그 외에 AllowUser계열의 모든 속성을 False로 하였음
         */

        private void AddDataInGridView(string Date, string MachCode, string QualCode, string value)
        {
            int rowIndex = HasDuplicate(Date, MachCode);
            int colIndex = 0;
            switch (QualCode)
            {
                case "TP1":
                    colIndex = 2;
                    break;
                case "TP2":
                    colIndex = 3;
                    break;
                case "PQ":
                    colIndex = 4;
                    break;
                default:
                    return;
            }

            if (rowIndex < 0)
            {
                if (Dgv_Data.Rows.Count >= MaxDataRow)
                    Dgv_Data.Rows.RemoveAt(Dgv_Data.Rows.Count - 1);
                Dgv_Data.Rows.Insert(0, Date, MachCode, "", "", "");
                rowIndex = 0;
            }
            Dgv_Data.Rows[rowIndex].Cells[colIndex].Value = value;
        }

        private int HasDuplicate(string date, string machCode)
        {
            if (Dgv_Data.Rows.Count <= 0)
                return -1;

            for(int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                DataGridViewRow row = Dgv_Data.Rows[i];
                if (string.Compare(date, row.Cells[0].Value.ToString()) > 0)
                    return -1;
                else if (date == row.Cells[0].Value.ToString() && machCode == row.Cells[1].Value.ToString())
                    return i;
            }

            return -1;
        }
        #endregion
        #region -------- FileLog
        DateTime DivTime_Base;       // 1시간마다 사용하는 엑셀 파일을 다르게 하기 위한 시간 기록 변수
        int rowIndex = 0;

        private void ResetExcel()
        {
            SaveExcel();
            Excel_Data.CurrentWorksheet.Reset();
            InitializeExcelData();
        }

        private void SaveExcel()
        {
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection dataSect = ini["Data"];
            string dataPath = Directory.GetCurrentDirectory() + $@"{dataSect["DataFolder"]}";
            string directoryPath = dataPath + $@"\{DivTime_Base.Year}-{DivTime_Base.Month}";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = directoryPath + $@"\{DivTime_Base.Day}일 {DivTime_Base.Hour}시.xlsx";

            Excel_Data.Save(filePath);
        }

        private void WriteLog(string Date, QualityData data)
        {
            int writeRowInd = HasDuplicate_Excel(Date, data.MachCode);
            int writeColInd = 0;
            switch (data.QualCode)
            {
                case "TP1":
                    writeColInd = 2;
                    break;
                case "TP2":
                    writeColInd = 3;
                    break;
                case "PQ":
                    writeColInd = 4;
                    break;
                default:
                    return;
            }

            Excel.Worksheet worksheet = Excel_Data.CurrentWorksheet;
            if (writeRowInd < 0)
            {
                rowIndex++;
                if (worksheet.RowCount <= rowIndex)
                    worksheet.AppendRows(rowIndex - worksheet.RowCount + 100);
                worksheet[rowIndex, 0] = Date;
                worksheet.Cells[new Excel.CellPosition(rowIndex, 0)].Style.HAlign = Excel.ReoGridHorAlign.Center;
                worksheet[rowIndex, 1] = data.MachCode;
                writeRowInd = rowIndex;
            }

            worksheet[writeRowInd, writeColInd] = data.Value;
            if (data.Value < data.Minimum || data.Value > data.Maximum)
            {
                worksheet.Cells[writeRowInd, writeColInd].Style.BackColor = Excel.Graphics.SolidColor.OrangeRed;
                worksheet.Cells[writeRowInd, writeColInd].Style.TextColor = Excel.Graphics.SolidColor.White;
            }
        }

        private int HasDuplicate_Excel(string Date, string MachCode)
        {
            if (rowIndex <= 0)
                return -1;

            Excel.Worksheet worksheet = Excel_Data.CurrentWorksheet;
            for (int i = rowIndex; i >= 1; i--)
            {
                if (Date == worksheet[i, 0].ToString() && MachCode == worksheet[i, 1].ToString())
                    return i;
                else if (string.Compare(Date, worksheet[i, 0].ToString()) > 0)
                    return -1;
            }
            return -1;
        }
        #endregion

        private void FRM_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
            SaveExcel();
        }
    }
}
