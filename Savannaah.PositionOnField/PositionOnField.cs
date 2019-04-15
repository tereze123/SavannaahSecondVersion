namespace Savannaah.PositionOnField
{
    public class PositionOnField
    {
        public PositionOnField()
        {

        }

        public PositionOnField(int rowPosition, int columnPosition)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }

        public int RowPosition { get; set; }

        public int ColumnPosition { get; set; }
    }
}
