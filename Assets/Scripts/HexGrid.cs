using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject Hex;
    public int[,] model;
    public int radius;
    private readonly float vOffset = 0.88f;

    // Start is called before the first frame update
    void Start()
    {
        model = new int[radius * 2 - 1, radius * 2 - 1];

        GameObject bg = Instantiate(Hex, new Vector3(0,0,20), new Quaternion());
        bg.transform.localScale = new Vector3(radius*2 + 0.5f, radius * 2 + 0.5f, radius * 2 + 0.5f);
        bg.transform.Rotate(new Vector3(0f, 0f, 90f));
        bg.GetComponent<SpriteRenderer>().color = Color.black;
        bg.name = "background";
        Destroy(bg.GetComponent<Controller>());


        Instantiate(Hex, new Vector3(), new Quaternion());
        if (radius < 2) radius = 2;
        for(int i = 1; i < radius; ++i)
        {
            Instantiate(Hex, new Vector3(-i, 0, 0), new Quaternion());
            Instantiate(Hex, new Vector3(i, 0, 0), new Quaternion());
            for(int j = 0; j <= i + 3; ++j)
            {
                Instantiate(Hex, new Vector3(-(i + 3) / 2f + j, (radius - i) * vOffset, 0), new Quaternion());
                Instantiate(Hex, new Vector3(-(i + 3) / 2f + j, -(radius - i) * vOffset, 0), new Quaternion());
            }
        }

        for(int i = 0; i <= radius; ++i)
        {
            for(int j = 4-i; j < radius * 2 - 1; ++j)
            {
                if (i != radius)
                {
                    model[0, j] = 1;
                    model[radius - i - 1, j - 4 + i] = 1;
                } else
                {
                    model[i, j] = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
