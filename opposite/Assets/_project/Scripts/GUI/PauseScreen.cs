using System;
using OppositeGame._project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace OppositeGame._project.Scripts.GUI
{
    public class PauseScreen:MonoBehaviour
    {
        [SerializeField] private Button leaveButton;

        private void Start()
        {
            leaveButton = GetComponentInChildren<Button>();
            leaveButton.onClick.AddListener(LeaveTheGame);
        }

        private void OnEnable()
        {
            GameManager.Instance.Pause();
            GameManager.Instance.currentScreen = GameScreen.Pause;
        }

        private void OnDisable()
        {
            GameManager.Instance.Resume();
        }

        private void LeaveTheGame()
        {
            if (Application.isEditor)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            { 
                Application.Quit();
            }
        }

        private void OnDestroy()
        {
            leaveButton.onClick.RemoveAllListeners();
        }
    }
    
}