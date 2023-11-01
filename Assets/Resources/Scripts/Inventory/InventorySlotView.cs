using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private InteractableItem itemView;
    public ItemsType ViewType => itemView.Type;

    private TextMeshProUGUI amountItems;
    private Image imageItem;

    public void Init()
    {
        imageItem = GetComponentInChildren<Image>();
        amountItems = GetComponentInChildren<TextMeshProUGUI>();
        imageItem.sprite = itemView.ItemSprite;
    }

    public void SetAmount(int amount)
    {
        amountItems.text = $"{amount}";
    }
}
