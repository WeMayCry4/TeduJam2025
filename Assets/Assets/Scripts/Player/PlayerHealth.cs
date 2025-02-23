using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    static public float health;
    private float lerpTimer;
    
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    public float damageTickRate = 1f;
    public float trapDamagePerTick = 2f;
    private float durationTimer;
    private bool isGameOver = false; // Prevent multiple triggers
    private bool isInTrap = false;

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        // GameOver Check
        if (health <= 0 && !isGameOver)
        {
            TriggerGameOver();
        }

        // Fade the damage overlay
        if (overlay.color.a > 0)
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.CeilToInt(health) + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("shieldTrap"))
        {
            isInTrap = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("shieldTrap"))
        {
            isInTrap = false;
        }
    }

        private IEnumerator TakeDamageOverTime()
    {
        while (isInTrap) // Keep damaging as long as we're in the trap
        {
            TakeDamage(trapDamagePerTick);
            yield return new WaitForSeconds(damageTickRate); // Wait for the specified tick rate
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
        
        // Load GameOver scene
        SceneManager.LoadScene("GameOver");

        // OR, activate GameOver screen
        // if (gameOverScreen != null)
        // {
        //     gameOverScreen.Setup();
        // }
    }
}
