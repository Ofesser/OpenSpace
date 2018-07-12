using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonsterCreature {

    protected override void InitCreatureParameters ()
    {
        hp = 300;
        maxSpeed = 4.5f;
        currSpeed = 0f;
        armor = 1f;
        meleeForce = 20f;
        monsterExp = 100;
        deathTime = 6f;
        movementDirection = Vector3.forward;
    }
        
    protected override IEnumerator AttackCreature (Collider defensiveCreatureCollider)
    {
        Creature defensiveCreature = defensiveCreatureCollider.transform.parent.gameObject.GetComponent<Creature>();

        // The Werewolf attacks according to its animation
        while (attackCoroutine != null)
        {
            GetComponent<WerewolfAnimation>().PlayMeleeAttackAnimation();
            yield return new WaitForSeconds(0.4f);

            actionsController.MeleeAttack(this, defensiveCreature);
            SoundsFXController.Instance.PlaySound(attackSound);

            yield return new WaitForSeconds(0.5f);

            actionsController.MeleeAttack(this, defensiveCreature);
            SoundsFXController.Instance.PlaySound(attackSound);
        }
    }
}
