using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private SoundController soundController;
    [SerializeField] private GameObject music;
    [SerializeField] private UIController UI;
    [SerializeField] private Animator playerDamageAnim;
    [SerializeField] private Animator playerhandDamageAnim;
    [SerializeField] private Image currentHealthBar;

    public float currentHealth;
    private bool isDead = false;

    public GameObject gameUI;
    public GameObject deathUI;

    private void Awake()
    {
        currentHealth = playerData.playerHealth;
        UI.UpdateHealthBar();
        playerDamageAnim = transform.Find("Player Sprite").GetComponent<Animator>();
        playerhandDamageAnim = transform.Find("Hand Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        UI.UpdateHealthBar();

        //if (Input.GetKeyDown(KeyCode.E))
          //  TakeDamage(1);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Dead();
            currentHealth = 0;
        }

        if (isDead && Input.GetKey(playerData.shootKey))
        {
            RestartScene();
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            soundController.DamageSFX();
            currentHealth -= damage;
            playerDamageAnim.SetTrigger("hurt");
            playerhandDamageAnim.SetTrigger("hurt");

            UI.UpdateHealthBar();
        }
    }

    private void Dead()
    {
        deathUI.SetActive(true);
        gameUI.SetActive(false);
        soundController.DieSFX();
        music.gameObject.SetActive(false);
        playerDamageAnim.SetTrigger("dead");
        playerhandDamageAnim.SetTrigger("dead");

        GetComponent<PlayerController>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; 
        rb.constraints = RigidbodyConstraints2D.FreezeAll;



    }

    private void RestartScene()
    {
        deathUI.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
