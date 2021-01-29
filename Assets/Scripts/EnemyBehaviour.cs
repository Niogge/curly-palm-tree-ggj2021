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
    [HideInInspector]
    public Transform Player;
    public float RotSpeed = 10f;
    public float MinChaseDist;
    public float MaxAttackDist;

    private Animator anim;
    private NavMeshAgent agent;
    private State state;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);

        CheckStates();
        DumpFSM();
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
