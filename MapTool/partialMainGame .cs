﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Diagnostics;

namespace MapTool
{
    public partial class MainGame
    { 
        private MainForm m_mainForm = null;
        private Device m_device = null;

        private void CreateDevice(MainForm mainForm)
        { 
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
                this.m_device = new Device(0, DeviceType.Hardware, mainForm.Handle,
                    CreateFlags.HardwareVertexProcessing, pp);
            }
            catch (DirectXException ex1)
            {
                Debug.WriteLine(ex1.ToString());
                try
                {
                    this.m_device = new Device(0, DeviceType.Hardware, mainForm.Handle,
                        CreateFlags.SoftwareVertexProcessing, pp);
                }
                catch (DirectXException ex2)
                {
                    Debug.WriteLine(ex2.ToString());
                    try
                    {             
                        this.m_device = new Device(0, DeviceType.Reference, mainForm.Handle,
                            CreateFlags.SoftwareVertexProcessing, pp);
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