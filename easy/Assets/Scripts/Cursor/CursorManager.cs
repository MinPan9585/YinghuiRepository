using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    RaycastHit[] hits;

    public Texture2D cursor1, cursor2;
    public bool isCursor2;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Update()
    {
        //Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
        SetCursorTexture();
        if (isCursor2)
        {
            Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor1, new Vector2(64, 64), CursorMode.Auto);
        }
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 100f);
        isCursor2 = false;
        for (int i = 0; i < hits.Length; i++)
        {
            //RaycastHit hit = hits[i];

            if (hits[i].collider.tag == "TurretMesh")
            {
                isCursor2 = true;
            }
        }
    }
}
