using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class NPCMove : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Transform[] destinations;
    private float catchDistance = 1f;
    private float rotationSpeed = 0.03f;
    private bool isOnBreak = false;
    Random rng = new Random(0);
    private int patrolCounter = 0;
    private int prevDest = -1;
    private int idleTime = 5; // in sec


    private void Start()
    {
        GetComponent<Animator>().SetBool("Walk", true);
        Patrol();
    }

    public void Chase()
    {
        GetComponent<Animator>().SetBool("Run", true);
        Vector3 realGoal = new Vector3(player.position.x, 
            transform.position.y, player.position.z);
        Vector3 direction = realGoal - transform.position;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(direction), rotationSpeed);
        
        if (direction.magnitude >= catchDistance)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.position);
        }
        else // caught the player
        {
            SceneManager.LoadScene(3);
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