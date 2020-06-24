using UnityEngine;
using System.Collections;

public class door : MonoBehaviour
{
	private const float OPEN_DOOR_COOLDOWN = 1;
	[SerializeField]
	GameObject thedoor;

	public KillCount numEnemysKilled;

	[SerializeField]
	private int enemysToKill;

	//private EnemyManager numInitialEnemys;
	//private EnemyAnimator numActualKills;

	private bool hasOpened = false;
	//private bool hasClosed = false;

    public AudioSource door_sound;

    private float lastOpenTime;
        private float nextTimeToOpen;

	void OnTriggerEnter(Collider obj)
	{
		if (noEnemys()) // && !hasOpened
		{
			//thedoor = GameObject.FindWithTag("SF_Door");
			thedoor.GetComponent<Animation>().Play("open");
            if(Time.time > nextTimeToOpen) {
                nextTimeToOpen= Time.time + OPEN_DOOR_COOLDOWN;
                door_sound.Play();
            }

			hasOpened = true;
		}
	}

	void OnTriggerExit(Collider obj)
	{
		//if (numEnemys == 0) // && !hasClosed
		if(hasOpened)
		{
		//thedoor = GameObject.FindWithTag("SF_Door");
		thedoor.GetComponent<Animation>().Play("close");

		//hasClosed = true;
		}
	}

	bool noEnemys()
	{
		return (numEnemysKilled.getCount() >= enemysToKill); // Cuando las dos lleguen al mismo numero
	}

}