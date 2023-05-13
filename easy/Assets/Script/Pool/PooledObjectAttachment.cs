using UnityEngine;

public class PooledObjectAttachment : MonoBehaviour
{
    private SubPool m_pool;
    [SerializeField] private bool isActive;
    public void SetPool(SubPool pool)
    {
        m_pool = pool;
    }

    public void PutBackToPool()
    {
        m_pool.Despawn(this.gameObject);
    }
    private void Update()
    {
        isActive = gameObject.activeInHierarchy;
    }
}
