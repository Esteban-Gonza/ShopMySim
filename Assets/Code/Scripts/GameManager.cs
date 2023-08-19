using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    [SerializeField] GameObject gameUI;
    private Animator animator;

    #region Singleton class: GameManager

    public static GameManager instance;

    private void Awake(){

        if (instance == null){
            instance = this;
        }
    }
    #endregion

    private void Start(){
        animator = GetComponent<Animator>();
        StartCoroutine(ShowUI());
    }

    public void GoToLevel(string level){
        StartCoroutine(TransitionScene(level));
    }

    public void GoToWebSite(string url){
        Application.OpenURL(url);
    }

    public void ExitGame(){
        Application.Quit();
    }

    IEnumerator TransitionScene(string level){
        gameUI.SetActive(false);
        animator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);
    }

    IEnumerator ShowUI(){
        gameUI.SetActive(false);
        yield return new WaitForSeconds(2);
        gameUI.SetActive(true);
    }
}