namespace Savannah.PositionOnField
{
    public interface IPositionOnFieldValidation
    {
        bool IsOutOfBounds(int rowNumber, int colNumber);
    }
}