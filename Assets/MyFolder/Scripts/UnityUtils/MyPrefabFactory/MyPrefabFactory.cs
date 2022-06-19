using UnityEngine;
using System.Collections.Generic;
public class MyPrefabFactory : MonoBehaviour
{
    [SerializeField, Tooltip("player prefab")]
    GameObject _playerPrefab;

    [SerializeField, Tooltip("player 生成ポイント1")]
    GameObject _playerSpawnPoint;

    [SerializeField, Tooltip("block prefab")]
    GameObject _blockPrefab;

    [SerializeField, Tooltip("enemy prefab")]
    GameObject _enemyPrefab;
    private List<GameObject> _createdEnemies = new List<GameObject>();

    private List<GameObject> _blocks = new List<GameObject>();

    /// <summary>
    /// プレイヤー生成
    /// </summary>
    /// <returns></returns>
    public GameObject CreatePlayer()
    {
        GameObject obj = Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);
        return obj;
    }

    /// <summary>
    /// ブロック生成
    /// </summary>
    /// <param name="point"></param>
    public void CreateBlock(BlockPoint point)
    {
        var block = Instantiate(_blockPrefab, new Vector3(point.x, point.y, point.z), Quaternion.identity) as GameObject;
        block.transform.parent = transform;

        _blocks.Add(block);
    }

    /// <summary>
    /// ブロック削除
    /// </summary>
    /// <param name="block"></param>

    public void DestroyBlock(GameObject block)
    {
        _blocks.Remove(block);
        Destroy(block);
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    /// <returns></returns>
    public GameObject CreateEnemy()
    {
        GameObject obj = Instantiate(_enemyPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);
        _createdEnemies.Add(obj);
        return obj;
    }
}
