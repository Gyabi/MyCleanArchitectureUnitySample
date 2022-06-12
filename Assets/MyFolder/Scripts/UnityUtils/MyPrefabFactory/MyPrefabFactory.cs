using UnityEngine;
using System.Collections.Generic;
public class MyPrefabFactory : MonoBehaviour
{
    [SerializeField]
    GameObject _playerPrefab;

    [SerializeField]
    GameObject _playerSpawnPoint;

    [SerializeField]
    GameObject _blockPrefab;

    private List<GameObject> _blocks = new List<GameObject>();

    public GameObject CreatePlayer()
    {
        GameObject obj = Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);
        return obj;
    }

    public void CreateBlock(BlockPoint point)
    {
        var block = Instantiate(_blockPrefab, new Vector3(point.x, point.y, point.z), Quaternion.identity) as GameObject;
        block.transform.parent = transform;

        _blocks.Add(block);
    }

    public void DestroyBlock(GameObject block)
    {
        _blocks.Remove(block);
        Destroy(block);
    }
}
