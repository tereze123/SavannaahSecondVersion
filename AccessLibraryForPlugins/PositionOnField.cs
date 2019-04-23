namespace AccessLibraryForPlugins
{
    public class PositionOnField
    {
        public PositionOnField()
        {
            RowPosition = default(int);
            ColumnPosition = default(int);
            IsEnemyInViewRange = default(bool);
        }

        public PositionOnField(int rowPosition, int columnPosition)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }

        public int RowPosition { get; set; }

        public int ColumnPosition { get; set; }

        public bool IsEnemyInViewRange;
    }
}
