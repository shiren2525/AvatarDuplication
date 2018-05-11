using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSwitch : MonoBehaviour {

    [SerializeField] GameObject MultiSwitchManager;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
           // MultiSwichManager.   
        }
        if (collision.gameObject.tag == "Avatar")
        {
            collision.gameObject.GetComponent<PlayerBase>().FlipMove();
        }
    }
}
