using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Razer.Chroma.Broadcast
{
    public class RzChromaBroadcastAPI : IDisposable
    {
        private const int maxColors = 5;

        private RzChromaBroadcastAPINative.RegisterEventNotificationCallback notificationCallback;

        public event EventHandler<RzChromaBroadcastColorChangedEventArgs> ColorChanged;

        public event EventHandler<RzChromaBroadcastConnectionChangedEventArgs> ConnectionChanged;

        public RzResult Init(Guid guid)
        {
            var i = new BigInteger(guid.ToByteArray());

            var a = (uint)((i >> 96) & 0xFFFFFFFF);
            var b = (uint)((i >> 64) & 0xFFFFFFFF);
            var c = (uint)((i >> 32) & 0xFFFFFFFF);
            var d = (uint)((i >> 0) & 0xFFFFFFFF);
            try
            {
                var initResult = RzChromaBroadcastAPINative.Init(a, b, c, d);

                if (initResult == RzResult.SUCCESS)
                {
                    notificationCallback = new RzChromaBroadcastAPINative.RegisterEventNotificationCallback(EventNotificationCallback);
                    initResult = RzChromaBroadcastAPINative.RegisterEventNotification(notificationCallback);
                }

                return initResult;
            } catch (SEHException e)
			{
                return RzResult.FAILED;
            }
        }

        public RzResult UnInit()
        {
            return RzChromaBroadcastAPINative.UnInit();
        }

        private int EventNotificationCallback(int message, IntPtr data)
        {
            if (message == 1)
            {
                if (data != IntPtr.Zero)
                {
                    Color[] colors = new Color[maxColors];
                    int[] colorData = new int[maxColors];

                    Marshal.Copy(data, colorData, 0, maxColors);

                    for (int i = 0; i < colorData.Length; i++)
                    {
                        int r = (byte)((colorData[i] >> 0) & 0xff);
                        int g = (byte)((colorData[i] >> 8) & 0xff);
                        int b = (byte)((colorData[i] >> 16) & 0xff);

                        colors[i] = Color.FromArgb(r, g, b);
                    }

                    ColorChanged?.Invoke(this, new RzChromaBroadcastColorChangedEventArgs(colors));
                }
            }
            else if (message == 2)
            {
                ConnectionChanged?.Invoke(this, new RzChromaBroadcastConnectionChangedEventArgs(data.ToInt32() == 1));
            }

            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            RzChromaBroadcastAPINative.UnRegisterEventNotification();
            RzChromaBroadcastAPINative.UnInit();
        }
    }
}
