using System;
using System.Collections;
using OppositeGame._project.Scripts.GUI;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace OppositeGame._project.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }
        [SerializeField] private GameObject startScreenPrefab;
        [SerializeField] private GameObject continueScreenPrefab;
        [SerializeField] private GameObject gameOverScreenPrefab;
        [SerializeField] private GameObject pauseScreenPrefab;
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private Image redEnergyFillImage;
        [SerializeField] private Image blueEnergyFillImage;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesText;
        
        private PlayerInput _playerInput;
        private InputAction _startAction;
        private InputAction _quiteAction;

        private GameObject _currentScreen;
        private GameScreen? _transitioningScreen;
        private GameObject _startScreen;
        private GameObject _continueScreen;
        private GameObject _gameOverScreen;
        private GameObject _pauseScreen; 
        private Animator _animationController;
        private bool _isTransitioning;
        private static readonly int FadeOutAnimationKey = Animator.StringToHash("shouldFadeOut");
        private static readonly int FadeInAnimationKey = Animator.StringToHash("shouldFadeIn");
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            
            inGameUI.SetActive(false);
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.SwitchCurrentActionMap("Menu");
            _startAction = _playerInput.actions["Start"];
            _quiteAction = _playerInput.actions["Quite"];
            _startAction.performed += OnStartPressed; 
            _quiteAction.performed += OnPausePressed; 
            
            _startScreen = Instantiate(startScreenPrefab, transform, true);
            _continueScreen = Instantiate(continueScreenPrefab, transform, true);
            _gameOverScreen = Instantiate(gameOverScreenPrefab, transform, true);
            _pauseScreen = Instantiate(pauseScreenPrefab, transform, true);
            
            _animationController = GetComponentInChildren<Animator>();
            _continueScreen.GetComponent<ContinueScreen>().OnLeaving += OnLeavingContinueScreen;
            GameManager.Instance.OnContinueScreen += DisplayContinueScreen;
            GameManager.Instance.OnGameOver += DisplayGameOverScreen;
            GameManager.Instance.OnMainMenu +=  DisplayStartScreen;
            GameManager.Instance.OnEnergyChanged +=  UpdateRedEnergy;
        }

        private void UpdateRedEnergy(float energy, PolarityType polarity)
        {
            if (polarity == PolarityType.Blue)
            {
                blueEnergyFillImage.fillAmount = energy;
            }
            else
            {
                redEnergyFillImage.fillAmount = energy;
            }
        }

        private void OnLeavingContinueScreen(GameScreen newScreen)
        {
            // if we are on continue and we have 
            if (newScreen == GameScreen.GameOver)
            {
                LeaveScreen(newScreen, () => DisplayGameOverScreen(this, EventArgs.Empty));
            }
        }

        private void EnterScreen(GameScreen newScreen, Action callback)
        {
            StartCoroutine(TransitionTo(newScreen, FadeOutAnimationKey, callback));
        }

        private void LeaveScreen(GameScreen newScreen, Action callback)
        {
            StartCoroutine(TransitionTo(newScreen, FadeInAnimationKey, callback));
        }
        
        private IEnumerator TransitionTo(GameScreen newScreen, int fadingID, Action callback)
        {
            _isTransitioning = true;
            _transitioningScreen = newScreen;
            // play fadein animation
            _animationController.SetTrigger(fadingID);
            yield return new WaitForSeconds(2f);
            // then change screen
            callback();
            _isTransitioning = false;
        }
        
        private void Start()
        {
            DisableAllScreens();
            EnterScreen(GameScreen.MainMenu, () => DisplayStartScreen(this, EventArgs.Empty) );
        }

        private void Update()
        {
            scoreText.text = $"score: {GameManager.Instance.CurrentScore}";
            livesText.text = $"lives: x{GameManager.Instance.currentLifePoint}";
        }

        private void OnStartPressed(InputAction.CallbackContext action)
        { 
            if(_isTransitioning) return;
            DisableAllScreens();
            switch (GameManager.Instance.currentScreen)
            {
                case GameScreen.MainMenu:
                    GameManager.Instance.StartGame();
                    inGameUI.SetActive(true);
                    break;
                case GameScreen.ContinueScreen when GameManager.Instance.IsGameOver == false:
                    GameManager.Instance.Revive();
                    break;
                case GameScreen.Pause:
                    GameManager.Instance.Resume();
                    break;
                case GameScreen.GameOver: 
                    GameManager.Instance.RestartFromMainMenu(); 
                    EnterScreen(GameScreen.MainMenu,() => DisplayStartScreen(this, EventArgs.Empty));
                    break;
            }
        }
        
        private void OnPausePressed(InputAction.CallbackContext action)
        {
            // provide the player the possibility to leave the game from to desktop at any moment.
            DisplayPauseScreen();
        }

        private void DisplayPauseScreen()
        {
            DisableAllScreens();
            _pauseScreen.gameObject.SetActive(true);
        }
        
        private void DisplayGameOverScreen(object sender, EventArgs e)
        {
            _currentScreen = _gameOverScreen;
            _gameOverScreen.SetActive(true);
            GameManager.Instance.IsGameOver = true;
            
            var highScore = PlayerPrefs.GetInt("high-score", 0);
            if (GameManager.Instance.CurrentScore > highScore)
            {
                PlayerPrefs.SetInt("high-score", GameManager.Instance.CurrentScore);
                PlayerPrefs.Save();
            }
        }
        
        private void DisplayStartScreen(object sender, EventArgs e)
        {
            DisableAllScreens();
            _currentScreen = _startScreen; 
            _currentScreen.SetActive(true);
        }
        
        private void DisplayContinueScreen(object sender, EventArgs e)
        {
            _currentScreen.SetActive(false);
            _continueScreen.gameObject.SetActive(true);
            _currentScreen = _continueScreen;
        }
        
        private void DisableAllScreens()
        {
            _startScreen.SetActive(false);
            _continueScreen.SetActive(false);
            _gameOverScreen.SetActive(false);
            _pauseScreen.SetActive(false);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnContinueScreen -= DisplayContinueScreen;
            GameManager.Instance.OnGameOver -= DisplayGameOverScreen;
            GameManager.Instance.OnMainMenu -=  DisplayStartScreen;
            _startAction.performed -= OnStartPressed; 
            _quiteAction.performed -= OnPausePressed;
            _continueScreen.GetComponent<ContinueScreen>().OnLeaving -= OnLeavingContinueScreen;
            GameManager.Instance.OnGameOver -= DisplayGameOverScreen;
            GameManager.Instance.OnEnergyChanged -=  UpdateRedEnergy;
        }
    }
}