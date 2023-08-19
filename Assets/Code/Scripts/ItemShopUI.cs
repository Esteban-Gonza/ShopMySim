using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemShopUI : MonoBehaviour{

    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = 5.0f;
    private float itemHeigh;

    [Header("UI elements")]
    [SerializeField] GameObject shopPanel;
    [SerializeField] Transform shopMenu;
    [SerializeField] Transform shopItemContainer;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] TMP_Text sellerText;

    [Space]
    [SerializeField] ItemShopDatabase itemDB;

    [Space]
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> clothes = new List<GameObject>();

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;

    private void Start(){

        GenerateShopItemsUI();

        SetSelectedCharacter();

        SelectItemUI(GameDataManager.GetSelectedItemIndex());
    }

    void SetSelectedCharacter(){
        int index = GameDataManager.GetSelectedItemIndex();

        GameDataManager.SetSelectedItem(itemDB.GetItem(index), index);
    }

    void GenerateShopItemsUI(){

        for (int i = 0; i < GameDataManager.GetAllPurchasedItems().Count; i++){

            int purchasedCharacterIndex = GameDataManager.GetPurchasedItem(i);
            itemDB.PurchaseItem(purchasedCharacterIndex);
        }

        itemHeigh = shopItemContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(shopItemContainer.GetChild(0).gameObject);
        shopItemContainer.DetachChildren();

        for (int i = 0; i < itemDB.ItemsCount; i++){

            Item item = itemDB.GetItem(i);
            ItemUI uiItem = Instantiate(itemPrefab, shopItemContainer).GetComponent<ItemUI>();

            uiItem.SetItemPosition(Vector2.down * i * (itemHeigh + itemSpacing));

            uiItem.gameObject.name = "Item" + i + "-" + item.name;

            uiItem.SetItemName(item.name);
            uiItem.SetItemImage(item.image);
            uiItem.SetItemPrice(item.price);

            if (item.isPurchased){
                uiItem.SetItemAsPurchased();
                uiItem.OnItemSelected(i, OnItemSelected);
            }else{
                uiItem.SetItemPrice(item.price);
                uiItem.OnItemPurchased(i, OnItemPurchased);
            }

            shopItemContainer.GetComponent<RectTransform>().sizeDelta = Vector2.up * (itemHeigh + itemSpacing) * itemDB.ItemsCount;
        }
    }

    void OnItemSelected(int index){
        SelectItemUI(index);

        //Set player clothes
        if (player.transform.childCount < 0)
        {
            GameObject newClothes = Instantiate(clothes[index], clothes[index].transform.parent = player.transform);
            newClothes.transform.parent = player.transform;
            newClothes.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            for (var i = player.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(player.transform.GetChild(i).gameObject);
            }
            GameObject newClothes = Instantiate(clothes[index]);
            newClothes.transform.parent = player.transform;
            newClothes.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            GameDataManager.SetSelectedItem(itemDB.GetItem(index), index);
        }
    }

    void SelectItemUI(int itemIndex){

        previousSelectedItemIndex = newSelectedItemIndex;
        newSelectedItemIndex = itemIndex;

        ItemUI previousUiItem = GetItemUI(previousSelectedItemIndex);
        ItemUI newUiItem = GetItemUI(newSelectedItemIndex);

        previousUiItem.DeselectItem();
        newUiItem.SelectItem();
    }

    ItemUI GetItemUI(int index){
        return shopItemContainer.GetChild(index).GetComponent<ItemUI>();
    }

    void OnItemPurchased(int index){
        Item item = itemDB.GetItem(index);
        ItemUI uiItem = GetItemUI(index);

        if (GameDataManager.CanSpendCoins(item.price)){

            GameDataManager.SpendCoins(item.price);

            UIManager.instance.UpdateCoinsText();

            itemDB.PurchaseItem(index);


            uiItem.SetItemAsPurchased();
            uiItem.OnItemSelected(index, OnItemSelected);

            GameDataManager.AddPurchasedItem(index);

            AudioManager.instance.CashMachine();

            StartCoroutine(PurchaseMessage());
        }else{

            StartCoroutine(NoCoinsMessage());
        }
    }

    IEnumerator NoCoinsMessage(){

        sellerText.text = "Not enough coins!!!";
        yield return new WaitForSeconds(5f);
        sellerText.text = "Purchase an item and select it to use it!!!";
    }

    IEnumerator PurchaseMessage(){

        sellerText.text = "Thanks for buying!! Now you can select your new item!!";
        yield return new WaitForSeconds(2f);
        sellerText.text = "Purchase an item and select it to use it!!!";
    }
}