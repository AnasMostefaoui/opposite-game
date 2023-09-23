using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Collectibles;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Player;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.weapons
{
    public class PlayerWeapon : Weapon
    {
        private InputReader _inputReader;
        private PlayerPolarity _playerPolarity;
        [SerializeField]
        private WeaponCollectible basicWeapon;
        private WeaponStrategy _redWeaponStrategy;
        private WeaponStrategy _blueWeaponStrategy;
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _playerPolarity = GetComponent<PlayerPolarity>();
            _playerPolarity.OnPolarityChanged += OnPolarityChanged;
            _redWeaponStrategy = basicWeapon.content.redWeaponStrategy;
            _blueWeaponStrategy = basicWeapon.content.blueWeaponStrategy;
            weaponStrategy = _playerPolarity.PolarityType == PolarityType.Red ?
                _redWeaponStrategy : _blueWeaponStrategy;
        }

        private void OnPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            var newWeapon = _playerPolarity.PolarityType == PolarityType.Red ?
                _redWeaponStrategy : _blueWeaponStrategy;
            ChangeWeaponStrategy(newWeapon);
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
            CurrentWeaponStrategy.Fire(startTransform, layer, _playerPolarity.PolarityType);
            FireRateCounter = 0;
        }
        
        // if you take power up change the weapon based on the powerup content
        private void OnTriggerEnter2D(Collider2D other)
        {
            var powerUp = other.GetComponent<WeaponCollectible>();
            if (powerUp == null) return;
            var powerUpContent = powerUp.content;
            var newWeapon = _playerPolarity.PolarityType == PolarityType.Red ?
                powerUpContent.redWeaponStrategy : powerUpContent.blueWeaponStrategy;
            _redWeaponStrategy = powerUpContent.redWeaponStrategy;
            _blueWeaponStrategy = powerUpContent.blueWeaponStrategy;
            ChangeWeaponStrategy(newWeapon);
            Destroy(other.gameObject);
        }
    }
}