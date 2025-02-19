using UnityEngine;

public class SlingshotLaunch : MonoBehaviour
{
    public float maxPower = 50f;
    public float powerMultiplier = 8.5f;
    public float movementThreshold = 0.2f; // ✅ Maximum velocity to allow launching

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
            if (rb.velocity.magnitude <= movementThreshold) // ✅ Check if player is still
            {
                startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
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
        if (rb.velocity.magnitude > movementThreshold) return; // ✅ Prevent launching if moving

        Vector2 launchDirection = startDragPosition - endDragPosition;
        float pullDistance = launchDirection.magnitude;
        float launchPower = Mathf.Clamp(pullDistance * powerMultiplier, 0, maxPower);

        rb.velocity = Vector2.zero;
        rb.AddForce(launchDirection.normalized * launchPower, ForceMode2D.Impulse);
    }
}
