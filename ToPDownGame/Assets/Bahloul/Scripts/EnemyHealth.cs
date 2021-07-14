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
	private void OnEnable()
	{
		enemyBehavior.currentHealth += getHealth;
		enemyBehavior.takeDamage += getDamage;
	}
	private void OnDisable()
	{
		enemyBehavior.currentHealth -= getHealth;
		enemyBehavior.takeDamage -= getDamage;
	}
	public float getHealth() { return currentHealth+currentShield; }
	void Start()
	{
		maxHealth= enemyBehavior.Item.health;
		HealthSlider.fillAmount = 1;
		maxShield= enemyBehavior.Item.shield;
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
				}
				else
				{
					enemyBehavior.enemyState(EnemyStates.State.Death);
					HealthSlider.fillAmount = 0;
					currentHealth = 0;
					HealthSlider.color = gradient.Evaluate(0);
				}

			}

		}
	}
}
