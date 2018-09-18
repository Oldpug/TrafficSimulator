using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class SimulationControl : MonoBehaviour
{
    public Button PlayPauseButton;
    public Sprite PauseButton;
    public Sprite playButton;
    public bool IsTimeStopped = true;
    public float x=1;

    [SerializeField]
    private Text textTimeScale;

    public void Awake()
    {
        Time.timeScale = 0;
        textTimeScale.text = "Speed x" + x;
    }

    public void PlayPause()
    {
            if (IsTimeStopped)
            {
                Time.timeScale = x;
                IsTimeStopped = false;
                PlayPauseButton.image.overrideSprite = PauseButton;
            }
            else
            {
                x = Time.timeScale;
                Time.timeScale = 0.0F;
                IsTimeStopped = true;
                PlayPauseButton.image.overrideSprite = playButton;
            }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpeedUp()
    {
        var timeScale = Mathf.Min(Time.timeScale * 2, 64);

        Time.timeScale = timeScale;
        textTimeScale.text = "Speed x" + timeScale.ToString();
    }

    public void SlowDown()
    {
        var timeScale = Mathf.Max(Time.timeScale / 2, 1);

        Time.timeScale = timeScale;
        textTimeScale.text = "Speed x" + timeScale.ToString();
    }

}