using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _aggroDistance = 15f;

    private PlayerController _player;
    private LifeController _enemyLifeController;
    private Rigidbody2D _rb;

    private AnimationHandler _enemiesAnimCon;

    private bool _hasHitPlayer = false;


    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<PlayerController>();

        _rb = GetComponent<Rigidbody2D>();
        _enemyLifeController = GetComponent<LifeController>();
        _enemiesAnimCon = GetComponent<AnimationHandler>();
    }

    void Start()
    {
        EnemyManager.Instance.AddEnemiesToList(transform);
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (_player == null) return;

        Vector2 direction = _player.transform.position - transform.position;
        float distance = direction.sqrMagnitude;

        if (distance < _aggroDistance * _aggroDistance)
        {
            direction.Normalize();
            _rb.velocity = direction * _speed;
            _enemiesAnimCon.SetSpeed(1f);
        }
        else
        {
            _rb.velocity = Vector2.zero;
            _enemiesAnimCon.SetSpeed(0f);
        }

        if (direction.x > 0)
        {
            _enemiesAnimCon.SetDirection(1f);
        }
        else if (direction.x < 0)
        {
            _enemiesAnimCon.SetDirection(-1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasHitPlayer) return;

        PlayerLifeController lifeController = collision.gameObject.GetComponent<PlayerLifeController>();
        if (lifeController == null) return;

        _hasHitPlayer = true;

        if (!lifeController.IsDead)
        {
            lifeController.TakeDamage(_damage);
        }

        _enemyLifeController.Death(); // il nemico muore
    }
}
