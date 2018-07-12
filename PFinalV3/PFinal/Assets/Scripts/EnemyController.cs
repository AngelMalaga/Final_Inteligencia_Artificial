using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

    GameObject player ;
	Vector3  data= new Vector3();
	public int init = 0;
	public  int end = 0;
	public   int Tnext = 0;
	public float force = 0.000f;

	int point ;
	 List<int> NextPoint;
	float t = 0;
	public string id = "";
	 public void setEnd(int variable)
    {
        end = variable;
    }
	public int getEnd()
      {
    return end;
     }
	public void setId(string variable)
    {
        id = variable; 
    }
	public string getId()
      {
    return id;
     }
	 public void setForce(float variable)
    {
        force = variable;
    }

	public float getForce()
      {
    return force;
     }
    public void setDestroy()
    {
     Destroy(this);
	 Destroy(gameObject);
    }
	void Start()
	{
        player = GameObject.Find("player");
       spawnPoint();
	    List<int> data = Disjkstra(DataList.m, init, 27);
	}
	void Update()
	{ 
			 transform.up =(DataList.conexiones[end]- transform.position);
             transform.position = Vector3.Lerp(DataList.conexiones[init], DataList.conexiones[end], t);
              t += force;
		        if (t >= 1)
		        {   
					 Next();
		        	t = 0;
		      }
	}
	void IsNear()
	{
     List<int> data = Disjkstra(DataList.m, init, 27);
		if(Tnext < data.Count)
		{
        init = end;
		end = data[Tnext];
		Tnext++;
		}else{
		init = end;
		}
	}
	void Next()
	{
		if(Tnext < NextPoint.Count)
		{
        init = end;
		end = NextPoint[Tnext];
		Tnext++;
		}else{
		init = end;
		}
	}
 void  spawnPoint()
{
   switch(Random.Range(1,5))
   {
	  case 1:
		//  point = 31;
		  init = 31;
		  end = 31;
       break;

	   case 2:
        //  point = 32;
		  init = 32;
		  end = 32;
       break;

	   case 3:
      //   point = 33;
		 init = 33;
		  end = 33;
       break;

	   case 4:
      //   point = 34;
		 init = 34;
		 end = 34;
       break;

	   case 5:
      //   point = 35;
		 init = 35;
		 end = 35;
       break;
   }

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
        NextPoint = path;
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
			//Debug.Log(path[i]);
		}
	}


}