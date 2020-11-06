using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = unvell.ReoGrid;

namespace SmartFactoryProject_Final_IOServer
{
    public partial class FRM_Main : Form
    {
        const int MaxDataRow = 200 + 1; // + 1은 DataGridView에 기본 생성되는 텅 빈 줄 1개에 해당함
        enum DataMode { PLC, AUTO }
        DataMode mode = DataMode.AUTO;
        bool running = false;

        string plcCode;
        Dictionary<string, QualityData> qualDic = new Dictionary<string, QualityData>();        // '기기코드 품질코드' - 해당 코드들에 대응되는 Config 요소의 List

        //CheckDate < CONCAT('2020-11-03', ' 00:00:00') // MSSQL에서의 문자열 비교는 단순 부호로 해결이 가능하다

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
            Tim_TimeCheck.Enabled = true;
            GetRealTime();

            plcCode = infoSec["PLCCode"].ToString();
            Lbl_IPVal.Text = infoSec["PLCIP"].ToString();
            Lbl_PortVal.Text = infoSec["PLCPort"].ToString();
            
            DivTime_Base = DateTime.Now;
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
                qualDic[$"{row[0].ToString()} {row[1].ToString()}"] = new QualityData(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                                                                                      double.Parse(row[4].ToString()), double.Parse(row[5].ToString()));
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
            ctrlLayout.Control_Sizing(Lbl_Connect, this.Size, 0.125, 0.075);
            ctrlLayout.Control_Positioning(Lbl_Connect, this.ClientSize, 0.01, 0.915);
            ctrlLayout.MakeCurvedBorder(Lbl_Connect, 10, 10);
            Lbl_Connect.TextAlign = ContentAlignment.MiddleCenter;

            ctrlLayout.Control_Sizing(Btn_PLCMode, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_PLCMode, this.ClientSize, 0.2, 0.915);
            Btn_Mode_SetImage(Btn_PLCMode, mode == DataMode.PLC, false);
            Btn_PLCMode.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_PLCMode.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_AutoMode, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_AutoMode, this.ClientSize, 0.32, 0.915);
            Btn_Mode_SetImage(Btn_AutoMode, mode == DataMode.AUTO, false);
            Btn_AutoMode.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_AutoMode.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_Start, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_Start, this.ClientSize, 0.76, 0.915);
            Btn_Start_SetImage(false);
            Btn_Start.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Start.Height, 0.6, true);

            ctrlLayout.Control_Sizing(Btn_Stop, this.Size, 0.1, 0.075);
            ctrlLayout.Control_Positioning(Btn_Stop, this.ClientSize, 0.88, 0.915);
            Btn_Stop_SetImage(false);
            Btn_Stop.Font = ctrlLayout.GetProperFontSize("Tahoma", Btn_Stop.Height, 0.6, true);
            
            ctrlLayout.Control_Sizing(Dgv_Data, this.ClientSize, 0.9, 0.75);
            ctrlLayout.Control_Positioning(Dgv_Data, this.ClientSize, 0.05, 0.1);

            Excel_Data.Visible = false;
        }

        private void Btn_Start_SetImage(bool pressed)
        {
            ControlLayout ctrlLayout = new ControlLayout();
            IniFile ini = new IniFile();
            ini.Load(IniData.SettingIniFile);
            IniSection resSect = ini["Resources"];
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

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
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

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
            string overallResPath = System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}{resSect["OverallFolder"]}";

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
            Start();
        }

        private void Btn_Stop_MouseDown(object sender, MouseEventArgs e)
        {
            Btn_Stop_SetImage(true);
        }

        private void Btn_Stop_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_Stop_SetImage(false);
            Stop();
        }
        #endregion

        private void ModeSelect(DataMode selectMode)
        {
            mode = selectMode;
        }

        private void Start()
        {
            if (running)
                return;

            running = true;

            if (mode == DataMode.PLC)
            {
                Task task = new Task(new Action(ConnectPLC));
                //task.Start();
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
            if (running)
            {
                if (mode == DataMode.PLC)
                    OperatePLCMode();
                else
                    OperateRandMode();
            }
        }

        private DateTime GetRealTime()
        {
            if (false)  /// 서버에서 시간을 가져올 수 있는지를 확인하는 곳
            {
                
            }
            else
            {
                return DateTime.Now;
            }
        }

        private void SetRealTime(DateTime time)
        {
            Lbl_Time.Text = time.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region ---- Data - Common
        private bool IsRepData(string time)
        {
            //if (time.Substring(17) == "00")       // 1분에 1회
            if (time.Substring(18) == "0")          // 10초에 1회
                return true;
            else
                return false; 
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
            foreach (QualityData data in qualDic.Values)
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

        private void OperatePLCMode()
        {
            if (!connected)
                return;

            DBConnect dbConnect = new DBConnect();
            DataSet confData = dbConnect.ExecuteProcedure("SPIO_GET_CONFIGS", new SqlDbType[] { SqlDbType.VarChar },
                                                                                 new object[] { plcCode });
            if (confData == null || confData.Tables[0].Rows.Count == 0)
                return;

            SqlCommand[] commandList = new SqlCommand[confData.Tables[0].Rows.Count];
            for (int i = 0; i < confData.Tables[0].Rows.Count; i++)
            {
                DataRow row = confData.Tables[0].Rows[i];
                QualityData data = qualDic[$"{row[0].ToString()} {row[1].ToString()}"];
                OPCItem item = opcItemDic[data.TagAddr];
                string flag = row[6].ToString();

                if (flag == "R")
                {
                    object value = null;
                    object quality = null;
                    object timestamp = null;

                    item.Read(1, out value, out quality, out timestamp);
                    data.Value = double.Parse(value.ToString());
                    bool represent = IsRepData(Lbl_Time.Text);
                    commandList[i] = dbConnect.GetSqlCommandForProd("SPIO_SET_Q_CONTROL",
                                                                    new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,     SqlDbType.VarChar,
                                                                                  SqlDbType.Bit, SqlDbType.NVarChar, SqlDbType.VarChar },
                                                                       new object[] { data.MachCode,     data.QualCode,     Lbl_Time.Text,     data.Value.ToString(), Lbl_Time.Text.Substring(11),
                                                                                  represent,     null,               null});

                    AddDataInGridView(Lbl_Time.Text, data.MachCode, data.QualCode, data.Value.ToString());
                    WriteLog(Lbl_Time.Text, data);
                }
                else
                {
                    item.Write(0);
                }
            }
            dbConnect.TryTransaction(commandList);
        }
        #endregion
        #region ---- RandMode
        private void OperateRandMode()
        {
            Random rand = new Random();
            DBConnect dbConnect = new DBConnect();
            DataSet confData = dbConnect.ExecuteProcedure("SPIO_GET_CONFIGS", new SqlDbType[] { SqlDbType.VarChar },
                                                                                 new object[] { plcCode });
            if (confData == null || confData.Tables[0].Rows.Count == 0)
                return;

            SqlCommand[] commandList = new SqlCommand[confData.Tables[0].Rows.Count];
            for (int i = 0; i < confData.Tables[0].Rows.Count; i++)
            {
                DataRow row = confData.Tables[0].Rows[i];
                QualityData data = qualDic[$"{row[0].ToString()} {row[1].ToString()}"];
                string flag = row[6].ToString();
                
                if (flag == "R")
                {
                    GenerateRandValue(data, rand);
                    bool represent = IsRepData(Lbl_Time.Text);
                    commandList[i] = dbConnect.GetSqlCommandForProd("SPIO_SET_Q_CONTROL",
                                                                    new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,     SqlDbType.VarChar,
                                                                                  SqlDbType.Bit, SqlDbType.NVarChar, SqlDbType.VarChar },
                                                                       new object[] { data.MachCode,     data.QualCode,     Lbl_Time.Text,     data.Value.ToString(), Lbl_Time.Text.Substring(11),
                                                                                  represent,     null,               null});

                    AddDataInGridView(Lbl_Time.Text, data.MachCode, data.QualCode, data.Value.ToString());
                    WriteLog(Lbl_Time.Text, data);
                }
                else
                {
                    data.Value = 0;
                }
            }
            dbConnect.TryTransaction(commandList);
        }

        private void GenerateRandValue(QualityData data, Random rand)
        {
            double randMin = data.Minimum - (data.Maximum - data.Minimum) * 0.1;
            double randWidth = (data.Maximum - data.Minimum) * 1.2;

            if (data.QualCode != "PQ")
                data.Value = (rand.NextDouble() * randWidth) + randMin;
            else
                data.Value = (rand.NextDouble() > 0.9) ? data.Value + 1 : data.Value;
        }
        #endregion
        #region -------- DataGridView
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
                    Dgv_Data.Rows.RemoveAt(Dgv_Data.Rows.Count - 2);
                Dgv_Data.Rows.Insert(0, Date, MachCode, "", "", "");
                rowIndex = 0;
            }
            Dgv_Data.Rows[rowIndex].Cells[colIndex].Value = value;
        }

        private int HasDuplicate(string date, string machCode)
        {
            if (Dgv_Data.Rows.Count <= 1)
                return -1;

            for(int i = 0; i < Dgv_Data.Rows.Count - 1; i++)
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
                worksheet[rowIndex, 0] = Date;
                worksheet.Cells[new Excel.CellPosition(rowIndex, 0)].Style.HAlign = Excel.ReoGridHorAlign.Center;
                worksheet[rowIndex, 1] = data.MachCode;
                writeRowInd = rowIndex;
            }

            worksheet[writeRowInd, writeColInd] = data.Value;
            if (data.Value < data.Minimum || data.Value > data.Maximum)
                worksheet.Cells[writeRowInd, writeColInd].Style.BackColor = Excel.Graphics.SolidColor.OrangeRed;
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
            SaveExcel();
        }
    }
}
