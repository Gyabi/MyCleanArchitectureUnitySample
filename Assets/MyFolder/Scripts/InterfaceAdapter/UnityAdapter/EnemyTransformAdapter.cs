using UnityEngine;
public class EnemyTransformAdapter : IObjectTransformChangeAdapter
{
    public void TransformChange(Player enemy, ReceivePacket packet)
    {
        Vector3 position = new Vector3(packet.position_x, packet.position_y, packet.position_z);
        Quaternion rotation = Quaternion.Euler(packet.rotation_x, packet.rotation_y, packet.rotation_z);
        ObjectTransformChamger.TransformChange(enemy._obj, position, rotation);
    }
}
