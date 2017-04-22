using System;

namespace Match3.Core.Domain
{
    internal class Cell
    {
        private Tile tile;
        private bool isHorizontalDirty;
        private bool isVerticalDirty;

        public bool IsDirty => this.isHorizontalDirty || this.isVerticalDirty;
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

                this.isHorizontalDirty = true;
                this.isVerticalDirty = true;
                this.tile = value;
            }
        }

        public bool IsEmpty => this.tile == null;

        private Cell()
        {
            this.isHorizontalDirty = true;
            this.isVerticalDirty = true;
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
            this.isHorizontalDirty = true;
            this.isVerticalDirty = true;
        }

        public void MarkHorizontalClean()
        {
            this.isHorizontalDirty = false;
        }

        public void MarkVerticalClean()
        {
            this.isVerticalDirty = false;
        }
    }
}
