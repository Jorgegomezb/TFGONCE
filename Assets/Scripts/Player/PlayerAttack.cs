using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private const float ASSAULT_RIFFLE_FIRE_RATE = 3;
    private const float ENEMYFOUND_RATE = 0.5f;
    private const float AXE_SWING_COOLDOWN = 1;

    private float lastAttackTime;

    private WeaponManager weapon_Manager;

    private float nextTimeToFire;
    private float nextTimeToCheck;
    public float damage = 2f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;
    public AudioSource knock_sound;
    public AudioClip enemy_found;
    private AudioSource audio;

    void Awake(){
        audio = GetComponent<AudioSource>();
        weapon_Manager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        checkEnemy();
    }

    void checkEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {

            if (hit.transform.tag == Tags.ENEMY_TAG && Time.time > nextTimeToCheck)
            {

                nextTimeToCheck = Time.time + (1f / ENEMYFOUND_RATE);
                Debug.Log("WE FOUND: " + hit.transform.gameObject.name);
                audio.PlayOneShot(this.enemy_found);

                
            }

        }

    }

    void WeaponShoot(){
        //aSS RIfle
        if(weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE){
            //mantener pulsado el boton izquierdo del ratón
            if(Input.GetButton("Fire1") && Time.time >nextTimeToFire ){

                Debug.Log("FIRE!");
                nextTimeToFire= Time.time+ (1f / ASSAULT_RIFFLE_FIRE_RATE);
                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            }
        } else {
            if(Input.GetMouseButton(0) && Time.time >nextTimeToFire ) {
                // handle axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    nextTimeToFire= Time.time + AXE_SWING_COOLDOWN;
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    RealizeAHit();
                }
            }
        }
    }

    void ZoomInandOut(){
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_aim == WeaponAim.AIM){ //puede apuntar
            if(Input.GetMouseButtonDown(1)){
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }
             if(Input.GetMouseButtonUp(1)){
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }
    }

   void BulletFired() {

        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == Tags.ENEMY_TAG) {
                //esto debería eliminar al enemigo
                Debug.Log("WE HIT: "+ hit.transform.gameObject.name + "for: "+ damage);
                //Destroy(hit.transform.gameObject); // destroy the object hit
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
            
        }
   }

   void RealizeAHit() {

           RaycastHit hit;

           if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

               if(hit.transform.tag == Tags.ENEMY_TAG && hit.distance < 5) {
                   //esto debería eliminar al enemigo
                   print("WE HIT: "+ hit.transform.gameObject.name);
                   //Destroy(hit.transform.gameObject); // destroy the object hit
                   hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
               } else if(hit.transform.tag == "Wall" && hit.distance < 3) {
                    knock_sound.Play();
               }
           }
      }
}
