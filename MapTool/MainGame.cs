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
    /// <summary>
    /// MainGame class
    /// </summary>
    class MainGame:IDisposable
    {
        /// <summary>
        /// 메인폼
        /// </summary>
        private MainForm m_mainForm = null;
        private Device m_device = null;

        /// <summary>
        /// 어플리케이션 초기화
        /// </summary>
        /// <param name="mainForm"></param>
        /// <returns>초가화가 하나라도 실패하면 false를 리턴한다.</returns>
        public bool initDevice(MainForm mainForm)
        {
            this.m_mainForm = mainForm;

            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            try
            {
                this.m_device = new Device(0,
                    DeviceType.Hardware,
                    m_mainForm, 
                    CreateFlags.HardwareVertexProcessing, 
                    pp);
            }
            catch (DirectXException ex1)
            {
                Debug.WriteLine(ex1.ToString());
                try
                {
                    this.m_device = new Device(0,
                        DeviceType.Hardware,
                        m_mainForm.Handle,
                        CreateFlags.SoftwareVertexProcessing,
                        pp);
                }
                catch (DirectXException ex2)
                {
                    Debug.WriteLine(ex2.ToString());
                    try
                    {
                        this.m_device = new Device(0,
                            DeviceType.Reference,
                            m_mainForm.Handle,
                            CreateFlags.SoftwareVertexProcessing,
                            pp);
                    }
                    catch (DirectXException ex3)
                    {
                        MessageBox.Show(ex3.ToString(),
                            "error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return false;
                    }
                }
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
