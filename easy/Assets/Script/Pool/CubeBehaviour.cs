using UnityEngine;
using UnityEngine.Pool;

public class CubeBehaviour : MonoBehaviour, IPooledObject //This interface is needed for pooled objects
{
    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;

        Invoke("TimeToDisabe", 2f);
    }
    //Must have, even left blank.
    public void OnObjectDespawn()
    {
    }

    private void OnDisable()//用这个来代替OnObjectDespawn()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    //using pool end

    public float upForce = 25f;
    public float sideForce = 7f;

    //option two, testing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("I have Hit the ground!");

            GetComponent<PooledObjectAttachment>().PutBackToPool();
        }
    }    

    private void TimeToDisabe()
    {
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }
}
