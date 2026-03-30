using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerPanel : MonoBehaviour
{
    public static UI_PlayerPanel _instance;

    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _weaponBar;

    public static UI_PlayerPanel Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdatePlayerHealthGraphics(int currentHP, int maxHP)
    {
        _lifeBar.fillAmount = (float)currentHP / maxHP;
    }

    public void UpdateWeaponGraphics(int damage, int maxDamage)
    {
        _weaponBar.fillAmount = (float)damage / maxDamage;
    }
}
