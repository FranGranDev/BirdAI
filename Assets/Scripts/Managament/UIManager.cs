using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;

    public void Initilize(GameController controller)
    {
        controller.OnPointUpdated += UpdatePoints;
    }

    private void UpdatePoints(int obj)
    {
        points.text = $"POINTS: {obj}";
    }
}
