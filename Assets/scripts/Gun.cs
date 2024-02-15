using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float firerate;
    public float bigDamage = 2f;
    public float smallDamage = 1f;

    private float nextTimeToFire;
    private BoxCollider gunTrigger;


    public LayerMask raycastLayerMask;
    public EnemyManager enemyManager;

    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0,range * 0.5f);
    }

    

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& Time.time > nextTimeToFire)
        {
            Fire();
        }

    }


    void Fire()
    {
        //damge enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            // get direction for enemy
            var direction = enemy.transform.position - transform.position;
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction, out hit, range * 1.5f, raycastLayerMask))
            {
                if(hit.transform == enemy.transform)
                {
                    //range check
                    float distance = Vector3.Distance(enemy.transform.position, transform.position);

                    if(distance > range * 0.5f)
                    {
                        //damage enemy small
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        //damage enemy  big
                        enemy.TakeDamage(bigDamage);
                    }
                }
            }
        }

        //reset timer
        nextTimeToFire = Time.time + firerate;
    }

    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // remove potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }

}
