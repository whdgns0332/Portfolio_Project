using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 베이스볼_결과_예상하기
{
    public partial class Form1 : Form
    {
        int[] TryNum = { 10, 10, 10 }; // 입력할 번호. 10은 할당되지 않은 상태
        int[] AnsNum = new int[3]; // 정답
        List<int[]> PossibleNum = new List<int[]> { }; // 답이 될 수 있는 숫자열 리스트
        List<int[]> RecordNum = new List<int[]> { }; // 이전에 입력했던 숫자열 기록
        List<int[]> RecordAns = new List<int[]> { }; // 각 Try마다 ball과 strike 기록
        int ball = 0;
        int strike = 0;
        Button[] BtnNum = new Button[10]; // 입력 버튼 0~9
        Label[] LblTry = new Label[3]; // 입력한 숫자 표시
        int TryCount = 1; // 시도 횟수
        bool isCorrect = false; // 정답을 맞추면 true

        public Form1()
        {
            InitializeComponent();

            // 버튼 0~9까지 생성
            for (int i = 0; i < 10; i++)
            {
                BtnNum[i] = new Button();
                BtnNum[i].Font = new Font("굴림", 27F, FontStyle.Regular, GraphicsUnit.Point, 129);
                BtnNum[i].Location = new Point(180 + 70 * (i - (i / 5) * 5), 100 + 70 * (i / 5));
                BtnNum[i].Name = "button" + (i + 1).ToString();
                BtnNum[i].Size = new Size(50, 50);
                BtnNum[i].Text = (i + 1).ToString();
                BtnNum[i].UseVisualStyleBackColor = true;
                BtnNum[i].TextAlign = ContentAlignment.MiddleCenter;
                BtnNum[i].Click += new EventHandler(button_Click);
                Controls.Add(BtnNum[i]);
            }
            BtnNum[9].Text = "0";

            // 입력한 숫자 표시 라벨 생성
            for (int i = 0; i < 3; i++)
            {
                LblTry[i] = new Label();
                LblTry[i].BackColor = SystemColors.ControlLightLight;
                LblTry[i].BorderStyle = BorderStyle.FixedSingle;
                LblTry[i].Font = new Font("굴림", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 129);
                LblTry[i].Location = new Point(220 + i*100, 30);
                LblTry[i].Name = "TryNum" + (i+1).ToString();
                LblTry[i].Size = new Size(52, 52);
                LblTry[i].Text = "";
                LblTry[i].TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(LblTry[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            do // 정답 설정
            {
                for (int i = 0; i < 3; i++)
                    AnsNum[i] = r.Next(9);
            }
            while (AnsNum[0] == AnsNum[1] || AnsNum[1] == AnsNum[2] || AnsNum[0] == AnsNum[2]);
            // 세 숫자 모두 다를 때까지 반복

            for (int i = 0; i < 10; i++) // 가능한 모든 번호 리스트 생성 ([0, 0, 0] ~ [9, 9, 9])
                for (int j = 0; j < 10; j++)
                    for (int k = 0; k < 10; k++)
                    {
                        if (i != j && j != k && k != i) // 세 개의 숫자가 모두 다르면 리스트에 추가
                        {
                            int[] arr = { i, j, k };
                            PossibleNum.Add(arr);
                            PossibleAnswerList.Items.Add("[" + arr[0] + ", " + arr[1] + ", " + arr[2] + "]");
                        }
                    }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TryNum.Contains(10)) // 값이 하나라도 입력이 안되있으면
            {
                LblMessage.Text = "세 버튼 모두 클릭하세요.";
                return;
            }

            foreach (int[] arr in RecordNum) // 이전에 입력했던 숫자열인지 확인
            {
                int isMatch = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (arr[i] != TryNum[i])
                        break;
                    isMatch++;
                }
                if (isMatch == 3)
                {
                    LblMessage.Text = "이전에 입력했던 숫자열 입니다.";
                    foreach (Button btn in BtnNum)
                        btn.Enabled = true;
                    TryNum[0] = TryNum[1] = TryNum[2] = 10;
                    LblTry[0].Text = LblTry[1].Text = LblTry[2].Text = "";
                    return;
                }
            }

            // 통상 분기 
            BtnStart.Enabled = false;
            BtnRetry.Enabled = true;
            BtnReset.Enabled = false;

            // strike와 ball 계산
            for (int i = 0; i < 3; i++)
            {
                if (AnsNum.Contains(TryNum[i])) // strike + ball
                    ball += 1;
                if (AnsNum[i] == TryNum[i])
                    strike += 1;
            }
            ball -= strike;

            if (strike == 3)
            {
                LblMessage.Text
                    = "정답입니다. " + TryCount.ToString() + "번 만에 맞추셨습니다.\n다시 시작하려면 Retry를 누르세요";
                isCorrect = true;
            }
            else
            {
                LblMessage.Text = ball.ToString() + "ball " + strike.ToString() + "strike 입니다.";
                RecordNum.Add(new int[] { TryNum[0], TryNum[1], TryNum[2] });
                // 주의 : 배열도 객체이므로 new로 새로 생성해야함. 그냥 TryNum을 대입하면 그 배열의 주소가 저장되서
                //        TryNum의 값이 바뀌면 RecordNum의 배열 값도 바뀌게 됨.(사실 TryNum 그 자체임)
            }

            // 시도한 번호 목록에 기록 추가
            AnswerRecordList.Items.Add(TryCount + "TRY : " + TryNum[0] + " " + TryNum[1] + " " + TryNum[2]);
            AnswerRecordList.Items.Add(ball.ToString() + "ball " + strike.ToString() + "strike");
            AnswerRecordList.Items.Add("");

            RecordAns.Add(new int[] { ball, strike });

            // 정답이 될 수 없는 숫자열 리스트 생성
            ball = 0;
            strike = 0;
            List<int[]> WorngNum = new List<int[]> { }; // 답이 아닌 번호 목록
            foreach (int[] arr in PossibleNum)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (arr.Contains(TryNum[i]))
                        ball += 1;
                    if (arr[i] == TryNum[i])
                        strike += 1;
                }
                ball -= strike;

                if (RecordAns[TryCount - 1][0] != ball || RecordAns[TryCount-1][1] != strike)
                {
                    WorngNum.Add(arr); // PossibleNum의 번호 중 답이 아닌 번호들 제외
                }

                ball = 0;
                strike = 0;
            }

            // 후보 목록에 오답 리스트의 요소들 제거
            foreach (int[] arr in WorngNum)
            {
                if (PossibleNum.Contains(arr))
                {
                    PossibleNum.Remove(arr);
                }
            }

            // 리스트 박스 갱신
            PossibleAnswerList.Items.Clear();
            foreach (int[] arr in PossibleNum)
            {
                PossibleAnswerList.Items.Add("[" + arr[0] + ", " + arr[1] + ", " + arr[2] + "]");
            }            
        }

        private void BtnRetry_Click(object sender, EventArgs e)
        {
            if (isCorrect == true) // 정답을 맞혔으면 모든 값 초기화
            {
                TryCount = 1;

                foreach (Button btn in BtnNum)
                    btn.Enabled = true;

                TryNum[0] = TryNum[1] = TryNum[2] = 10;
                LblTry[0].Text = LblTry[1].Text = LblTry[2].Text = "";

                BtnReset.Enabled = true;
                BtnStart.Enabled = true;
                BtnRetry.Enabled = false;

                AnswerRecordList.Items.Clear();
                PossibleAnswerList.Items.Clear();

                Random r = new Random();
                do // 정답 설정
                {
                    for (int i = 0; i < 3; i++)
                        AnsNum[i] = r.Next(9);
                }
                while (AnsNum[0] == AnsNum[1] || AnsNum[1] == AnsNum[2] || AnsNum[0] == AnsNum[2]);

                PossibleNum.Clear();
                for (int i = 0; i < 10; i++) // 가능한 모든 번호 리스트 생성
                    for (int j = 0; j < 10; j++)
                        for (int k = 0; k < 10; k++)
                        {
                            if (i != j && j != k && k != i) // 세 개의 숫자가 모두 다르면 리스트에 추가
                            {
                                int[] arr = { i, j, k };
                                PossibleNum.Add(arr);
                                PossibleAnswerList.Items.Add("[" + arr[0] + ", " + arr[1] + ", " + arr[2] + "]");
                            }
                        }

                RecordAns.Clear();
                RecordNum.Clear();

                LblMessage.Text = "숫자 버튼 세 개를 클릭하세요.";
                isCorrect = false;
            }
            else
            {
                TryCount++;
                foreach (Button btn in BtnNum)
                    btn.Enabled = true;
                TryNum[0] = TryNum[1] = TryNum[2] = 10;
                LblTry[0].Text = LblTry[1].Text = LblTry[2].Text = "";
                BtnReset.Enabled = true;
                BtnStart.Enabled = true;
                BtnRetry.Enabled = false;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TryNum[0] = TryNum[1] = TryNum[2] = 10;
            LblTry[0].Text = LblTry[1].Text = LblTry[2].Text = "";
            foreach (Button btn in BtnNum)
            {
                btn.Enabled = true;
            }
        }

        private void button_Click(object sender, EventArgs e) // 숫자 입력 버튼
        {
            Button button = sender as Button;

            for (int i = 0; i < 3; i++)
            {
                if (TryNum[i] == 10)
                {
                    LblTry[i].Text = button.Text;
                    TryNum[i] = int.Parse(button.Text);
                    button.Enabled = false;
                    break;
                }
            }
        }
    }
}
