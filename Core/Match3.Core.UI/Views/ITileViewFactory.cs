using Match3.Core.UI.Presenters;

namespace Match3.Core.UI.Views {
    interface ITileViewFactory
    {
        ITileView CreateInitial(IBoardPresenter presenter, string type, int x, int y);
        ITileView Create(IBoardPresenter presenter, string type, int x, int y);
    }
}
