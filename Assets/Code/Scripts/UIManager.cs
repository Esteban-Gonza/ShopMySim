using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour{
    #region Singleton class: UIManager

    public static UIManager instance;

    private void Awake(){
        
        if(instance == null){
            instance = this;
        }
    }
    #endregion

    [SerializeField] TMP_Text[] coinsUIText;

    private void Start(){
        UpdateCoinsText();
    }

    public void UpdateCoinsText(){
        for(int i = 0; i < coinsUIText.Length; i++){
            SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
        }
    }

    private void SetCoinsText(TMP_Text textMesh, int value){
        if (value >= 1000)
            textMesh.text = string.Format("{0}k.{1}", (value / 1000), GetFirstDigitFromNumber(value % 1000));
        else
            textMesh.text = value.ToString();
    }

    private int GetFirstDigitFromNumber(int number){
        return int.Parse(number.ToString()[0].ToString());
    }
}
