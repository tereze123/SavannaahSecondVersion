
namespace Savannah.Common
{
    public class Configuration
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
    }
}
