using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UndergroundArea : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float NextLevelDelay;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private Level LevelManager;
    [SerializeField] private Magnet Magnet;
    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        if (!Game.IsGameOver)
        {
            if (tag.Equals("Object"))
            {
                LevelManager.ObjectsInScene--;
                UIManager.UpdateLevelProgress();
            }
            
            if (LevelManager.ObjectsInScene == 0)
            {
                Game.IsGameOver = true;

                LevelManager.PlayWinFX();
                UIManager.ShowLevelCompletedUI();

                Invoke("NextLevel", 2f);
            }
            
            if (tag.Equals("Obstacle"))
            {
                Game.IsGameOver = true;

                Camera.main.transform
                    .DOShakePosition(1f, .2f, 20, 90f)
                    .OnComplete(() => {
                        LevelManager.RestartLevel();
                    });
            }
        }
        Magnet.RemoveFromMagnetField(other.attachedRigidbody);
        Destroy(other.gameObject);
        
    }

    private void NextLevel()
    {
        LevelManager.LoadNextLevel();
    }
}
