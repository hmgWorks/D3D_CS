using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace MapTool
{   
    /// <summary>
    /// MainGame class
    /// </summary>
    public partial class MainGame:IDisposable
    {
        private Microsoft.DirectX.Direct3D.Font m_font = null;

        public bool initializeApplication(MainForm mainForm)
        {
            this.m_mainForm = mainForm;

            try
            {
                this.CreateDevice(mainForm);

                FontDescription fd = new FontDescription();

                fd.Height = 24;
                fd.FaceName = "consolas";

                this.m_font = new Microsoft.DirectX.Direct3D.Font(this.m_device, fd);
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
        public void MainLoop()
        {            
            this.m_device.Clear(ClearFlags.Target, System.Drawing.Color.DarkBlue, 1.0f, 0);
            this.m_device.BeginScene();

            this.m_font.DrawText(null, "directX", 0, 0, System.Drawing.Color.White);
            
            this.m_font.DrawText(null,
                "point", 
                new Point(250, 40), 
                System.Drawing.Color.PaleVioletRed);
            
            this.m_font.DrawText(null,
                "rectangle", 
                new Rectangle(100, 260, 500, 400), 
                DrawTextFormat.Left | DrawTextFormat.Top, 
                System.Drawing.Color.Tomato);
            
            this.m_font.DrawText(null,
                "directX" + Environment.NewLine + "fjklsdjfla",
                new Rectangle(400, 320, 500, 400),
                DrawTextFormat.Left | DrawTextFormat.Top, 
                System.Drawing.Color.Green);

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
