using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour, IService
{
    [Header("View")]
    [SerializeField] private Canvas viewRecipeCanvas;
    [SerializeField] private RecieptView recipeViewPrefab;

    [Header("Recipes")]
    [SerializeField] private RecipeSO[] recipes;

    [Header("Effect")]
    [SerializeField] private ParticleSystem particleFire;

    [Header("SpawnPoint")]
    [SerializeField] private Transform spawnPoint;

    private Inventory inventory;
    private List<RecieptView> receiptViews;
    public void Init()
    {
        inventory = SceneServices.ServiceLocator.Get<Inventory>();
        receiptViews = new List<RecieptView>();
        CreateRecipesView();
        spawnPoint.GetComponent<DetectedCharacter>().IsCharacterHere += ShowReciept;
    }

    private void CreateRecipesView()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            var recieptView = Instantiate(recipeViewPrefab, viewRecipeCanvas.transform, false);
            recieptView.GetComponentInChildren<Button>().onClick.AddListener(() => Cooking(recieptView.ItemType));
            recieptView.SetValue(recipes[i]);
            recieptView.gameObject.SetActive(false);
            receiptViews.Add(recieptView);
        }        
    }

    private void ShowReciept(bool value)
    {
        foreach (var item in receiptViews)
        {
            item.gameObject.SetActive(value);
        }       
    }

    private void Cooking(ItemsType type)
    {
        var currentRecipes = recipes.First(x => x.TypeDish == type);

        if (currentRecipes != null && currentRecipes.Ingredients.All(ingredient => ingredient.Amount <= inventory.GetValue(ingredient.TypeIngredient)))
        {
            foreach (var item in currentRecipes.Ingredients)
            {
                inventory.RemoveItem(item.TypeIngredient,item.Amount);
            }

            var createDish = SceneServices.ServiceLocator.Get<Pool>().GetItemPool(type).Get();
            createDish.transform.position = spawnPoint.position;
            createDish.MoveToInventory();
            inventory.AddItem(createDish);
        }
    }
}
