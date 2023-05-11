using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSenses : MonoBehaviour
{
    public GameObject player;
    private float viewingFieldDistance = 7;
    private float viewingFieldAngle = 30;
    private float hearingThreshold = 7;

    // Update is called once per frame
    void Update()
    {
        if (IsInSight())
        {
            Debug.Log("Gotcha!");
        }

        if (IsInSight())
        {
            Debug.Log("Heard You!");
        }
    }

    bool IsInSight()
    {
        // distance
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > viewingFieldDistance)
        {
            return false;
        }
        // angle
        Vector3 distanceVector = player.transform.position - transform.position;
        distanceVector.y = 0;
        float angle = Vector3.Angle(transform.forward, distanceVector);
        if (angle > viewingFieldAngle / 2)
        {
            return false;
        }
        // obsticles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, distanceVector.normalized, out hit, distance))
        {
            return hit.collider.gameObject == player;
        }

        return false;
    }

    bool IsHeard()
    {
        // distance
        float hearingDistance = Vector3.Distance(transform.position, player.transform.position);
        return (hearingDistance < hearingThreshold);
    }
}
