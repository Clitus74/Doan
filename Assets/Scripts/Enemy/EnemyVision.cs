using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    [SerializeField] public Transform startView;

    [SerializeField] public float radius;
    public GameObject player;
    [Range(0, 360)]
    [SerializeField]public float viewAngle;
    [SerializeField] LayerMask playerMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vision();
    }

    private void Vision()
    {
        Collider[] viewCheck = Physics.OverlapSphere(startView.position, radius, playerMask);

        if (viewCheck.Length != 0)
        {
            Transform target = viewCheck[0].transform;
            Vector3 directionTarget = (target.position - startView.position).normalized;
            if (Vector3.Angle(transform.forward, directionTarget) < viewAngle / 2)
            {
                float distanceTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(startView.position, directionTarget, distanceTarget, obstructionMask ))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    
}
