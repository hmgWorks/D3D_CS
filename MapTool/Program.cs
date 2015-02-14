using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace MapTool
{
    /// <summary>
    /// 프로그램 시작 클래스
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (MainForm mainForm = new MainForm())
            using (MainGame mainGame = new MainGame())
            {
                if (mainGame.initDevice(mainForm))
                {
                    mainForm.Show();

                    while (mainForm.Created)
                    {
                        mainGame.MainLoop();

                        //이벤트가 있는 경우 처리
                        Application.DoEvents();
                    }
                }
                else
                {
                    //초기화 실패
                }
            }

        }
    }
}
