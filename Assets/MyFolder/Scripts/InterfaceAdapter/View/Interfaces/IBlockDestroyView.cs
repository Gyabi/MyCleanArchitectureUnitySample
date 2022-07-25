using UniRx;
using UnityEngine;
public interface IBlockDestroyView
{
    Subject<GameObject> OnBlockDestroySubject
    {
        get;
    }
}