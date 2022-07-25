using UniRx;
using UnityEngine;
public interface IBlockDestroyController
{
    Subject<GameObject> OnBlockDestroySubject { get; }
}