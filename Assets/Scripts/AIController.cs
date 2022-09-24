using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject destroyedPrefab;

    public Transform target;
    public float minSpeed, maxSpeed;


    private Rigidbody rb;
    private float movementSpeed;

    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        rb = GetComponent<Rigidbody>();
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        if (target == null) return;


        rb.position = Vector3.MoveTowards(rb.position, target.position, movementSpeed * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("AI") || collision.collider.CompareTag("Player"))
        {
            DestroyBoat();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.CompareTag("Sphere"))
            {
                Destroy(other.gameObject);
                DestroyBoat();
            }

    }

    private void DestroyBoat()
    {
        GameObject destroyedGO = Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Destroy(destroyedGO, 2f);
        Destroy(gameObject);
    }
}
