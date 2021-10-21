using UnityEngine;

public class NewGameCreator : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();

    public void CreateNewGame()
    {
        playerData.money = 0;
        playerData.currentLevel = 2;
        playerData.isInShop = false;
        playerData.shipModel = 1;
        playerData.simpleWeaponTier = 1;
        playerData.tripleWeaponTier = 0;
        playerData.lazerTier = 0;
        playerData.homingWeaponTier = 0;

        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(playerData));
    }
}

public class PlayerData
{
    public int currentLevel;
    public int money;
    public bool isInShop;

    public int shipModel;
    public int simpleWeaponTier;
    public int tripleWeaponTier;
    public int lazerTier;
    public int homingWeaponTier;
}
