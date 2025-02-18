using UnityEngine;

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