
using Savannah.Common;
using System.ComponentModel.Composition;

namespace Savannah.PositionOnField
{
    public class PositionOnFieldValidation : IPositionOnFieldValidation
    {
        private readonly IConfiguration configuration;
        [ImportingConstructor]
        public PositionOnFieldValidation(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool IsOutOfBounds(int rowNumber, int colNumber)
        {
            int fieldSize = configuration.GetGameFieldSize();
            return (IsPositionRowOrColumnLessThanZero(rowNumber, colNumber)
                    || IsPositionRowOrColumnMoreOrEqualToFieldSize(rowNumber, colNumber, fieldSize)) ? true : false;
        }

        private bool IsPositionRowOrColumnLessThanZero(int rowNumber, int colNumber)
        {
            return (rowNumber < 0 || colNumber < 0) ? true : false;
        }

        private bool IsPositionRowOrColumnMoreOrEqualToFieldSize(int rowNumber, int colNumber, int fieldSize)
        {
            return (rowNumber >= fieldSize
                || colNumber >= fieldSize) ? true : false;
        }
    }
}
