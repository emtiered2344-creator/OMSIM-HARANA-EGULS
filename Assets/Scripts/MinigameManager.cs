using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance {get;private set;}

    public bool minigameOpened = false;

    public GameObject MinigamesPanel;
    public GameObject PatinteroPanel;
    public GameObject PatinteroMinigame;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;
    }


    public void openPatintero()
    {
        MinigamesPanel.SetActive(false);
        PatinteroPanel.SetActive(true);
        PatinteroMinigame.SetActive(true);
    }
    public void closePatintero()
    {
        PatinteroPanel.SetActive(false);
        PatinteroMinigame.SetActive(false);
        GameManager.Instance.closeMenu();
    }

}
