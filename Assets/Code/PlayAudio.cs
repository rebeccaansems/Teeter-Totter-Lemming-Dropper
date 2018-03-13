using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip[] AudioClips;

    public void Play()
    {
        AudioSource.PlayClipAtPoint(AudioClips[0], Camera.main.transform.position, PlayerStats.k_SFXVolume);
    }

    public void Play(int num)
    {
        if (AudioClips.Length > num)
        {
            AudioSource.PlayClipAtPoint(AudioClips[num], Camera.main.transform.position, PlayerStats.k_SFXVolume);
        }
    }

    public void PlayRandom()
    {
        if (AudioClips.Length > 0)
        {
            AudioSource.PlayClipAtPoint(AudioClips[Random.Range(0, AudioClips.Length)], Camera.main.transform.position, PlayerStats.k_SFXVolume);
        }
    }

    public void PlayRandom(int min, int max)
    {
        if (AudioClips.Length > 0 && AudioClips.Length > max)
        {
            AudioSource.PlayClipAtPoint(AudioClips[Random.Range(min, max)], Camera.main.transform.position, PlayerStats.k_SFXVolume);
        }
    }
}