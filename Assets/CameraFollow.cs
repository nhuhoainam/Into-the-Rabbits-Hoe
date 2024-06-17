using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector

    public Vector3 offset; // Set the offset to position the camera

    void Start()
    {
        // If you want to calculate the initial offset automatically, uncomment the next line
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Set the position of the camera to follow the player, with the offset
        transform.position = player.position + offset;
    }
}
