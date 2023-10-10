using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "WeakPoint")
        {
            Debug.Log("inside function-> enemy hit");
            Destroy(collision.collider.gameObject);

        }

        Debug.Log(collision.gameObject.name);
    }

}