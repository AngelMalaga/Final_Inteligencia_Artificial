using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Misil : MonoBehaviour
{

	
    GameObject player ,GameManage ;
	Vector3  data= new Vector3();
	int init = 27;
	int end = 27;
    int Tnext = 0;
     float t = 0;
	int target  ;
    string enemyName ;
	public string enemyTargetList ;
	public int enemyTarget;
	 GameManager game;
     public void setTarget(int variable)
    {
        target = variable;
    }
	public int getTarget()
      {
    return target;
     }
	 public void setEnemyTarget(string variable)
    {
        enemyTargetList = variable;
    }
	public string getEnemyTarget()
      {
    return enemyTargetList;
     }
	void Start()
	{
        player = GameObject.Find("player");
	   GameManage = GameObject.Find("GameManager");
       game = GameManage.GetComponent<GameManager>();
	
	   enemyTargetListSet();
	}
	void Update()
	{
		if(GameManager.state == GameManager.CubeState.LOSE)
		{
         Dead();
		}
           EnemyIsNear();
			 transform.up =(DataList.conexiones[end]- transform.position);
             transform.position = Vector3.Lerp(DataList.conexiones[init], DataList.conexiones[end], t);
              t += 0.100f;

		        if (t >= 1)
		        {   	
					Next();
		
					IsAlive();
		        	t = 0;
		      }
	}
	void IsAlive()
	{
   int cuenta = 0;

      if(DataList.enemies.Count <= 0)
		{
           Dead();
		}else{
	 for(int i = 0; i < DataList.enemies.Count;i++)
	{
		GameObject enemy = DataList.enemies[i];
	     EnemyController EnemyTarget = enemy.GetComponent<EnemyController>(); 
		 if(EnemyTarget.getId() == enemyName )
		 { 		
			  target =  EnemyTarget.getEnd();
			  cuenta = 1;
		 }else if(EnemyTarget.getId() != enemyName && cuenta == 0 && i == DataList.enemies.Count-1)
		 {
			Dead(); 
		 }
	  }
	 }
	}
   void enemyTargetListSet()
   {
	   if(enemyTargetList!= ""){
	   enemyName = enemyTargetList;
	   }
   }
	void Next()
	{
		if(target!= init)
		{
	     List<int> NextPoint =  Disjkstra(DataList.m, init, target);
		if(Tnext < 2)
		{
        init = end;
		end = NextPoint[Tnext];
		Tnext++;
		}else{
		init = end;
		Tnext -= 1;
		}
	  }
	}
	void EnemyIsNear()
	{
	  if(DataList.enemies != null)
		{
	    for(int i = 0; i < DataList.enemies.Count;i++)
	    {
				  GameObject enemy =	DataList.enemies[i];
                   float distancia = (transform.position - enemy.transform.position).magnitude;
				   if(distancia < 1f )
					 {
						 EnemyController EnemyTargeting = enemy.GetComponent<EnemyController>(); 
						game.setScore(1);
                        Dead();
						EnemyTargeting.setDestroy();
						DataList.enemies.RemoveAt(i);
					 }
	         }
		}
	}
	void Dead()
	{
		Destroy(this);
	 	Destroy(gameObject);
	}

  // ///////////////// PathFinding

	  public List<int> Disjkstra(int[,] graph, int start, int end)
	{
		// Inicializa valores

		int N = graph.GetLength(0);

		int[] distances = new int[N];
		int[] procedences = new int[N];
		bool[] blacklist = new bool[N];

		for (int i = 0; i < N; i++)
		{
			distances[i] = int.MaxValue;
			procedences[i] = -1;
			blacklist[i] = false;
		}
		distances[start] = 0;
		// Se repite esto N veces
		
		for (int count = 0; count < N - 1; count++)
		{
			int minIndex = MinDistance(distances, blacklist);
			blacklist[minIndex] = true;

			for (int neighborIndex = 0; neighborIndex < N; neighborIndex++)
			{
				if (!blacklist[neighborIndex] &&
					graph[minIndex, neighborIndex] != 0 &&
					distances[minIndex] + graph[minIndex, neighborIndex] < distances[neighborIndex])
				{
					distances[neighborIndex] = distances[minIndex] + graph[minIndex,neighborIndex];
					procedences[neighborIndex] = minIndex;
				}
			}
		}
		List<int> path = new List<int>();
		int pre = -1;
		int current = end;
		path.Add(current);
		while(pre != start)
		{
			pre = procedences[current];
			path.Add(pre);
			current = pre;
		}
		path.Reverse();
		PrintSolution(path);
		return path;
	}
	 private int MinDistance(int[] distances, bool[] blacklist)
	{
		int min = int.MaxValue;
		int minIndex = 0;

		for (int v = 0; v < blacklist.Length; v++)
		{
			if (blacklist[v] == false && distances[v] <= min)
			{
				min = distances[v];
				minIndex = v;
			}
		}
		return minIndex;
	}
	 private void PrintSolution(List<int> path)
	{
		for (int i = 0; i < path.Count; i++)
		{
			Debug.Log(path[i]);
		}
	}
}
