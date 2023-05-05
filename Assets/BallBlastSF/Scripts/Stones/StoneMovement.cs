using System;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _reboundSpeed;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _gravityOffset;

    private bool _useGravity;
    private Vector3 _velocity;
    private bool _freezeState;

    private void Awake()
    {
        _velocity.x = -Mathf.Sign(transform.position.x) * _horizontalSpeed;
    }

    private void Update()
    {
        TryEnableGravity();
        Move();
    }

    private void TryEnableGravity()
    {
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - _gravityOffset)
            _useGravity = true;
    }

    private void Move()
    {
        if (_useGravity && _freezeState)
            return;

        if (_useGravity)
        {
            _velocity.y -= _gravity * Time.deltaTime;
            transform.Rotate(0, 0, -Mathf.Sign(_velocity.x) * _rotationSpeed * Time.deltaTime);
        }


        _velocity.x = Mathf.Sign(_velocity.x) * _horizontalSpeed;
        transform.position += _velocity * Time.deltaTime;

        if ((transform.position.x < LevelBoundary.Instance.LeftBorder && _velocity.x < 0)
            || (transform.position.x > LevelBoundary.Instance.RightBorder && _velocity.x > 0))
            _velocity.x *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge;

        if (collision.TryGetComponent<LevelEdge>(out levelEdge))
        {
            if (levelEdge.Type == EdgeType.Bottom)
                _velocity.y = _reboundSpeed;
            else if ((levelEdge.Type == EdgeType.Left && _velocity.x < 0) || (levelEdge.Type == EdgeType.Right && _velocity.x > 0))
                _velocity.x *= -1;
        }
    }

    public void AddVerticalVelocity(float velocity)
    {
        _velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        _velocity.x = Mathf.Sign(direction) * _horizontalSpeed;
    }

    public void SetFreezeState(bool freezeState)
    {
        _freezeState = freezeState;
    }

}
