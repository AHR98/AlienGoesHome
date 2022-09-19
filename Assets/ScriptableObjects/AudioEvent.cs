

using UnityEngine;
[CreateAssetMenu(fileName = "MusicProfile", menuName = "Data/SFX")]
public class AudioEvent : MusicManager
{
    public AudioClip[] sfxClips;

    public override void Play(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
