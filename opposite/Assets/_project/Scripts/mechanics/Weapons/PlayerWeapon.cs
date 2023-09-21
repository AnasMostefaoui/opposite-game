using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Collectibles;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class PlayerWeapon : Weapon
    {
        private InputReader _inputReader; 
        private PolarityType _polarity; 
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
        }

        private void OnDisable()
        {
            ChangeWeaponStrategy(weaponStrategy);
        }

        private void Update()
        {
            var currentScreen = GameManager.Instance.currentScreen;
            // enable the player to test the weapon in the main menu
            if(currentScreen != GameScreen.Game && currentScreen != GameScreen.MainMenu ) return;
            FireRateCounter += Time.deltaTime;
            if (!_inputReader.IsFiring || !DidReload) return;
            CurrentWeaponStrategy.Fire(startTransform, layer, _polarity);
            FireRateCounter = 0;
        }
        
        // if you take power up change the weapon based on the powerup content
        private void OnTriggerEnter2D(Collider2D other)
        {
            var powerUp = other.GetComponent<WeaponCollectible>();
            if (powerUp == null) return;
            ChangeWeaponStrategy(powerUp.weaponStrategy);
            Destroy(other.gameObject);
        }
    }
}