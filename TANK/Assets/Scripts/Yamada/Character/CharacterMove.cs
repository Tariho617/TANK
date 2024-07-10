// ---------------------------------------------------------  
// CharacterMove.cs  
//   
// 作成日:  6/13
// 作成者:  山田智哉
// ---------------------------------------------------------
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CharacterMove : MonoBehaviour
{
    #region 変数
    [SerializeField, Tooltip("キャタピラ")]
    protected Transform _caterpillar = default;

    // 移動方向格納用
    protected Vector3 _moveDirection = default;

    // 移動速度格納用
    protected float _moveSpeed = default;

    // 回転速度定数
    protected const float ROTATION_SPEED = 5f;
    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // 移動値0以外でアップデート処理を実行
        this.FixedUpdateAsObservable()
            .Where(_ => _moveDirection != Vector3.zero)
            .Subscribe(_ =>
            {
                Movement();
            });
    }

    #region publicメソッド群

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="moveDirection">移動方向</param>
    /// <param name="moveSpeed">移動速度</param>
    public void Moving(Vector3 moveDirection, float moveSpeed)
    {
        _moveDirection = moveDirection;

        _moveSpeed = moveSpeed;
    }

    /// <summary>
    /// 移動挙動
    /// </summary>
    public void Movement()
    {
        // 移動方向を整理
        Vector3 movementDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y);

        // キャタピラの方向を入力方向に向ける
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        _caterpillar.rotation =
            Quaternion.Slerp(_caterpillar.rotation, targetRotation, Time.fixedDeltaTime * ROTATION_SPEED);

        // 移動
        transform.position += movementDirection * Time.fixedDeltaTime * _moveSpeed;
    }
    #endregion
}
