using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateItems : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if()
    }
}
