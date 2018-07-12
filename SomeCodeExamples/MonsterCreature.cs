using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCreature : Creature {

    protected GameObject playerObject;
    protected int monsterExp;
    protected NavMeshAgent navMesh;
    protected Coroutine attackCoroutine;
    protected float deathTime;

    [SerializeField]
    protected AudioClip deathSound;
    [SerializeField]
    protected AudioClip attackSound;
    [SerializeField]
    protected AudioClip hitSound;

    protected virtual void Start ()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerCollisionObject");
        navMesh = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update ()
    {
        if (!creatureDying)
        {
            DirectionControll();
            HealthControll();
            SpeedControll();
        }
    }

    public override void Death()
    {
        base.Death();
        ItemDropController.Instance.CheckItemDrop(transform);
        navMesh.isStopped = true;
        SoundsFXController.Instance.PlaySound(deathSound);
        StartCoroutine(DeathProcess());

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<NavMeshAgent>());

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    private void HealthControll ()
    {
        if (hp <= 0)
        {
            Death();
        }
    }

    private void DirectionControll ()
    {
        navMesh.SetDestination(playerObject.transform.position);
    }

    private void SpeedControll ()
    {
        navMesh.speed = currSpeed;
    }


    protected virtual void OnTriggerEnter (Collider triggerCollider)
    {
        if (triggerCollider.tag == "PlayerCollisionObject")
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackCreature(triggerCollider));
            }
        }
    }

    protected virtual void OnTriggerExit (Collider triggerCollider)
    {
        if (triggerCollider.tag == "PlayerCollisionObject")
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            attackCoroutine = null;
        }
    }       

    protected virtual IEnumerator AttackCreature (Collider defensiveCreatureCollider)
    {
        yield return null;
    }

    private IEnumerator DeathProcess ()
    {
        // Destroy object after deathTime
        DestroyAfterTime dt = gameObject.AddComponent<DestroyAfterTime>();
        dt.lifeTime = deathTime;


        // Creature are smoothly moving into ground after death
        while (true)
        {
            transform.Translate(-Vector3.up * 0.01f);
            yield return null;
        }
    }
}
