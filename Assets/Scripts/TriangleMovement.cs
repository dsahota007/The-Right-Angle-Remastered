using UnityEngine;

public class SlingshotLaunch : MonoBehaviour
{
    public float maxPower = 15f;
    public float powerMultiplier = 5f;
    private Vector2 startDragPosition;
    private Vector2 endDragPosition;
    private bool isDragging = false;
    private bool canLaunch = true; // ✅ Prevents launching while moving
    private Rigidbody2D rb;

    public float velocityThreshold = 0.1f; // ✅ Allow launching only when speed is very low

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Load saved position
        Vector2 savedPosition = SaveManager.instance.LoadPlayerPosition();

        // Prevent spawning inside walls or mid-air
        RaycastHit2D groundCheck = Physics2D.Raycast(savedPosition, Vector2.down, 1f);
        if (groundCheck.collider != null)
        {
            transform.position = savedPosition;
        }
        else
        {
            transform.position = new Vector2(savedPosition.x, -3); // Default to safe ground
        }
    }

    void Update()
    {
        // ✅ Only allow launching when the player is not moving
        if (canLaunch)
        {
            HandleInput();
        }
        else
        {
            CheckIfStopped(); // Continuously check if the player has stopped moving
        }
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
        if (!canLaunch) return; // ✅ Prevent launching while moving

        Vector2 launchDirection = startDragPosition - endDragPosition;
        float pullDistance = launchDirection.magnitude;
        float launchPower = Mathf.Clamp(pullDistance * powerMultiplier, 0, maxPower);

        rb.velocity = Vector2.zero;
        rb.AddForce(launchDirection.normalized * launchPower, ForceMode2D.Impulse);

        canLaunch = false; // ❌ Disable launching until the player stops moving
    }

    // ✅ Check if the player has completely stopped moving
    void CheckIfStopped()
    {
        if (rb.velocity.magnitude < velocityThreshold)
        {
            canLaunch = true; // ✅ Enable launching when fully stopped
        }
    }
}
