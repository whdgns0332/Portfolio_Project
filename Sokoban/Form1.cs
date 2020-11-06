using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Properties;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        int[,] CurrentMapArray;
        const int block = 64;
        static int stage = 0;
        int restGoal; // 남은 목표물
        int moveCount = 0; // 이동 횟수
        int X_Char; // 캐릭터 X좌표 위치
        int Y_Char; // 캐릭터 Y좌표 위치
        bool OnDestination = false; // 플레이어가 목적지 위에 있으면 On
        PictureBox[,] pictureBoxes;
        Dictionary<Bitmap, int> imageNum =
            new Dictionary<Bitmap, int>(){ { Resources.Space, 0 }, { Resources.Wall, 1 }, { Resources.Ball, 2 },
                                          { Resources.BallOnGoal, 3 }, { Resources.Goal, 4 }, { Resources.Down, 5 },
                                          { Resources.Left, 5 }, { Resources.Right, 5 }, { Resources.Up, 5 } };
        
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MapSetting(MapArrays.MapList[stage]);
        }

        private void MapSetting(int[,] mapArray) // 맵 구성
        {
            CurrentMapArray = (int[,])mapArray.Clone();
            ClientSize = new Size(block * (mapArray.GetLength(1)), block * (mapArray.GetLength(0)));
            pictureBoxes = new PictureBox[mapArray.GetLength(0), mapArray.GetLength(1)];

            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    pictureBoxes[i, j] = new PictureBox();
                    pictureBoxes[i, j].Location = new Point(j * block, i * block);
                    pictureBoxes[i, j].Size = new Size(block, block);

                    switch (mapArray[i, j])
                    {
                        case 0: // 빈 공간
                            pictureBoxes[i, j].Image = Resources.Space;
                            break;
                        case 1: // 벽
                            pictureBoxes[i, j].Image = Resources.Wall;
                            break;
                        case 2: // 공
                            pictureBoxes[i, j].Image = Resources.Ball;
                            break;
                        case 3: // 목표지의 공
                            pictureBoxes[i, j].Image = Resources.BallOnGoal;
                            break;
                        case 4: // 목표지
                            pictureBoxes[i, j].Image = Resources.Goal;
                            restGoal++;
                            break;
                        default: // 플레이어(5)
                            pictureBoxes[i, j].Image = Resources.Down;
                            X_Char = j;
                            Y_Char = i;
                            break;
                    }
                    pictureBoxes[i, j].Tag = mapArray[i, j];
                    Controls.Add(pictureBoxes[i, j]);
                }
            }
        }

        private int[] xyCal(KeyEventArgs e)
        {
            int[] xyResult = new int[2];
            switch (e.KeyCode)
            {
                case Keys.Up:
                    xyResult[0] = -1;
                    xyResult[1] = 0;
                    break;

                case Keys.Left:
                    xyResult[0] = 0;
                    xyResult[1] = -1;
                    break;

                case Keys.Right:
                    xyResult[0] = 0;
                    xyResult[1] = 1;
                    break;

                case Keys.Down:
                    xyResult[0] = 1;
                    xyResult[1] = 0;
                    break;
            }

            return xyResult;
        } // 방향키에 따른 벡터값 반환

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R) // 리셋
            {
                NewMap(false);
            }

            int[] currentLoc = { Y_Char, X_Char }; // 캐릭터 현재 좌표
            int[] oneBlockLoc = { Y_Char + xyCal(e)[0], X_Char + xyCal(e)[1] }; // 한 칸 앞 좌표
            int[] twoBlockLoc = { Y_Char + 2 * xyCal(e)[0], X_Char + 2 * xyCal(e)[1] }; // 두 칸 앞 좌표

            int oneBlock = CurrentMapArray[Y_Char + xyCal(e)[0], X_Char + xyCal(e)[1]]; // 한 칸 앞의 오브젝트 번호
            int twoBlock;// 두 칸 앞의 오브젝트 번호
            try
            { twoBlock = CurrentMapArray[Y_Char + 2 * xyCal(e)[0], X_Char + 2 * xyCal(e)[1]]; } 
            catch (IndexOutOfRangeException)
            {
                twoBlock = 0;
            }

            switch (oneBlock)
            {
                case 0: // 앞에 빈 공간
                case 4: // 또는 목적지
                    ObjectChange(oneBlockLoc, CharMove(xyCal(e), true), 5); // 이동한 위치의 이미지를 플레이어로
                    
                    if (OnDestination)
                        ObjectChange(currentLoc, Resources.Goal, 4);
                    else
                        ObjectChange(currentLoc, Resources.Space, 0);

                    OnDestination = (oneBlock == 0) ? false : true;
                    break;

                case 1: // 앞에 벽
                    ObjectChange(currentLoc, CharMove(xyCal(e), false), 5);
                    return;

                case 2: // 앞에 공
                case 3: // 또는 목적지의 공
                    if (twoBlock == 1 || twoBlock == 2 || twoBlock == 3) // 그 공 앞에 물체
                    {
                        ObjectChange(currentLoc, CharMove(xyCal(e), false), 5);
                        return;
                    }

                    if (OnDestination)
                        ObjectChange(currentLoc, Resources.Goal, 4);
                    else
                        ObjectChange(currentLoc, Resources.Space, 0);

                    if (twoBlock == 4) // 그 공 앞에 목적지
                    {
                        ObjectChange(oneBlockLoc, CharMove(xyCal(e), true), 5);
                        ObjectChange(twoBlockLoc, Resources.BallOnGoal, 3);
                        if (oneBlock == 2) restGoal--;

                        if (restGoal == 0) // 미션 완료
                        {
                            NewMap(true);
                            return;
                        }
                    }
                    else // 그 공 앞에 빈 공간
                    {
                        ObjectChange(oneBlockLoc, CharMove(xyCal(e), true), 5);
                        ObjectChange(twoBlockLoc, Resources.Ball, 2);
                        if (oneBlock == 3) restGoal++;
                    }

                    OnDestination = (oneBlock == 2)? false : true;

                    break;
            }
        }

        /// <summary>
        /// 원하는 좌표의 오브젝트 이미지 및 번호 변경
        /// </summary>
        /// <param name="loc">오브젝트 좌표</param>
        /// <param name="img">변경 이미지</param>
        /// <param name="num">변경 이미지의 번호</param>
        private void ObjectChange(int[] loc, Bitmap img ,int num)
        {
            pictureBoxes[loc[0], loc[1]].Image = img;
            CurrentMapArray[loc[0], loc[1]] = num;
        }
        private Bitmap CharMove(int[] xyCal, bool move) // 캐릭터 이동 시 변수값 처리 및 변경 이미지 반환
        {
            if (move)
            {
                moveCount++;
                Y_Char += xyCal[0];
                X_Char += xyCal[1];
            }

            if (xyCal[1] == 0 && xyCal[0] == -1)
                return Resources.Up;
            else if (xyCal[1] == -1 && xyCal[0] == 0)
                return Resources.Left;
            else if (xyCal[1] == 1 && xyCal[0] == 0)
                return Resources.Right;
            else
                return Resources.Down;
        }

        private void NewMap(bool success) // 성공
        {
            if (success)
                stage++;

            if (stage == MapArrays.MapList.Count)
            {
                MessageBox.Show("끝");
                Application.Exit();
            }
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    pictureBoxes[i, j].Dispose();
                }
            }

            restGoal = 0;
            moveCount = 0;
            OnDestination = false;

            MapSetting(MapArrays.MapList[stage]);
            CenterToScreen();
        }
    }
}
