using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;
using System.Windows.Forms;

namespace MapTool
{    
    public partial class MainGame:IDisposable
    {   
        public bool Setup(MainForm mainForm)
        {
            try
            {
                this.CreateDevice(mainForm);
            }
            catch (DirectXException ex)
            {

                MessageBox.Show(ex.ToString(),
                    "error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 메인 루프 처리
        /// </summary>
        public void Update()
        {            
            this.m_device.Clear(ClearFlags.Target, System.Drawing.Color.DarkBlue, 1.0f, 0);
            this.m_device.BeginScene();

            this.m_device.EndScene();
            this.m_device.Present();
        }

        /// <summary>
        /// 자원 파기
        /// </summary>
        void IDisposable.Dispose()
        {
            if (this.m_device != null)
            {
                this.m_device.Dispose();
            }
        }
    }
}
