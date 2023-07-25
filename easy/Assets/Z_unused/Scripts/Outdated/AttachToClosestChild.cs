using UnityEngine;

public class AttachToClosestChild : MonoBehaviour
{
    private GameObject childObject;
    private float detectionRadius = 10f;

    public GameObject closestChild;

    void LateUpdate()
    {
        // Find the closest child to the parent object
        float closestDistance = Mathf.Infinity;
        foreach (Transform child in GameObject.Find("Ground").transform)
        {
            float distance = Vector3.Distance(child.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestChild = child.gameObject;

                childObject = closestChild;
            }
        }

        // If the closest child has changed, make this object a child of the new closest child
        if (closestChild != null && closestChild != childObject)
        {
            childObject.transform.parent = closestChild.transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around this object to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

