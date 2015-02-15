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
        private Texture texture = null;

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
            this.SetupCamera();
            this.vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionTextured),
                4, this.m_device, Usage.None, CustomVertex.PositionTextured.Format, Pool.Managed);

            CustomVertex.PositionTextured[] vertex = new CustomVertex.PositionTextured[4];

            vertex[0] = new CustomVertex.PositionTextured(-4.0f, 4.0f, 0.0f, 0.0f, 0.0f);
            vertex[1] = new CustomVertex.PositionTextured(4.0f, 4.0f, 0.0f, 1.0f, 0.0f);
            vertex[2] = new CustomVertex.PositionTextured(-4.0f, -4.0f, 0.0f, 0.0f, 1.0f);
            vertex[3] = new CustomVertex.PositionTextured(4.0f, -4.0f, 0.0f, 1.0f, 1.0f);

            using (GraphicsStream data = this.vertexBuffer.Lock(0,0,LockFlags.None))
            {
                data.Write(vertex);
                this.vertexBuffer.Unlock();
            }

            this.texture = TextureLoader.FromFile(this.m_device, "phm.jpg");
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

            this.m_device.SetTexture(0, this.texture);
            this.m_device.SetStreamSource(0, this.vertexBuffer, 0);
            this.m_device.VertexFormat = CustomVertex.PositionTextured.Format;
            this.m_device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);

            this.m_font.DrawText(null, "texture", 0, 0, System.Drawing.Color.White);

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
