using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTheEquippedWeapon : MonoBehaviour
{
    [SerializeField]
    public GameObject[] weapons_prefab;
    public float spawnDistance;

    public Vector3 spawnPoint;

    public void SpawnWeapon(int weaponIndex){
        GameObject player = GameObject.Find("Player");
        Vector3 playerPosition = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;

        Vector3 spawnPos = playerPosition + playerDirection * spawnDistance;

        GameObject weapon = Instantiate(weapons_prefab[weaponIndex],spawnPos, playerRotation);
        PlayerNearestWeapon nearestWeaponScript = player.GetComponent<PlayerNearestWeapon>();

        int i = 0;
        bool found = false;
        while (i < nearestWeaponScript.weapons.Length && !found) {
            if(nearestWeaponScript.weapons[i] == null) {
                nearestWeaponScript.weapons[i] = weapon;
                found = true;
            }
            i++;
        }
    }
}
