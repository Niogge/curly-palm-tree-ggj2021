using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum State
{
    Chase, Attack, NONE
}
public class EnemyBehaviour : MonoBehaviour
{
    public DamageBehaviour Weapon;
    public GameObject DropPrefab;

    [HideInInspector]
    public Transform Player;
    public float RotSpeed = 10f;
    public float MinChaseDist;
    public float MaxAttackDist;

    [HideInInspector]
    public Vector3 InitialPosition;
    private Animator anim;
    private NavMeshAgent agent;
    private State state;

    [Header("Audio")]
    public List<AudioClip> attackClips;
    AudioSource src;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        src = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        transform.position = InitialPosition;
    }

    public void OnDeath()
    {
        GameObject go = Instantiate(DropPrefab);
        go.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        go.GetComponent<Pickable>().CreateItem();
    }

    void Update()
    {
        transform.LookAt(Player);

        CheckStates();
        DumpFSM();
    }


    public void BeginSwing()
    {
        src.PlayOneShot(attackClips[Random.Range(0, attackClips.Count)], 0.5f);
        Weapon.CanDamage = true;
    }

    public void EndSwing()
    {
        Weapon.CanDamage = false;
    }


void CheckStates()
    {
        float dist = Vector3.Distance(transform.position, Player.position);
        if (dist > MaxAttackDist)
        {
            state = State.Chase;
            return;
        }
        else if(dist < MinChaseDist)
        {
            state = State.Attack;
            return;
        }
        state = State.NONE;
    }
    void DumpFSM()
    {
        switch (state)
        {
            case State.Chase:
                ChaseState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.NONE:
                break;
        }
    }

    public void ChaseState()
    {
        if (anim.GetBool("attack"))
            anim.SetBool("attack", false);
        if (!anim.GetBool("walk"))
            anim.SetBool("walk", true);

        agent.SetDestination(Player.position);
    }

    public void AttackState()
    {
        if (!agent.isStopped)
            agent.isStopped = true;
        if (anim.GetBool("walk"))
            anim.SetBool("walk", false);
        if (!anim.GetBool("attack"))
            anim.SetBool("attack", true);
    }
}
