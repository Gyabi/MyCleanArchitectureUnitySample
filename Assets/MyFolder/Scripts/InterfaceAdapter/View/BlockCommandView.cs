using UnityEngine;
using UniRx;
public class BlockCommandView : MonoBehaviour, IBlockCreateView, IBlockModeView, IBlockDestroyView, IBlockCreateUIView
{
    private Subject<BlockPoint> _onBlockCreateSubject = new Subject<BlockPoint>();
    public Subject<BlockPoint> OnBlockCreateSubject
    {
        get
        {
            return _onBlockCreateSubject;
        }
    }

    private Subject<Unit> _onBlockModeToCreateSubject = new Subject<Unit>();
    public Subject<Unit> OnBlockModeToCreateSubject
    {
        get
        {
            return _onBlockModeToCreateSubject;
        }
    }
    private Subject<Unit> _onBlockModeToDeleteSubject = new Subject<Unit>();
    public Subject<Unit> OnBlockModeToDeleteSubject
    {
        get
        {
            return _onBlockModeToDeleteSubject;
        }
    }

    private Subject<GameObject> _onBlockDestroySubject = new Subject<GameObject>();
    public Subject<GameObject> OnBlockDestroySubject
    {
        get
        {
            return _onBlockDestroySubject;
        }
    }
    
    [SerializeField, Tooltip("Createモード切り替えボタン")]
    private KeyCode _blockModeCreateButton = KeyCode.B;
    [SerializeField, Tooltip("Deleteモード切り替えボタン")]
    private KeyCode _blockModeDeleteButton = KeyCode.N;

    #region CreateUI
    private int _layerMask = 1 << 6;

    /// <summary>
    /// 描画用UIを作成するかどうか
    /// </summary>
    private bool _isDrawCreateUI = true;
    #endregion

    private GameObject _blockCreateUI;
    void Start()
    {
        _blockCreateUI = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _blockCreateUI.transform.localPosition = new Vector3(0, 0, 0);
        _blockCreateUI.transform.SetParent(this.transform);
        _blockCreateUI.GetComponent<MeshRenderer>().material.color = new Color(0.8f,0.7f, 0.5f, 0.6f);
        Destroy(_blockCreateUI.GetComponent<BoxCollider>());
    }
    void Update()
    {
        // 適切な位置に表示を出して
        if(_isDrawCreateUI)
        {
            if(Camera.main != null){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
                {
                    // オブジェクトとhit.pointの位置関係から、ブロックの位置を決める                
                    _blockCreateUI.transform.localPosition = hit.transform.position + hit.normal;
                }
            }
        }

        // 生成点の座標を送り返す
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                Vector3 tmp = hit.transform.position + hit.normal;
                BlockPoint point = new BlockPoint(tmp.x, tmp.y, tmp.z);
                _onBlockCreateSubject.OnNext(point);
            }
        }

        if(Input.GetKeyDown(_blockModeCreateButton))
        {
            _onBlockModeToCreateSubject.OnNext(Unit.Default);
        }

        if(Input.GetKeyDown(_blockModeDeleteButton))
        {
            _onBlockModeToDeleteSubject.OnNext(Unit.Default);
        }

        if(Input.GetMouseButtonDown(1))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                if(hit.transform.gameObject.tag == "UserBlock")
                {
                    _onBlockDestroySubject.OnNext(hit.transform.gameObject);
                }
            }
        }
    }

    public void ShowBlockCreateUI()
    {

        _blockCreateUI.SetActive(true);
        _isDrawCreateUI = true;
    }
    public void HideBlockCreateUI()
    {
        _blockCreateUI.SetActive(false);
        _isDrawCreateUI = false;
    }
}
