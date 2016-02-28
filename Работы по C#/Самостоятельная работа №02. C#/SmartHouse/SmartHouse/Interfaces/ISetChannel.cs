namespace SmartHouse
{
    public interface ISetChannel
    {
        int MAXchannel { get; set; }
        void NextChannel();
        void PreviousChannel();
        void EarlyChannel();
        void GoToChannel(int input);
    }
}