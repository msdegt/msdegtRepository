namespace SmartHouse
{
    public interface ISetVolume
    {
        int CurrentVolume { get; set; }
        void SetVolume(int input);
        void MaxVolume();
        void MinVolume();
        void SetMute();
    }
}