using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float speed = 10f; // Скорость движения корабля
    public float rotationSpeed = 100f; // Скорость поворота
    public float rollSpeed = 50f; // Скорость крена (вращение вокруг оси Z)
    public GameObject laserPrefab; // Префаб лазера для стрельбы
    public Transform laserSpawnPoint; // Точка, откуда будет вылетать лазер

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Движение вперёд (по оси Z)
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }

        // Движение назад (по оси Z)
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        }

        // Движение вверх (по оси Y)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode.VelocityChange);
        }

        // Движение вниз (по оси Y)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(-transform.up * speed * Time.deltaTime, ForceMode.VelocityChange);
        }

        // Поворот влево (вращение вокруг оси Y)
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // Поворот вправо (вращение вокруг оси Y)
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Крен влево (вращение вокруг оси Z)
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);
        }

        // Крен вправо (вращение вокруг оси Z)
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rollSpeed * Time.deltaTime);
        }

        // Стрельба
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Левая кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (laserPrefab != null && laserSpawnPoint != null)
        {
            Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        }
    }
}