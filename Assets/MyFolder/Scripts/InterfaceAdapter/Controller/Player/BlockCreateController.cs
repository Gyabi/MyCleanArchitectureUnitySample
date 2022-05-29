using UniRx;
public class BlockCreateController :IBlockCreateController
{
    private Subject<BlockPoint> _onBlockCreateSubject = new Subject<BlockPoint>();
    public BlockCreateController(IBlockCreateView blockCreateView)
    {
        blockCreateView.OnBlockCreateSubject.Subscribe(point =>
        {
            _onBlockCreateSubject.OnNext(point);
        });
    }

    public Subject<BlockPoint> OnBlockCreateSubject
    {
        get
        {
            return _onBlockCreateSubject;
        }
    }
} 