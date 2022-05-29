using UniRx;
using UnityEngine;
public class BlockDestroyController : IBlockDestroyController
{
    private Subject<GameObject> _onBlockDestroySubject = new Subject<GameObject>();
    public Subject<GameObject> OnBlockDestroySubject
    {
        get
        {
            return _onBlockDestroySubject;
        }
    }

    public BlockDestroyController(IBlockDestroyView blockDestroyView)
    {
        blockDestroyView.OnBlockDestroySubject.Subscribe(block =>
        {
            _onBlockDestroySubject.OnNext(block);
        });
    }
}