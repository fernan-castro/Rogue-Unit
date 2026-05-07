using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 0;

    //attack stuff
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackDamage = 10;
    private float lastAttackTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // Rotate to face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.15f));
        }

        if (movement == Vector3.zero)
        {
            rb.linearVelocity *= 0.85f;
        }
    }

    //attacking
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Detect all colliders in a sphere in front of the player
        Collider[] hitEnemies = Physics.OverlapSphere(
            transform.position + transform.forward * 1.5f,
            attackRange
        );
        Debug.Log("Attack hit " + hitEnemies.Length + " colliders");
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " +enemy.name);
                EnemyHealth enemyHP = enemy.GetComponentInParent<EnemyHealth>();
                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(attackDamage);
                }
            }
        }
    }

    // Visualize the attack range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.5f, attackRange);
    }

    //atk end


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

}
