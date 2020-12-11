using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [Header("General options")]
    [SerializeField] private ParticleSystem WinFX;
    [SerializeField] private Transform ObjectsParent;   
    public int ObjectsInScene;
    public int TotalObjects;

    [Space]
    [Header("Level Objects & Obstacles")]
    [SerializeField] private Material GroundMaterial;
    [SerializeField] private Material ObjectMaterial;
    [SerializeField] private Material ObstacleMaterial;
    [SerializeField] private SpriteRenderer GroundBorderSprite;
    [SerializeField] private SpriteRenderer GroundSideSprite;
    [SerializeField] private Image ProgressFillImage;
    [SerializeField] private SpriteRenderer BgFadeSprite;
    [SerializeField] private SpriteRenderer ShadowFade;

    [Space]
    [Header("Level Color")]
    [Space]
    [Header("Ground")]
    [SerializeField] private Color GroundColor;
    [SerializeField] private Color BorderColor;
    [SerializeField] private Color SideColor;
    [SerializeField] private Color ShadowColor;

    [Header("Objects & Obstacles")]
    [SerializeField] private Color ObjectColor;
    [SerializeField] private Color ObstacleColor;

    [Header("UI ProgressBar")]
    [SerializeField] private Color ProgressFillColor;

    [Header("Objects & Obstacles")]
    [SerializeField] private Color CameraColor;
    [SerializeField] private Color FadeColor;

    private void Start()
    {
        CountObjects();
        UpdateLevelColors();
    }

    private void CountObjects()
    {
        TotalObjects = ObjectsParent.childCount;
        ObjectsInScene = TotalObjects;
    }

    public void PlayWinFX()
    {
        WinFX.Play();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnValidate()
    {
        UpdateLevelColors();
    }

    private void UpdateLevelColors()
    {
        GroundMaterial.color = GroundColor;
        GroundSideSprite.color = SideColor;
        GroundBorderSprite.color = BorderColor;
        ShadowFade.color = ShadowColor;

        ObstacleMaterial.color = ObstacleColor;
        ObjectMaterial.color = ObjectColor;

        ProgressFillImage.color = ProgressFillColor;

        Camera.main.backgroundColor = CameraColor;
        BgFadeSprite.color = FadeColor;
    }
}
