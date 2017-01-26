using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    //sets the amount of health a character has
    public RectTransform healthBar;
    public RectTransform backgroundHealthBar;
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    //if there is damage received this will change the current health, and the health bar
    public void takeDamage(int amount)
    {
        Debug.Log("In health, take damage");
        currentHealth -= amount;
        if (amount > 5)
        {
            //if damage is received change health bar
            onChangeHealth(.1f);
        }
        

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead");
        }
    }

    void onChangeHealth(float amount)
    {
        //change the size of the health bar 
        healthBar.sizeDelta -= new Vector2(amount, 0); 
        
    }
    public void resetHealth()
    {
        currentHealth = maxHealth;
        healthBar.sizeDelta = new Vector2(backgroundHealthBar.sizeDelta.x, backgroundHealthBar.sizeDelta.y);
    }
}