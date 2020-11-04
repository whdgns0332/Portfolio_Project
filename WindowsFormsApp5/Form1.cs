using System.Collections.Generic;
using System.Windows.Forms;
using SharpPcap;
using System.Drawing;
using System;
using System.Linq;
using System.Net;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Size = new Size(900, 550);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            List<string> NicName = new List<string>();
            foreach (ICaptureDevice Temp in CaptureDeviceList.Instance)
            {
                string[] arrTemp = Temp.ToString().Split('\n');
                NicName.Add(arrTemp[1].Substring("FriendlyName: ".Length));
            }

            for(int iTemp = 0; iTemp < NicName.Count; ++iTemp)
            {
                Button aButton = new Button();
                aButton.Text = NicName[iTemp];
                aButton.Name = "Facto" + iTemp;
                aButton.Location = new Point(30, 30 + iTemp*30);
                aButton.Click += new EventHandler(Facto_Click1);
                Controls.Add(aButton);
            }

            (sender as Button).Enabled = false;
        }

        private void Print_MAC(byte[] EtherData)
        {
            GroupBox aGroupBox = new GroupBox();
            aGroupBox.Location = new Point(20, 0);
            aGroupBox.Name = "GroupEther";
            aGroupBox.Size = new Size(290, 110);
            aGroupBox.Text = "Mac Layer";

            Label aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Destination MAC";
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(30, 20);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Source MAC";
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(30, 50);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Next Protocol";
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(30, 80);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            string data = string.Format("{0:X2}", EtherData[0]);
            for (int iTemp = 1; iTemp < 6; ++iTemp)
            {
                data += string.Format("-{0:X2}", EtherData[iTemp]);
            }
            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = data;
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(160, 20);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            data = string.Format("{0:X2}", EtherData[6]);
            for (int iTemp = 7; iTemp < 12; ++iTemp)
            {
                data += string.Format("-{0:X2}", EtherData[iTemp]);
            }
            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = data;
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(160, 50);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            int iNum = BitConverter.ToInt16(EtherData.Skip<byte>(12).Take(2).Reverse<byte>().ToArray(), 0);
            switch(iNum)
            {
                case 0x0800:
                    data = "IP";
                    break;
                case 0x0200:
                    data = "Xerox PUP";
                    break;
                case 0x0500:
                    data = "Sprite";
                    break;
                case 0x0806:
                    data = "Address resolution";
                    break;
                case 0x8035:
                    data = "Reverse ARP";
                    break;
                case 0x809B:
                    data = "AppleTalk protocol";
                    break;
                case 0x80F3:
                    data = "AppleTalk ARP";
                    break;
                case 0x8100:
                    data = "IEEE 802.1Q VLAN tagging";
                    break;
                case 0x8137:
                    data = "IPX";
                    break;
                case 0x86dd:
                    data = "IP protocol version 6";
                    break;
                case 0x9000:
                    data = "used to test interfaces";
                    break;
                default:
                    data = "Unknown";
                    break;
            }
            aLabel.Text = data;
            aLabel.Name = "LabelDMAC";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(160, 80);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(aLabel);

            Controls.Add(aGroupBox);
        }

        private int Del_Button_Device(object sender)
        {
            int DeviceNum = Convert.ToInt32((sender as Button).Name.Substring("Facto".Length));
            CaptureDeviceList.Instance[DeviceNum].Open(DeviceMode.Promiscuous, 0);

            for (int iTemp = 0; iTemp < CaptureDeviceList.Instance.Count; ++iTemp)
            {
                foreach (Control Temp in Controls)
                {
                    if (Temp.Name == "Facto" + iTemp)
                    {
                        Controls.Remove(Temp);
                    }
                }
            }

            return DeviceNum;
        }

        private void Print_IP(byte[] IPData)
        {
            // 그룹박스 설정
            GroupBox aGroupBox = new GroupBox();
            aGroupBox.Location = new Point(20, 110);
            aGroupBox.Name = "GroupIP";
            aGroupBox.Size = new Size(290, 390);
            aGroupBox.Text = "IP Layer";

            Label aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IP Version";
            aLabel.Name = "LabelIPV";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 20);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IP Head Length";
            aLabel.Name = "LabelIPHL";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 50);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IP TOS";
            aLabel.Name = "LabelIPTOS";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 80);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IP Total Length";
            aLabel.Name = "LabelIPTL";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 110);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IP IDentification";
            aLabel.Name = "LabelIPID";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 140);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Fragment DF";
            aLabel.Name = "LabelIPDF";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 170);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Fragment MF";
            aLabel.Name = "LabelIPMF";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 190);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Fragment Offset";
            aLabel.Name = "LabelIPOFF";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 210);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Time To Live";
            aLabel.Name = "LabelIPTTL";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 240);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "L3 Protocol";
            aLabel.Name = "LabelIPPROTO";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 270);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Header Checksum";
            aLabel.Name = "LabelIPCHK";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 300);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Source IP";
            aLabel.Name = "LabelIPS";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 330);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "Destination IP";
            aLabel.Name = "LabelIPD";
            aLabel.Size = new Size(120, 20);
            aLabel.Location = new Point(10, 360);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            ///////////////////////////////////////////////////////////////////////////
            
            byte byteTemp = IPData[14];

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = "IPv" + (byteTemp >> 4).ToString();
            aLabel.Name = "LabelIPVData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 20);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = ((byteTemp & 0x0F) * 4).ToString() + " Bytes";
            aLabel.Name = "LabelIPHLData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 50);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            byteTemp = IPData[15];

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = string.Format("0x{0:X2}", byteTemp);
            aLabel.Name = "LabelIPTOSData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 80);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            int iNum = BitConverter.ToUInt16(IPData.Skip<byte>(16).Take(2).Reverse().ToArray(), 0);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = iNum.ToString() + " Bytes";
            aLabel.Name = "LabelIPTLData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 110);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            iNum = BitConverter.ToUInt16(IPData.Skip<byte>(18).Take(2).Reverse().ToArray(), 0);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = iNum.ToString();
            aLabel.Name = "LabelIPIDData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 140);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            iNum = BitConverter.ToUInt16(IPData.Skip<byte>(20).Take(2).Reverse().ToArray(), 0);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = (0 != (iNum & (1 << 14))).ToString();
            aLabel.Name = "LabelIPDFData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 170);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = (0 != (iNum & (1 << 13))).ToString();
            aLabel.Name = "LabelIPMFData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 190);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = (iNum & 0x1FFF).ToString();
            aLabel.Name = "LabelIPOFFData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 210);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            byteTemp = IPData[22];

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = byteTemp.ToString();
            aLabel.Name = "LabelIPTTLData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 240);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            byteTemp = IPData[23];

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            switch (byteTemp)
            {
                case 0:
                    aLabel.Text = "IP";
                    break;
                case 1:
                    aLabel.Text = "ICMP";
                    break;
                case 2:
                    aLabel.Text = "IGMP";
                    break;
                case 4:
                    aLabel.Text = "IPIP";
                    break;
                case 6:
                    aLabel.Text = "TCP";
                    break;
                case 8:
                    aLabel.Text = "EGP";
                    break;
                case 12:
                    aLabel.Text = "PUP";
                    break;
                case 17:
                    aLabel.Text = "UDP";
                    break;
                case 22:
                    aLabel.Text = "IDP";
                    break;
                case 29:
                    aLabel.Text = "TP";
                    break;
                case 33:
                    aLabel.Text = "DCCP";
                    break;
                case 41:
                    aLabel.Text = "IPV6";
                    break;
                default:
                    aLabel.Text = "Unknown";
                    break;
            }
            aLabel.Name = "LabelIPPROTOData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 270);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            iNum = BitConverter.ToUInt16(IPData.Skip<byte>(24).Take(2).Reverse().ToArray(), 0);

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = string.Format("0x{0:X4}", iNum);
            aLabel.Name = "LabelIPCHKData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 300);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            IPAddress aIPAddress = new IPAddress(IPData.Skip<byte>(26).Take(4).Reverse().ToArray());

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = aIPAddress.ToString();
            aLabel.Name = "LabelIPSData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 330);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            aIPAddress = new IPAddress(IPData.Skip<byte>(30).Take(4).Reverse().ToArray());

            aLabel = new Label();
            aLabel.TextAlign = ContentAlignment.MiddleCenter;
            aLabel.Text = aIPAddress.ToString();
            aLabel.Name = "LabelIPDData";
            aLabel.Size = new Size(140, 20);
            aLabel.Location = new Point(140, 360);
            aLabel.BorderStyle = BorderStyle.FixedSingle;
            aGroupBox.Controls.Add(aLabel);

            Controls.Add(aGroupBox);

        }

        private void Facto_Click1(object sender, System.EventArgs e)
        {
            int DeviceNum = Del_Button_Device(sender);
            CaptureDeviceList.Instance[DeviceNum].Open(DeviceMode.Normal, 0);
            RawCapture aRawCapture = CaptureDeviceList.Instance[DeviceNum].GetNextPacket();
            Print_MAC(aRawCapture.Data); // 1계층 출력

            int iNum = BitConverter.ToInt16(aRawCapture.Data.Skip<byte>(12).Take(2).Reverse<byte>().ToArray(), 0);
            if (0x0800 == iNum)
            {
                Print_IP(aRawCapture.Data); // 2계층 출력
                Print_TCP(aRawCapture.Data); // 3계층 출력
            }
        }

        private void Print_TCP(byte[] Data)
        {
            // Data 입력           
            List<FactoLabel> aFactoLabel = new List<FactoLabel>();

            aFactoLabel.Add(new FactoLabel() { Name = "L4 Protocol", Value = "" });
            int Port1 = BitConverter.ToUInt16(Data.Skip<byte>(34).Take(2).Reverse().ToArray(), 0);
            int Port2 = BitConverter.ToUInt16(Data.Skip<byte>(36).Take(2).Reverse().ToArray(), 0);
            Port1 = (Port1 < 1024) ? Port1 : 65535;
            Port2 = (Port2 < 1024) ? Port2 : 65535;
            Port1 = (Port1 < Port2) ? Port1 : Port2;
            switch (Port1)
            {
                case 7:
                    aFactoLabel[0].Value = "ECHO";
                    break;
                case 13:
                    aFactoLabel[0].Value = "DAYTIME";
                    break;
                case 20:
                    aFactoLabel[0].Value = "FTP : Data";
                    break;
                case 21:
                    aFactoLabel[0].Value = "FTP : Control";
                    break;
                case 22:
                    aFactoLabel[0].Value = "Secure SHell";
                    break;
                case 23:
                    aFactoLabel[0].Value = "Telnet Protocol";
                    break;
                case 25:
                    aFactoLabel[0].Value = "SMTP";
                    break;
                case 37:
                    aFactoLabel[0].Value = "TIME Protocol";
                    break;
                case 53:
                    aFactoLabel[0].Value = "DNS";
                    break;
                case 80:
                    aFactoLabel[0].Value = "HTTP";
                    break;
                case 443:
                    aFactoLabel[0].Value = "HTTPS";
                    break;
                default:
                    aFactoLabel[0].Value = "Not Support";
                    break;
            }

            UInt32 iNum = BitConverter.ToUInt16(Data.Skip<byte>(34).Take(2).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Source Port", Value = iNum.ToString() });

            iNum = BitConverter.ToUInt16(Data.Skip<byte>(36).Take(2).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Destination Port", Value = iNum.ToString() });

            iNum = BitConverter.ToUInt32(Data.Skip<byte>(38).Take(4).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Sequence Number", Value = iNum.ToString() });

            iNum = BitConverter.ToUInt32(Data.Skip<byte>(42).Take(4).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Ack Number", Value = iNum.ToString() });

            iNum = (UInt32)((Data[46] >> 4) * 4);
            aFactoLabel.Add(new FactoLabel() { Name = "Offset", Value = iNum.ToString() });

            iNum = Data[47];
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Urgent", Value = (0 != (iNum & 1 << 5)).ToString() });
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Ack", Value = (0 != (iNum & 1 << 4)).ToString() });
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Push", Value = (0 != (iNum & 1 << 3)).ToString() });
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Res", Value = (0 != (iNum & 1 << 2)).ToString() });
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Syn", Value = (0 != (iNum & 1 << 1)).ToString() });
            aFactoLabel.Add(new FactoLabel() { Name = "Bit:Fin", Value = (0 != (iNum & 1 << 0)).ToString() });

            iNum = BitConverter.ToUInt16(Data.Skip<byte>(48).Take(2).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Windows Size", Value = iNum.ToString() });

            iNum = BitConverter.ToUInt16(Data.Skip<byte>(50).Take(2).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Checksum", Value = string.Format("0x{0:X4}", iNum) });

            iNum = BitConverter.ToUInt16(Data.Skip<byte>(52).Take(2).Reverse().ToArray(), 0);
            aFactoLabel.Add(new FactoLabel() { Name = "Urgent Pointer", Value = string.Format("0x{0:X4}", iNum) });

            // 변수 선언
            int XMargin = 10;
            int XSize = 120;
            int XInterval = 10;
            int YMargin = 15;
            int YSize = 20;
            int YInterval = 10;

            // 그룹박스 설정
            GroupBox aGroupBox = new GroupBox();
            aGroupBox.Location = new Point(315, 22);
            aGroupBox.Name = "GroupTCP";
            aGroupBox.Size = new Size(XMargin * 2 + XSize * 2 + XInterval, (YInterval + YSize) * aFactoLabel.Count + YMargin);
            aGroupBox.Text = "TCP Layer";

            // 라벨 설정
            for (int Count = 0; Count < aFactoLabel.Count; ++Count)
            {
                Label aLabel = new Label();
                aLabel.TextAlign = ContentAlignment.MiddleCenter;
                aLabel.Text = aFactoLabel[Count].Name;
                aLabel.Name = "****";
                aLabel.Size = new Size(XSize, YSize);
                aLabel.Location = new Point(XMargin, (YSize + YInterval) * Count + YMargin);
                aLabel.BorderStyle = BorderStyle.FixedSingle;
                aGroupBox.Controls.Add(aLabel);

                aLabel = new Label();
                aLabel.TextAlign = ContentAlignment.MiddleCenter;
                aLabel.Text = aFactoLabel[Count].Value;
                aLabel.Name = "****1";
                aLabel.Size = new Size(XSize, YSize);
                aLabel.Location = new Point(XMargin + XSize + XInterval, (YSize + YInterval) * Count + YMargin);
                aLabel.BorderStyle = BorderStyle.FixedSingle;
                aGroupBox.Controls.Add(aLabel);
            }
            // 그룹박스 출력
            Controls.Add(aGroupBox);
        }
    }
}
/*
IPPROTO_IP = 0 Dummy protocol for TCP
IPPROTO_ICMP = 1   Internet Control Message Protocol
IPPROTO_IGMP = 2   Internet Group Management Protocol
IPPROTO_IPIP = 4   IPIP tunnels(older KA9Q tunnels use 94)
IPPROTO_TCP = 6    Transmission Control Protocol
IPPROTO_EGP = 8    Exterior Gateway Protocol
IPPROTO_PUP = 12   PUP protocol
IPPROTO_UDP = 17   User Datagram Protocol
IPPROTO_IDP = 22   XNS IDP protocol
IPPROTO_TP = 29    SO Transport Protocol Class 4  
IPPROTO_DCCP = 33  Datagram Congestion Control Protocol
IPPROTO_IPV6   = 41  IPv6 header
*/