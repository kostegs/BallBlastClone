using UnityEngine;

public class GamePlaySettings : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private int _projectileAmount;
    [SerializeField] private float _projectileDistance;

    public float FireRate { get { return _fireRate; } set { _fireRate = value < 0 ? _fireRate : value; } }
    public int Damage { get => _damage; set => _damage = value; }
    public int ProjectileAmount { get => _projectileAmount; set { _projectileAmount = (value < 0 || value > 3) ? _projectileAmount : value; } }
    public float ProjectileDistance => _projectileDistance;
    
    private void Awake()
    {
        if (DataStorage.SettingsDataInitialized)
        {
            _fireRate = DataStorage.FireRate;
            _damage = DataStorage.Damage;
            _projectileAmount = DataStorage.ProjectileAmount;
            _projectileDistance = DataStorage.ProjectileDistance;
        }
    }

    private void OnDestroy()
    {
        DataStorage.FillDataFromSettings(this);
    }
}
