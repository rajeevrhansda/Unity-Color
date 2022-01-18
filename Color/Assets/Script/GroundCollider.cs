using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundCollider : MonoBehaviour
{
    private void Start()
    {
        Game.isGameover = false;
        Game.won = false;
        Game.lost = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag.Equals("Object"))
        {
            Level.Instance.objectsInScene--;
            UIManager.Instance.UpdateLevelProgress();
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
            Destroy(other.gameObject);

            int points = PlayerPrefs.GetInt("Score");
            int addPoints = points + 1;

            if (addPoints > PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", addPoints);
            }

            if (Level.Instance.objectsInScene == 0)
            {
                UIManager.Instance.ActivateWinPanel();
                
                Game.isGameover = true;
                Game.won = true;
                


            }
        }


        if (tag.Equals("Obstacle"))
        {
            
            Game.isGameover = true;
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
            Destroy(other.gameObject);
            Camera.main.transform.DOShakePosition(1f, .2f, 20, 90f).OnComplete(() =>
            {
                UIManager.Instance.ActivateGameOverPanel();
                //Invoke("GameOver", 0.5f);
            });
        }
    }

    void GameOver()
    {
        UIManager.Instance.ActivateGameOverPanel();
    }
}
