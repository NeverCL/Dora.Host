using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Agent.Handle.Camera
{
    public class CameraHandle : BaseHandle
    {
        private Bitmap photo;

        public override async Task ReceiveAsync(Context context)
        {
            int index;
            int.TryParse(context.Data.ToString(), out index);
            photo = await new Camera(index).SnapShot();
        }

        public override Task SendAsync(Context context)
        {
            context.Data = photo;
            return base.SendAsync(context);
        }
    }

    public class Camera
    {
        private int _deviceIndex;

        public Camera(int index)
        {
            _deviceIndex = index;
        }

        public async Task<Bitmap> SnapShot()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var name = videoDevices[_deviceIndex].MonikerString;
            var videoSource = new VideoCaptureDevice(name);
            videoSource.ProvideSnapshots = true;
            bool isShot = false; Bitmap photo = null;
            videoSource.NewFrame += (s, e) =>
            {
                isShot = true;
                photo = e.Frame;
            };
            videoSource.Start();
            await Task.Run(() =>
            {
                Thread.Sleep(10);
                while (true)
                    if (isShot)
                    {
                        videoSource.SignalToStop();
                        break;
                    }
            });
            return photo;
        }
    }
}
