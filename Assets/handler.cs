using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class handler : MonoBehaviour
{
    bool once;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject block;
    [SerializeField] GameObject futureblock;
    [SerializeField] GameObject future_platform;
    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.E)&&once==false) {
            gameObject.transform.Rotate(0, 0, -90, Space.Self);
            platform.transform.Rotate(0, 0, -25, Space.Self);
            block.transform.position= block.transform.position+ new Vector3(0,9,0);
          
            once = true;
            future_platform.transform.Rotate(0, 0, -25, Space.Self);
            futureblock.transform.localPosition = new Vector3(-145, -45, 0);

        }

    }
    
}
