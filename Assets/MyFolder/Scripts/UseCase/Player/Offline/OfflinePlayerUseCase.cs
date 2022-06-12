using UniRx;
using Zenject;
public class OfflinePlayerUseCase : IPlayerUseCase
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
    #endregion


    #region ユースケース内オブジェクト
    // プレイヤーの状態を保持するクラス
    private PlayerState _playerState;

    // playerのオブジェクト情報をもったドメインモデル
    private Player _player;
    #endregion

    public OfflinePlayerUseCase()
    {
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
    }
}
