using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CharacterMove : MonoBehaviour
{
    private Vector3 _moveDirection = default;

    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _rotationSpeed = 1f;

    [SerializeField]
    private Transform _caterpillar = default;


    private void Start()
    {
        this.FixedUpdateAsObservable()
            .Where(_ => _moveDirection != Vector3.zero)
            .Subscribe(_ =>
            {
                Movement();
            });
    }


    public void MoveDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    /// <summary>
    /// 移動挙動
    /// </summary>
    public void Movement()
    {
        Vector3 movementDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y);

        // キャタピラの方向を入力方向に向ける
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        _caterpillar.rotation =
            Quaternion.Slerp(_caterpillar.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);

        transform.position += movementDirection * Time.fixedDeltaTime * _moveSpeed;
    }
}
