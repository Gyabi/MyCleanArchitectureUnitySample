// playerユースケースと送信処理をつなげる

using UnityEngine;
public class PlayerServerSendAdapterTest : IPlayerServerSendAdapter
{
    public PlayerServerSendAdapterTest()
    {
        Debug.Log("PlayerServerSendAdapterTest");
    }
    public void Send(SendPacket packet)
    {
        Debug.Log(packet.player._obj.transform.position);
    }
}