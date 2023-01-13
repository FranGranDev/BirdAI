using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PipeFactory pipeFactory;
    [SerializeField] private UIManager uiManager;

    private List<Bird> birds;

    private int points;


    public event System.Action<int> OnPointUpdated;


    private void Awake()
    {
        uiManager.Initilize(this);

        birds = new List<Bird>(GetComponentsInChildren<Bird>());
        foreach(Bird bird in birds)
        {
            bird.OnDie += OnBirdDie;
        }
        birds[0].OnPoint += OnBirdPoint;
    }
    private void Start()
    {
        pipeFactory.StartBuild();
    }

    private void OnBirdPoint(Bird obj)
    {
        points++;
        OnPointUpdated?.Invoke(points);
    }

    private void OnBirdDie(Bird obj)
    {
        foreach(Bird bird in birds)
        {
            bird.Restart();
        }
        pipeFactory.Restart();

        SaveResult();

        points = 0;
        OnPointUpdated?.Invoke(points);
    }

    private void SaveResult()
    {
        if(points > PlayerPrefs.GetInt("MaxPoint"))
        {
            PlayerPrefs.SetInt("MaxPoint", points);
            Debug.Log($"New Record: {points}");
        }
    }
}
