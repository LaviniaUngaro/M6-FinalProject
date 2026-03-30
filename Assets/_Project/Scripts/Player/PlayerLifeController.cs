using UnityEngine;

public class PlayerLifeController : LifeController
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AnimationHandler _playerAnimCon;

    void Awake()
    {
        if (_player == null) _player = GetComponent<PlayerController>();
        if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
        if (_playerAnimCon == null) _playerAnimCon = GetComponent<AnimationHandler>();
    }

    void OnEnable()
    {
        OnDamage += HandleDamage;
        OnHeal += HandleHeal;
        OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        OnDamage -= HandleDamage;
        OnHeal -= HandleHeal;
        OnDeath -= HandleDeath;
    }

    public void HandleDamage()
    {
        _playerAnimCon.SetDamage();
        SoundManager.Instance.PlaySFXSound("Damage");
    }

    public void HandleHeal()
    {
        SoundManager.Instance.PlaySFXSound("Heal");
    }

    public void HandleDeath()
    {
        _playerAnimCon.SetDead();
        SoundManager.Instance.PlaySFXSound("Death");
        Destroy(gameObject, 0.5f);
        _gameManager.GameOver();
    }
}