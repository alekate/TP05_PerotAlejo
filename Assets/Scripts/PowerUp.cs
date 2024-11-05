using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private float duration = 5f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float attackCooldownModifier = 0.2f;

    [SerializeField] private PlayerAttack playerAtack;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject spriteRenderer;
    private bool isPowerUpActive = false;

    [SerializeField] private SoundController soundController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPowerUpActive)
        {
            playerController = collision.GetComponent<PlayerController>();

            StartCoroutine(ActivatePowerUp());
            soundController.PickupSFX();
        }
    }
    private IEnumerator ActivatePowerUp()
    {
        isPowerUpActive = true;

        spriteRenderer.gameObject.SetActive(false);

        float originalSpeed = playerController.horizontallInput;
        float originalCD = playerAtack.attackCooldown;

        playerController.horizontallInput *= speedMultiplier;
        playerAtack.attackCooldown = attackCooldownModifier;

        yield return new WaitForSeconds(duration);

        playerController.horizontallInput = originalSpeed;
        playerAtack.attackCooldown = originalCD;

        Destroy(gameObject);
    }
}
