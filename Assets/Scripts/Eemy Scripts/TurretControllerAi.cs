using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class TurretControllerAi : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyState enemy_State;
    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    public GameObject Gun;
    public GameObject Hit;
    private Puntuacion Score;
    public float chase_Distance = 30f;
    private float current_Chase_Distance;
    public float attack_Distance = 25f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;
    public bool esTorreta;
    private bool dead=false;

    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("PLAYER_TAG").transform;
        Score = GameObject.FindGameObjectWithTag("PLAYER_TAG").GetComponent<Puntuacion>();
        enemy_Audio = GetComponent<EnemyAudio>();

    }

    // Use this for initialization
    void Start()
    {
        SetRigidBodyState(true);
        SetColliderState(false);
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        // when the enemy first gets to the player
        // attack right away
        attack_Timer = wait_Before_Attack;

        // memorize the value of chase distance
        // so that we can put it back
        current_Chase_Distance = chase_Distance;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dead);
        if (dead == false)
        {
            if (enemy_State == EnemyState.PATROL)
            {
                Patrol();
            }

            if (enemy_State == EnemyState.CHASE)
            {
                Chase();
            }

            if (enemy_State == EnemyState.ATTACK)
            {
                Attack();
            }
        }
        else {
            gameObject.GetComponent<NavMeshAgent>().enabled = false ;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<TurretControllerAi>().enabled=false;
        }

    }
 

    void Patrol()
    {

        // tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time && !esTorreta)
        {

            SetNewRandomDestination();
            patrol_Timer = 0f;

        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            Debug.Log("Camina");
            Debug.Log(chase_Distance);
            enemy_Anim.Walk(true);

        }
        else
        {

          enemy_Anim.Walk(false);

        }

        // test the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {

           enemy_Anim.Walk(false);
            Debug.Log("CHASE");
            enemy_State = EnemyState.CHASE;

            // play spotted audio
           // enemy_Audio.Play_ScreamSound();

        }


    } // patrol

    void Chase()
    {

        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // set the player's position as the destination
        // because we are chasing(running towards) the player
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {

           // enemy_Anim.Run(true);

        }
        else
        {

          //  enemy_Anim.Run(false);

        }

        // if the distance between enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {

            // stop the animations
           // enemy_Anim.Run(false);
           // enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
               current_Chase_Distance=chase_Distance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            // player run away from enemy

            // stop running
           // enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }


        } // else

    } // chase

    void Attack()
    {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;
        
        if (attack_Timer > wait_Before_Attack)
        {
            Vector3 a = new Vector3(target.position.x, target.position.y - 4.56f, target.position.z);
            gameObject.transform.LookAt(a);
            enemy_Anim.Attack();

            attack_Timer = 0f;

            // play attack sound
            enemy_Audio.Play_AttackSound();

            //Shoot
            Gun.SendMessage("shoot");

        }

        if (Vector3.Distance(transform.position, target.position) >
           attack_Distance + chase_After_Attack_Distance)
        {

            enemy_State = EnemyState.CHASE;

        }


    } // attack
    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);

    }

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }

    public void die() {
        Debug.Log("CE MURIO");
        dead = true;
        Score.SendMessage("addScore");
        Destroy(Hit);
        GetComponent<Animator>().enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
        
        
    }

    void SetRigidBodyState(bool state) {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }
} // class
