using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

using Random = System.Random;
using UnityEngine.UI;

namespace Player
{
    public class P_FireWeapon : MonoBehaviour
    {
        /// <summary>
        /// This class covers the weapon firing
        /// </summary>
        [SerializeField] private GameObject BulletPrefab, CasingPrefab, ShotSpawner, CasingSpawner;
        [SerializeField] private float rateOfFire = 0.3f, shotGap;
        [SerializeField] private AudioSource bulletNoise;
        [SerializeField] private GameObject Head, Gun;
        [SerializeField] private int shotNum;
        [SerializeField] private float[] shotVariation;
        [SerializeField] private int variationMinimiser = 2;
        [SerializeField] private Image crosshair;
        [SerializeField] private int crosshairScaler = 7;
        [SerializeField] private Light muzzleFlash;
        [SerializeField] private bool paused;

        /// <summary>
        /// disables firing if the game is paused
        /// </summary>
        void Update()
        {
            paused = Time.timeScale < 1;

            if(!paused)
                Pew();
        }
        
        /// <summary>
        /// contains an aim function for the gun, to cover for the target depth
        /// contains a muzzle flash controller
        /// rotates and enlarges the crosshair to represent the increased shot varience in sustained fire
        /// </summary>
        private void FixedUpdate()
        {
            
            if(Physics.Raycast(Head.transform.position, Head.transform.forward, out var hit, Mathf.Infinity))
            {
                //gun looks at where the player is looking
                Gun.transform.LookAt(hit.point);
            }

            if (muzzleFlash.intensity > 0)
            {
                muzzleFlash.intensity -= 1;
            }

            //go back to gun's rotation
            ShotSpawner.transform.rotation = Quaternion.Slerp(ShotSpawner.transform.rotation, Gun.transform.rotation, Time.deltaTime * variationMinimiser);
            //crosshair size & rotation
            crosshair.transform.localScale =  Vector3.Slerp(Vector3.one, new Vector3(1 + (crosshairScaler * shotNum), 1 + (crosshairScaler * shotNum), 1), Time.deltaTime/ 2);
        }

        
        /// <summary>
        /// this functions covers the firing action
        /// after 4 shots the shot variance increases to a maximum
        /// spawn a bullet, play the correct audio, and set the muzzle flash intensity
        /// start the timer for when the next shot is able to be fired
        /// </summary>
        public void Fire()
        {
            if (Time.time > shotGap)
            {
                //limits shot variation
                shotNum++;
                if (shotNum > 4)
                    shotNum = 4;
                
                //creates variation
                if (shotNum > 1)
                {
                    for (int i = 0; i < shotVariation.Length; i++)
                    {
                        shotVariation[i] = UnityEngine.Random.Range(-0.5f, 0.5f) * shotNum;
                    }
                    ShotSpawner.transform.Rotate(new Vector3( shotVariation[0],shotVariation[1],0));
                }
                
                //normal firing outputs
                GameObject bulletClone = Instantiate(BulletPrefab, ShotSpawner.gameObject.transform.position, ShotSpawner.transform.rotation);
                GameObject casing = Instantiate(CasingPrefab, CasingSpawner.gameObject.transform.position,
                    CasingSpawner.transform.rotation);
                bulletNoise.Play();
                muzzleFlash.intensity = 2;
                shotGap = Time.time + rateOfFire;
            }
        }

        
        /// <summary>
        /// covers player inputs
        /// </summary>
        public void Pew()
        {
            if(Input.GetKey(KeyCode.Mouse0) && !paused)
            {
                Fire();
            }
            
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                shotNum = 0;
            }
        }
    }
}

