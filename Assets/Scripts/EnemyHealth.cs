using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 30;
    private int currHP;

    void Start()
    {
        currHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currHP -= damage;
        Debug.Log(gameObject.name + " HP " + currHP);

        if (currHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}
