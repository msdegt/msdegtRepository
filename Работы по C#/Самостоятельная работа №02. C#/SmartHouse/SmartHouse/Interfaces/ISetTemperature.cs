namespace SmartHouse
{
    public interface ISetTemperature : ITemperature
    {
        void SetLevelTemperature(double input);
    }
}