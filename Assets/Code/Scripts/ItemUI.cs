using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ItemUI : MonoBehaviour {

    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemPrice;
    [SerializeField] Button itemPurchaseButton;

    [Space]
    [SerializeField] Button itemButton;

    public void SetItemPosition(Vector2 position) {
        GetComponent<RectTransform>().anchoredPosition += position;
    }

    public void SetItemImage(Sprite sprite) {
        itemImage.sprite = sprite;
    }

    public void SetItemName(string name) {
        itemName.text = name;
    }

    public void SetItemPrice(int price) {
        itemPrice.text = price.ToString();
    }

    public void SetItemAsPurchased(){
        itemPurchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;
    }

    public void OnItemPurchased(int itemIndex, UnityAction<int> action){
        itemPurchaseButton.onClick.RemoveAllListeners();
        itemPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void OnItemSelected(int itemIndex, UnityAction<int> action){
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void SelectItem(){
        itemButton.interactable = false;
    }

    public void DeselectItem(){
        itemButton.interactable = true;
    }
}