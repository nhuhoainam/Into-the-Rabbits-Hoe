using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject settingsPopup;  
    public GameObject instructionsPopup; 

    void Start()
    {
        
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }
        else
        {
            Debug.LogError("Settings Popup is not assigned in the Inspector.");
        }

        if (instructionsPopup != null)
        {
            instructionsPopup.SetActive(false);
        }
        else
        {
            Debug.LogError("Instructions Popup is not assigned in the Inspector.");
        }
    }

    public void ShowSettingsPopup()
    {
        Debug.Log("ShowSettingsPopup called");
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(true);
        }
        else
        {
            Debug.LogError("Settings Popup is not assigned in the Inspector.");
        }
    }

    public void HideSettingsPopup()
    {
        Debug.Log("HideSettingsPopup called");
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }
        else
        {
            Debug.LogError("Settings Popup is not assigned in the Inspector.");
        }
    }

    public void ShowInstructionsPopup()
    {
        Debug.Log("ShowInstructionsPopup called");
        if (instructionsPopup != null)
        {
            instructionsPopup.SetActive(true);
        }
        else
        {
            Debug.LogError("Instructions Popup is not assigned in the Inspector.");
        }
    }

    public void HideInstructionsPopup()
    {
        Debug.Log("HideInstructionsPopup called");
        if (instructionsPopup != null)
        {
            instructionsPopup.SetActive(false);
        }
        else
        {
            Debug.LogError("Instructions Popup is not assigned in the Inspector.");
        }
    }
}
