using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Used as a token script

    private void OnTriggerEnter(Collider aCol)
    {
        if (aCol.tag == "Player")
        {
            if (gameObject.tag == "Ammo")
            {
                AmmoManager.Instance.ReloadAmmo(gameObject);
            }
            else if (gameObject.tag == "Health")
            {
                AmmoManager.Instance.RestoreHealth(gameObject);
            }
        }
    }
}
