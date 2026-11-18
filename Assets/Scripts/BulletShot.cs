using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
    }
}
