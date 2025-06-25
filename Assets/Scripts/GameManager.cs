using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        restartSreenObject.SetActive(true);
        slingShotHandler.enabled = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    #endregion
}
