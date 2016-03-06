namespace Database
{
    public interface IValidate
    {
        bool Check(string userText, string regex);
    }
}