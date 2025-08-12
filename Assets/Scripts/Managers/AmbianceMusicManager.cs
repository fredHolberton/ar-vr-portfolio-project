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
        audioSource.Play(0);
    }

}
