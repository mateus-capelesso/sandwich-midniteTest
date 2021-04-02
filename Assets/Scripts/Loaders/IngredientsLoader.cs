using Ingredients;
using UnityEngine;

namespace Loaders
{
    public class IngredientsLoader : ObjectLoader
    {
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
        

        private void Start()
        {
            LoadObjects();
        }

        private void LoadObjects()
        {
            _empty = Resources.Load<GameObject>("Sandwich/Prefabs/Empty");
            _bread = Resources.Load<GameObject>("Sandwich/Prefabs/Bread");
            _bacon = Resources.Load<GameObject>("Sandwich/Prefabs/Bacon");
            _cheese = Resources.Load<GameObject>("Sandwich/Prefabs/Cheese");
            _egg = Resources.Load<GameObject>("Sandwich/Prefabs/Egg");
            _ham = Resources.Load<GameObject>("Sandwich/Prefabs/Ham");
            _onion = Resources.Load<GameObject>("Sandwich/Prefabs/Onion");
            _salad = Resources.Load<GameObject>("Sandwich/Prefabs/Salad");
            _salami = Resources.Load<GameObject>("Sandwich/Prefabs/Salami");
            _tomato = Resources.Load<GameObject>("Sandwich/Prefabs/Tomato");
        }

        public override GameObject GetObjectFromContent(int contentIndexType)
        {
            var content = (IngredientType) contentIndexType;
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
}