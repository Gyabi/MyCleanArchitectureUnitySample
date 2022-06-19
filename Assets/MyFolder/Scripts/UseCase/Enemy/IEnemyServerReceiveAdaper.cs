using UniRx;
public interface IEnemyServerReceiveAdaper
{
    // メッセージ受信の通知
    Subject<ReceivePacket> OnReceiveSubject { get; }

    // 別クライアント接続終了時
    Subject<int> OnDisconnectionSubject { get; }
}