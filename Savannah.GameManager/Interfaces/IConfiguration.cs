namespace Savannah.Common
{
    public interface IConfiguration
    {
        int GetGameFieldSize();
        string GetNameOfAntelope();
        string GetNameOfLion();
        string GetNameOfExitKey();
        string GetAssemblyPath();
    }
}