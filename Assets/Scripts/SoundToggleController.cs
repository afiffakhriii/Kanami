using UnityEngine;
using UnityEngine.UI;

public class SoundToggleController : MonoBehaviour
{
    public Button soundOnButton; // Assign your Sound On button in the Inspector
    public Button soundOffButton; // Assign your Sound Off button in the Inspector

    void Start()
    {
        // Add listeners to buttons
        soundOnButton.onClick.AddListener(TurnSoundOff);
        soundOffButton.onClick.AddListener(TurnSoundOn);

        // Initialize buttons' state
        ShowSoundOnButton();
    }

    void ShowSoundOnButton()
    {
        soundOnButton.gameObject.SetActive(true);
        soundOffButton.gameObject.SetActive(false);

        // Play the backsound
        BacksoundManager.Instance.PlayBacksound();
    }

    void ShowSoundOffButton()
    {
        soundOnButton.gameObject.SetActive(false);
        soundOffButton.gameObject.SetActive(true);

        // Stop the backsound
        BacksoundManager.Instance.StopBacksound();
    }

    void TurnSoundOn()
    {
        ShowSoundOnButton();
    }

    void TurnSoundOff()
    {
        ShowSoundOffButton();
    }
}
