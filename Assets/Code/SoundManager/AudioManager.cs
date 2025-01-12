using UnityEngine;

namespace Code.SoundManager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;

        [SerializeField] private AudioClip fireSound;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private AudioClip loseSound;
        [SerializeField] private AudioClip victorySound;
        [SerializeField] private AudioClip backgroundMusic;
        

        private bool isMusicPlaying = false;

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic()
        {
            if (musicAudioSource != null && !musicAudioSource.isPlaying && !isMusicPlaying)
            {
                musicAudioSource.clip = backgroundMusic;
                musicAudioSource.Play();
                isMusicPlaying = true;
            }
        }

        public void PauseMusic()
        {
            if (musicAudioSource != null && musicAudioSource.isPlaying)
            {
                musicAudioSource.Pause();
                isMusicPlaying = false;
            }
        }

        public void StopMusic()
        {
            if (musicAudioSource != null)
            {
                musicAudioSource.Stop();
                isMusicPlaying = false;
            }
        }

        public void PlayFireSound()
        {
            if (sfxAudioSource != null && fireSound != null)
            {
                sfxAudioSource.PlayOneShot(fireSound);
            }
        }

        public void PlayExplosionSound()
        {
            if (sfxAudioSource != null && explosionSound != null)
            {
                sfxAudioSource.PlayOneShot(explosionSound);
            }
        }
        
        public void PlayLoseSound()
        {
            if (sfxAudioSource != null && loseSound != null)
            {
                sfxAudioSource.PlayOneShot(loseSound);
            }
        }
        
        public void PlayVictorySound()
        {
            if (sfxAudioSource != null && victorySound != null)
            {
                sfxAudioSource.PlayOneShot(victorySound);
            }
        }

        public void SetVolume(float volume)
        {
            if (musicAudioSource != null)
                musicAudioSource.volume = volume;

            if (sfxAudioSource != null)
                sfxAudioSource.volume = volume;
        }
    }
}
