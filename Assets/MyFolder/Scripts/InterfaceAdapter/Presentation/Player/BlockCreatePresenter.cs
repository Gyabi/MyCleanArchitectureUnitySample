public class BlockCreatePresenter : IBlockCreatePresetner
{
    private IBlockCreateUIView _view;

    public BlockCreatePresenter(IBlockCreateUIView view)
    {
        this._view = view;
    }

    public void ShowBlockCreateUI()
    {
        this._view.ShowBlockCreateUI();
    }

    public void HideBlockCreateUI()
    {
        this._view.HideBlockCreateUI();
    }
}