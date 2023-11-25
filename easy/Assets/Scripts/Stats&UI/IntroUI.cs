using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

[System.Serializable]
public class BaseInfo
{
    public string icon;
    public List<string> panel;
    public string text;
    public string cameraPosition;
}

[System.Serializable]
public class IntroInfo
{
    public Dictionary<string, List<BaseInfo>> introDictionary;

    public IntroInfo()
    {
        introDictionary = new Dictionary<string, List<BaseInfo>>();
    }
}

public class IntroUI : MonoBehaviour
{
    [Header("Intro Display")]
    public Image BGImage;
    public StatusManager statusManager;

    private string currentIntro = "bgIntro";
    private IntroInfo introInfo = new();
    private int currentIntroIndex;
    private TextMeshProUGUI introText;
    private Button nextBut;
    private GameObject mainCamera;
    private Vector3 initCameraPosition;
    private Quaternion initCameraRotation;

    private void Awake()
    {
        LoadIntroStringFromJson();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.OnUpdateIntro += UpdateIntro;

        currentIntroIndex = 0;
        introText = BGImage.GetComponentInChildren<TextMeshProUGUI>();
        nextBut = BGImage.GetComponentInChildren<Button>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        initCameraPosition = mainCamera.transform.position;
        initCameraRotation = mainCamera.transform.rotation;

        if (introInfo.introDictionary[currentIntro]?.Count > 0)
        {
            ShowIntroPanel();
        }
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnUpdateIntro -= UpdateIntro;
    }

    public void OnButClick()
    {
        // ��ǰ����û����ͼ�����
        if (introInfo.introDictionary[currentIntro].Count > currentIntroIndex + 1)
        {
            introText.text = introInfo.introDictionary[currentIntro][currentIntroIndex + 1].text;

            // ������� cameraPosition ����������Ҫת�������λ�ã���������ڣ��ҵ�ǰ���λ�úͳ�ʼλ�ò�ͬ������Ҫ��ԭ���λ��
            //if (introInfo.introDictionary[currentIntro][currentIntroIndex + 1].cameraPosition != null)
            //{
            //    GameObject target = GameObject.FindGameObjectWithTag(introInfo.introDictionary[currentIntro][currentIntroIndex + 1].cameraPosition);
            //    Debug.Log(target);
            //    if (target)
            //    {
            //        Debug.Log("111111");
            //        mainCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            //    }
            //    else
            //    {
            //        if (mainCamera.transform.position != initCameraPosition)
            //        {
            //            mainCamera.transform.position = initCameraPosition;
            //            // mainCamera.transform.rotation = initCameraRotation;
            //        }
            //    }
            //}

            currentIntroIndex += 1;

        }
        else
        {
            if (currentIntro == "bgIntro")
            {
                GameEvents.Instance.StatusManagerInit();
            }
            CloseIntroPanel();
        }
    }

    public void UpdateIntro(string introName)
    {
        currentIntro = introName;
        if (introInfo.introDictionary[currentIntro]?.Count > 0)
        {
            ShowIntroPanel();
        }
    }

    // ��չʾ����ʱ����Ϸʱ����Ҫ��ͣ
    private void ShowIntroPanel()
    {
        introText.text = introInfo.introDictionary[currentIntro][0].text;
        BGImage.enabled = true;
        introText.enabled = true;
        nextBut.image.enabled = true;
        nextBut.interactable = true;
        Time.timeScale = 0f;
    }

    // ���رգ����Ż�����㣬��Ϸʱ��ָ�
    private void CloseIntroPanel()
    {
        BGImage.enabled = false;
        introText.enabled = false;
        nextBut.image.enabled = false;
        nextBut.interactable = false;
        introText.text = null;
        currentIntro = null;
        currentIntroIndex = 0;
        Time.timeScale = 1f;
    }

    private void LoadIntroStringFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("IntroJson/" + SceneManager.GetActiveScene().name);
        if (jsonFile)
        {
            string jsonData = jsonFile.text;
            introInfo.introDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<BaseInfo>>>(jsonData);
        }
        else
        {
            Debug.LogError("Cannot find Intro JSON file!");
        }
    }
}
