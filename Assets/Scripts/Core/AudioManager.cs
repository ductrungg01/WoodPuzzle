using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class AudioManager : MonoBehaviour, IGameFlow
    {
        public static AudioManager Instance;

        public List<AudioData> sfxList;
        public List<AudioData> bgmList;

        private AudioSource sfxSource;
        private AudioSource bgmSource;

        private void Awake()
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

            sfxSource = gameObject.AddComponent<AudioSource>();
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySFX(string name, float volume = .5f, bool loop = false)
        {
            AudioData sfx = sfxList.Find(s => s.audioName == name);
            if (sfx != null)
            {
                sfxSource.clip = sfx.audioClip;
                sfxSource.volume = volume; 
                sfxSource.loop = loop;
                sfxSource.Play();

                Debug.Log($"Play {name}");
            }
            else
            {
                Debug.LogWarning("SFX not found: " + name);
            }
        }

        public void PlayBGM(string name, bool loop = true)
        {
            AudioData bgm = bgmList.Find(b => b.audioName == name);
            if (bgm != null)
            {
                bgmSource.clip = bgm.audioClip;
                bgmSource.loop = loop;
                bgmSource.Play();
            }
            else
            {
                Debug.LogWarning("BGM not found: " + name);
            }
        }

        public void PauseSFX()
        {
            if (sfxSource.isPlaying)
            {
                sfxSource.Pause();
            }
        }

        public void PauseBGM()
        {
            if (bgmSource.isPlaying)
            {
                bgmSource.Pause();
            }
        }

        public void StopSFX()
        {
            if (sfxSource.isPlaying)
            {
                sfxSource.Stop();
            }
        }

        public void StopBGM()
        {
            if (bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }
        }

        public void ResumeSFX()
        {
            if (!sfxSource.isPlaying)
            {
                sfxSource.UnPause();
            }
        }

        public void ResumeBGM()
        {
            if (!bgmSource.isPlaying)
            {
                bgmSource.UnPause();
            }
        }

        public void OnPrepareGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnStartGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnPauseGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnResumeGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnFinishGame(EndGameType type)
        {
            if (type == EndGameType.WIN)
            {
                PlaySFX("Yeah");
            } 

            if (type == EndGameType.LOSE)
            {
                PlaySFX("Lose");
            }
        }
    }
}


