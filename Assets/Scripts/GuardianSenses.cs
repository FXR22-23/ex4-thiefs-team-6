using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSenses : MonoBehaviour
{
    public GameObject player;
    private float viewingFieldDistance = 7;
    private float viewingFieldAngle = 45;
    private float hearingThreshold = 3;

    // Update is called once per frame
    void Update()
    {
        if (IsInSight() || IsHeard())
        {
            GetComponent<NPCMove>().Chase();
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
        if (angle > viewingFieldAngle)
        {
            return false;
        }
        // obsticles
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + new Vector3(0, 1, 0);
        if (Physics.Raycast(rayOrigin, distanceVector, out hit, distance))
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
