using UnityEngine;

public class Knife_Script : MonoBehaviour
{
    [Header("Minimum velocity needed for knife to cut")]   
    public float minVelocityNeeded = 0.01f;

    private Collider knifeCollider;
    private Camera mainCam;
    private TrailRenderer trailRenderer;
    public bool isCutting { get; private set; }
    public Vector3 cutDirection { get; private set; }

    private void Awake()
    {   
        mainCam = Camera.main;
        knifeCollider = GetComponent<Collider>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopCutting();
    }

    private void OnDisable()
    {
        StopCutting();
    }

    private void Update()
    {
        CheckInput();      
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        else if (isCutting)
        {
            UpdateCutting();
        }
    }

    private void StartCutting()
    {   
        isCutting = true;
        knifeCollider.enabled = true;
        trailRenderer.enabled = true;
        Vector3 position = GetMousePosition();
        transform.position = position;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        return position;
    }

    private void StopCutting()
    {
        isCutting = false;
        knifeCollider.enabled = false;
        trailRenderer.enabled = false;
        trailRenderer.Clear();
    }

    private void UpdateCutting()
    {
        Vector3 newPosition = GetMousePosition();
        Vector3 direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        knifeCollider.enabled = velocity > minVelocityNeeded;
        
        transform.position = newPosition;
        cutDirection = direction;

    }

}
