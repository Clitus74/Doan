using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private List<Transform> destination;

    [SerializeField] private Transform player;

    private Transform target;

    [SerializeField] LayerMask playerMask;

    [SerializeField]
    private Animator enemyAnimator;
    private float distance ;
    private int indexPosition ;

    private Transform cube;

    // Start is called before the first frame update
    void Start()
    {
        agent.speed = speed;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //indexPosition=0;
        distance = Vector3.Distance(transform.position, destination[0].position);

        /*
        foreach (Transform cube in destination)
        {
            if (Vector3.Distance(transform.position, cube.position) <= distance)
            {
                distance = Vector3.Distance(transform.position, cube.position);
                target = cube;
            }

        }
        */
        
        for (int i=0; i<destination.Count; i++)
        {
            if (Vector3.Distance(transform.position, destination[i].position) <= distance)
            {
                distance = Vector3.Distance(transform.position, destination[i].position);
                target = destination[i];
            }
        }

        if (Physics.CheckSphere(transform.position, 10, playerMask))
        {
            target = player;
        }
        else
        {
            //target = cube;
        }

        if(Vector3.Distance(target.position, transform.position) > 3f)
        {
            agent.SetDestination(target.position);
            enemyAnimator.SetFloat("Speed", 0.2f);
        }

        else
        {
            agent.SetDestination(transform.position);
            enemyAnimator.SetFloat("Speed", 0f);


            target = destination[1];
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, 10);
    }
}
