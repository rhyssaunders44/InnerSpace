using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

namespace Player
{
    public class P_Bullet : MonoBehaviour
    {
        /// <summary>
        /// this class covers bullet movement
        /// </summary>
        private Rigidbody rb;
        private int bulletVelocity = 100;
        private float spawnTime, deathTime;
        private int hitID = 0;
        private float die;

        /// <summary>
        /// on instantiation acquire the bullet rigidbody
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// set up bullet variables.
        /// this leaves object pooling to be optimized
        /// </summary>
        void Start()
        {
            deathTime = Time.time + 3;
            die = UnityEngine.Random.Range(0, 0.2f);
            rb.velocity = transform.forward * bulletVelocity;
        }

        /// <summary>
        /// destroy this bullet after a certain time
        /// yes Destroy(gameObject, deathTime) would have been better
        /// </summary>
        private void Update()
        {
            // kills on miss
            if (Time.time > deathTime)
                Destroy(gameObject);
        }
        /// <summary>
        /// relays damage dealt to the player
        /// starts the ricochet coroutine if it does not hit an enemy
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                hitID = 1;
            }
			if (other.transform.GetComponent<HealthManager>())
				other.transform.GetComponent<HealthManager>().ChangeHP(-1);
            StartCoroutine(Death());
        }

        
        /// <summary>
        /// kills the bullet if it hits an enemy,
        /// ricochet if it hits anything else
        /// </summary>
        /// <returns></returns>
        private IEnumerator Death()
        {
            if (hitID != 1)
            {
                yield return new WaitForSeconds(die);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}

