using System;
using UnityEngine;
using System.Collections;

public class ComputerVoiceManager : MonoBehaviour
{
    /// <summary>
    /// instance of the class ComputerVoicemanager
    /// </summary>
    public static ComputerVoiceManager instance;

    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private AudioClip gameRulesClip = null;

    [SerializeField] private AudioClip glassDoorLockedClip = null;

    [SerializeField] private AudioClip glassDoorUnlockedClip = null;

    [SerializeField] private AudioClip mainDoorLockedClip = null;

    [SerializeField] private AudioClip mainDoorUnlockedClip = null;

    [SerializeField] private AudioClip chessboardCompleteClip = null;

    [SerializeField] private AudioClip victoryMessageClip = null;

    [SerializeField] private AudioClip defeatMessageClip = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SayGameRules(float volume, Action onComplete)
    {
        StartCoroutine(PlaySoundAndWait(gameRulesClip, volume, onComplete));
    }

    public void SayGlassDoorIsLocked(float volume)
    {
        audioSource.PlayOneShot(glassDoorLockedClip, volume);
    }

    public void SayGlassDoorIsUnLocked(float volume)
    {
        audioSource.PlayOneShot(glassDoorUnlockedClip, volume);
    }

    public void SayMainDoorIsLocked(float volume)
    {
        audioSource.PlayOneShot(mainDoorLockedClip, volume);
    }

    public void SayMainDoorIsUnLocked(float volume)
    {
        audioSource.PlayOneShot(mainDoorUnlockedClip, volume);
    }

    public void SayChessboardComplete(float volume)
    {
        audioSource.PlayOneShot(chessboardCompleteClip, volume);
    }

    public void SayVictoryMessage(float volume)
    {
        audioSource.PlayOneShot(victoryMessageClip, volume);
    }

    public void SayDefeatMessage(float volume)
    {
        audioSource.PlayOneShot(defeatMessageClip, volume);
    }

    private IEnumerator PlaySoundAndWait(AudioClip audioClip, float volume, Action onComplete)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        onComplete?.Invoke();
    }
}
