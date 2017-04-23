using System;

namespace Match3.Core.Domain
{
    public class Cell
    {
        private Tile tile;

        public bool IsDirty => this.IsHorizontalDirty || this.IsVerticalDirty;
        public int X { get; }
        public int Y { get; }

        public Tile Tile
        {
            get => this.tile;
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException();
                }

                this.IsHorizontalDirty = true;
                this.IsVerticalDirty = true;
                this.tile = value;
            }
        }

        public bool IsEmpty => this.tile == null;

        public bool IsHorizontalDirty { get; private set; }

        public bool IsVerticalDirty { get; private set; }

        private Cell()
        {
            this.IsHorizontalDirty = true;
            this.IsVerticalDirty = true;
            this.tile = null;
        }

        public Cell(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public void Clear()
        {
            this.tile = null;
            this.IsHorizontalDirty = true;
            this.IsVerticalDirty = true;
        }

        public void MarkHorizontalClean()
        {
            this.IsHorizontalDirty = false;
        }

        public void MarkVerticalClean()
        {
            this.IsVerticalDirty = false;
        }
    }
}
