using UnityEngine;
using UnityEngine.InputSystem;

public class PatinteroManager : MonoBehaviour
{
    public static PatinteroManager Instance {get;private set;}
    public GameObject Patintero;
    public GameObject player2d;
    public GameObject losePanel;
    public GameObject winPanel;

    Vector3 startPos;

    public int GoalState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Enforce the Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;

        startPos = player2d.transform.position;
    }

    void Update()
    {
        if(GoalState == 1)
        {
            winPanel.SetActive(true);
            player2d.GetComponent<PlayerInput>().enabled = false;
        }
        else if(GoalState == 2)
        {
            losePanel.SetActive(true);
            player2d.GetComponent<PlayerInput>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player2D")
        {
            Debug.Log("Win");
            GoalState = 1;
        }
    }

    public void closePatintero()
    {
        ResetPatintero();
        MinigameManager.Instance.closePatintero();
    }

    public void ResetPatintero()
    {
        player2d.transform.position = startPos;
        player2d.GetComponent<PlayerInput>().enabled = true;
        GoalState = 0;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        
    }
}
