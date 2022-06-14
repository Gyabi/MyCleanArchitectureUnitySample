using UniRx;
using Zenject;
public class OnlinePlayerUseCase : IPlayerUseCase
{
    #region DI Objects
    // Player生成関連
    [Inject]
    private IPlayerFactoryAdapter _playerFactoryAdapter;
    // ブロック操作関連
    [Inject]
    private IBlockModeController _blockModeConroller;
    [Inject]
    private IBlockCreateController _blockCreateController;
    [Inject]
    private IBlockDestroyController _blockDestroyController;
    [Inject]
    private IBlockCreateAdapter _blockCreateAdapter;
    [Inject]
    private IBlockDestroyAdapter _blockDestroyAdapter;
    [Inject]
    private IBlockCreatePresetner _blockCreatePresetner;
    [Inject]
    private IBlockResourcePresenter _blockResourcePresenter;
    // 通信関連
    [Inject]
    private IPlayerServerSendAdapter _playerServerSendAdapter;
    #endregion


    #region ユースケース内オブジェクト
    // プレイヤーの状態を保持するクラス
    private PlayerState _playerState;

    // playerのオブジェクト情報をもったドメインモデル
    private Player _player;
    #endregion

    public OnlinePlayerUseCase()
    {
        this._playerState = new PlayerState();
    }

    /// <summary>
    /// prefabをインスタンス化する
    /// </summary> 
    public void Init()
    {
        // プレイヤー生成通知を購読
        this._playerServerSendAdapter.OnPlayerCreateSubject.ObserveOnMainThread()
        .Subscribe(
            (int id) =>
            {
                // プレイヤー生成
                this._player = this._playerFactoryAdapter.CreatePlayer(id);
        });

        // サーバに接続要求
        this._playerServerSendAdapter.connection();
        
        // 各入力の購読
        // ブロック生成命令
        this._blockCreateController.OnBlockCreateSubject.Subscribe(point =>
        {
            // 生成可能な状態なら
            if(this._playerState.CanCreateBlock())
            {
                this._blockCreateAdapter.CreateBlock(point);
            }
        });

        // Createモード切り替え命令
        this._blockModeConroller.OnBlockModeToCreateSubject.Subscribe(_ =>
        {
            this._playerState.ChangeBlockMode(BlockMode.Create);
            this._blockCreatePresetner.ShowBlockCreateUI();
        });

        // Deleteモード切り替え命令
        this._blockModeConroller.OnBlockModeToDeleteSubject.Subscribe(_ =>
        {
            this._playerState.ChangeBlockMode(BlockMode.Delete);
            this._blockCreatePresetner.HideBlockCreateUI();
        });

        // ブロック削除命令
        this._blockDestroyController.OnBlockDestroySubject.Subscribe(point =>
        {
            if(this._playerState.CanDeleteBlock())
            {
                this._blockDestroyAdapter.DestroyBlock(point);
            }
        });
    }

    public void FixedUpdate()
    {
        // Todo:WebSocketSharp-keyによる管理をやめてHTTPを最初にかませることでこの処理を消す
        if(this._player != null)
        {
            // プレイヤーの座標を送信する
            this._playerServerSendAdapter.Send(new SendPacket(this._player,SendBlockActions.None));
        }
    }
}
