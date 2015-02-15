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
        private VertexBuffer vertexBuffer = null;

        public bool Setup(MainForm mainForm)
        {
            
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

            this.m_device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -10.0f),
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));

            this.m_device.Transform.Projection = Matrix.PerspectiveFovLH(
                Geometry.DegreeToRadian(60.0f),
                (float)this.m_device.Viewport.Width / (float)this.m_device.Viewport.Height,
                1.0f, 100.0f);

            this.vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored),
                3, this.m_device, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);

            CustomVertex.PositionColored[] vertex = new CustomVertex.PositionColored[3];
            vertex[0] = new CustomVertex.PositionColored(0.0f, 5.0f, 0.0f, System.Drawing.Color.Red.ToArgb());
            vertex[1] = new CustomVertex.PositionColored(4.0f, -3.0f, 0.0f, System.Drawing.Color.Blue.ToArgb());
            vertex[2] = new CustomVertex.PositionColored(-4.0f, -3.0f, 0.0f, System.Drawing.Color.Green.ToArgb());

            using (GraphicsStream data  = this.vertexBuffer.Lock(0,0,LockFlags.None))
            {
                data.Write(vertex);
                this.vertexBuffer.Unlock();
            }
            this.m_device.RenderState.Lighting = false;
            return true;
        }

        /// <summary>
        /// 메인 루프 처리
        /// </summary>
        public void Update()
        {            
            this.m_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer,
                System.Drawing.Color.DarkBlue, 1.0f, 0);
            this.m_device.BeginScene();

            this.m_device.SetStreamSource(0, this.vertexBuffer, 0);
            this.m_device.VertexFormat = CustomVertex.PositionColored.Format;
            this.m_device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

            this.m_font.DrawText(null, "3D triangle", 0, 0, System.Drawing.Color.White);
            this.m_device.EndScene();
            this.m_device.Present();
        }

        /// <summary>
        /// 자원 파기
        /// </summary>
        void IDisposable.Dispose()
        {
            if (this.vertexBuffer != null)
            {
                this.vertexBuffer.Dispose();
            }
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
