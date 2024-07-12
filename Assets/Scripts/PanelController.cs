using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using System.Collections.Generic; // Menambahkan direktif ini untuk menggunakan List<>

public class PanelController : MonoBehaviour
{
    public GameObject videoPanel; // Assign your panel GameObject here
    public Button showVideoButton;
    public VideoPlayer videoPlayer; // Assign your VideoPlayer here

    void Start()
    {
        // Ensure the panel is initially hidden
        videoPanel.SetActive(false);

        // Add listener to the button
        showVideoButton.onClick.AddListener(ShowVideoPanel);

        // Set video to loop
        videoPlayer.isLooping = true;
    }

    void Update()
    {
        // Detect if the video panel is active and a touch or mouse click occurred
        if (videoPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Check if the click was outside the panel
            if (!IsPointerOverUIElement())
            {
                CloseVideoPanel();
            }
        }
    }

    void ShowVideoPanel()
    {
        videoPanel.SetActive(true);
        videoPlayer.Play();
    }

    void CloseVideoPanel()
    {
        // Stop the video player only if you want to stop the video
        //videoPlayer.Stop();
        videoPanel.SetActive(false);
    }

    // Helper function to check if the pointer is over a UI element
    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            if (result.gameObject == videoPanel || result.gameObject.transform.IsChildOf(videoPanel.transform))
            {
                return true; // Pointer is over the video panel or its children
            }
        }
        return false; // Pointer is not over the video panel or its children
    }
}
