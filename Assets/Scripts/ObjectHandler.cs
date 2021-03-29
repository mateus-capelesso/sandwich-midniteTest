using System;
using Nodes;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public static ObjectHandler Instance;
    
    private GameObject _empty;
    private GameObject _bread;
    private GameObject _cheese;
    private GameObject _egg;
    private GameObject _ham;
    private GameObject _onion;
    private GameObject _salad;
    private GameObject _salami;
    private GameObject _tomato;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadObjects();
    }

    private void LoadObjects()
    {
        _empty = Resources.Load<GameObject>("ingredients/Empty");
        _bread = Resources.Load<GameObject>("ingredients/Bread");
        _cheese = Resources.Load<GameObject>("ingredients/Cheese");
        _egg = Resources.Load<GameObject>("ingredients/Egg");
        _ham = Resources.Load<GameObject>("ingredients/Ham");
        _onion = Resources.Load<GameObject>("ingredients/Onion");
        _salad = Resources.Load<GameObject>("ingredients/Salad");
        _salami = Resources.Load<GameObject>("ingredients/Salami");
        _tomato = Resources.Load<GameObject>("ingredients/Tomato");
    }

    public GameObject GetObjectFromContent(NodeContent content)
    {
        switch (content)
        {
            case NodeContent.Empty:
                return _empty;
            case NodeContent.Bread:
                return _bread;
            case NodeContent.Cheese:
                return _cheese;
            case NodeContent.Egg:
                return _egg;
            case NodeContent.Ham:
                return _ham;
            case NodeContent.Onion:
                return _onion;
            case NodeContent.Salad:
                return _salad;
            case NodeContent.Salami:
                return _salami;
            case NodeContent.Tomato:
                return _tomato;
            default:
                Debug.LogError($"Couldn't find ingredient {content}");
                return null;
        }

        
    }
}