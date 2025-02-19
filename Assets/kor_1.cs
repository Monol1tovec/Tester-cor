using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float pitchPower = 2f;
    public float rollPower = 2f;
    public float yawPower = 1f;
    public float baseEnginePower = 10f;
    public float maxEnginePower = 50f;
    public float accelerationRate = 5f;
    public float rotationSmoothness = 5f;

    [Header("Physics Settings")]
    public float drag = 1f;
    public float angularDrag = 2f;

    private float currentEnginePower;
    private Rigidbody rb;
    private float currentPitch;
    private float currentRoll;
    private float currentYaw;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
        currentEnginePower = baseEnginePower;
    }

    private void Update()
    {
        HandleThrottle();
        HandleRotationInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    void HandleThrottle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressingThrottle = !pressingThrottle;
        }
=======
public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(
            -mouseDistance.y * lookRateSpeed * Time.deltaTime,
            mouseDistance.x * lookRateSpeed * Time.deltaTime,
            rollInput * rollSpeed * Time.deltaTime,
            Space.Self
        );

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
>>>>>>> 123
    }

    void HandleRotationInput()
    {
        // ������� ������������ �����
        float targetPitch = Input.GetAxis("Vertical") * pitchPower;
        float targetRoll = Input.GetAxis("Horizontal") * rollPower;
        float targetYaw = Input.GetAxis("Yaw") * yawPower;

        currentPitch = Mathf.Lerp(currentPitch, targetPitch, rotationSmoothness * Time.deltaTime);
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, rotationSmoothness * Time.deltaTime);
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, rotationSmoothness * Time.deltaTime);
    }

    void ApplyMovement()
    {
        if (pressingThrottle)
        {
            // ������� ������
            currentEnginePower = Mathf.Lerp(currentEnginePower, maxEnginePower, accelerationRate * Time.deltaTime);
            rb.AddForce(transform.forward * currentEnginePower, ForceMode.Acceleration);
        }
        else
        {
            // ������� ����������
            currentEnginePower = Mathf.Lerp(currentEnginePower, baseEnginePower, accelerationRate * Time.deltaTime);
        }
    }

    void ApplyRotation()
    {
        // ��������� �������� ����� ������
        Vector3 rotationTorque = new Vector3(
            currentPitch,
            currentYaw,
            -currentRoll
        );

        rb.AddTorque(rotationTorque, ForceMode.Acceleration);
    }

    public bool pressingThrottle { get; private set; } = false;
}