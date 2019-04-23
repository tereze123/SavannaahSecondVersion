using System.Collections.Generic;

namespace Savannah.PositionOnField.Factories
{
    public interface IPositionOnFieldFactory
    {
        AccessLibraryForPlugins.PositionOnField GetNewPositionOnField();
        AccessLibraryForPlugins.PositionOnField GetNewPositionOnField(int row, int column);
        List<AccessLibraryForPlugins.PositionOnField> GetNewListOfPositionsOnField();
    }
}