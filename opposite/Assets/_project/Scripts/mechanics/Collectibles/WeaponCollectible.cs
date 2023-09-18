using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Collectibles
{
    public class WeaponCollectible : MonoBehaviour
    {
        [SerializeField] public WeaponStrategy weaponStrategy;
    }
}