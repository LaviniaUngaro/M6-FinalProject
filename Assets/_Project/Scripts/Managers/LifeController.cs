using System;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _currentHp = 100;
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private bool _isDead;

    [SerializeField] private UnityEvent<int, int> _onLifeChanged;
    public event Action OnHeal;
    public event Action OnDamage;
    public event Action OnDeath;

    public int HP => _currentHp;
    public int MaxHP => _maxHp;
    public bool IsDead => _isDead;

    public void SetHP(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != HP)
        {
            _currentHp = hp;
            _onLifeChanged.Invoke(HP, _maxHp);
        }
    }

    public void AddHp(int amount)
    {
        SetHP(HP + amount);
        OnHeal?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        SetHP(HP - damage);
        if (HP > 0)
        {
            OnDamage?.Invoke();
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        if (_isDead) return;
        
        _isDead = true;
        OnDeath?.Invoke();
    }
}
