using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecieptView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDish;
    [SerializeField] private Image imageDish;
    [SerializeField] private GameObject[] ingredients;
    public ItemsType ItemType { get; private set; }

    public void SetValue(RecipeSO recipe)
    {
        nameDish.text = recipe.NameDish;
        imageDish.sprite = recipe.ImageDish;
        ItemType = recipe.TypeDish;
        SetIngredients(ingredients, recipe);
        
    }

    private void SetIngredients(GameObject[] ingredients, RecipeSO recipe)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].GetComponentInChildren<Image>().sprite = recipe.Ingredients[i].ImageIngredient; 
            ingredients[i].GetComponentInChildren<TextMeshProUGUI>().text = recipe.Ingredients[i].Amount.ToString();
        }
    }
}
