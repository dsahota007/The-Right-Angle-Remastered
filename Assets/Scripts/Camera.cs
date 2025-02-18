using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign TrianglePlayer in the Inspector
    public float fixedX = 0f; // Set this to the desired X position
    public Vector3 offset = new Vector3(0, 0, -10); // Adjust for proper view

    void LateUpdate()
    {
        if (player != null)
        {
            // Keep X fixed, only follow Y and Z
            transform.position = new Vector3(fixedX, player.position.y + offset.y, offset.z);

            // Lock rotation to prevent spinning
            transform.rotation = Quaternion.identity;
        }
    }
}
