using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;


public class BirdAI : Agent
{
    [SerializeField] private Bird bird;
    [SerializeField] private Sensor eyes;

    [SerializeField] private KeyCode heuristicKey = KeyCode.Mouse0;

    private void Awake()
    {
        bird.OnDie += OnDie;
        bird.OnPoint += OnPoint;
        bird.OnReset += OnReset;
    }

    private void OnPoint(Bird obj)
    {
        AddReward(1f);   
    }
    private void OnDie(Bird obj)
    {
        AddReward(-10f);       
    }

    private void OnReset(Bird obj)
    {
        OnEpisodeBegin();
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(bird.Velocity);

        foreach(Sensor.Eye eye in eyes)
        {
            sensor.AddObservation(eye.NormalizedDistance);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
        {
            bird.Jump();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;


        if (Input.GetKey(heuristicKey))
        {
            discreteActions[0] = 1;
        }
        else
        {
            discreteActions[0] = 0;
        }
    }

}
