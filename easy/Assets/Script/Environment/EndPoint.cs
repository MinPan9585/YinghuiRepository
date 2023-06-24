using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [Header("SFX")]
    public string damageSFX = "Buzzer";
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBase>(out EnemyBase go))
        {
            AudioManager.Instance.PlaySFX(damageSFX);
            go.EndPath();
        }
    }
}
