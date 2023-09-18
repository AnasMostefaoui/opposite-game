using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class WeaponCarrier : MonoBehaviour
    {
        public WeaponStrategy CurrentWeapon {get; set;}
        private void ChangeWeapon(WeaponStrategy weapon)
        {
           // _weaponHolder.ChangeWeaponStrategy(currentWeapon);
        }
    }
}