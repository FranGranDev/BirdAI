using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Types type;

    public Types Type => type;

    public enum Types { Obstacle, Ground, Point}
}
