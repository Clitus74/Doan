using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyVision))]
public class DrawVision : Editor
{
    

    private void OnSceneGUI()
    {
        EnemyVision enemyVision = (EnemyVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyVision.startView.position, Vector3.up, Vector3.forward, 360, enemyVision.radius);

        Vector3 viewAngle01 = DirectionFromAngle(enemyVision.startView.eulerAngles.y, -enemyVision.viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemyVision.startView.eulerAngles.y, enemyVision.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemyVision.startView.position, enemyVision.startView.position + viewAngle01 * enemyVision.radius);
        Handles.DrawLine(enemyVision.startView.position, enemyVision.startView.position + viewAngle02 * enemyVision.radius);
        if (enemyVision.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(enemyVision.startView.position, enemyVision.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegree)
    {
        angleInDegree += eulerY;
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }
}
