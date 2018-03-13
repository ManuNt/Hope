using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    
	public void Close()         // Since the controls screen is static, the only method used here is when the "Close" button is clicked
    {
        Destroy(gameObject);
    }
}
