using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexController : MonoBehaviour {

	float time;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

      time += Time.deltaTime;
	  OtherVortexIsNear();
	 
	  if(time >= 3f){
		  IsNeghtbor();
          DieVortex();
	  }
	}
	void Vortex(int Index)
	{
	 for(int i = 0;i< 35;i++)
	 {
		 if(DataList.m[i,Index] == 1)
		 {
            DataList.m[i,Index] = 5;
		 }
	 }
	}

	int IsNeghtbor()
	{
      int Point = 0;
	  for(int i = 0;i < DataList.conexiones.Length;i++)
	  {
        float Distancia = (transform.position - DataList.conexiones[i]).magnitude;

		if(Distancia < 2f)
		{
           Point =i;
		}
	  }
	  Vortex(Point);
	  return Point;
	}
	public void DieVortex()
	{
      Destroy(this);
	  Destroy(gameObject);
	}
    void OtherVortexIsNear()
	{
	    for(int i = 0; i < DataList.vortexs.Count;i++)
	    { 
				  GameObject vortex =	DataList.vortexs[i];               
		           if(vortex.transform.position != transform.position){
                   float distancia = (transform.position - vortex.transform.position).magnitude;
				   if(distancia <= 3 )
					 {
						 VortexController vortexTargeting = vortex.GetComponent<VortexController>(); 
						vortexTargeting.DieVortex();
						DataList.vortexs.RemoveAt(i);
					 }	 		  
	              	}
	     }		
	}	
}
