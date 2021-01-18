using System.Drawing;

namespace Razer.Chroma.Broadcast
{
    public class RzChromaBroadcastColorChangedEventArgs
    {
        public Color[] Colors { get; }

        public RzChromaBroadcastColorChangedEventArgs(Color[] colors)
        {
            Colors = colors;
        }
    }
}
