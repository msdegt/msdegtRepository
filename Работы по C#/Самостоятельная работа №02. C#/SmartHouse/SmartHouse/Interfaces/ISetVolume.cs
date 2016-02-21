namespace SmartHouse
{
    public interface ISetVolume
    {
        void SetVolume(string input);
        void MaxVolume();
        void MinVolume();
        void SetMute();
    }
}