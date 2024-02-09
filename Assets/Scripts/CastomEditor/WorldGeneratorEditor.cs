using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CastomEditor
{
    public class WorldGeneratorTools
    {
        private static Transform _asteroids;
        private static Transform _planets;
        
        private static List<GameObject> _asteroidsPrefabs = new ();
        private static List<GameObject> _planetsPrefabs = new ();
        
        [MenuItem("WorldGeneratorTools/Generate")]
        public static void GenerateAll()
        {
            Generate();
        }

        [MenuItem("WorldGeneratorTools/Delete")]
        public static void DeleteAll()
        {
            Delete();
        }

        private static void Generate()
        {
            if (CheckInit())
                SetInit();

            var randomPoints = PoissonDiscSampling.GeneratePoints(20, new Vector2(2000,2000), 2000);
            List<Vector2> offsets = new List<Vector2>();
            float hw = 2000 / 2f;
            for (int n = 0; n < randomPoints.Count; n++)
	            offsets.Add(new Vector2(randomPoints[n].x - hw, randomPoints[n].y - hw));
            Debug.Log(randomPoints.Count);

            var i = 0;
            foreach (var randomPoint in offsets)
            {
	            if (i <= 7)
	            {
		            InstantiateRandom(_asteroidsPrefabs, randomPoint, _asteroids);
	            }
	            else
	            {
		            InstantiateRandom(_planetsPrefabs, randomPoint, _planets);
	            }
	            
	            if(i > 10)
	            {
		            i = 0;
	            }
	            i++;
            }
        }
        private static void Delete()
        {
	        if (CheckInit())
		        SetInit();

	        for (int i = 0; i < 20; i++)
	        {
		        foreach (Transform child in _asteroids)
			        Object.DestroyImmediate(child.gameObject);

		        foreach (Transform child in _planets)
			        Object.DestroyImmediate(child.gameObject);
	        }
        }

        private static bool CheckInit()
        {
	        return _asteroids == null || _planets == null || _asteroidsPrefabs == null || _planetsPrefabs == null;
        }

        private static void SetInit()
        {
            var world = GameObject.Find("World").transform;
            _planets = world.Find("Planets");
            _asteroids = world.Find("Asteroids");
            
            foreach (var prefab in Resources.LoadAll<GameObject>("Imports/#OnePotatoKingdom_FULL/Prefabs"))
            {
	            _planetsPrefabs.Add(prefab);
            }
            foreach (var prefab in Resources.LoadAll<GameObject>("Imports/Asteroids Pack/Assets/Prefabs"))
            {
	            _asteroidsPrefabs.Add(prefab);
            }
            Debug.Log("Has _planetsPrefabs: " + _planetsPrefabs.Count);
            Debug.Log("Has _asteroidsPrefabs: " + _asteroidsPrefabs.Count);
        }

        private static void InstantiateRandom(List<GameObject> prefabs, Vector2 spawnPoint, Transform parent)
        {
	        System.Random random = new System.Random();
	        var index = random.Next(0, prefabs.Count);
	        
	        var newSpawnPoint = new Vector3(spawnPoint.x, 0, spawnPoint.y);
	        Object.Instantiate(prefabs[index], newSpawnPoint, GivRandomRotate(), parent);
        }

        private static Quaternion GivRandomRotate()
        {
	        System.Random random = new System.Random();
	        var x = random.Next(0, 360);
	        var y = random.Next(0, 360);
	        var z = random.Next(0, 360);
	        return Quaternion.Euler(new Vector3(x, y, z));
        }
    }
}
