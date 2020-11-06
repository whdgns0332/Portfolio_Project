using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final_IOServer
{
    class ControlLayout
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        public void MakeCurvedBorder(Control cnt, int curveAmount_X, int curveAmount_Y)
        {
            IntPtr ip = CreateRoundRectRgn(0, 0, cnt.Width, cnt.Height, curveAmount_X, curveAmount_Y);
            int i = SetWindowRgn(cnt.Handle, ip, true);
        }

        /// <summary>
        /// 화면의 x비율, y비율에 맞춰서 컨트롤의 크기를 조정하는 함수
        /// </summary>
        /// <param name="control">크기를 조정할 컨트롤 컨트롤</param>
        /// <param name="formSize">화면의 크기</param>
        /// <param name="xPosRatio">화면에서 컨트롤이 차지할 x축 길이의 비율(0~1)</param>
        /// <param name="yPosRatio">화면에서 컨트롤이 차지할 y축 길이의 비율(0~1)</param>
        public void Control_Sizing(Control control, Size formSize,
                                   double xPosRatio, double yPosRatio)
        {
            if (IsInRange(xPosRatio, 0, 1) &&
                IsInRange(yPosRatio, 0, 1))
            {
                int width = (int)(formSize.Width * xPosRatio);
                int height = (int)(formSize.Height * yPosRatio);

                control.Size = new Size(width, height);
            }
        }

        public enum HorizontalSiding { Left, Center, Right }
        public enum VerticalSiding { Top, Center, Bottom }
        /// <summary>
        /// 화면의 x비율, y비율에 맞춰서 컨트롤의 위치를 옮기는 함수
        /// </summary>
        /// <param name="control">위치를 옮길 컨트롤</param>
        /// <param name="formSize">화면의 크기</param>
        /// <param name="xPosRatio">화면에서 컨트롤이 위치할 x좌표의 비율(0~1)</param>
        /// <param name="yPosRatio">화면에서 컨트롤이 위치할 y좌표의 비율(0~1)</param>
        /// <param name="horizontalSiding">x좌표의 비율이 컨트롤의 좌측, 중앙, 우측 중 어느곳을 기준으로 하는가를 나타낸다</param>
        /// <param name="verticalSiding">y좌표의 비율이 컨트롤의 상단, 중앙, 하단 중 어느곳을 기준으로 하는가를 나타낸다</param>
        public void Control_Positioning(Control control, Size formSize,
                                        double xPosRatio, double yPosRatio,
                                        HorizontalSiding horizontalSiding = HorizontalSiding.Left,
                                        VerticalSiding verticalSiding = VerticalSiding.Top)
        {
            if (IsInRange(xPosRatio, 0, 1) &&
                IsInRange(yPosRatio, 0, 1))
            {
                int xCenter = (int)(formSize.Width * xPosRatio);
                int yCenter = (int)(formSize.Height * yPosRatio);

                int controlXHalf = control.Size.Width / 2;
                int controlYHalf = control.Size.Height / 2;

                int xpoint = 0, ypoint = 0;
                switch (horizontalSiding)
                {
                    case HorizontalSiding.Left:
                        xpoint = xCenter;
                        break;
                    case HorizontalSiding.Center:
                        xpoint = xCenter - controlXHalf;
                        break;
                    case HorizontalSiding.Right:
                        xpoint = xCenter - (controlXHalf * 2);
                        break;
                }

                switch (verticalSiding)
                {
                    case VerticalSiding.Top:
                        ypoint = yCenter;
                        break;
                    case VerticalSiding.Center:
                        ypoint = yCenter - controlYHalf;
                        break;
                    case VerticalSiding.Bottom:
                        ypoint = yCenter - (controlYHalf * 2);
                        break;
                }

                control.Location = new Point(xpoint, ypoint);
            }
        }

        public int GetXPosByRatio(Size size, double xRatio)
        {
            if (IsInRange(xRatio, 0, 1))
                return (int)(size.Width * xRatio);
            else
                return -1;
        }
        public double GetXRatioByPos(Size size, int xPos)
        {
            if (IsInRange(xPos, 0, size.Width))
                return xPos / (double)size.Width;
            else
                return -1;
        }
        public int GetYPosByRatio(Size size, double yRatio)
        {
            if (IsInRange(yRatio, 0, 1))
                return (int)(size.Height * yRatio);
            else
                return -1;
        }
        public double GetYRatioByPos(Size size, int yPos)
        {
            if (IsInRange(yPos, 0, size.Height))
                return yPos / (double)size.Height;
            else
                return -1;
        }

        private bool IsInRange(double num, double min, double max)
        {
            return num >= min && num <= max;
        }

        public void SetBackgroundImage(Control ctrl, string image)
        {
            try
            {
                ctrl.BackgroundImage = new Bitmap(Image.FromFile(image), ctrl.Size);
                ctrl.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (System.IO.FileNotFoundException excep)
            {
                string className = nameof(ControlLayout);
                string funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               $"이미지 {image}가 존재하지 않습니다");
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
            }
            catch (Exception excep)
            {
                string className = nameof(ControlLayout);
                string funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               $"{ctrl.Name}에 이미지 {image}를 할당하던 중 오류가 발생했습니다");
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
            }
        }
        public void SetImage(PictureBox picture, string image)
        {
            try
            {
                picture.Image = Image.FromFile(image);
            }
            catch (System.IO.FileNotFoundException excep)
            {
                string className = nameof(ControlLayout);
                string funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               $"이미지 {image}가 존재하지 않습니다");
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
            }
            catch (Exception excep)
            {
                string className = nameof(ControlLayout);
                string funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string logText = string.Concat(excep.Message.ToString(), Environment.NewLine,
                                               $"{picture.Name}에 이미지 {image}를 할당하던 중 오류가 발생했습니다");
                Log.WriteLog(Log.LogType.Error, className, funcName, logText);
            }
        }

        public void DrawTextOnImage(Image img, string text, Font font, Brush brush, Rectangle rec)
        {   // CreateGraphics를 통해 Graphics를 받아 그릴 경우 해당 Control에 이미지가 할당되어 있으면 가려져서 안보일 수 있다
            // 특히 BackgroundImage가 아닌 일반 Image(Foreground)의 경우 Draw함수로 그리더라도 이미지에 가려서 안보인다

            Graphics graphics = Graphics.FromImage(img);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            graphics.DrawString(text, font, brush, rec, drawFormat);

            //외부 폰트 사용은 다음과 같은 방법으로 한다
            //PrivateFontCollection privateFonts = new PrivateFontCollection();
            //privateFonts.AddFontFile(System.IO.Directory.GetCurrentDirectory() + $@"{resSect["ResourceFolder"]}\Font\210_맨발의청춘L.ttf");
            //Font font = new Font(privateFonts.Families[0], 16);
        }

        public Font GetProperFontSize(string fontfamily, int height, double sizeRatio, bool bold = false)
        {
            int textHeight = (int)(height * sizeRatio) + 1;
            Font font = null;
            if (bold)
                font = new Font(fontfamily, textHeight * 0.6f, FontStyle.Bold);     // Tahoma 폰트로 실험한 결과 3n 포인트의 글씨의 세로 높이는 5n - 1임을 알 수 있었음
            else
                font = new Font(fontfamily, textHeight * 0.6f);
            return font;
        }
    }
}
