using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

public class FakeSpawnPointLoader : ISpawnPointLoader
{
    public int[] ownerIds = { 0, 1, 0, 1 };

    public int[] siblings = { 3, 2, 1, 0 };

    public Vector3[] position = { new Vector3( -3.0f, -3.0f, 0.0f ), 
                                  new Vector3(-3.0f, 3.0f, 0.0f), 
                                  new Vector3(3.0f, -3.0f, 0.0f), 
                                  new Vector3(3.0f, 3.0f, 0.0f) };

    private int currentIndex = -1;

    public int GetOwner()
    {
        return ownerIds[currentIndex];
    }

    public Vector3 GetPosition()
    {
        return position[currentIndex];
    }

    public int GetSibilingId()
    {
        return siblings[currentIndex];
    }

    public bool HasAnotherSpawnPoint()
    {
        return currentIndex < ownerIds.Length-1;
    }

    public void LoadNextSpawnPoint()
    {
        currentIndex++;
    }
}

public class BaseSpawnPointSpawnerTest{

    private BaseSpawnPointSpawner _baseSpawnPointSpawner;

    private FakeSpawnPointLoader _fakeSpawnPointLoader;

    [SetUp]
    public void Setup()
    {
        _baseSpawnPointSpawner = new BaseSpawnPointSpawner();
        _fakeSpawnPointLoader = new FakeSpawnPointLoader();
        _baseSpawnPointSpawner.SetSpawnPointLoader(_fakeSpawnPointLoader as ISpawnPointLoader);
    }

    [TearDown]
    public void Teardown()
    {
        _baseSpawnPointSpawner = null;
        _fakeSpawnPointLoader = null;
    }

    [Test]
    public void InializesSpawnPointsProperly()
    {
        _baseSpawnPointSpawner.LoadSpawnPoints();

        List<SpawnPoint> spawnPoints = _baseSpawnPointSpawner.GetSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Assert.AreEqual(_fakeSpawnPointLoader.ownerIds[i], spawnPoints[i].GetOwnerId());
            Assert.AreEqual(_fakeSpawnPointLoader.siblings[i], spawnPoints[i].GetSibling());
            Assert.AreEqual(_fakeSpawnPointLoader.position[i], spawnPoints[i].GetPosition());
        }
    }


}
