using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TempMovementJ : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.25f;
    public float groundCheckOffset = 0.1f;
    public LayerMask ignoredLayers; // Optioneel: zet hier layers in die niet als grond tellen

    [SerializeField] private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = CheckGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift)
            ? sprintSpeed
            : speed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity & vertical movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool CheckGrounded()
    {
        float bottomOffset = Mathf.Max(controller.height * 0.5f - controller.radius, 0f);
        Vector3 spherePosition = transform.position
            + controller.center
            - Vector3.up * (bottomOffset + groundCheckOffset);

        int groundLayers = ~ignoredLayers;
        Collider[] hits = Physics.OverlapSphere(spherePosition, groundCheckRadius, groundLayers, QueryTriggerInteraction.Ignore);

        foreach (Collider hit in hits)
        {
            if (hit.transform != transform && !hit.transform.IsChildOf(transform))
            {
                return true;
            }
        }

        return controller.isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
        if (controller == null) return;

        float bottomOffset = Mathf.Max(controller.height * 0.5f - controller.radius, 0f);
        Vector3 spherePosition = transform.position
            + controller.center
            - Vector3.up * (bottomOffset + groundCheckOffset);

        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(spherePosition, groundCheckRadius);
    }
}