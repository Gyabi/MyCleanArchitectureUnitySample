using UniRx;
public interface IBlockCreateView
{
    Subject<BlockPoint> OnBlockCreateSubject
    {
        get;
    }
}