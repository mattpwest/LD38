using System;

namespace Match3.Core.Domain
{
    public struct Match
    {
        public int StartX { get; }
        public int StartY { get; }
        public int EndX { get; }
        public int EndY { get; }
        public int Length { get; }

        public Match(int startX, int startY, int endX, int endY)
        {
            if(startX != endX && startY != endY)
            {
                throw new ArgumentException("diagonal matches are not allowed");
            }

            this.StartX = startX;
            this.StartY = startY;
            this.EndX = endX;
            this.EndY = endY;
            this.Length = Math.Abs(this.StartX - this.EndX) + Math.Abs(this.StartY - this.EndY) + 1;
        }

        public bool CheckInMatch(int xInMatch, int yInMatch)
        {
            return xInMatch >= StartX && xInMatch <= EndX && yInMatch >= StartY && yInMatch <= EndY;
        }
    }
}
