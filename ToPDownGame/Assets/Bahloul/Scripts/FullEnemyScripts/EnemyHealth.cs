using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public Image HealthSlider;
	public Image ShieldSlider;
	public Gradient gradient;
	private float currentHealth;
	private float currentShield;
	private float maxHealth;
	private float maxShield;

	public EnemyBehavior enemyBehavior;
	public SniperBehavior sniperBehavior;
	private void OnEnable()
	{
		if (enemyBehavior != null)
		{
			enemyBehavior.currentHealth += getHealth;
			enemyBehavior.takeDamage += getDamage;
		}
		if (sniperBehavior != null)
		{
			sniperBehavior.currentHealth += getHealth;
			sniperBehavior.takeDamage += getDamage;
		}
	}
	private void OnDisable()
	{
		if (enemyBehavior != null)
		{
			enemyBehavior.currentHealth -= getHealth;
			enemyBehavior.takeDamage -= getDamage;
		}
        if (sniperBehavior != null)
        {
			sniperBehavior.currentHealth -= getHealth;
			sniperBehavior.takeDamage -= getDamage;
		}
	}
	public float getHealth() { return currentHealth+currentShield; }
	void Start()
	{
		if (enemyBehavior != null)
		{
			maxHealth = enemyBehavior.Item.health;
			maxShield = enemyBehavior.Item.shield;
		}
		if(sniperBehavior!=null)
        {
			maxHealth = sniperBehavior.Item.health;
			maxShield = sniperBehavior.Item.shield;
		}
		HealthSlider.fillAmount = 1;
		ShieldSlider.fillAmount = 1;
		currentHealth = maxHealth;
		currentShield = maxShield;
	}
	
	public void getDamage(float health)
	{
		if (currentHealth > 0)
		{
			if (currentShield >= health)
			{
				currentShield -= health;
				ShieldSlider.fillAmount -= health/maxShield;
				health = 0;
			}
			else {
				health = health - currentShield;
				currentShield = 0;
				ShieldSlider.fillAmount = 0;
			}
			if (health > 0)
			{
				if (currentHealth > health)
				{
					
					currentHealth -= health;
					HealthSlider.fillAmount -= health/maxHealth;
					HealthSlider.color = gradient.Evaluate(currentHealth / maxHealth);
					if(enemyBehavior!=null)
						enemyBehavior.toHide();
				}
				else
				{
					if (enemyBehavior != null)
					{
						enemyBehavior.enemyState(EnemyStates.State.Death);

					}
					if (sniperBehavior != null)
						sniperBehavior.changeState(SniperStates.State.Death);
					HealthSlider.fillAmount = 0;
					currentHealth = 0;
					HealthSlider.color = gradient.Evaluate(0);
					return;
				}
			
			}
			if (enemyBehavior != null)
				enemyBehavior.toHide();
		}
	}
}
