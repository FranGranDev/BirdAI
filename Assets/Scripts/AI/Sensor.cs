using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour, IEnumerable
{
    [SerializeField] private float height;
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private List<Eye> eyes;
    [SerializeField] private bool visualize;

    public List<Eye> Eyes => eyes;
    public IEnumerator GetEnumerator()
    {
        return eyes.GetEnumerator();
    }

    private void Awake()
    {
        foreach (Eye eye in eyes)
        {
            eye.Initilize(transform, obstacle, height);
        }
    }
    private void OnValidate()
    {
        foreach (Eye eye in eyes)
        {
            eye.Initilize(transform, obstacle, height);
        }
    }
    private void OnDrawGizmos()
    {
        if (!visualize)
            return;
        Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
        foreach(Eye eye in eyes)
        {
            Vector3 target = transform.position + eye.Direction * eye.Distance;
            Gizmos.DrawLine(transform.position, target);
        }
    }


    [System.Serializable]
    public class Eye
    {
        [SerializeField] private Transform direction;

        private float maxDst = 12;
        private LayerMask mask;
        private Transform origin;

        public void Initilize(Transform origin, LayerMask mask, float maxDst)
        {
            this.origin = origin;
            this.mask = mask;
            this.maxDst = maxDst;
        }

        public Vector3 Direction
        {
            get => origin.InverseTransformDirection(direction.right);
        }

        public float Distance
        {
            get
            {
                RaycastHit2D hit = Physics2D.Raycast(origin.position, Direction, maxDst, mask.value);
                if(hit.collider)
                {
                    return hit.distance;
                }
                return maxDst;
            }
        }
        public float NormalizedDistance
        {
            get
            {
                return Distance / maxDst;
            }
        }
    }
}
