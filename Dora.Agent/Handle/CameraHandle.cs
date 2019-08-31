using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Handle
{
    public class CameraHandle : BaseHandle
    {
        public override Task ReceiveAsync(Context context)
        {
            var index = 0;
            int.TryParse(context.Data.ToString(), out index);
            new Camera(index).Start();
            return base.ReceiveAsync(context);
        }
    }

    public class Camera
    {
        private int _deviceIndex;

        public Camera(int index)
        {
            this._deviceIndex = index;
        }

        public void Start()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var name = videoDevices[_deviceIndex].MonikerString;
            var videoSource = new VideoCaptureDevice(name);
            videoSource.ProvideSnapshots = true;
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
            videoSource.SignalToStop();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
