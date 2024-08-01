using UnityEngine;

public class FollowMouse_Script : MonoBehaviour
{
    private Camera mainCam;

    [Header("Smooth following speed")]
    public float smoothSpeed = 0.1f;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        Vector3 mousePosition = GetMousePosition();
        FollowMousePosition(mousePosition);
        
    }
    private void FollowMousePosition(Vector3 position)
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothPosition;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        return position;
    }
}
