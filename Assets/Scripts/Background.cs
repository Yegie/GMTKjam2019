using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private readonly float rate = 0.34f;
    private readonly float snapVal = 0.015f;
    private SpriteRenderer sr;

    public Color target;
    // Start is called before the first frame update
    void Start()
    {
        target = HexGrid.COLOR_BG;
        sr = GetComponent<SpriteRenderer>();
        sr.color = target;
    }

    // Update is called once per frame
    void Update()
    {
        Controller.ChangeColor(sr, target, rate, snapVal);
    }
}
