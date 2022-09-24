using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public GameObject destroyedPrefab;
    public GameObject spherePrefab;

    public float movementSpeed = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            float rotationInput = Input.mousePosition.x - Camera.main.pixelWidth / 2f;

            transform.Rotate(Vector3.up * Mathf.Clamp(rotationInput, -1f, 1f) * rotationSpeed * Time.deltaTime);

        }


        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("AI"))
        {
            Debug.Log("Game Over!");
            GameOver();
        }else if (collision.collider.CompareTag("Gift"))
        {
            Destroy(collision.gameObject);
            Instantiate(spherePrefab, transform);
        }
    }

    private void GameOver()
    {
        GameObject destroyedGO = Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Destroy(destroyedGO, 2f);
        Destroy(gameObject);
    }
}
