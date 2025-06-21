using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int maxNumberOfBirds = 3;
    private int usedBirds = 0;
    private IconHandler iconHandler;


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
    }
    public void UseBird()
    {
        usedBirds++;
        iconHandler.UseBird(usedBirds);
    }

    public bool HasEnoughBirds()
    {
        return usedBirds < maxNumberOfBirds;
    }
}
