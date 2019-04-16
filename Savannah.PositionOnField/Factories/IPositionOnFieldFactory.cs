using System.Collections.Generic;

namespace Savannah.PositionOnField.Factories
{
    public interface IPositionOnFieldFactory
    {
        PositionOnField GetNewPositionOnField();
        PositionOnField GetNewPositionOnField(int row, int column);
        List<PositionOnField> GetNewListOfPositionsOnField();
    }
}