using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;  // Drag your panel here in the Inspector

    void Start()
    {
        // Ensure the panel is hidden at the start
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector.");
        }
    }

    public void ShowPopup()
    {
        Debug.Log("ShowPopup called");
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector.");
        }
    }

    public void HidePopup()
    {
        Debug.Log("HidePopup called");
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector.");
        }
    }
}
