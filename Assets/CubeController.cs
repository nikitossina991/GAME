using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 2f; // Скорость машины
    public float turnSpeed = 50f; // Скорость поворота машины

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddForce(transform.forward * speed);
      

        // Применяем движение машины вперед или назад

        // Применяем поворот машины влево или вправо
    }
}
