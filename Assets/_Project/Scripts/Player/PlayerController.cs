using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _footstepInterval = 0.4f;

    private float _footstepTimer;
    private bool _canMove = true;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    private AnimationHandler _playerAnimCon;

    public Vector2 Direction
    {
        get => _direction;
        private set => _direction = value;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimCon = GetComponent<AnimationHandler>();
    }

    void Update()
    {
        if (!_canMove) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _direction = new Vector2(h, v);
        _direction.Normalize();

        // setto animazione walk/idle
        if (h != 0 || v != 0)
        {
            _playerAnimCon.SetSpeed(1f);
            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer <= 0)
            {
                SoundManager.Instance.PlaySFXSound("Walk");
                _footstepTimer = _footstepInterval;
            } 
        }
        else
        {
            _playerAnimCon.SetSpeed(0f);
            _footstepTimer = 0;
        }

        // setto animazione dx/sx
        if (h > 0)
        {
            _playerAnimCon.SetDirection(1f);
        }
        else if (h < 0)
        {
            _playerAnimCon.SetDirection(-1f);
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (_speed * Time.fixedDeltaTime));
    }

    public void StopMovement()
    {
        _canMove = false;
    }
}