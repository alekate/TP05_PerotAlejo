using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private InitialEnemyData enemyData;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private SoundController soundController;
    [SerializeField] private BoxCollider2D bCollider;

    private bool movingLeft;
    private float rightEdge;
    private float leftEdge;
    private float enemyCurrentHealth;

    public bool isFrog;

    [Header("Move Speed & Distance")]
    public float enemyMovementSpeed = 2f;
    public float movementDistance;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        enemyCurrentHealth = enemyData.enemyHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = enemyData.enemyHealth;
            healthSlider.value = enemyCurrentHealth;
        }
    }

    private void Update()
    {
        if (isFrog)
        {
            MoveEnemy();
            UpdateHealthSliderPosition();
        }
    }

    private void MoveEnemy()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - enemyMovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = Vector3.one;
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + enemyMovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void EnemyTakeDamage()
    {
        if (enemyCurrentHealth > 0)
        {
            enemyCurrentHealth = Mathf.Max(0, enemyCurrentHealth - 1);

            UpdateHealthSlider();

            StartCoroutine(FlashRed());

            if (enemyCurrentHealth == 0)
            {
                Die();
            }
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = enemyCurrentHealth;
        }
    }

    private void UpdateHealthSliderPosition()
    {
        if (healthSlider != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);
            healthSlider.transform.position = screenPosition;
        }
    }

    private void Die()
    {
        healthSlider.gameObject.SetActive(false);
        bCollider.enabled = false;
        soundController.EnemyDeathSFX();

         StartCoroutine(deathAfterAnim());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(enemyData.enemyDamage);
        }

        if (collision.CompareTag("Projectile") && isFrog)
        {
            EnemyTakeDamage();
        }

        if (collision.CompareTag("PlayerFeet") && isFrog)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator deathAfterAnim()
    {
        enemyAnim.SetTrigger("dead");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
