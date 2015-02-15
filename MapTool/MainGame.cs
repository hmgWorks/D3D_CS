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
        private float lensPosTheta = 270.0f;
        private float lensPosPhi = 0.0f;

        private bool[] keys = new bool[256];

        public bool Setup(MainForm mainForm)
        {
            mainForm.KeyDown += new KeyEventHandler(this.mainForm_KeyDown);            
            mainForm.KeyUp += new KeyEventHandler(this.mainForm_KeyUp);

            try
            {
                this.CreateDevice(mainForm);
                this.SetupCamera();
                this.CreateFont();
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

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode < this.keys.Length)
            {                
                this.keys[(int)e.KeyCode] = false;
            }
        }

        void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode < this.keys.Length)
            {
                this.keys[(int)e.KeyCode] = true;
            }
        }

        /// <summary>
        /// 메인 루프 처리
        /// </summary>
        public void Update()
        {            
            this.m_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, 
                System.Drawing.Color.DarkBlue, 1.0f, 0);
            this.m_device.BeginScene();
            this.m_font.DrawText(null, "키보드 입력받기", 0, 0, System.Drawing.Color.White);
            int height = 24;
            for (int i = 0; i < this.keys.Length; i++)
            {
                if (this.keys[i])
                {
                    this.m_font.DrawText(null, ((Keys)i).ToString(), 0, height,
                    System.Drawing.Color.White);
                    height += 24;
                }
            }
            this.m_device.EndScene();
            this.m_device.Present();
        }

        /// <summary>
        /// 자원 파기
        /// </summary>
        void IDisposable.Dispose()
        {
            if (this.m_font != null)
            {
                this.m_font.Dispose();
            }
            if (this.m_device != null)
            {
                this.m_device.Dispose();
            }
        }
    }
}
