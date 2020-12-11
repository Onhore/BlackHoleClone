using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Level Progress UI")]
    [SerializeField] private int SceneOffset;
    [SerializeField] private TMP_Text NextLevelText;
    [SerializeField] private TMP_Text CurrentLevelText;
    [SerializeField] private Image ProgressFillImage;
    [SerializeField] private TMP_Text WinText;
    [SerializeField] private Image FadePanel;
    [SerializeField] private Level LevelManager;

    private void Start()
    {
        FadeAtStart();

        ProgressFillImage.fillAmount = 0f;

        SetLevelProgressText();
    }

    private void SetLevelProgressText()
    {
        var level = SceneManager.GetActiveScene().buildIndex + SceneOffset;
        CurrentLevelText.text = (level).ToString();
        NextLevelText.text = (level + 1).ToString();
    }

    public void UpdateLevelProgress()
    {
        var value = 1f - ((float)LevelManager.ObjectsInScene / LevelManager.TotalObjects);
        ProgressFillImage.DOFillAmount(value, 0.4f);
    }

    public void ShowLevelCompletedUI() => WinText.DOFade(1f, .6f).From(0f);
    public void FadeAtStart() => FadePanel.DOFade(0f, 1.3f).From(1f);
}
