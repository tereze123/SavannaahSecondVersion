
using Savannah.Common;
namespace Savannah.PositionOnField
{
    public class PositionOnFieldValidation
    {
        private readonly IConfiguration configuration;

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
