using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _vehicleWidth;

    [Header("Wheels")]
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelRadius;

    [HideInInspector] public UnityEvent OnStoneCollision;

    private Vector3 _movementTarget;

    private void Start()
    {
        _movementTarget = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stone stone = collision.transform.root.GetComponent<Stone>();

        if (stone != null)
        {
            OnStoneCollision.Invoke();
        }
    }

    private void Move()
    {
        float lastPositionX = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, ClampMovementTarget(_movementTarget), _movementSpeed * Time.deltaTime);

        float deltaMovement = transform.position.x - lastPositionX;
        RotateWheel(deltaMovement);
    }

    private void RotateWheel(float deltaMovement)
    {
        float angle = (180f * deltaMovement) / (Mathf.PI * _wheelRadius * 2);

        for (int i = 0; i < _wheels.Length; i++)        
            _wheels[i].Rotate(0, 0, -angle);        
    }

    public void SetMovementTarget(Vector3 movementTarget)
    {
        _movementTarget = movementTarget;
    }

    private Vector3 ClampMovementTarget(Vector3 movementTarget)
    {
        float leftBorder = LevelBoundary.Instance.LeftBorder + (_vehicleWidth * 0.5f);
        float rightBorder = LevelBoundary.Instance.RightBorder - (_vehicleWidth * 0.5f);        

        Vector3 moveTarget = new Vector3(movementTarget.x, transform.position.y);

        if (moveTarget.x < leftBorder)
            moveTarget.x = leftBorder;

        if (moveTarget.x > rightBorder)
            moveTarget.x = rightBorder;

        return moveTarget;
    }
   

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position - new Vector3(_vehicleWidth * 0.5f, 0.5f, 0), transform.position + new Vector3(_vehicleWidth * 0.5f, -0.5f, 0));
    }
#endif
}
