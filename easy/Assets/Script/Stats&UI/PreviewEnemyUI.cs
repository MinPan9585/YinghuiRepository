using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PreviewEnemyUI : MonoBehaviour
{
    [Header("Needs Assign")]
    //public TMP_Text nextRoundText;
    public Button nextRoundButton;
    public Image BGImage;
    public List<GameObject> checkWaves;
    public List<GameObject> enemyPreviews;
    [Header("Tuning")]
    public int horizontalOffset = 200;
    public int verticalOffset = 50;
    [Header("Parameters, ignore")]
    public List<PreviewElement> previewElements = new List<PreviewElement>();
    public List<Wave> waves;
    public int enemyCount = 0;
    public List<int> indexs = new List<int>();
    void Start()
    {
        GameEvents.Instance.OnPreviewEnemy += UpdateEnemyPreview;
        GameEvents.Instance.OnRoundSpawned += ClearPreview;

        foreach (GameObject obj in enemyPreviews)
        {
            Image image = obj.GetComponentInChildren<Image>();
            TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
            PreviewElement go = new PreviewElement(image, text);
            previewElements.Add(go);
        }
        //ClearPreview();
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnPreviewEnemy -= UpdateEnemyPreview;
        GameEvents.Instance.OnRoundSpawned -= ClearPreview;
    }
    public void UpdateEnemyPreview(List<Wave> waves)
    {
        ClearPreview();
        nextRoundButton.image.enabled = true;
        nextRoundButton.GetComponentInChildren<TMP_Text>().enabled = true;
        BGImage.enabled = true;
        BGImage.GetComponentInChildren<TMP_Text>().enabled = true;
        //nextRoundText.text = "Next Round: ";
        this.waves = waves;

        foreach (Wave wave in waves)
        {
            if (checkWaves.Contains(wave.enemyPrefab))
            {
                if (indexs.Contains(checkWaves.IndexOf(wave.enemyPrefab)))
                {
                    continue;
                }
                int index = checkWaves.IndexOf(wave.enemyPrefab);
                previewElements[index].enemyImage.enabled = true;
                previewElements[index].enemyText.text = "× " + wave.count.ToString();
                indexs.Add(index);

                enemyCount++;
            }
        }
        ArrangePosition();

    }
    public void ArrangePosition()
    {
        int positionCount = 0;
        for (int i = 0; i < indexs.Count; i++)
        {
            positionCount++;
            float k = -(float)(enemyCount - 1) / 2 + positionCount - 1;
            RectTransform trans = enemyPreviews[indexs[i]].GetComponent<RectTransform>();
            trans.anchoredPosition = Vector3.up * verticalOffset
                + (Vector3.right * (horizontalOffset * k));
        }

    }
    public void ClearPreview()
    {
        nextRoundButton.image.enabled = false;
        nextRoundButton.GetComponentInChildren<TMP_Text>().enabled = false;
        BGImage.enabled = false;
        BGImage.GetComponentInChildren<TMP_Text>().enabled = false;

        //nextRoundText.text = "";
        foreach (PreviewElement go in previewElements)
        {
            go.enemyImage.enabled = false;
            go.enemyText.text = null;
        }

        enemyCount = 0;
        indexs.Clear();
    }
}

