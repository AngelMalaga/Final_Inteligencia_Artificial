using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	// Use this for initialization
	float time = 0.0f;
	public static float force = 0;
	 GameObject Enemy;
    string EnemyId = "EyeBall";
	int id = 0;
	void Start () {
	Enemy = Resources.Load<GameObject>("Enemy");
	}
	void Update () {
        time += Time.deltaTime;
		if(time >= 2.0 )
		{
		id++;
         string ID = EnemyId + id;
		 EnemyController EnemyWithId = Enemy.GetComponent<EnemyController>(); 
		 EnemyWithId.setId(ID);
	     EnemyWithId.setForce(force);
         DataList.enemies.Add(Instantiate(Enemy,transform.position,Quaternion.identity));
		   time = 0f;
	
		}

		
	}
}
