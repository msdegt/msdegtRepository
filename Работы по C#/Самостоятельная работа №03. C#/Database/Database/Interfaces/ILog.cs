namespace Database
{
    public interface ILog
    {
        void LogSave(object entity);
        object LogOut();
    }
}