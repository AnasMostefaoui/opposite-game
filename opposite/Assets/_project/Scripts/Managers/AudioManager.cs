using System;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.Managers
{
    public class AudioManager: Singleton<AudioManager>
    {
        [SerializeField] private AudioClip introBGMusic;
        [SerializeField] private AudioClip inGameBGMusic;
        [SerializeField] private AudioClip bossMusic;
        
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            audioSource.pitch = Time.timeScale;
        }

        public void PlayIntroMusic()
        {
            audioSource.clip = introBGMusic;
            audioSource.Play();
        }       
        
        public void PlayInGame()
        {
            audioSource.clip = inGameBGMusic;
            audioSource.Play();
        }      
        
        public void PlayBossMusic()
        {
            audioSource.clip = bossMusic;
            audioSource.Play();
        }
    }
}