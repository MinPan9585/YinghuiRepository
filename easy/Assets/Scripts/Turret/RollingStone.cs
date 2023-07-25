using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour, IPooledObject
{
    public int stoneDamage = 20;
    public float lifeTime = 5f;

    [Header("SFX")]
    public string spawnSFX = "Buzzer";
    public string rollSFX = "Phasor";
    [SerializeField] private int sfxID;
    private bool playingLoop = false;
    [Header("VFX")]
    public GameObject rollVFX;

    private void Start()
    {
        GameObject go = PoolManager.Instance.SpawnFromSubPool(rollVFX.name.ToString(), transform);
        go.transform.SetParent(transform, false);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        if (go.TryGetComponent<HandleRotation>(out HandleRotation hR))
            hR.SetStone(GetComponent<Rigidbody>());
    }
    #region Pool
    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        Invoke(nameof(DestroyStone), lifeTime);
        AudioManager.Instance.PlaySFX(spawnSFX);
        
        playingLoop = false;
        if (!playingLoop)
        {
            playingLoop = true;
            sfxID = AudioManager.Instance.PlaySFXLoop(rollSFX);
        }
    }
    //Must have, even left blank.
    public void OnObjectDespawn()
    {
    }
    //specifically for restoring some values, like health
    public void RestoreValues()
    {
        
    }
    //using pool end
    #endregion

    private void DestroyStone()
    {
        if (playingLoop)
        {
            playingLoop = false;
            AudioManager.Instance.StopSFXLoop(sfxID);
        }
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<KnockedOff>(out KnockedOff minion))
            {
                minion.KnockOff();
            }
            else
            {
                ITakeDamage e = other.GetComponent<ITakeDamage>();
                e.TakeDamage(stoneDamage);
            }
        }
    }
}
