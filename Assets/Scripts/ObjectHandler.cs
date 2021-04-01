using Ingredients;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public static ObjectHandler Instance;
    
    private GameObject _empty;
    private GameObject _bread;
    private GameObject _bacon;
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
        _bacon = Resources.Load<GameObject>("ingredients/Bacon");
        _cheese = Resources.Load<GameObject>("ingredients/Cheese");
        _egg = Resources.Load<GameObject>("ingredients/Egg");
        _ham = Resources.Load<GameObject>("ingredients/Ham");
        _onion = Resources.Load<GameObject>("ingredients/Onion");
        _salad = Resources.Load<GameObject>("ingredients/Salad");
        _salami = Resources.Load<GameObject>("ingredients/Salami");
        _tomato = Resources.Load<GameObject>("ingredients/Tomato");
    }

    public GameObject GetObjectFromContent(IngredientType content)
    {
        switch (content)
        {
            case IngredientType.Empty:
                return _empty;
            case IngredientType.Bread:
                return _bread;
            case IngredientType.Bacon:
                return _bacon;
            case IngredientType.Cheese:
                return _cheese;
            case IngredientType.Egg:
                return _egg;
            case IngredientType.Ham:
                return _ham;
            case IngredientType.Onion:
                return _onion;
            case IngredientType.Salad:
                return _salad;
            case IngredientType.Salami:
                return _salami;
            case IngredientType.Tomato:
                return _tomato;
            default:
                Debug.LogError($"Couldn't find ingredient {content}");
                return null;
        }

        
    }
}