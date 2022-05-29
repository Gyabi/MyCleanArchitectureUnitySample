using UniRx;
public interface IBlockCreateController
{
    Subject<BlockPoint> OnBlockCreateSubject {get;}
}