using UnityEngine;
using UnityEngine.Pool;

public class CubeBehaviour : MonoBehaviour
{
    private ObjectPool<GameObject> thatCubePool;
    public void SetPool(ObjectPool<GameObject> pool)
    {
        thatCubePool= pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit the ground");
            thatCubePool.Release(this.gameObject);
        }
    }
}
