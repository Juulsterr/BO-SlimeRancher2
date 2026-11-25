using UnityEngine;
using System.Collections.Generic;

public class CurrentShootScript : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private float suckRange = 5.0f;
    [SerializeField] private float shootSpeed = 10.0f;
    [SerializeField] private float shootUpwardBoost = 1.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float radius = 5f;
    public float vacuumeSpeed = 3.0f;
    private List<GameObject> objectsToVacuum = new List<GameObject>();


    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Collider[] nearbyObjects = Physics.OverlapSphere(firePoint.position, suckRange);
            foreach (Collider nearbyObject in nearbyObjects)
            {
                GameObject objectToVacuum = nearbyObject.gameObject;
                if (!nearbyObject.CompareTag("Vacuumable") || objectsToVacuum.Contains(objectToVacuum))
                {
                    continue;
                }

                float distance = Vector3.Distance(firePoint.position, objectToVacuum.transform.position);
                if (distance <= 0.3f)
                {
                    if (objectToVacuum.TryGetComponent<Rigidbody>(out Rigidbody body))
                    {
                        body.linearVelocity = Vector3.zero;
                    }

                    objectToVacuum.SetActive(false);
                    objectsToVacuum.Add(objectToVacuum);
                }
                else if (distance <= suckRange)
                {
                    objectToVacuum.GetComponent<Rigidbody>().linearVelocity = (Random.onUnitSphere * radius);
                    objectToVacuum.transform.position = Vector3.MoveTowards(
                        objectToVacuum.transform.position,
                        firePoint.position,
                        vacuumeSpeed * Time.deltaTime);
                }
            }
        }

        Shoot();
        
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(1) && objectsToVacuum.Count > 0)
        {
            GameObject slime = objectsToVacuum[0];
            objectsToVacuum.RemoveAt(0);
            slime.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            slime.SetActive(true);

            if (slime.TryGetComponent<Rigidbody>(out Rigidbody body))
            {
                body.linearVelocity = firePoint.forward * shootSpeed;
                body.AddForce(Vector3.down * shootUpwardBoost, ForceMode.VelocityChange);
            }
        } 
    }
}