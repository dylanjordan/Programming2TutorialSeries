using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //Static reference to the player object
    static GameObject _player;

    public static bool _debug = true;

    public static GameObject GetPlayerObject()
    {
        //If we don't have player object ref
        if (_player == null)
        {
            //Find player object ref
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        //Return player object ref
        return _player;
    }

    public static void AddScoreToManager(int points)
    {
        //If we don't have player object ref
        if (_player == null)
        {
            //Get player object ref
            GetPlayerObject();
        }
        
        //Get score manager from player and add points
        _player.GetComponent<ScoreManager>().AddPoints(points);
    }

    public static void RemoveScoreFromManager(int points)
    {
        //If we don't have player object ref
        if (_player == null)
        {
            //Get player object ref
            GetPlayerObject();
        }

        //Get score manager from player and remove points
        _player.GetComponent<ScoreManager>().RemovePoints(points);
    }
}
