using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MerchantItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TMPro.TMP_Text nameText = null;
    [SerializeField] protected TMPro.TMP_Text costText = null;
    [SerializeField] protected TMPro.TMP_Text quantityText = null;
    [SerializeField] protected Image image = null;
    [Header("Runtime")]
    [SerializeField] protected InventoryStorage item = null;
    public InventoryStorageEvent MerchantOnSellItemEvent = new InventoryStorageEvent();

    private bool pointerDown;
    private float pointerDownTimer = 0.0f;

    private float requiredHoldTime = 0.2f;

    public InventoryStorage Item { get { return item; } }

    private MerchantUI merchant;

    public virtual void UpdateUI(InventoryStorage item, MerchantUI merchant)
    {
        this.merchant = merchant;
        this.item = item;
        nameText.text = "Name : " + this.item.item.Name;
        costText.text = "Cost : " + this.item.item.Price;
        image.sprite = this.item.item.Icon;
    }

    public void OnClick()
    {
        MerchantOnSellItemEvent.Invoke(this.item);
    }

	private void Update()
	{
        Debug.Log(pointerDown);
		if (pointerDown)
		{
            Debug.Log("test");
			pointerDownTimer += Time.unscaledDeltaTime;
			if (pointerDownTimer >= requiredHoldTime)
			{
                Debug.Log("coucou");
                Action();
			}
		}
	}

	private void Action()
	{
		pointerDownTimer = 0;
        //OnClick();
        if(merchant != null)
            merchant.SellItem(item);
	}

    public void TriggerButton()
    {
        pointerDownTimer = 0;
        Debug.Log("on");
        pointerDown = true;
    }

    public void ReleaseButton()
    {
        Debug.Log("off");
        pointerDown = false;
    }
}
