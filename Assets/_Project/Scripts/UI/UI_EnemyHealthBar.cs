using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _lifeBar;

    public void UpdateEnemiesHealthGraphics(int currentHP, int maxHP)
    {
        _lifeBar.fillAmount = (float)currentHP / maxHP;
    }
}
