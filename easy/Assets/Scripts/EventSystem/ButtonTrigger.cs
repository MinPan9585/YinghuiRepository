using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    private Button delegateButton;
    private Button actionButton;
    private Button addLivesButton;
    private Button minusLivesButton;
    private Button addMoneyButton;
    private Button minusMoneyButton;

    [Header("Object to spawn from pool")]
    public GameObject objectFromPool;

    [Header("Test")]
    [SerializeField] private GameObject go;
    private void Awake()
    {
        delegateButton = GameObject.Find("DelegateButton").GetComponent<Button>();
        actionButton = GameObject.Find("ActionButton").GetComponent<Button>();
        addLivesButton = GameObject.Find("AddLivesButton").GetComponent<Button>();
        minusLivesButton = GameObject.Find("MinusLivesButton").GetComponent<Button>();
        addMoneyButton = GameObject.Find("AddMoneyButton").GetComponent<Button>();
        minusMoneyButton = GameObject.Find("MinusMoneyButton").GetComponent<Button>();
    }

    private void Start()
    {
        delegateButton.onClick.AddListener(DelegateSpawn);
        actionButton.onClick.AddListener(ActionSpawn);

        addLivesButton.onClick.AddListener(() => ChangeStatus(0));
        minusLivesButton.onClick.AddListener(() => ChangeStatus(1));
        addMoneyButton.onClick.AddListener(() => ChangeStatus(2));
        minusMoneyButton.onClick.AddListener(() => ChangeStatus(3));
    }

    private void ChangeStatus(int id)
    {
        switch (id)
        {
            case 0:
                {
                    LevelStatus.Lives++;
                    Debug.Log("New lives is: " + LevelStatus.Lives);
                    return;
                }
            case 1:
                {
                    LevelStatus.Lives--;
                    Debug.Log("New lives is: " + LevelStatus.Lives);
                    return;
                }
            case 2:
                {
                    LevelStatus.Money += 100;
                    Debug.Log("New money is: " + LevelStatus.Money);
                    return;
                }
            case 3:
                {
                    LevelStatus.Money -= 100;
                    Debug.Log("New money is: " + LevelStatus.Money);
                    return;
                }
        }
    }

    private void DelegateSpawn()
    {
        Debug.Log("mouse on for new delegate pooling");
        //go = GameEvents.Instance.ButClicked(objectFromPool, transform);
    }

    private void ActionSpawn()
    {
        Debug.Log("mouse on for pooling");
        //GameEvents.Instance.MiddleMouseCliked(objectFromPool, transform);
    }

}
