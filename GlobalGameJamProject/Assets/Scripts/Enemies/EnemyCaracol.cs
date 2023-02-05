using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaracol : MonoBehaviour
{

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private int target = 0;
    [SerializeField] private float speed = 1f;

    private void Update()
    {   
        float step = speed * Time.deltaTime; 
        transform.position = Vector2.MoveTowards(transform.position, waypoints[target].position, step);

        if (Mathf.Abs(transform.position.x - waypoints[target].position.x)  < 0.05f)
        {
            target++;
            transform.right = new Vector3(transform.right.x * -1, transform.right.y);
            if (target >= waypoints.Count)
            {
                target = 0;
            }
        }

    }

}
