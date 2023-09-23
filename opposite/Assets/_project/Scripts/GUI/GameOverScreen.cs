using System;
using OppositeGame._project.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class GameOverScreen:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerScore;
        [SerializeField] private TextMeshProUGUI highestscore;
        
        private void OnEnable()
        {
            GameManager.Instance.currentScreen = GameScreen.GameOver;
            playerScore.text = GameManager.Instance.CurrentScore.ToString();
            highestscore.text = PlayerPrefs.GetInt("high-score", 0).ToString();
        }
    }
}