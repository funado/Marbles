using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    [SerializeField]
    private GameObject AnimalPrefab;
    private List<GameObject> animalPool = new List<GameObject>();

    [SerializeField]
    private int numAnimals;

    [SerializeField]
    private Vector2 animOffset;

    [SerializeField]
    private float animalSpawnTimer;

    //private int CurrentDirection = 0;

    private bool spawnAnimals = true;
    private float health;
    private float stamina;

    private void Awake()
    {
        //playerController = FindObjectOfType<PlayerController>();

        for (int i = 0; i < numAnimals; i++)
        {
            GameObject spawnedDuck = Instantiate(AnimalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            //spawnedDuck.GetComponent<ObjectDeactivate>().animalManager = this;
            spawnedDuck.SetActive(false);
            animalPool.Add(spawnedDuck);
        }

        StartCoroutine(spawnAnimalOnTimer());
    }

    private void Update()
    {
        spawnAnimal();
    }

    private void spawnAnimal()
    {
        Vector3 newAnimalOffset = new Vector3(0, 0, 0);

        if(numAnimals == 0)
        {
            newAnimalOffset = new Vector3(0, animOffset.x, animOffset.y);

        }
        if (numAnimals == 1)
        {
            newAnimalOffset = new Vector3(0, -animOffset.x, -animOffset.y);

        }
        if (numAnimals == 2)
        {
            newAnimalOffset = new Vector3(0, -animOffset.x, animOffset.y);

        }

        GameObject currentAnimal;
        currentAnimal = animalPool[0];
        animalPool.RemoveAt(0);
        animalPool.Add(currentAnimal);
        currentAnimal.SetActive(true);
        currentAnimal.transform.SetPositionAndRotation(transform.position + newAnimalOffset, Quaternion.Euler(-90, 0, 0));
    }

    private IEnumerator spawnAnimalOnTimer()
    {
        yield return new WaitForSeconds(animalSpawnTimer);

        if (spawnAnimals)
        {
            spawnAnimal();
            StartCoroutine(spawnAnimalOnTimer());
        }
    }

    //public void StopSpawning()
    //{
    //    playerController.HitByBomb();
    //    SpawnBombs = false;
    //}
}
