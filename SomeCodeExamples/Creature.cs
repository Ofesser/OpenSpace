using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour{


    public int hp;
    [HideInInspector]
    public int maxHp;
    [HideInInspector]
    public float maxSpeed;
    [HideInInspector]
    public float currSpeed;
    [HideInInspector]
    public float armor;
    [HideInInspector]
    public float meleeForce;
    [HideInInspector]
    public Vector3 movementDirection;
    [HideInInspector]
    public bool creatureDying;
    [HideInInspector]
    public bool creatureInvunerable;

    protected CreatureActionsController actionsController;
    protected Weapon creatureWeapon;

    protected void Awake ()
    {
        if (actionsController == null)
        {
            actionsController = CreatureActionsController.Instance;
        }
        InitCreatureParameters();
    }

    // Default initialization of parameters. If need to define in specific way - override in children
    protected virtual void InitCreatureParameters ()
    {
        maxHp = 10;
        hp = maxHp;
        maxSpeed = 1f;
        currSpeed = 0f;
        armor = 1f;
        meleeForce = 1f;
        movementDirection = Vector3.forward;
    }

    public virtual void Move()
    {
        actionsController.Move(this);
    }
    public virtual void Stand(){}
    public virtual void MeleeAttack(){}

    public virtual void Death()
    {
        creatureDying = true;
    }

    public virtual void Shot()
    {
        creatureWeapon.Shot();
    }

}