using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;
    public float gravity = -9.81f;

    public float stepNoiseOuter = 8f;
    public float stepNoiseInner = 3f;
    public float stepCooldown = 0f;

    // 摆腿相关
    public Transform leftLeg;
    public Transform rightLeg;
    public float legSwingSpeed = 8f;
    public float legSwingAngle = 25f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalVelocity;

    private float stepTimer;

    // 击退速度
    private Vector3 knockbackVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleLegSwing();
        ApplyGravity();
        MoveCharacter();
        FaceMouse();
        HandleFootstepNoise();

        // 击退逐渐减弱
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, 5f * Time.deltaTime);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMove = new Vector3(moveX, 0, moveZ);

        if (horizontalMove.sqrMagnitude > 0.01f)
        {
            horizontalMove = horizontalMove.normalized;
        }

        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sneakSpeed;
        }

        moveDirection = horizontalMove * currentSpeed;
    }

    void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;
    }

    void MoveCharacter()
    {
        // 把击退加入移动
        Vector3 finalMove = moveDirection + knockbackVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    void FaceMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane ground = new Plane(Vector3.up, transform.position);
        float distance;

        if (ground.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    void HandleFootstepNoise()
    {
        stepTimer -= Time.deltaTime;

        bool isSneaking = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = new Vector3(moveDirection.x, 0, moveDirection.z).magnitude > 0.1f;

        if (!isSneaking && isMoving && stepTimer <= 0f)
        {
            if (NoiseSystem.Instance != null)
            {
                NoiseSystem.Instance.MakeNoise(
                    transform.position,
                    stepNoiseOuter,
                    stepNoiseInner
                );
            }

            stepTimer = stepCooldown;
        }
    }

    void HandleLegSwing()
    {
        Vector3 horizontalMove = new Vector3(moveDirection.x, 0, moveDirection.z);
        bool isMoving = horizontalMove.magnitude > 0.1f;

        if (isMoving)
        {
            float angle = Mathf.Sin(Time.time * legSwingSpeed) * legSwingAngle;

            if (leftLeg != null)
                leftLeg.localRotation = Quaternion.Euler(angle, 0, 0);

            if (rightLeg != null)
                rightLeg.localRotation = Quaternion.Euler(-angle, 0, 0);
        }
        else
        {
            if (leftLeg != null)
                leftLeg.localRotation = Quaternion.Lerp(leftLeg.localRotation, Quaternion.identity, Time.deltaTime * 8f);

            if (rightLeg != null)
                rightLeg.localRotation = Quaternion.Lerp(rightLeg.localRotation, Quaternion.identity, Time.deltaTime * 8f);
        }
    }

    // 敌人调用的击退函数
    public void AddKnockback(Vector3 force)
    {
        knockbackVelocity = force;
    }
}