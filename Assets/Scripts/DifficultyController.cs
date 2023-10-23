using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private float timeForMaxDifficulty;
    private float passedTime;
    public float Difficulty { get; private set; }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime; // A dificuldade aumentará com o passar do tempo.
        Difficulty = passedTime / timeForMaxDifficulty;
        Difficulty = Mathf.Max(1, Difficulty); //Limitando dificuldade entre 0 e 1;
    }
}
