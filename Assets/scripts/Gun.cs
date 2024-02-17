using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;

    public float bigDamage = 2f;
    public float smallDamage = 1f;

    public float firerate = 1f;
    private float nextTimeToFire;
   
    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;


    public EnemyManager enemyManager;
    private BoxCollider gunTrigger;
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
        // simulate gun shot radios
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
        //alert any enemy nearby
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }


        //play test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

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
