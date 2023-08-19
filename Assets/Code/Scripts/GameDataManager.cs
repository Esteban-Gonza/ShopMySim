using System.Collections.Generic;

//Player data holder
[System.Serializable] public class ShopData{

    public List<int> purchasedItemsIndexes = new List<int>();
}

[System.Serializable]public class PlayerData{

    public int coins = 0;
    public int selectedItemsIndex = 0;
}

public class GameDataManager {

    static PlayerData playerData = new PlayerData();
    static ShopData itemsShopData = new ShopData();

    static Item selectedItem;

    static GameDataManager(){
        LoadPlayerData();
        LoadItemShopData();
    }

    //PlayerData Methods
    public static Item GetSelectedItem(){
        return selectedItem;
    }

    public static void SetSelectedItem(Item item, int index){
        selectedItem = item;
        playerData.selectedItemsIndex = index;
        SavePlayerData();
    }

    public static int GetSelectedItemIndex(){
        return playerData.selectedItemsIndex;
    }

    public static int GetCoins(){
        return playerData.coins;
    }

    public static void AddCoins(int amount){
        playerData.coins += amount;
        SavePlayerData();
    }

    public static bool CanSpendCoins(int amount){
        return(playerData.coins >= amount);
    }

    public static void SpendCoins(int amount){
        playerData.coins -= amount;
        SavePlayerData();
    }

    static void LoadPlayerData(){
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    }
    static void SavePlayerData(){
        BinarySerializer.Save(playerData, "player-data.txt");
        UnityEngine.Debug.Log("<color=green>[PlayerData] Saved.</color>");
    }

    //Character shop data methods
    public static void AddPurchasedItem(int itemIndex){
        itemsShopData.purchasedItemsIndexes.Add(itemIndex);
        SaveItemShopData();
    }
    public static List<int> GetAllPurchasedItems (){
        return itemsShopData.purchasedItemsIndexes;
    }

    public static int GetPurchasedItem(int index){
        return itemsShopData.purchasedItemsIndexes[index];
    }

    static void LoadItemShopData(){
        itemsShopData = BinarySerializer.Load<ShopData>("item-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[ShopData] Loaded.</color>");
    }
    static void SaveItemShopData(){
        BinarySerializer.Save(playerData, "item-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[ShopData] Saved.</color>");
    }
}