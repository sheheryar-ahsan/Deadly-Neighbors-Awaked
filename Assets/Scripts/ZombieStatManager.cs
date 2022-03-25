using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour
{
    ZombieManager zombie;

    [Header("Damage Modifiers")]
    public float headshotDamageMultiplier = 1.5f;

    [Header("Overall Health")]
    public int overallHealth = 100;

    [Header("Head Health")]
    public int headHealth = 100;

    [Header("Torso Health")]
    public int torsoHealth = 100;

    [Header("Upperbody Health")]
    public int leftArmHealth = 100;
    public int rightArmHealth = 100;

    [Header("Lowerbody Health")]
    public int leftLegHealth = 100;
    public int rightLegHealth = 100;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void DealHeadShotDamage(int damage)
    {
        headHealth = headHealth - Mathf.RoundToInt(damage * headshotDamageMultiplier);
        overallHealth = overallHealth - Mathf.RoundToInt(damage * headshotDamageMultiplier);
        CheckForDeath();
    }

    public void DealTorsoDamage(int damage)
    {
        torsoHealth = torsoHealth - damage;
        overallHealth = overallHealth - damage;
        CheckForDeath();
    }

    public void DealArmDamage(bool leftArmDamage, int damage)
    {
        if (leftArmDamage)
        {
            leftArmHealth = leftArmHealth - damage;
        }
        else
        {
            rightArmHealth = rightArmHealth - damage;
        }
        CheckForDeath();
    }

    public void DealLegDamage(bool leftLegDamage, int damage)
    {
        if (leftLegDamage)
        {
            leftLegHealth = leftLegHealth - damage;
        }
        else
        {
            rightLegHealth = rightLegHealth - damage;
        }
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (overallHealth < 0)
        {
            overallHealth = 0;
            zombie.isDead = true;
            zombie.ZombieAnimatorManager.PlayTargetActionAnimation("Zombie Death");
        }
    }
}
