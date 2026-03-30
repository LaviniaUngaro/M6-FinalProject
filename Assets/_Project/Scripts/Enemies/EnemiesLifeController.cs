using UnityEngine;

public class EnemiesLifeController : LifeController
{
    [SerializeField] private EnemiesController _enemy;
    [SerializeField] private AnimationHandler _enemiesAnimCon;
    
    void Awake()
    {
        if (_enemy == null) _enemy = GetComponent<EnemiesController>();
        if (_enemiesAnimCon == null) _enemiesAnimCon = GetComponent<AnimationHandler>();
    }

    void OnEnable()
    {
        OnDamage += EnemyHandleDamage;
        OnDeath += EnemyHandleDeath;
    }

    void OnDisable()
    {
        OnDamage -= EnemyHandleDamage;
        OnDeath -= EnemyHandleDeath;
    }

    public void EnemyHandleDamage()
    {
        _enemiesAnimCon.SetDamage();
    }

    public void EnemyHandleDeath()
    {
        EnemyManager.Instance.RemoveEnemiesToList(transform);
        _enemiesAnimCon.SetDead();
        SoundManager.Instance.PlaySFXSound("Enemies Death");
        Destroy(gameObject, 0.5f);
    }
}
