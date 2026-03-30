using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private GameManager _gameManager;

    private List<Transform> _enemies = new List<Transform>();

    public List<Transform> GetEnemies() => _enemies;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
    }

    public void AddEnemiesToList(Transform enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemiesToList(Transform enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0 && _gameManager != null)
        {
            _gameManager.Win();
        }
    }
}
