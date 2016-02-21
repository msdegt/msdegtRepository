namespace SmartHouse
{
    public interface ISetChannel
    {
        void NextChannel();
        void PreviousChannel();
        void EarlyChannel();
        void GoToChannel(string input);
    }
}