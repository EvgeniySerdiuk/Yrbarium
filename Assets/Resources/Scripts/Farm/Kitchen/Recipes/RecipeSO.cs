using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Create Recipe")]
public class RecipeSO : ScriptableObject
{
    [Header("Dish Info")]
    [SerializeField] private string nameDish;
    [SerializeField] private InteractableItem prefabDish;
    [SerializeField] private List<IngredientData> ingredients;

    public InteractableItem PrefabDish => prefabDish;
    public ItemsType TypeDish => prefabDish.Type;
    public string NameDish => nameDish;
    public Sprite ImageDish => prefabDish.ItemSprite;
    public List<IngredientData> Ingredients 
    {
        get 
        {
            List<IngredientData> newList = new List<IngredientData>(ingredients);
            return newList;
        }
    }

    [System.Serializable]
    public struct IngredientData
    {
        [SerializeField] private ItemsType typeIngredient;
        [SerializeField] private Sprite imageIngredient;
        [SerializeField] private int amount;

        public Sprite ImageIngredient => imageIngredient;
        public int Amount => amount;
        public ItemsType TypeIngredient => typeIngredient;
    }
}
