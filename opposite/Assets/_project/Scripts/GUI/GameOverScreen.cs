using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class GameOverScreen:MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.currentScreen = GameScreen.GameOver;
        }
    }
}