using UnityEngine;

public class WeaponPickUp : PickUp
{
    [SerializeField] private Gun _weaponPrefab;

    protected override void ApplyEffect(GameObject player)
    {
        Gun _hasWeapon = player.GetComponentInChildren<Gun>();
        if (_hasWeapon == null)
        {
            Gun newWeapon = Instantiate(_weaponPrefab, player.transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon._onWeaponPowerChanged.AddListener(UI_PlayerPanel.Instance.UpdateWeaponGraphics);
            Debug.Log($"{player.gameObject.name} ha raccolto l'arma!");
        }
        else
        {
            _hasWeapon.Upgrade();
            Debug.Log($"Arma potenziata!");
        }
    }
}