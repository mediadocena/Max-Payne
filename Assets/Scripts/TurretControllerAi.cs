using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretControllerAi : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public float attackDist = 3f;
    public float movspeed = 4f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDist;
        agent.speed = movspeed;
    }
    void Update()
    {
        
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        agent.destination = player.transform.position;
    }
}
