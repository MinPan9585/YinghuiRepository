using UnityEngine;

public class ChildOnTrigger : MonoBehaviour
{
    public GameObject parentObejct;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            transform.SetParent(other.gameObject.transform);

            Debug.Log("Parent is set to: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            transform.SetParent(null);

            Debug.Log("Parent is set to null");
        }
    }
}
