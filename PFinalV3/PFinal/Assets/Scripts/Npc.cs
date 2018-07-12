using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Npc : MonoBehaviour
{
	//List<Vector3> path = new List<Vector3>();
	//Array<int> conexiones = new Array<int>();
	Vector3[]conexiones = new Vector3[19];
    GameObject player ;
	Vector3  data= new Vector3();
	int init = 0;
	int end = 0;
	static int [] [] points = new int[] []{
		new int [] {1,2,3}, //0
		new int [] {0,4},//1
		new int [] {0,17,6},//2
		new int [] {0,17},//3
		new int [] {1,5},//4
		new int [] {1,4,6,8},//End
		new int [] {5,2,9},//6
		new int [] {8,9},//7
		new int [] {5,7},//8
		new int [] {7,6,10,11},//9
		new int [] {9,13},//10
		new int [] {12,9},//11
		new int [] {11,13},//12
		new int [] {10,12,14,15},//13
		new int [] {15,17,13},//14
		new int [] {13,14,18},//15
		new int [] {18,17},//16
		new int [] {3,2,16,14},//17
		new int [] {15,16},//18
	};
	public GameObject P0,P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16,P17,P18;
	float t = 0;
	void Start()
	{
        player = GameObject.Find("player");
		conexiones[0]=P0.transform.position;
		conexiones[1]=P1.transform.position;
		conexiones[2]=P2.transform.position;
		conexiones[3]=P3.transform.position;
		conexiones[4]=P4.transform.position;
		conexiones[5]=P5.transform.position;
		conexiones[6]=P6.transform.position;
		conexiones[7]=P7.transform.position;
		conexiones[8]=P8.transform.position;
		conexiones[9]=P9.transform.position;
		conexiones[10]=P10.transform.position;
		conexiones[11]=P11.transform.position;
		conexiones[12]=P12.transform.position;
		conexiones[13]=P13.transform.position;
		conexiones[14]=P14.transform.position;
		conexiones[15]=P15.transform.position;
		conexiones[16]=P16.transform.position;
		conexiones[17]=P17.transform.position;
		conexiones[18]=P18.transform.position;
	}
	void Update()
	{
          float distancia = (player.transform.position - this.transform.position).magnitude;
           // Debug.Log(distancia);
			if (distancia < 4) {
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		        mousePosition.z = 0;
		        transform.up = (mousePosition - transform.position);
			}else{
			 transform.up =(conexiones[end]- transform.position);
             transform.position = Vector3.Lerp(conexiones[init], conexiones[end], t);
              t += 0.01f;

		        if (t >= 1)
		        {   
		         	IsNear ();
		        	t = 0;
		      }
			}
	}
	void IsNear()
	{
		int aleatorio = 0;
		int[] vecinos = points[end];
		int LastPoint = 0;
		int Vlenght;
        Vlenght = vecinos.Length;
		LastPoint = Random.Range(0,Vlenght);
		aleatorio = vecinos[LastPoint];
		init = end;
		end = aleatorio;
	}
}