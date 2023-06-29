using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandleRotation : MonoBehaviour
{
    private Rigidbody stone;
    public Vector3 forwardDir;
    public void SetStone(Rigidbody stone)
    {
        this.stone = stone;
    }
    private void FixedUpdate()
    {
        GetDirection();
    }
    //private void Start()
    //{
    //    InvokeRepeating(nameof(GetDirection), 0, 0.2f);
    //}
    void GetDirection()
    {
        forwardDir = stone.velocity;

        transform.LookAt(transform.position + forwardDir);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardDir);
    }

}
