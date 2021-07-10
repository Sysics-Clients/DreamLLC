using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;


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
	public float getHealth() { return slider.value; }
	void Start()
	{
		slider.maxValue = enemyBehavior.Item.health;
		slider.value = enemyBehavior.Item.health;
		fill.color = gradient.Evaluate(1f);
	}

	public void getDamage(float health)
	{
		if (slider.value > health)
		{
			slider.value -= health;
			fill.color = gradient.Evaluate(slider.normalizedValue);
		}
		else {
			enemyBehavior.enemyState(EnemyStates.State.Death);
			slider.value = 0;
			fill.color = gradient.Evaluate(0);
		}
	}
}
