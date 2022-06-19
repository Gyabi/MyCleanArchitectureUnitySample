using UnityEngine;
using System.Collections.Generic;
public class Player
{
    // public static List<int> _ids = new List<int>();
    public GameObject _obj;
    public int _id;

    public Player(GameObject obj, int id)
    {
        this._obj = obj;
        this._id = id;
        // _ids.Add(id);
    }
}