using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePair : MonoBehaviour
{
    [Header("Compontns")]
    [SerializeField] private Transform topPipe;
    [SerializeField] private Transform bottomPipe;

    private Data data;


    public event System.Action<PipePair> OnEnd;


    public void Initilize(Data data)
    {
        this.data = data;

        transform.position = new Vector2(data.StartPositionX, 0);

        float center = Random.Range(-data.CenterOffset, data.CenterOffset);


        float topPipeHeight = center + Random.Range(data.MinPipeOffset, data.MaxPipeOffset);
        float bottomPipeHeight = center - Random.Range(data.MinPipeOffset, data.MaxPipeOffset);

        topPipe.localPosition = new Vector2(0, topPipeHeight);
        bottomPipe.localPosition = new Vector2(0, bottomPipeHeight);
    }

    private void Move()
    {
        transform.Translate(-data.Speed * Time.fixedDeltaTime, 0, 0);
    }
    private void CheckEnd()
    {
        if (transform.position.x < data.EndPositionX)
        {
            OnEnd?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        CheckEnd();
        Move();
    }

    
    [System.Serializable]
    public class Data
    {
        [HideInInspector] public float StartPositionX;
        [HideInInspector] public float EndPositionX;

        public float CenterOffset;
        public float MinPipeOffset;
        public float MaxPipeOffset;
        public float Speed;
    }
}
