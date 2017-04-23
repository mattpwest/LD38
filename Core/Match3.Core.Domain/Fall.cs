using System;

namespace Match3.Core.Domain
{
    public class Fall
    {
        public Cell From { get; }
        public Cell To { get; }
        public Tile Tile { get; }

        public Fall(Cell from, Cell to)
        {
            if(from.Y <= to.Y || from.X != to.X)
            {
                throw new InvalidOperationException("Tile can't fall up or accross columns");
            }

            this.From = from;
            this.To = to;
            this.Tile = from.Tile;

            this.To.Tile = this.Tile;
            this.From.Clear();
        }
    }
}
