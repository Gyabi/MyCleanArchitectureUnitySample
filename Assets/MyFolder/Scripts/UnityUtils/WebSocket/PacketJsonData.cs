using UnityEngine;
using System;

[Serializable]
public class PacketJsonData
{
    public bool connection_message;
    public int id;
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;


    public void  SetBySendPacket(SendPacket packet)
    {
        this.connection_message = false;
        this.id = packet.player._id;
        this.position_x = packet.player._obj.transform.position.x;
        this.position_y = packet.player._obj.transform.position.y;
        this.position_z = packet.player._obj.transform.position.z;
        this.rotation_x = packet.player._obj.transform.rotation.x;
        this.rotation_y = packet.player._obj.transform.rotation.y;
        this.rotation_z = packet.player._obj.transform.rotation.z;
    }
}