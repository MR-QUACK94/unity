using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    public TMP_Text text;
    public float num;

    private void Start()
    {
        ChangeNum();
    }

    public void ChangeNum()
    {
        num = Random.Range(0.0f, 100.0f);
    }

    private void Update()
    {
        text.text = num.ToString();
    }
}
