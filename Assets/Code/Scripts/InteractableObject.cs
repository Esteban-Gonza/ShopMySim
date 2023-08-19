using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum InteractiveObject{

    chest, signal, door, shop
}

public class InteractableObject : MonoBehaviour {


    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private GameObject interactionMark;

    [Space][Header("Chest")]
    [SerializeField] private Sprite openChestSprite;
    [SerializeField] private int coinsAmout;
    [SerializeField] private AudioClip coinSound;

    [Space]
    [Header("Shop")]
    [SerializeField] GameObject shopUI;

    [Space]
    [Header("Signal")]
    [SerializeField, TextArea(4, 6)] string[] dialogueLines;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] float typingTime;

    bool didDialogueStart;
    int lineIndex;

    [Header("References")]
    private SpriteRenderer objectRender;
    private BoxCollider2D boxCollider;
    private PlayerController player;
    bool isInRange;

    private void Start(){
        
        switch (interactiveObject){

            case InteractiveObject.chest:
                objectRender = GetComponent<SpriteRenderer>();
                boxCollider = GetComponent<BoxCollider2D>();
                break;
            case InteractiveObject.shop:
                shopUI.SetActive(false);
                break;
        }

        player = FindObjectOfType<PlayerController>();

        interactionMark.SetActive(false);
        isInRange = false;
    }

    private void Update(){

        if(isInRange == true && Input.GetKeyDown(KeyCode.E)){

            switch (interactiveObject){

                case InteractiveObject.chest:
                    OpenChest();
                    break;
                case InteractiveObject.shop:
                    if (shopUI.activeSelf){
                        shopUI.SetActive(false);
                        player.speed = 4;
                    }else{
                        shopUI.SetActive(true);
                        player.speed = 0;
                    }
                    break;
                case InteractiveObject.signal:
                    if (!didDialogueStart){
                        StartDialogue();
                    }else if(dialogueText.text == dialogueLines[lineIndex]){
                        NextDialogueLine();
                    }else{
                        StopAllCoroutines();
                        dialogueText.text = dialogueLines[lineIndex];
                    }
                    break;
            }

            interactionMark.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player")){
            interactionMark.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){

        if (collision.CompareTag("Player")){
            interactionMark.SetActive(false);
            isInRange = false;
        }
    }

    private void OpenChest(){

        boxCollider.enabled = false;
        objectRender.sprite = openChestSprite;
        AudioManager.instance.PlayCoin();

        GameDataManager.AddCoins(coinsAmout);

        //Cheat to get extra coins
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.C)){
            GameDataManager.AddCoins(20);
        }
        #endif

        UIManager.instance.UpdateCoinsText();
    }

    void StartDialogue(){
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        interactionMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    void NextDialogueLine(){
        lineIndex++;
        if (lineIndex < dialogueLines.Length) {

            StartCoroutine(ShowLine());
        } else {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            interactionMark.SetActive(true);
        }
    }

    IEnumerator ShowLine(){
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex]){
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
}