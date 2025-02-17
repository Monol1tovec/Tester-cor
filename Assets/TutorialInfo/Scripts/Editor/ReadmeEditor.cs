using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float thrustForce = 10f; // Сила тяги двигателя
    public float rotationTorque = 100f; // Момент силы для поворота
    public float rollTorque = 50f; // Момент силы для крена
    public float maxSpeed = 50f; // Максимальная скорость корабля
    public GameObject laserPrefab; // Префаб лазера для стрельбы
    public Transform laserSpawnPoint; // Точка, откуда будет вылетать лазер

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.linearDamping = 1f; // Сопротивление движению
        rb.angularDamping = 1f; // Сопротивление вращению
    }

    void Update()
    {
        // Стрельба
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Левая кнопка мыши
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Движение вперёд (по оси Z)
        if (Input.GetKey(KeyCode.W))
        {
            ApplyThrust(Vector3.forward);
        }

        // Движение назад (по оси Z)
        if (Input.GetKey(KeyCode.S))
        {
            ApplyThrust(Vector3.back);
        }

        // Движение вверх (по оси Y)
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(Vector3.up);
        }

        // Движение вниз (по оси Y)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ApplyThrust(Vector3.down);
        }

        // Поворот влево (вращение вокруг оси Y)
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.up, -rotationTorque);
        }

        // Поворот вправо (вращение вокруг оси Y)
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.up, rotationTorque);
        }

        // Крен влево (вращение вокруг оси Z)
        if (Input.GetKey(KeyCode.Q))
        {
            ApplyRotation(Vector3.forward, rollTorque);
        }

        // Крен вправо (вращение вокруг оси Z)
        if (Input.GetKey(KeyCode.E))
        {
            ApplyRotation(Vector3.forward, -rollTorque);
        }
    }

    void ApplyThrust(Vector3 direction)
    {
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddRelativeForce(direction * thrustForce, ForceMode.Force);
        }
    }

    void ApplyRotation(Vector3 axis, float torque)
    {
        rb.AddRelativeTorque(axis * torque, ForceMode.Force);
    }

    void Shoot()
    {
        if (laserPrefab != null && laserSpawnPoint != null)
        {
            Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        }
    }
}