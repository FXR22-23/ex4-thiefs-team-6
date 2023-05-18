using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject gameManager;

    public Transform[] destinations;
    private float catchDistance = 1f;
    private float rotationSpeed = 0.03f;
    private bool isOnBreak = false;
    Random rng = new Random(0);
    private int patrolCounter = 0;
    private int prevDest = -1;
    private int idleTime = 5; // in sec
    private bool startedChaseMusic = false;
    private bool isChasing = false;
    

    private void Start()
    {
        GetComponent<Animator>().SetBool("Walk", true);
        Patrol();
    }

    private void Update()
    {
        if (isChasing)
        {
            Chase();
        }
    }

    public void Chase()
    {
        isChasing = true;
        if (!startedChaseMusic)
        {
            gameManager.GetComponent<GameManager>().TurnOnChaseMusic();
            startedChaseMusic = true;
        }
        GetComponent<Animator>().SetBool("Run", true);
        Vector3 realGoal = new Vector3(player.transform.position.x, 
            transform.position.y, player.transform.position.z);
        Vector3 direction = realGoal - transform.position;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(direction), rotationSpeed);
        
        if (direction.magnitude >= catchDistance)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
        else // caught the player
        {
            gameManager.GetComponent<GameManager>().TurnOnLoseMusic();
            SceneManager.LoadScene(3);
            player.transform.position = new Vector3(12.836f, 2.6766f, 37.968f);
            avatar.transform.position = new Vector3(12.836f, 2.6766f, 37.968f);
        }
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
}