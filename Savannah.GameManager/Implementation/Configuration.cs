namespace Savannah.Common
{
    public class Configuration : IConfiguration
    {
        public int GetGameFieldSize()
        {
            return int.Parse(Resources.FieldSize);
        }

        public string GetNameOfLion()
        {
            return Resources.LionName;
        }
        public string GetNameOfAntelope()
        {
            return Resources.AntelopeName;
        }

        public string GetNameOfExitKey()
        {
            return Resources.ExitGameKey;
        }

        public string GetAssemblyPath()
        {
            return Resources.AssemblyPath;
        }
    }
}
