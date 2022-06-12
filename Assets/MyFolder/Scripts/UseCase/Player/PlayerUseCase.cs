using UniRx;
public class PlayerUseCase
{
    #region DI Objects
    // Player生成関連
    private IPlayerFactoryAdapter _playerFactoryAdapter;
    // ブロック操作関連
    private IBlockModeController _blockModeConroller;
    private IBlockCreateController _blockCreateController;
    private IBlockDestroyController _blockDestroyController;
    private IBlockCreateAdapter _blockCreateAdapter;
    private IBlockDestroyAdapter _blockDestroyAdapter;
    private IBlockCreatePresetner _blockCreatePresetner;
    private IBlockResourcePresenter _blockResourcePresenter;
    // 通信関連
    private IPlayerServerSendAdapter _playerServerSendAdapter;
    #endregion


    #region ユースケース内オブジェクト
    // プレイヤーの状態を保持するクラス
    private PlayerState _playerState;

    // playerのオブジェクト情報をもったドメインモデル
    private Player _player;
    #endregion

    public PlayerUseCase(IBlockModeController blockModeConroller, IBlockResourcePresenter blockResourcePresenter, IBlockDestroyAdapter blockDestroyAdapter, IBlockCreateAdapter blockCreateAdapter, IPlayerFactoryAdapter playerFactoryAdapter, IBlockCreateController blockCreateController, IBlockDestroyController blockDestroyController, IBlockCreatePresetner blockCreatePresetner, IPlayerServerSendAdapter playerServerSendAdapter)
    {
        this._blockModeConroller = blockModeConroller;
        this._blockResourcePresenter = blockResourcePresenter;
        this._blockDestroyAdapter = blockDestroyAdapter;
        this._blockCreateAdapter = blockCreateAdapter;
        this._playerFactoryAdapter = playerFactoryAdapter;
        this._blockCreateController = blockCreateController;
        this._blockDestroyController = blockDestroyController;
        this._blockCreatePresetner = blockCreatePresetner;
        this._playerServerSendAdapter = playerServerSendAdapter;

        this._playerState = new PlayerState();
    }

    /// <summary>
    /// prefabをインスタンス化する
    /// </summary> 
    public void Init()
    {
        // プレイヤー生成
        this._player = this._playerFactoryAdapter.CreatePlayer();

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
        // プレイヤーの座標を送信する
        this._playerServerSendAdapter.Send(new SendPacket(this._player,SendBlockActions.None));
    }
}
