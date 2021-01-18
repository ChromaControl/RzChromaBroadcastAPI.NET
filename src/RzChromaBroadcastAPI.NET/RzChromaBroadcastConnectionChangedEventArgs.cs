namespace Razer.Chroma.Broadcast
{
    public class RzChromaBroadcastConnectionChangedEventArgs
    {
        public bool Connected { get; }

        public RzChromaBroadcastConnectionChangedEventArgs(bool connected)
        {
            Connected = connected;
        }
    }
}
