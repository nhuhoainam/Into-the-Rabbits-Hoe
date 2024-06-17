using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraBound : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        // Get the new bounding box
        var virtualCamera = GameObject.FindWithTag("VirtualCamera");
        if (virtualCamera == null)
        {
            Debug.LogWarning("No virtual camera found in the scene");
            return;
        }
        var boundingBox = GameObject.FindWithTag("CameraBounding");
        if (boundingBox != null)
        {
            var confiner = virtualCamera.GetComponent<CinemachineConfiner>();
            if (confiner != null)
            {
                confiner.m_BoundingShape2D = boundingBox.GetComponent<PolygonCollider2D>();
            }
            else {
                Debug.LogWarning("No CinemachineConfiner found on the virtual camera");
            }
        }
        else {
            Debug.LogWarning("No bounding box found in the scene");
        }
    }
}
