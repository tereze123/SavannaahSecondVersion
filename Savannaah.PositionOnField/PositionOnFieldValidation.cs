using Savannah.GameField;

namespace Savannaah.PositionOnField
{
    public class PositionOnFieldValidation
    {
        private readonly GameField gameField;

        public PositionOnFieldValidation(GameField gameField)
        {
            this.gameField = gameField;
        }
        public bool IsOutOfBounds(PositionOnField positionOnField)
        {
            int fieldSize = gameField.GetGameFieldSize();
            return (IsPositionRowOrColumnLessThanZero(positionOnField)
                    || IsPositionRowOrColumnMoreOrEqualToFieldSize(positionOnField, fieldSize)) ? true : false;                
        }

        private bool IsPositionRowOrColumnLessThanZero(PositionOnField positionOnField)
        {
            return (positionOnField.RowPosition < 0 || positionOnField.ColumnPosition < 0) ? true : false;
        }

        private bool IsPositionRowOrColumnMoreOrEqualToFieldSize(PositionOnField positionOnField, int fieldSize)
        {
            return (positionOnField.RowPosition >= fieldSize 
                || positionOnField.ColumnPosition >= fieldSize) ? true : false;
        }
    }
}
