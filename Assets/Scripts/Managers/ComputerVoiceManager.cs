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
