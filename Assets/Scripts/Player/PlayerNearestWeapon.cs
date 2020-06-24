using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNearestWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject nearestWeapon;
    public int indexNearestWeapon;

    public Text interactionText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(weapons.Length > 0) {
            int indexOfNearWeapon = GetClosestWeapon(weapons);
            GameObject weapon = weapons[indexOfNearWeapon];
            GameObject Player = GameObject.Find("Player");
            if(weapon != null && Vector3.Distance(weapon.transform.position, Player.transform.position) <= 3.00) {
                if (nearestWeapon == null) {
                    interactionText.text = "press F to change your weapon";
                }
                indexNearestWeapon = weapon.GetComponent<WeaponDroppedInfo>().weaponIndex;
                nearestWeapon = weapon;
                if(Input.GetKeyDown(KeyCode.F)){ //tecla F
                    int index = Player.GetComponent<WeaponManager>().current_Weapon_Index;
                    Player.GetComponent<WeaponManager>().TurnOnSelectedWeapon(indexNearestWeapon);
                    Destroy(nearestWeapon);
                    weapons[indexOfNearWeapon] = null;
                    nearestWeapon = null;
                    interactionText.text = "";
                    Player.GetComponent<DropTheEquippedWeapon>().SpawnWeapon(index);
                }
            } else {
                if (interactionText.text != "") {
                    interactionText.text = "";
                }
                nearestWeapon = null;
            }
        } else {
            if (interactionText.text != "") {
                interactionText.text = "";
            }
            nearestWeapon = null;
        }
    }

    int GetClosestWeapon(GameObject[] weapons)
    {
        int bestTarget = -1;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = GameObject.Find("Player").transform.position;
        for(int i = 0; i < weapons.Length; i++) {
            if(weapons[i] != null) {
                Vector3 directionToTarget = weapons[i].transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = i;
                }
            }
        }

        return bestTarget;
    }
}
