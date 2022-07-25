using UnityEngine;
public interface IObjectTransformChangeAdapter
{
    void TransformChange(Player enemy, ReceivePacket packet);
}