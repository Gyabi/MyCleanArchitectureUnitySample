using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクト位置、回転、スケール変更
/// </summary>
public class ObjectTransformChamger : MonoBehaviour
{  
    public static void TransformChange(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
}
