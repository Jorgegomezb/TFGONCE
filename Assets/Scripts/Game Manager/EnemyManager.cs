using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject enemy_prefab;

    public Transform[] spawnPoints;
    
    [SerializeField]
private int enemy_count;
private int initial_enemy_count;

public float wait_between_spawn = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void Start(){
        initial_enemy_count = enemy_count;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance(){
        if(instance== null){
            instance=this;
        }
    }
    void SpawnEnemies(){
        
        int index=0;
        Debug.Log("enemy_count " + enemy_count);
        for(int i= 0; i<enemy_count; i++){
            if(index>= spawnPoints.Length){
                index=0;
            }
            Debug.Log("Spawn ");
            Instantiate(enemy_prefab,spawnPoints[index].position,Quaternion.identity);
            index++;
        }
        //enemy_count=0; 
    }

    IEnumerator CheckToSpawnEnemies(){
        yield return new WaitForSeconds(wait_between_spawn);
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDies(bool dead){
        if(dead && enemy_count<=initial_enemy_count){
            enemy_count++;
        }else if(enemy_count>initial_enemy_count){
            enemy_count=initial_enemy_count;
        }

    }

    public void stopSpawn(){
        StopCoroutine("CheckToSpawnEnemies");
    }

    public int getEnemyCount()
    {
        return this.initial_enemy_count;
    }
}
