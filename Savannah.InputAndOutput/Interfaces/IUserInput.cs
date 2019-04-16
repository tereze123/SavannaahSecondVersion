namespace Savannah.InputAndOutput
{
    public interface IUserInput
    {
        bool IsKeyPressed();
        string ReturnKeyPressed();
    }
}