using UnityEngine;

[RequireComponent(typeof(GamePlaySettings))]
public class Turret : MonoBehaviour
{
    [SerializeField] private GamePlaySettings _gamePlaySettings;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    
    private float _timer;    

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        float startPosX = _shootPoint.position.x - _gamePlaySettings.ProjectileDistance * (_gamePlaySettings.ProjectileAmount - 1) * 0.5f;

        for (int i = 0; i < _gamePlaySettings.ProjectileAmount; i++)
        {
            Projectile projectile = Instantiate(_projectilePrefab, new Vector3(startPosX + i * _gamePlaySettings.ProjectileDistance, _shootPoint.position.y, _shootPoint.position.z), transform.rotation);            
        }
    }

    public void Fire()
    {
        if (_timer >= _gamePlaySettings.FireRate)
        {
            SpawnProjectile();
            _timer = 0;
        }
    }
}
