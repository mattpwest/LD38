namespace Match3.Core.UI.Views {
    public interface ITileView {
        int X { get; }
        int Y { get; }

        void Fall(int x, int y);

        void Move(int x, int y);

        void Destroy(int tileMatchValue);
    }
}
