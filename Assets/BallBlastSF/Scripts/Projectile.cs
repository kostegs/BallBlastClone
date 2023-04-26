using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private int _damage;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructable destructable;

        if (collision.transform.root.TryGetComponent<Destructable>(out destructable))
            destructable.ApplyDamage(_damage);

        Destroy(gameObject);
    }

    public void SetDamage(int damage) => _damage = damage;
}
