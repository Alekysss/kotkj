using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{

    public float awarnessRadius = 8f;
    public bool isAggro;
    public Material aggroMat;
    private Transform playerTransform;


    private void Start()
    {
        playerTransform = FindObjectOfType<playerMovement>().transform;
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, playerTransform.position);

        if(distance < awarnessRadius)
        {
            isAggro = true;
        }

        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
