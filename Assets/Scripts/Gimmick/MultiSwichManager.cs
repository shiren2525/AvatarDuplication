using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSwichManager : MonoBehaviour {

    public int Switches;
    public int PushedSwitches;
    

    public void AddPush(int push)
    {
        PushedSwitches += push;
        Debug.Log(PushedSwitches);
        if (Switches == PushedSwitches)
        {
            Debug.Log("Clear");
        }
    }
}
