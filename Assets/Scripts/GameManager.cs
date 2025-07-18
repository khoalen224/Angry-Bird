using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int maxNumberOfBirds = 3;
    private int usedBirds = 0;
    private IconHandler iconHandler;

    [SerializeField] private float timeBeforeLost = 5f;

    private List<Baddie> baddiesList = new List<Baddie>();
    [SerializeField] private GameObject restartSreenObject;
    [SerializeField] private SlingShotHandler slingShotHandler;
    [SerializeField] private Image nextLevelImg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        iconHandler = FindAnyObjectByType<IconHandler>();

        Baddie[] baddies = FindObjectsByType<Baddie>(FindObjectsSortMode.None);
        foreach (Baddie baddie in baddies)
        {
            baddiesList.Add(baddie);
        }
        if (nextLevelImg == null)
            nextLevelImg = FindAnyObjectByType<Image>();

        if (nextLevelImg != null)
            nextLevelImg.enabled = false;   
    }


    public void UseBird()
    {
        usedBirds++;
        iconHandler.UseBird(usedBirds);

        CheckLastBird();
    }

    public bool HasEnoughBirds()
    {
        return usedBirds < maxNumberOfBirds;
    }

    public void CheckLastBird()
    {
        if (usedBirds == maxNumberOfBirds)
        {
            StartCoroutine(TimeBeforeLost());

        }
    }

    private IEnumerator TimeBeforeLost()
    {
        yield return new WaitForSeconds(timeBeforeLost);
        if (baddiesList.Count == 0)
        {
            WinGame();
        }
        else
        {
            RestartGame();
        }
    }

    public void RemoveBadddie(Baddie baddie)
    {
        baddiesList.Remove(baddie);
        CheckForAllBaddiesDestroyed();
    }

    private void CheckForAllBaddiesDestroyed()
    {
        if (baddiesList.Count == 0)
        {
            WinGame();
        }
    }
    #region win/lose conditions
    private void WinGame()
    {
        if (restartSreenObject != null)
        {
            restartSreenObject.SetActive(true);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Restart screen object is missing or destroyed.");
        }

        if (slingShotHandler != null)
        {
            slingShotHandler.enabled = false;
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxLevels = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex + 1 < maxLevels)
        {
            if (nextLevelImg != null)
            {
                nextLevelImg.enabled = true;
            }
            // Remove automatic call to NextLevel()
            // NextLevel() should only be called by a button click
        }
    }
    public void RestartGame()
    {
        if (restartSreenObject != null)
            restartSreenObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
