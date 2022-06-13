// playerユースケースと送信処理をつなげる
using UniRx;
using UnityEngine;
public class PlayerServerSendAdapter : IPlayerServerSendAdapter
{
    WebSocketClient _webSocketClient;

    public Subject<int> OnPlayerCreateSubject {
        get { return _webSocketClient.OnPlayerCreateSubject; }
    }

    public PlayerServerSendAdapter(WebSocketClient webSocketClient)
    {
        _webSocketClient = webSocketClient;
    }
    
    /// <summary>
    /// webSocket接続開始
    /// </summary>
    public void connection()
    {
        _webSocketClient.Connect();
    }

    /// <summary>
    /// webSocketで送信
    /// </summary>
    /// <param name="packet">送信するパケットドメイン</param>
    public void Send(SendPacket packet)
    {
        // パケットドメインからjson互換を持つパケットクラスを作成して送信
        PacketJsonData data = new PacketJsonData();
        data.SetBySendPacket(packet);
        _webSocketClient.SendJson(data);
    }
}