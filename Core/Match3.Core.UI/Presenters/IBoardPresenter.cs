using Match3.Core.UI.Views;

namespace Match3.Core.UI.Presenters {
    public interface IBoardPresenter
    {
        void Grabbed(ITileView tileView);

        void Moved(int x, int y);

        void Released();
    }
}
