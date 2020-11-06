using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryProject_Final
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FRM_Main());
            /*
            Frm_LogIn frm_LogIn = new Frm_LogIn();
            Application.Run(frm_LogIn);

            if(frm_LogIn.DialogResult == DialogResult.OK)
                Application.Run(new Frm_Main());        // 로그인에 성공 후 로그인 폼을 닫아도 Main 폼이 닫히지 않게 한다
                                                        // https://robotc.tistory.com/entry/C-WinForm-부모폼-닫고-자식폼-열기 참고
                                                        // if문이 없으면 로그인 없이 로그인 폼을 바로 닫을 시 로그인을 하지 않았음에도
                                                        //               메인 폼이 열리게 되므로 이에 대한 처리를 위해 if문을 추가
            */
        }
    }
}