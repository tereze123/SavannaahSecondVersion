using System.Collections.Generic;

namespace Savannah.PositionOnField.Factories
{
    public class PositionOnFieldFactory : IPositionOnFieldFactory
    {
        public List<AccessLibraryForPlugins.PositionOnField> GetNewListOfPositionsOnField()
        {
            return new List<AccessLibraryForPlugins.PositionOnField>();
        }

        public AccessLibraryForPlugins.PositionOnField GetNewPositionOnField()
        {
            return new AccessLibraryForPlugins.PositionOnField();
        }

        public AccessLibraryForPlugins.PositionOnField GetNewPositionOnField(int row, int column)
        {
            return new AccessLibraryForPlugins.PositionOnField(row, column);
        }
    }
}
