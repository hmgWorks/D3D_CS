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
        private MainForm m_mainForm = null;
        private CustomVertex.TransformedColored[] vertex = new CustomVertex.TransformedColored[3];

        public bool Setup(MainForm mainForm)
        {
            m_mainForm = mainForm;
            try
            {
                this.CreateDevice(mainForm);
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

            this.vertex[0] = new CustomVertex.TransformedColored(
                200.0f, 50.0f, 0.0f, 1.0f,
                System.Drawing.Color.Red.ToArgb());
            this.vertex[1] = new CustomVertex.TransformedColored(
                350.0f, 50.0f + 150.0f *(float)Math.Sqrt(3), 0.0f, 1.0f,
                System.Drawing.Color.Blue.ToArgb());
            this.vertex[2] = new CustomVertex.TransformedColored(
                50.0f, 50.0f+150.0f*(float)Math.Sqrt(3), 0.0f, 1.0f,
                System.Drawing.Color.Green.ToArgb());
            return true;
        }

        /// <summary>
        /// 메인 루프 처리
        /// </summary>
        public void Update()
        {            
            this.m_device.Clear(ClearFlags.Target, System.Drawing.Color.DarkBlue, 1.0f, 0);
            this.m_device.BeginScene();

            this.m_device.VertexFormat = CustomVertex.TransformedColored.Format;
            this.m_device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, vertex);

            this.m_font.DrawText(null, "2D 삼각형의 출력", 0, 0, System.Drawing.Color.White);

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
