using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}
    public GameObject player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;
    }

    public void closeMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MinigameManager.Instance.MinigamesPanel.SetActive(false);
        player.GetComponent<PlayerInput>().enabled = true;
        MinigameManager.Instance.minigameOpened = false;
    }
}
