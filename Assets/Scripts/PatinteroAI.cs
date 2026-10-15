using UnityEngine;

public class PatinteroAI : MonoBehaviour
{
    [Header("Movement Stat")]
    [SerializeField] float speed = 2f;
    [SerializeField] float patrolDistance = .7f;


    Vector3 startPosition;
    int direction = 1;


    public bool isFollow;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFollow)MoveSide();
        else{
            MoveFollow();
        }
    }

    void MoveFollow()
    {
        Vector3 position = transform.position;
        float playerX = PatinteroManager.Instance.player2d.transform.position.x;
        position.x = Mathf.Lerp(position.x, playerX, speed * Time.deltaTime);
        transform.position = position;
    }

    void MoveSide()
    {
        Vector3 position = transform.position;
        position.x += direction * speed * Time.deltaTime;

        float leftLimit = startPosition.x - patrolDistance;
        float rightLimit = startPosition.x + patrolDistance;

        if (position.x >= rightLimit)
        {
            position.x = rightLimit;
            direction = -1;
        }
        else if (position.x <= leftLimit)
        {
            position.x = leftLimit;
            direction = 1;
        }

        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player2D")
        {
            Debug.Log("Player Touched Lose");
        }
        PatinteroManager.Instance.GoalState = 2;
    }
}
