using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPCMove : MonoBehaviour
{
    public Transform[] destinations;
    private float speed = 0.003f;
    private float rotationSpeed = 0.03f;
    private bool isOnBreak = false;
    Random rng = new Random(0);
    private int patrolCounter = 0;
    private int prevDest = -1;
    private int idleTime = 5; // in sec


    private void Start()
    {
        // Invoke("StartChasing", 2);
        GetComponent<Animator>().SetBool("Walk", true);
        Patrol();
    }

    void Chase()
    {
        // Vector3 realGoal = new Vector3(goal.position.x, 
        //     transform.position.y, goal.position.z);
        // Vector3 direction = realGoal - transform.position;
        //
        // transform.rotation = Quaternion.Slerp(transform.rotation, 
        //     Quaternion.LookRotation(direction), rotationSpeed);
        //
        // if (direction.magnitude >= distance)
        // {
        //     GetComponent<NavMeshAgent>().SetDestination(goal.position);
        // }
        // else // caught the player
        // {
        //     GetComponent<Animator>().SetBool("Is Walking", false);
        //     GetComponent<Animator>().SetBool("Is Running", false);
        //     isChaser = false;
        //     canBeCaught = false;
        //     Invoke("CanBeCaught", 3);
        // }
    }

    Transform getNextDest()
    {
        int dest;
        do
        {
            dest = rng.Next(4);
        } while (dest == prevDest);

        prevDest = dest;
        return destinations[prevDest];
    }

    void Patrol()
    {
        GetComponent<Animator>().SetBool("Walk", true);
        isOnBreak = false;
        Transform patrolDestination = getNextDest();
        Vector3 realGoal =
            new Vector3(patrolDestination.position.x, transform.position.y, patrolDestination.position.z);
        Vector3 direction = realGoal - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

        GetComponent<NavMeshAgent>().SetDestination(patrolDestination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PatrolDest") || isOnBreak)
        {
            return;
        }
        patrolCounter++;
        if (patrolCounter == 2)
        {
            patrolCounter = 0;
            GetComponent<Animator>().SetBool("Walk", false);
            isOnBreak = true;
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Invoke("Patrol", idleTime);
        }
        else
        {
            Patrol();
        }
    }

    void StartRunning()
    {
        GetComponent<Animator>().SetBool("Is Running", true);
        GetComponent<Animator>().SetBool("Is Walking", false);
        speed = 0.006f;
    }
}