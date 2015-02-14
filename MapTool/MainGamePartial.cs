using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MapTool
{
    public partial class MainGame
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
        public void CreateDevice(MainForm mainForm)
        {
            this.m_mainForm = mainForm;

            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            try
            {
                this.CreateDevice(mainForm, pp);
            }
            catch (DirectXException ex)
            {
                
                throw ex;
            }
            
        }

        private void CreateDevice(MainForm mainForm, PresentParameters pp)
        {
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
                        throw ex3;
                    }
                }
            }            
        }        
    }
}
