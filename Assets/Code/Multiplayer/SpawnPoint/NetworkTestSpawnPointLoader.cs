using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTestSpawnPointLoader : ISpawnPointLoader
{
    private int[] _owners = { 0,0,0,0,0,1,1,1,1,1 };
    private int[] _siblings = { 5,6,7,8,9,0,1,2,3,4};
    private Vector3[] _position = {
        // Player ones spawns
        new Vector3(-3.0f,-3.0f),new Vector3(-1.5f,-3.0f),new Vector3(0.0f,-3.0f), new Vector3(1.5f,-3.0f),new Vector3(3.0f,-3.0f),
        //Player Twos spawns
        new Vector3(-3.0f,3.0f), new Vector3(-1.5f,3.0f), new Vector3(0.0f, 3.0f),  new Vector3(1.5f,3.0f), new Vector3(3.0f,3.0f)};

    private int _currentIndex = -1;

    public int GetOwner()
    {
        return _owners[_currentIndex];
    }

    public Vector3 GetPosition()
    {
        return _position[_currentIndex];
    }

    public int GetSibilingId()
    {
        return _siblings[_currentIndex];
    }

    public bool HasAnotherSpawnPoint()
    {
        return _currentIndex < _owners.Length-1; 
    }

    public void LoadNextSpawnPoint()
    {
        _currentIndex++;
    }
}
