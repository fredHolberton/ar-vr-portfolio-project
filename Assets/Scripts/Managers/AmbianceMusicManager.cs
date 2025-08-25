using UnityEngine;
using System.Collections;

public class AmbianceMusicManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class ComputerVoicemanager
    /// </summary>
    public static AmbianceMusicManager instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip introClip;

    [SerializeField] private AudioClip ambianceClip;

    [SerializeField] private AudioClip victoryMusicClip;

    [SerializeField] private AudioClip defeatMusicClip;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayIntro(float volume)
    {
        audioSource.clip = introClip;
        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.Play(0);
    }

    public void PlayAmbianceMusic(float volume)
    {
        audioSource.clip = ambianceClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play(0);
    }

    public void PlayVictoryMusique(float volume)
    {
        audioSource.Stop();
        
        audioSource.clip = victoryMusicClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play(0);
    }

    public void PlayDefeatMusique(float volume)
    {
        audioSource.clip = defeatMusicClip;
        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.Play(0);
    }

}
