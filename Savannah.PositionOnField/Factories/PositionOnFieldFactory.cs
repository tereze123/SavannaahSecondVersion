using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Savannah.PositionOnField.Factories
{
    public class PositionOnFieldFactory : IPositionOnFieldFactory
    {
        public List<PositionOnField> GetNewListOfPositionsOnField()
        {
            return new List<PositionOnField>();
        }

        public PositionOnField GetNewPositionOnField()
        {
            return new PositionOnField();
        }

        public PositionOnField GetNewPositionOnField(int row, int column)
        {
            return new PositionOnField(row, column);
        }
    }
}
