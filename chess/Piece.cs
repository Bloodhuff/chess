using System.Windows.Controls;

namespace chess
{
    public class Piece
    {
        public string Color { get; set; }

        public Grid Position { get; set; }
        public Canvas Img { get; set; }
        public string PieceType { get; set; }
        public bool HasMoved { get; set; }
        readonly GetCanvas _img = new GetCanvas();
        public MoveSet MoveSet;

        public Piece(string color, string pieceType, Grid position = null)
        {
            Color = color;
            Position = position;
            PieceType = pieceType;
            Img = _img.Canvas(Color, PieceType);
        }
    }
}
