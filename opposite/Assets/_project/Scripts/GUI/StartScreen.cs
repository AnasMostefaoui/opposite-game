using System;
using OppositeGame._project.Scripts.Managers;
using UnityEngine;

namespace OppositeGame._project.Scripts.GUI
{
    public class StartScreen : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.currentScreen = GameScreen.MainMenu;
            GameManager.Instance.Resume();
        }
    }
}