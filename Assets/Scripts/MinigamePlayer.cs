using UnityEngine;
using UnityEngine.InputSystem;

public class MinigamePlayer : MonoBehaviour
{
    
    [Header("Movement Stat")]
    [SerializeField] float speed;
    bool IsWalking;
    Vector2 playerVelocity;
    Vector2 moveInp;
    
    Rigidbody2D rb;
    public Animator anim;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        rb.linearVelocity = moveInp * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        anim.SetBool("IsWalking",context.performed);
        moveInp = context.ReadValue<Vector2>();
        Debug.Log($"MoveInp {moveInp}");
    }
}
