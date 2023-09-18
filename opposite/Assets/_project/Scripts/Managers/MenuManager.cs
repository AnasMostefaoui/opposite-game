using System;
using OppositeGame._project.Scripts.GUI;
using OppositeGame._project.Scripts.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace OppositeGame._project.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject startScreenPrefab;
        [SerializeField] private GameObject continueScreenPrefab;
        [SerializeField] private GameObject gameOverScreenPrefab;
        [SerializeField] private GameObject pauseScreenPrefab;
        
        private PlayerInput _playerInput;
        private InputAction _startAction;
        private InputAction _quiteAction;
        
        private GameObject _startScreen;
        private GameObject _continueScreen;
        private GameObject _gameOverScreen;
        private GameObject _pauseScreen;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.SwitchCurrentActionMap("Menu");
            _startAction = _playerInput.actions["Start"];
            _quiteAction = _playerInput.actions["Quite"];
            _startAction.performed += OnStartPressed; 
            _quiteAction.performed += OnPausePressed; 
            
            _startScreen = Instantiate(startScreenPrefab);
            _continueScreen = Instantiate(continueScreenPrefab);
            _gameOverScreen = Instantiate(gameOverScreenPrefab);
            _pauseScreen = Instantiate(pauseScreenPrefab);
            
            GameManager.Instance.OnContinueScreen += DisplayContinueScreen;
            GameManager.Instance.OnGameOver += DisplayGameOverScreen;
        }

        private void Start()
        {
            DisableAllScreens();
            DisplayStartScreen();
        }

        private void OnStartPressed(InputAction.CallbackContext action)
        { 
            DisableAllScreens();
            switch (GameManager.Instance.currentScreen)
            {
                case GameScreen.MainMenu:
                    GameManager.Instance.StartGame();
                    break;
                case GameScreen.ContinueScreen when GameManager.Instance.IsGameOver == false:
                    GameManager.Instance.Revive();
                    break;
                case GameScreen.Game:
                    GameManager.Instance.Pause();
                    DisplayPauseScreen();
                    break;
                case GameScreen.Pause:
                    GameManager.Instance.Resume();
                    break;
                case GameScreen.GameOver:
                    // show leaderboard
                    break;
            }
        }
        
        private void OnPausePressed(InputAction.CallbackContext action)
        {
            DisplayPauseScreen();
        }

        private void DisplayPauseScreen()
        {
            DisableAllScreens();
            _continueScreen.gameObject.SetActive(true);
        }
        
        private void DisplayGameOverScreen(object sender, EventArgs e)
        {
            DisableAllScreens();
            _gameOverScreen.gameObject.SetActive(true);
        }
        
        private void DisplayStartScreen()
        {
            DisableAllScreens();
            _startScreen.gameObject.SetActive(true);
        }
        
        private void DisplayContinueScreen(object sender, EventArgs e)
        {
            DisableAllScreens();
            _continueScreen.gameObject.SetActive(true);
        }
        
        private void DisableAllScreens()
        {
            _startScreen.SetActive(false);
            _continueScreen.SetActive(false);
            _gameOverScreen.SetActive(false);
        }

        private void OnDestroy()
        {
            _startAction.performed -= OnStartPressed; 
            _quiteAction.performed -= OnPausePressed;
            GameManager.Instance.OnGameOver -= DisplayGameOverScreen;
        }
    }
}