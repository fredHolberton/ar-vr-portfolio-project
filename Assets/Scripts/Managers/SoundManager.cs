using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class SoundManager
    /// </summary>
    public static SoundManager instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip doorOpenSoundClip;

    [SerializeField] private AudioClip projectorUnlockedSoundClip;


    [SerializeField] private AudioClip mainDoorLockedSoundClip;

    [SerializeField] private AudioClip consoleScreenDisplaySoundClip;

    [SerializeField] private AudioClip alarmSoundClip;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayDoorUnlockSound(float volume)
    {
        audioSource.PlayOneShot(doorOpenSoundClip, volume);
    }

    public void PlayProjectorUnloackedSound(float volume)
    {
        audioSource.PlayOneShot(projectorUnlockedSoundClip, volume);
    }

    public void PlayMainDoorLockSound(float volume)
    {
        audioSource.PlayOneShot(doorOpenSoundClip, volume);
    }

    public void PlayConsoleScreenDisplaySound(float volume)
    {
        audioSource.PlayOneShot(consoleScreenDisplaySoundClip, volume);
    }

    public void PlayAlarmSound(float volume)
    {
        audioSource.clip = alarmSoundClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play(0);
    }

    public void StopSound()
    {
        if (audioSource.isPlaying && audioSource.clip == alarmSoundClip) audioSource.Stop();
    }

}

