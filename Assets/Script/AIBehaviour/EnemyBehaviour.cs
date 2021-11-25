using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator AIAnimator;
    [SerializeField] private GameObject Player, MonsterBullet, MonsterBulletSpawn;
    [SerializeField] private NavMeshAgent agent;
    private float meleeDistance = 3f;
    [SerializeField] private float hitPoints, maxHitPoints;
    [SerializeField] private bool dead;
    [SerializeField] private AudioClip[] monsterSoundEffects;
    private int despawnTime = 9, speed = 5;
    private void Start()
    { 
        AIAnimator = GetComponent<Animator>();
        maxHitPoints = 6;
        hitPoints = maxHitPoints;
        agent.speed = speed;
        Player = GameObject.FindWithTag("Player");

        Physics.Raycast(Vector3.zero, Vector3.forward);
    }

    private void FixedUpdate()
    {
        AIAnimator.SetFloat("MovementSpeed", agent.velocity.magnitude);

        if (!dead)
            agent.destination = Player.transform.position;

        if (dead || AIAnimator.GetBool("Shootin") || AIAnimator.GetBool("MeleeRange"))
        {
            agent.velocity = Vector3.zero;
        }
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, agent.transform.forward, out hit, Mathf.Infinity) && !AIAnimator.GetBool("MeleeRange"))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (agent.remainingDistance > meleeDistance && !AIAnimator.GetBool("MeleeRange"))
                    RollToShoot();
            }
        }
        
        if (Vector3.Distance(Player.transform.position, agent.transform.position) < meleeDistance)
        {
            AIAnimator.SetBool("MeleeRange", true);
        }
        else
        {
            AIAnimator.SetBool("MeleeRange", false);
        }
        
    }

    private void Swipe()
    {
        if (Vector3.Distance(agent.transform.position ,Player.transform.position) < meleeDistance)
        {
            Player.GetComponent<HealthManager>().ChangeHP(-1);
        }
    }
    
    private void PlayAudio(int sfxID)
    {
        AudioSource monsterSFX = GetComponent<AudioSource>();
        if (sfxID == 1)
        {
            monsterSFX.volume = 0.2f;
        }
        else
        {
            monsterSFX.volume = 1;
        }
        monsterSFX.clip = monsterSoundEffects[sfxID];
        monsterSFX.Play();
    }

    private void EndAnimation()
    {
        AIAnimator.SetBool("Shootin", false);
    }

    private IEnumerator DeathDespawn()
    {
        Options.ligma--;
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void RollToShoot()
    {
        int shotChance = UnityEngine.Random.Range(0, 250);

        if (shotChance <= 2)
        {
            AIAnimator.SetBool("Shootin", true);
            agent.velocity = Vector3.zero;
        }
    }

    private void MonsterShot()
    {
        Instantiate(MonsterBullet, MonsterBulletSpawn.transform.position, MonsterBulletSpawn.transform.rotation);
        PlayAudio(1);
    }
    private void LateUpdate()
    {
        if(hitPoints < 1)
            dead = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("bullet")) return;

        hitPoints -= UnityEngine.Random.Range(1, 5);
        AIAnimator.SetFloat("Hitpoints", hitPoints);

        if (hitPoints < 1)
        {
            Destroy(GetComponent<CapsuleCollider>());
        }
    }
}
