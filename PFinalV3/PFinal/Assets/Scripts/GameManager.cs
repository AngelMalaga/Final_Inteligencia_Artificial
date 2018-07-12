using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	int level = 1;
	int enemys  = 0;
	int health  = 1;
	public Text UITextScore,UITextLevel;
   GameObject Vortex;
	public enum CubeState {
		INIT,
        PLAY,
		WIN,
		LOSE,
		STAND
	}
	 static public  CubeState state;
	public void setHealth(int variable)
    {
        health -= variable;
        Debug.Log("health:"+health);
    }
	public int getHealth()
    {
    return health;
     }
	 public void setScore(int variable)
    {
        enemys += variable;
		UITextScore.text = "Enemigos:" + enemys;
    }
	public int getScore()
      {
    return enemys;
     }
	void Start () {
		state = CubeState.INIT;
		    Vortex = Resources.Load<GameObject>("Vortex");
			  	 Level();
	}
	void Update () {
		switch (state)
		{
			case CubeState.INIT:
			{
			     	Time.timeScale = 0;
					 GameManager.state = GameManager.CubeState.PLAY;
				break;
			}
			case CubeState.PLAY:
			{
				 Time.timeScale = 1; 
				 if(health <= 0)
		         {
		               health = 0;
			           GameManager.state = GameManager.CubeState.LOSE;
			     }else if( enemys >=20)
				 {                  
					 level += 1;
					 Level();
					 print("Level: "+ level);
					 if(level >=4 && health >=1)
					 {
                       GameManager.state = GameManager.CubeState.WIN;
					   level = 0;
					 }
                     enemys = 0;
				 }
				break;
			}
			case CubeState.WIN:
			{				
               if(DataList.enemies.Count > 0){
		        foreach(GameObject enemys in DataList.enemies)
				 {
                      EnemyController EnemyTargeting = enemys.GetComponent<EnemyController>(); 
                      EnemyTargeting.setDestroy();    
				 }
				 DataList.enemies.Clear();
				 SceneLoad("Win");
				}
			 Time.timeScale = 0; 
              Debug.Log("Ganaste!");
				Time.timeScale = 0; 

				break;
			}
			case CubeState.LOSE:
			{
				Time.timeScale = 0; 
                if(DataList.enemies.Count > 0){
		        foreach(GameObject enemys in DataList.enemies)
				 {
                      EnemyController EnemyTargeting = enemys.GetComponent<EnemyController>(); 
                      EnemyTargeting.setDestroy();
				 }
				 DataList.enemies.Clear();
				 SceneLoad("Lose");
				 Debug.Log("Perdiste!");
				}
				break;
			}
			case CubeState.STAND:
			{
				break;
			}
		}
	}
 public void SceneLoad(string SceneName)
 {
    SceneManager.LoadScene(SceneName);
 }

 void SpawnVortex()
 {
		 DataList.vortexs.Clear();
          for(int i = 0 ; i<= 9;i++)
           {
			int x,y;
			x = Random.Range(-8,8);
		    y = Random.Range(-5,5);
            DataList.vortexs.Add(Instantiate(Vortex,new Vector3(x,y),Quaternion.identity));
		   }
 }
 void EnemyMove(float force)
 {
	 EnemySpawn.force = force;
 }

 void Level()
 {
	    UITextLevel.text = "Level: " + level;
		switch(level)
		{
          case 1:
            EnemyMove(0.010f);
            SpawnVortex();
		  break;
		 case 2:
            EnemyMove(0.025f);
            SpawnVortex();
		  break;
		 case 3:
           EnemyMove(0.035f);
		   SpawnVortex();
		  break;
		
		}
   }
}









