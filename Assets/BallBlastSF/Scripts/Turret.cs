using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private int _projectileAmount;
    [SerializeField] private float _projectileDistance;

    public int Damage => _damage;
    public int ProjectileAmount => _projectileAmount;
    public float FireRate => _fireRate;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        float startPosX = _shootPoint.position.x - _projectileDistance * (_projectileAmount - 1) * 0.5f;

        for (int i = 0; i < _projectileAmount; i++)
        {
            Projectile projectile = Instantiate(_projectilePrefab, new Vector3(startPosX + i * _projectileDistance, _shootPoint.position.y, _shootPoint.position.z), transform.rotation);
            projectile.SetDamage(_damage);
        }


    }

    public void Fire()
    {
        if (_timer >= _fireRate)
        {
            SpawnProjectile();
            _timer = 0;
        }
    }
}
