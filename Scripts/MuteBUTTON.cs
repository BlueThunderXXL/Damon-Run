using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class MuteBUTTON : MonoBehaviour
{
    public bool CanMute;

    void Start()
    {
        CanMute = true;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(7,160,50,50),"Mute"))
        {
            if (CanMute)
            {
                AudioListener.pause = true;
                CanMute = false;
            }
            else
            {
                AudioListener.pause = false;
                CanMute = true;
            }        
        }    
    }
}