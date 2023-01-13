using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFactory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float spawnTime;
    [SerializeField] private PipePair.Data data;
    [Header("Compontns")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform destroyPoint;
    [Header("Prefab")]
    [SerializeField] private PipePair prefab;

    private Coroutine buildCoroutine;
    private List<PipePair> pipes = new List<PipePair>();


    public void Restart()
    {
        DestroyPrev();
        StartBuild();
    }
    public void StartBuild()
    {
        data.StartPositionX = spawnPoint.position.x;
        data.EndPositionX = destroyPoint.position.x;

        buildCoroutine = StartCoroutine(BuildCour());
    }
    private void DestroyPrev()
    {
        if (buildCoroutine != null)
        {
            StopCoroutine(buildCoroutine);
        }
        foreach (PipePair pair in pipes)
        {
            pair.OnEnd -= OnPipeDestroyed;
            Destroy(pair.gameObject);
        }
        pipes.Clear();
    }

    private IEnumerator BuildCour()
    {
        while(true)
        {
            BuildPipe();
            yield return new WaitForSeconds(spawnTime * Random.Range(0.75f, 1.25f));
        }
    }
    private void BuildPipe()
    {
        PipePair pair = Instantiate(prefab, transform);
        pair.Initilize(data);

        pipes.Add(pair);
        pair.OnEnd += OnPipeDestroyed;
    }

    private void OnPipeDestroyed(PipePair pipePair)
    {
        pipes.Remove(pipePair);
    }
}
