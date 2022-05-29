using UniRx;
public interface IBlockModeView
{
    Subject<Unit> OnBlockModeToCreateSubject
    {
        get;
    }
    Subject<Unit> OnBlockModeToDeleteSubject
    {
        get;
    }
}