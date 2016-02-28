namespace SmartHouse
{
    public interface ICreate
    {
        Refrigerator CreateRef();
        Television CreateTv();
        WindowShutters CreateShut();
        Boiler CreateBoiler();
        WateringSystem CreateWs();
    }
}