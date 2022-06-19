using UniRx;
public class EnemyServerReceiveAdapter : IEnemyServerReceiveAdaper
{
    WebSocketClient _webSocketClient;

    private Subject<ReceivePacket> _onReceiveSubject = new Subject<ReceivePacket>();
    public Subject<ReceivePacket> OnReceiveSubject{
        get { return this._onReceiveSubject; }

    }

    private Subject<int> _onDisconnectionSubject = new Subject<int>();
    public Subject<int> OnDisconnectionSubject{
        get { return this._onDisconnectionSubject; }
    }


    public EnemyServerReceiveAdapter(WebSocketClient webSocketClient)
    {
        _webSocketClient = webSocketClient;
        _webSocketClient.OnReceiveSubject.ObserveOnMainThread()
        .Subscribe(
            (PacketJsonData data) =>
            {
                OnReceiveSubject.OnNext(data.CreateReceivePacket());
            }
        );

        _webSocketClient.OnDisconnectionSubject.ObserveOnMainThread()
        .Subscribe(
            (int id) =>
            {
                OnDisconnectionSubject.OnNext(id);
            }
        );
    }
}