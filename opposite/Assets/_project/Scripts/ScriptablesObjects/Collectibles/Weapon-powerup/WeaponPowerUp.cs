using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Collectibles.Weapon_powerup
{
    [CreateAssetMenu(fileName = "WeaponPowerUp", menuName = "OppositeGame/Collectibles/WeaponPowerUp", order = 1)]

    public class WeaponPowerUp : ScriptableObject
    {
        [SerializeField] public WeaponStrategy redWeaponStrategy;
        [SerializeField] public WeaponStrategy blueWeaponStrategy;
    }
}