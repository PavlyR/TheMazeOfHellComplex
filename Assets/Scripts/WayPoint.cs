using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPoint : MonoBehaviour
{

    [SerializeField]
    protected float influenceRadius = 50f; //How big of an area the waypoint will cause Alan to search in

    [SerializeField]
    GameObject player;

    public GameObject alanRef;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, influenceRadius);

    }
   /* public void RandomTarget(NavMeshAgent Alan)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * influenceRadius;
        NavMeshHit navhit;

        if (NavMesh.SamplePosition(randomDirection, out navhit, 1.0f, NavMesh.AllAreas))
        {
            print(randomDirection);
            alanRef.GetComponent<Alan_Controller>().patrolTarget = randomDirection;
        }
    }
   */
}
