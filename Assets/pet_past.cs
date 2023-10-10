using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_past : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool alive=false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("test");
            alive = true;
        }
    }
}

