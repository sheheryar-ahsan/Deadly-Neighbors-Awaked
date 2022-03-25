using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEffectsManager : MonoBehaviour
{
    private ZombieManager zombie;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void DamageZombieHead(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Head Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealHeadShotDamage(damage);
    }

    public void DamageZombieTorso(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Torso Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealTorsoDamage(damage);
    }

    public void DamageZombieRightArm(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Torso Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealArmDamage(false, damage);
    }

    public void DamageZombieLeftArm(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Torso Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealArmDamage(true, damage);
    }

    public void DamageZombieLeftLeg(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Torso Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealLegDamage(true, damage);
    }
    public void DamageZombieRightLeg(int damage)
    {
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Torso Hit Reaction", 0.1f);
        zombie.zombieStatManager.DealLegDamage(false, damage);
    }
}
