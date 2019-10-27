using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    [SerializeField] Animator animator;
    [SerializeField] SceneController sc;

    public int GetLevelNumber()
    {
        return sc.GetCurrentLevelIndex();
    }

    public void FadeLevelIn()
    {
        StartCoroutine(SetTimeScaleToOneAfterNSeconds(2f));
        animator.ResetTrigger("FadeOut");
    }

    public void FadeLevelOut()
    {
        Time.timeScale = 0;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeOutComplete()
    {
        sc.StartNextScene();
    }

    public void StartGameOver()
    {
        Time.timeScale = 0;
        sc.LoadMainMenu();
    }

    private IEnumerator SetTimeScaleToOneAfterNSeconds(float n)
    {
        yield return new WaitForSeconds(n);
        Time.timeScale = 1;
    }

}
