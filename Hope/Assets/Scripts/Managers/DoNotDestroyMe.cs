using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyMe : MonoBehaviour
{
    // Used to do not destroy specific GameObjects in between waves (certain managers) 
	private void Awake ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
}
