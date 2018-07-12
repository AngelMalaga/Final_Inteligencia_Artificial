using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 

public class Player : MonoBehaviour
{
 GameObject Misil,GameManage;
 //int health  = 1;
 GameManager game;
 int test ;
  void Start()
  {
	  GameManage = GameObject.Find("GameManager");
      game = GameManage.GetComponent<GameManager>();
	  Misil = Resources.Load<GameObject>("Misil");
  }
	void Update()
	{
    EnemyIsNear();
		if(Input.GetMouseButtonDown(0) &&	GameManager.state == GameManager.CubeState.PLAY)
		{
			 if(DataList.enemies.Count >= 0){
				  Misil MisilClone =Misil.GetComponent<Misil>();
					MisilClone.setEnemyTarget(MouseVector());
					Instantiate(MisilClone,transform.position,Quaternion.identity);
	     	}
		}

	}

string	MouseVector()
	{
	Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	  mousePosition.z = 0;
		string id ="";
		string[] dataSend = new string[2];
    if(DataList.enemies != null)
		{
	    for(int i = 0; i < DataList.enemies.Count;i++)
	    { 
			    GameObject enemy =	DataList.enemies[i];
         float distancia = (mousePosition - enemy.transform.position).magnitude;
				   if(distancia < 1f )
					 {
						  EnemyController datos = enemy.GetComponent<EnemyController>(); 		
						  id = datos.getId();
					 }
         
	      }
		}

    return id;
	}
void EnemyIsNear()
	{
   
	  if(DataList.enemies.Count > 0)
		{
	    for(int i = 0; i < DataList.enemies.Count;i++)
	    { 
				  GameObject enemy =	DataList.enemies[i];
                   float distancia = (transform.position - enemy.transform.position).magnitude;
				   if(distancia < 1f )
					 {
						 GameObject enemyTarget = DataList.enemies[i];
						 EnemyController datos = enemy.GetComponent<EnemyController>(); 						
						   game.setHealth(1);
						datos.setDestroy();
						DataList.enemies.RemoveAt(i);

					 }


	        }
		}

	}


	

}