using UnityEngine;
using System.Collections.Generic;
public class Player
{
    public GameObject _obj;
    public static List<int> _ids = new List<int>();
    public int _id;

    public Player(GameObject obj)
    {
        _obj = obj;
        _id = _ids.Count;
        _ids.Add(_id);
    }
}