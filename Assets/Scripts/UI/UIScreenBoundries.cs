using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenBoundries : MonoBehaviour
{
    public static UIScreenBoundries Instance;

    private Vector3[] screenBoundries = new Vector3[4];
    private void Awake()
    {
        Instance = this;

        GetComponent<RectTransform>().GetWorldCorners(screenBoundries);
    }
    public Vector3 GetBoundries()
    {
        return screenBoundries[2];
    }
}
