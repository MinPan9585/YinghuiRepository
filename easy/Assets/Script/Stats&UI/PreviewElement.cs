using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreviewElement
{
    public Image enemyImage;
    public TMP_Text enemyText;

    public PreviewElement() { }
    public PreviewElement(Image enemyImage, TMP_Text enemyText)
    {
        this.enemyImage = enemyImage;
        this.enemyText = enemyText;
    }
}
