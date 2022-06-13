using UniRx;
public interface IPlayerServerSendAdapter
{
    // プレイヤー生成可能の通知
    Subject<int> OnPlayerCreateSubject { get; }

    // websocket接続
    void connection();

    // websocket送信
    void Send(SendPacket packet);
}