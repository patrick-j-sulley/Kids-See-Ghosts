using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class DataServiceManager : MonoBehaviour {

    public static DataService ds;

	void Start () {

        if (DataServiceManager.ds == null)
        {
            DataServiceManager.ds = new DataService("KSG_DB.db");
        }
		
	}
}
