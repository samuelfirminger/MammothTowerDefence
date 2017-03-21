using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	public static void dealDamageToEnemy(Enemy target, float damageValue) {
        float currentHealth = target.getHealth();
        float newHealth     = currentHealth - damageValue;
        target.setHealth(newHealth);
        Debug.Log(damageValue);
    }
}
