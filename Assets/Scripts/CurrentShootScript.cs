using UnityEngine;
using System.Collections.Generic;

public class CurrentShootScript : MonoBehaviour
{
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask suckableLayers = ~0;
    [SerializeField] private float vacuumRange = 8f;
    [SerializeField] private float vacuumRadius = 0.5f;
    [SerializeField] private float suctionSpeed = 8f;
    [SerializeField] private float captureDistance = 1.25f;
    [SerializeField] private float launchSpeed = 15f;
    [SerializeField] private int storageCapacity = 5;

    private readonly Queue<StoredObject> storedObjects = new Queue<StoredObject>();
    private Rigidbody suctionTarget;

    private sealed class StoredObject
    {
        public GameObject gameObject;
        public bool useGravity;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && storedObjects.Count < storageCapacity)
        {
            Suction();
        }
        else
        {
            suctionTarget = null;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Suction()
    {
        if (suctionTarget == null)
        {
            Vector3 origin = transform.position + transform.forward * 0.5f;
            if (!Physics.SphereCast(origin, vacuumRadius, transform.forward, out RaycastHit hit,
                    vacuumRange, suckableLayers, QueryTriggerInteraction.Ignore))
            {
                return;
            }

            suctionTarget = hit.rigidbody;
            if (suctionTarget == null || suctionTarget.transform.IsChildOf(transform))
            {
                suctionTarget = null;
                return;
            }
        }

        Vector3 holdPosition = transform.position + transform.forward * captureDistance;
        Vector3 toHoldPosition = holdPosition - suctionTarget.position;
        if (toHoldPosition.sqrMagnitude <= captureDistance * captureDistance * 0.04f)
        {
            Store(suctionTarget);
            suctionTarget = null;
            return;
        }

        suctionTarget.linearVelocity = toHoldPosition.normalized * suctionSpeed;
    }

    private void Store(Rigidbody target)
    {
        storedObjects.Enqueue(new StoredObject
        {
            gameObject = target.gameObject,
            useGravity = target.useGravity
        });

        target.linearVelocity = Vector3.zero;
        target.angularVelocity = Vector3.zero;
        target.gameObject.SetActive(false);
    }

    private void Shoot()
    {
        if (storedObjects.Count > 0)
        {
            StoredObject storedObject = storedObjects.Dequeue();
            GameObject item = storedObject.gameObject;
            item.transform.SetPositionAndRotation(transform.position, transform.rotation);
            item.SetActive(true);

            Rigidbody body = item.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.isKinematic = false;
                body.useGravity = storedObject.useGravity;
                body.linearVelocity = transform.forward * launchSpeed;
            }

            return;
        }

        // if (bulletPrefab != null)
        // {
        //     GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //     Destroy(bullet, 1.5f);
        // }
    }
}
