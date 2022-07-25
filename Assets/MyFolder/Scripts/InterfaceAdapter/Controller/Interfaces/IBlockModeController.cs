using UniRx;
public interface IBlockModeController
{
    Subject<Unit> OnBlockModeToCreateSubject { get; }
    Subject<Unit> OnBlockModeToDeleteSubject { get; }
}