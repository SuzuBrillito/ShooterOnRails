using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodging : MonoBehaviour
{
    public PlayerMovement player;

    public void isDodgingOn()
    {
        if (player == null)
        {
            player = GetComponent<PlayerMovement>();
        }
        player.isDodging = true;
    }

    public void isDodgingOff()
    {
        player.isDodging = false;
    }
}
