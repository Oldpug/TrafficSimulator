using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class SimulationControl : MonoBehaviour
{
    public Button    PlayPauseButton;
    public Sprite pausebutton;
    public Sprite playbutton;
    public bool IsTimeStopped = true;

    [SerializeField]
    private Text textTimeScale;

    public void Awake()
    {
        Time.timeScale = 0;
    }

    public void PlayPause()
    {
            if (IsTimeStopped)
            {
                Time.timeScale = 1.0F;
                IsTimeStopped = false;
                PlayPauseButton.image.overrideSprite = pausebutton;
            }
            else
            {
                Time.timeScale = 0.0F;
                IsTimeStopped = true;
                PlayPauseButton.image.overrideSprite = playbutton;
            }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpeedUp()
    {
        Time.timeScale *= 2;
        if (Time.timeScale >= 100)
            Time.timeScale = 100;
        textTimeScale.text = Time.timeScale.ToString();
    }

    public void SlowDown()
    {
        Time.timeScale /= 2;
    }

}