using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorStart : MonoBehaviour
{
    public Texture2D cursor;

    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(128, 128), CursorMode.Auto);
    }

}
