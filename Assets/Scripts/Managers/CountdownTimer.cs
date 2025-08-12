using UnityEngine;
using TMPro; // Nécessaire pour TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    /// <summary>
    /// Countdown start time in minuts
    /// </summary>
    [SerializeField] private int countdownTime = 30; 
    [SerializeField] private TextMeshProUGUI countdownText = null;

    private int remainingSeconds;
    private bool isRunning = false;
    private float timer;

    private void Start()
    {
        // Initialisation
        remainingSeconds = countdownTime * 60;
        UpdateCountdownDisplay();
        StartCountdown();

    }

    private void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;
        if (timer >= 1f) // Chaque seconde
        {
            timer = 0f;
            remainingSeconds--;

            if (remainingSeconds <= 0)
            {
                remainingSeconds = 0;
                isRunning = false;
                CountdownFinished();
            }

            UpdateCountdownDisplay();
        }
    }

    public void StartCountdown()
    {
        remainingSeconds = countdownTime * 60;
        timer = 0f;
        isRunning = true;
    }

    private void UpdateCountdownDisplay()
    {
        int minutes = remainingSeconds / 60;
        int seconds = remainingSeconds % 60;
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CountdownFinished()
    {
        Debug.Log("Compte à rebours terminé !");
        // Tu peux lancer une autre action ici (par ex. démarrer le jeu)
    }
}
