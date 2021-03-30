using System.Collections;
using DG.Tweening;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceControl : MonoBehaviour
{
    public GameObject undoButton;
    public GameObject nextLevelButton;
    public GameObject tryAgainButton;
    public TextMeshProUGUI textLevel;
    public CanvasGroup initialFade;

    private void Start()
    {
        LevelManager.Instance.OnNewLevelStart += SetTextLevel;
        GridManager.OnWin += ShowNextButton;
        GridManager.OnLose += ShowTryAgain;
    }

    private void SetTextLevel()
    {
        textLevel.gameObject.SetActive(true);
        textLevel.text = LevelManager.Instance.GetActualLevel().ToString();
        undoButton.SetActive(true);
        nextLevelButton.SetActive(false);
        tryAgainButton.SetActive(false);
        initialFade.DOFade(0f, 0.5f).OnComplete(() =>
        {
            initialFade.gameObject.SetActive(false);
        });
    }

    private void ShowNextButton()
    {
        StartCoroutine(WaitSandwichMovement());
    }

    private void ShowTryAgain()
    {
        undoButton.SetActive(false);
        var canvas = tryAgainButton.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        tryAgainButton.SetActive(true);
        canvas.DOFade(1, 0.5f);
    }

    IEnumerator WaitSandwichMovement()
    {
        yield return new WaitForSeconds(1f);
        undoButton.SetActive(false);
        var canvas = nextLevelButton.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        nextLevelButton.SetActive(true);
        canvas.DOFade(1, 0.5f);
    }
    
    
}