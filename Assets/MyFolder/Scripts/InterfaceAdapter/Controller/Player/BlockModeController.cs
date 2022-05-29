using UniRx;
public class BlockModeController : IBlockModeController
{
    private Subject<Unit> _onBlockModeToCreateSubject = new Subject<Unit>();
    public Subject<Unit> OnBlockModeToCreateSubject
    {
        get
        {
            return _onBlockModeToCreateSubject;
        }
    }
    private Subject<Unit> _onBlockModeToDeleteSubject = new Subject<Unit>();
    public Subject<Unit> OnBlockModeToDeleteSubject
    {
        get
        {
            return _onBlockModeToDeleteSubject;
        }
    }

    public BlockModeController(IBlockModeView blockModeView)
    {
        blockModeView.OnBlockModeToCreateSubject.Subscribe(_ =>
        {
            _onBlockModeToCreateSubject.OnNext(Unit.Default);
        });

        blockModeView.OnBlockModeToDeleteSubject.Subscribe(_ =>
        {
            _onBlockModeToDeleteSubject.OnNext(Unit.Default);
        });
    }
}