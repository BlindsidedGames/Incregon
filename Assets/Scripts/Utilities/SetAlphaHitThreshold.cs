using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAlphaHitThreshold : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Debug.Log($"Hi I was clicked? {transform.position}"));
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
}