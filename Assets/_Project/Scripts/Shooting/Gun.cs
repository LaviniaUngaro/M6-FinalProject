using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _fireRange;
    [SerializeField] private int _damage = 5;
    [SerializeField] private int _maxDamage = 55;
    [SerializeField] private BulletPoolSystem _bulletPoolSystem;

    private float _lastShotTime;
    private Camera _cam;


    [SerializeField] public UnityEvent<int, int> _onWeaponPowerChanged;

    void Awake()
    {
        _cam = Camera.main;
        if (_bulletPoolSystem == null) _bulletPoolSystem = FindObjectOfType<BulletPoolSystem>();
    }

    void Update()
    {
        if (Time.time - _lastShotTime > _fireRate)
        {
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private Transform NearEnemy()
    {
        Transform nearestEnemy = null;
        float minDistance = _fireRange * _fireRange;

        foreach (Transform enemy in EnemyManager.Instance.GetEnemies())
        {
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public void Shoot()
    {
        Transform nearestEnemy = NearEnemy();

        if (nearestEnemy)
        {
            Vector3 mouse = _cam.ScreenToWorldPoint(Input.mousePosition); // -> sparo in direzione del mouse
            Vector2 direction = mouse - transform.position;
            direction.Normalize();

            _bulletPoolSystem.SpawnBullet(transform.position, direction, _damage);
            SoundManager.Instance.PlaySFXSound("Shoot");
        }
    }

    public void Upgrade()
    {
        _damage += 5;
        _onWeaponPowerChanged.Invoke(_damage, _maxDamage);
        SoundManager.Instance.PlaySFXSound("Level Up");
    }
}
