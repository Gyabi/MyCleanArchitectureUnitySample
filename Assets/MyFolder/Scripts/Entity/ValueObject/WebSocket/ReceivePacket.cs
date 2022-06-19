using UnityEngine;
public class ReceivePacket
{
    public int _id;
    public bool disconnection_message;
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;

    public ReceivePacket(int id, bool disconnection_message, float position_x, float position_y, float position_z, float rotation_x, float rotation_y, float rotation_z)
    {
        this._id = id;
        this.disconnection_message = disconnection_message;
        this.position_x = position_x;
        this.position_y = position_y;
        this.position_z = position_z;
        this.rotation_x = rotation_x;
        this.rotation_y = rotation_y;
        this.rotation_z = rotation_z;
    }
}