using UnityEngine;
using System.Collections.Generic;

public class CurrentShootScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject slime;
    [SerializeField] private Transform firePoint;
    public float vacumeSpeed = 1.0f;
    private List<GameObject> objectsToVacuum = new List<GameObject>();

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
                if(objectsToVacuum.Count > 0)
                {
                    for(int i = 0; i < objectsToVacuum.Count; i++)
                    {
                        float distance = Vector3.Distance(firePoint.position, objectsToVacuum[i].transform.position);
                        if (distance >= 0.1f)
                        {
                            // objectsToVacuum[i].GetComponent<Rigidbody>().linearVelocity = -(objectsToVacuum[i].transform.position - transform.position) * vacumeSpeed;
                            objectsToVacuum[i].transform.position = Vector3.MoveTowards(objectsToVacuum[i].transform.position, firePoint.transform.position, vacumeSpeed * Time.deltaTime);
                            Debug.Log("#Sucked" + distance);
                            
                        }
                        else
                        {
                            objectsToVacuum[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                            Debug.Log("Stop");
                        }
                    }
                }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vacuumable") && !objectsToVacuum.Contains(other.gameObject))
        {
            objectsToVacuum.Add(other.gameObject);
        }
    }
}

    // private void Shoot()
    // {
    //     if (bulletPrefab != null)
    //     {
    //         GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    //         Destroy(bullet, 1.5f);
    //     }
    // }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Shoot();
        // }