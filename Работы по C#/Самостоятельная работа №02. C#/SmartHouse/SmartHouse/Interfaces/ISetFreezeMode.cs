namespace SmartHouse
{
    public interface ISetFreezeMode
    {
        void SetLowFreeze();
        void SetColderFreezing();
        void SetDeepFreeze();
        void SetDefrost();
    }
}