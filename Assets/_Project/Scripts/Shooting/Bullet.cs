using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 8;
    [SerializeField] private int _bulletLifetime = 3;
    [SerializeField] private float _bulletBornTime;

    private Rigidbody2D _rb;
    private Vector2 _direction;
    private int _damage;

    private IObjectPool<Bullet> _bulletPool;
    private bool _isActive = false;


    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _isActive = true;
        _bulletBornTime = Time.time;
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    void Update()
    {
        if (Time.time > _bulletBornTime + _bulletLifetime)
        {
            ReturnToPool();
        }
        // Destroy(gameObject, _bulletLifetime);
    }

    public void Setup(Vector2 direction, int damage)
    {
        _direction = direction;
        _damage = damage;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _bulletPool = pool;
    }

    public void ReturnToPool()
    {
        if (!_isActive) return;

        _isActive = false;
        _rb.velocity = Vector2.zero;

        if (_bulletPool != null)
        {
            _bulletPool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        LifeController lifeController = collision.gameObject.GetComponent<LifeController>();

        if (lifeController == null) // distrugge proiettile se colpisce altro tipo i palazzi
        {
            ReturnToPool();
            return;
        }

        if (!lifeController.IsDead)
        {
            lifeController.TakeDamage(Damage);
        }
        ReturnToPool();
    }
}