using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretThrowStone : MonoBehaviour
{
    [Header("Rolling Stone Baby")]
    public GameObject stonePrefab;
    private Transform throwPosition;
    public float rollingSpeed = 8f;

    public float coolDown = 1f;
    private float timer = 0f;
    private bool readyToThrow;
    public float animationGap = 0.4f;
    public Animator myAnim;
    //private BoxCollider col;
    //private float colCenter;


    private void Start()
    {
        timer = 0f;
        readyToThrow = true;
        throwPosition = transform.Find("ThrowPosition");
        //col = GetComponent<BoxCollider>();
        //colCenter = rollingSpeed / 2f + 0.5f;
        //col.center = new Vector3(0, 0, colCenter);
    }

    IEnumerator ThrowStone()
    {
        Debug.Log(gameObject.name + ": Start Animation here");
        myAnim.SetTrigger("Attack");

        yield return new WaitForSeconds(animationGap);

        GameObject go = PoolManager.Instance.SpawnFromSubPool(stonePrefab.name.ToString(), transform);
        go.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
        go.transform.position = throwPosition.position;

        float zForce = Random.Range(rollingSpeed - 1f, rollingSpeed + 1f);
        go.GetComponent<Rigidbody>().velocity = transform.forward * zForce;
    }

    private void Update()
    {
        #region Timer
        if (timer < coolDown && !readyToThrow)
        {
            timer += Time.deltaTime;
        }
        else
        {
            readyToThrow = true;
            timer = 0f;
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && readyToThrow)
        {
            readyToThrow = false;
            StartCoroutine(ThrowStone());
        }
    }
}
