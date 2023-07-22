using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the position of the mouse in screen coordinates
        Vector3 mousePositionScreen = Input.mousePosition;

        // Convert the screen coordinates to world coordinates using the camera
        Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(mousePositionScreen);

        // Ensure the object stays at its original Z position (you can adjust this if needed)
        mousePositionWorld.z = transform.position.z;

        // Update the object's position to follow the mouse
        transform.position = mousePositionWorld;
    }
}
