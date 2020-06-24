using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    public int current_Weapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index=0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){ //tecla 1
            TurnOnSelectedWeapon(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){ //tecla 1
            TurnOnSelectedWeapon(1);
        }
    }
    public void TurnOnSelectedWeapon(int weaponIndex){
    	if(current_Weapon_Index== weaponIndex){
    		return;
    	}
        weapons[current_Weapon_Index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        current_Weapon_Index = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon(){
        return weapons[current_Weapon_Index];
    }
}
