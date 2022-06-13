// playerユースケースと送信処理をつなげる
using UniRx;
using UnityEngine;
public class PlayerServerSendAdapterTest : IPlayerServerSendAdapter
{
    public Subject<int> OnPlayerCreateSubject { get; }
    public PlayerServerSendAdapterTest()
    {
        // Debug.Log("PlayerServerSendAdapterTest");
    }
    public void connection()
    {
    }
    public void Send(SendPacket packet)
    {
        Debug.Log(packet.player._obj.transform.position);
    }
}