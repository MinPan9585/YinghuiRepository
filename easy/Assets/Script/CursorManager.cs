using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    RaycastHit hitInfo;

    public Texture2D cursor1, cursor2;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Update()
    {
        SetCursorTexture();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo))
        {
            //set cursor textures
            switch (hitInfo.collider.tag)
            {
                case "Shoot":
                    Cursor.SetCursor(cursor2, new Vector2(128,128), CursorMode.Auto);
                    break;
                case "ThrowStone":
                    Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
                    break;
                case "Lazer":
                    Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
                    break;
                case "AOE":
                    Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
                    break;
                case "Untagged":
                    Cursor.SetCursor(cursor2, new Vector2(128, 128), CursorMode.Auto);
                    break;
                case "TurretMesh":
                    Cursor.SetCursor(cursor1, new Vector2(128, 128), CursorMode.Auto);
                    break;

            }
        }
    }
}
