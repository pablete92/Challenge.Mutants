namespace Challenge.Mutants.Application.Models
{
    public class Position
    {
        public int Width { get; }
        public int Height { get; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Direction Direction { get; }

        public Position(int width, int height, int positionX, int positionY, Direction direction)
        {
            Width = width;
            Height = height;
            PositionX = positionX;
            PositionY = positionY;
            Direction = direction;
        }

        public Position Clone(int positionX, int positionY, Direction direction)
        {
            return new Position(Width, Height, positionX, positionY, direction);
        }
    }

    public enum Direction
    {
        Center,
        Horizontal,
        DiagonalRight,
        Vertical,
        DiagonalLeft
    }
}
