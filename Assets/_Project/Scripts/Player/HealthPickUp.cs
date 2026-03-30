using UnityEngine;

public class HealthPickUp : PickUp
{
    [SerializeField] private int _heal;

    protected override void ApplyEffect(GameObject player)
    {
        LifeController _playerLife = player.GetComponent<LifeController>();
        _playerLife.AddHp(_heal);
        // Debug.Log($"+{_heal} hp ricevuti!");
    }
}