using UnityEngine;
using UnityEngine.Pool;

public class CubeBehaviour : MonoBehaviour, IPooledObject
{
    //option one, functional
    public TestSpawn testSpawn;

    //option two, functional but a bit tricky
    private SubPool m_pool;

    public float upForce = 25f;
    public float sideForce = 7f;

    public void SetPool(SubPool pool)
    {
        m_pool = pool;
    }

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity= force;
    }

    //option two, testing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("I have Hit the ground!");

            m_pool.Despawn(this.gameObject);

        }
    }

    public void OnObjectDespawn()
    {
        transform.localPosition = Vector3.zero; 
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
