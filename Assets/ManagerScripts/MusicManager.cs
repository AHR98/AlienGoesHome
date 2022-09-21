using UnityEngine;
using System;
class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicLevel1;
    [SerializeField]
    private AudioSource musicLevel2;
    [SerializeField]
    private AudioSource musicChase;
    private int level = 0;

    void Update()
    {
        setLevel();
    }
    private void setLevel()
    {
        level = GameManager.instance.getGameLevel();
        musicToPlay();
    }
    private void musicToPlay()
    {
        switch(level)
        { 
        
            case 1:
                StopMusic(musicLevel2);
                StopMusic(musicChase);
                if(!musicLevel1.isPlaying)
                    PlayMusic(musicLevel1);  break;
            case 2:
                StopMusic(musicLevel1);
                StopMusic(musicChase);
                if (!musicLevel2.isPlaying)
                    PlayMusic(musicLevel2); break;
            case 3:
                StopMusic(musicLevel1);
                StopMusic(musicLevel2);
                if (!musicChase.isPlaying)
                    PlayMusic(musicChase); break;

            default: StopAllMusic(); break;
        }
    }
    public void PlayMusic(AudioSource _audioSource)
    {
        _audioSource.Play();
    }

    public void StopMusic(AudioSource _audioSource)
    {
        _audioSource.Stop();
    }


    private void StopAllMusic()
    {
        musicLevel1.Stop();
        musicLevel2.Stop();
        musicChase.Stop();

    }
}
