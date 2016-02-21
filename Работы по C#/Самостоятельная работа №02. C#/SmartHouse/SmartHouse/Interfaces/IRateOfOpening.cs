namespace SmartHouse
{
    public interface IRateOfOpening
    {
        bool StatusOpen { get; set; }
        void Open();
        void Close();
    }
}