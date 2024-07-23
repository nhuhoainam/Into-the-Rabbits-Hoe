using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {

        popupPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            popupPanel.SetActive(!popupPanel.activeSelf);
        }
    }
}