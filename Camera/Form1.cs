using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Camera
{
    public partial class Form1 : Form
    {

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cbCamera.Items.Add(filterInfo.Name);
            //cbCamera.SelectedIndex = cbCamera.Items.Count - 1;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void updateScreenCamera()
        {
            videoCaptureDevice.Stop();
            picScreen.Image = null;
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cbCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDeviceNewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDeviceNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picScreen.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
                videoCaptureDevice.Stop();
        }

        private void cbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateScreenCamera();
        }
    }
}
