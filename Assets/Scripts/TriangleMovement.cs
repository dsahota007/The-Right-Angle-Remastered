using UnityEngine;

public class SlingshotLaunch : MonoBehaviour
{
    public float maxPower = 15f;
    public float powerMultiplier = 5f;
    private Vector2 startDragPosition;
    private Vector2 endDragPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ✅ Load position (If new game, defaultSpawnPoint is used)
        Vector2 savedPosition = SaveManager.instance.LoadPlayerPosition();
        transform.position = savedPosition;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LaunchPlayer();
            isDragging = false;
        }
    }

    void LaunchPlayer()
    {
        Vector2 launchDirection = startDragPosition - endDragPosition;
        float pullDistance = launchDirection.magnitude;
        float launchPower = Mathf.Clamp(pullDistance * powerMultiplier, 0, maxPower);

        rb.velocity = Vector2.zero;
        rb.AddForce(launchDirection.normalized * launchPower, ForceMode2D.Impulse);
    }
}
