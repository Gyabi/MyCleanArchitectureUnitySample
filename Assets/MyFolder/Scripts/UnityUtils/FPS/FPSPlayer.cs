using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// fps視点移動を行うためのコンポーネント
/// </summary>
public class FPSPlayer : MonoBehaviour
{
    // 基本fps移動速度パラメータ
    private float _moveSpeed = 5.0f;
    // 基本fps視点移動速度パラメータ
    private float _rotateSpeed = 5.0f;

    [SerializeField, Tooltip("fps移動速度ユーザ設定用パラメータ")]
    private float _settingMoveSpeed = 1.0f;

    [SerializeField, Tooltip("fps視点移動速度ユーザ設定用パラメータ")]
    private float _settingRotateSpeed = 1.0f;

    [SerializeField, Tooltip("fps移動加速時倍率")]
    private float _accelerationSensitiveity = 2.0f;

    [SerializeField, Tooltip("前進キー")]
    private KeyCode _forwardKey = KeyCode.W;
    [SerializeField, Tooltip("後退キー")]
    private KeyCode _backKey = KeyCode.S;
    [SerializeField, Tooltip("左キー")]
    private KeyCode _leftKey = KeyCode.A;
    [SerializeField, Tooltip("右キー")]
    private KeyCode _rightKey = KeyCode.D;
    [SerializeField, Tooltip("ジャンプキー")]
    private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField, Tooltip("ダッシュキー")]
    private KeyCode _dashKey = KeyCode.LeftShift;

    [SerializeField, Tooltip("回転オブジェクト")]
    private Transform _rotateObject;

    [SerializeField, Tooltip("ジャンプ力")]
    private float _jumpPower = 5.0f;
    [SerializeField, Tooltip("グラウンドチェッカー")]
    private GameObject _groundChecker;
    private float _acceleration
    {
        get
        {
            return Input.GetKey(this._dashKey) ? _accelerationSensitiveity : 1.0f;
        }   
    }

    // ジャンプ判定用フラグ
    private bool _isGround
    {
        get
        {
            return Physics.Raycast(_groundChecker.transform.position, Vector3.down, 0.1f);
        }
    }

    private void Start()
    {
        _rotateObject.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    private void Update()
    {
        // fps移動
        if(Input.GetKey(this._forwardKey))
        {
            this.transform.position += this.transform.forward * this._moveSpeed * this._settingMoveSpeed * this._acceleration * Time.deltaTime;
        }
        if(Input.GetKey(this._backKey))
        {
            this.transform.position -= this.transform.forward * this._moveSpeed * this._settingMoveSpeed * this._acceleration * Time.deltaTime;
        }
        if(Input.GetKey(this._leftKey))
        {
            this.transform.position -= this.transform.right * this._moveSpeed * this._settingMoveSpeed * this._acceleration * Time.deltaTime;
        }
        if(Input.GetKey(this._rightKey))
        {
            this.transform.position += this.transform.right * this._moveSpeed * this._settingMoveSpeed * this._acceleration * Time.deltaTime;
        }

        // 視点移動
        this.transform.rotation *= Quaternion.AngleAxis(this._rotateSpeed * this._settingRotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, Vector3.up);
        _rotateObject.transform.rotation *= Quaternion.AngleAxis(this._rotateSpeed * this._settingRotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime, Vector3.left);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, this.transform.rotation.eulerAngles.y, 0));

        // ジャンプ
        if(Input.GetKeyDown(this._jumpKey) && this._isGround)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * this._jumpPower, ForceMode.Impulse);
        }

        // 倒れないように
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
