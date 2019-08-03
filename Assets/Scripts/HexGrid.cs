using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public static readonly Color COLOR_1 = Color.red;
    public static readonly Color COLOR_2 = Color.blue;
    public static readonly Color COLOR_3 = Color.green;
    private readonly float vOffset = 0.88f;

    public GameObject Hex;
    public int[,] model;
    bool advancedMode;
    public int radius;
    private int realSize;

    public void ClickOnTile(int x, int y)
    {
        for (int i = -1; i < 2; ++i)
        {
            for (int j = -1; j < 2; ++j)
            {
                if(
                    x + i >= 0 &&
                    x + i < realSize &&
                    y + j >= 0 &&
                    y + j < realSize
                    )
                {
                    if (!((i == -1 && j == -1) || (i == 1 && j == 1)))
                        model[x + i, y + j] = Flip(model[x + i, y + j]);
                }
            }
        }
    }

    private int Flip(int cur)
    {
        switch(cur)
        {
            case 1:
                return 2;
            case 2:
                if (advancedMode)
                    return 3;
                else
                    return 1;
            case 3:
                return 1;
            default:
                return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        if (radius < 2) radius = 2;
        advancedMode = true;

        realSize = radius * 2 - 1;
        model = new int[realSize, realSize];
        Camera.main.orthographicSize = 1 + radius;

        GameObject bg = Instantiate(Hex, new Vector3(0,0,20), new Quaternion());
        bg.transform.localScale = new Vector3(radius*2 + 0.5f, radius * 2 + 0.5f, radius * 2 + 0.5f);
        bg.transform.Rotate(new Vector3(0f, 0f, 90f));
        bg.GetComponent<SpriteRenderer>().color = Color.black;
        bg.name = "background";
        Destroy(bg.GetComponent<Controller>());

        //Instantiate(Hex, new Vector3(), new Quaternion());
        //for (int i = 1; i < radius; ++i)
        //{
        //    Instantiate(Hex, new Vector3(-i, 0, 0), new Quaternion());
        //    Instantiate(Hex, new Vector3(i, 0, 0), new Quaternion());
        //    for (int j = 0; j <= i + 3; ++j)
        //    {
        //        Instantiate(Hex, new Vector3(-(i + 3) / 2f + j, (radius - i) * vOffset, 0), new Quaternion());
        //        Instantiate(Hex, new Vector3(-(i + 3) / 2f + j, -(radius - i) * vOffset, 0), new Quaternion());
        //    }
        //}

        for (int i = 0; i < radius; ++i)
        {
            if (i < radius-1)
            {
                for (int j = radius - 1 - i; j < realSize; ++j)
                {
                    model[i, j] = 1;
                }

                for (int j = 0; j < radius + i; ++j)
                {
                    model[realSize - 1 - i, j] = 1;
                }
            }
            else
            {
                for (int j = 0; j < realSize; ++j)
                {
                    model[i, j] = 1;
                }
            }
        }

        CreatePrefabs();
    }

    private void CreatePrefabs()
    {
        for (int i = 0; i < realSize; ++i)
        {
            for (int j = 0; j < realSize; ++j)
            {
                if(model[i, j] != 0)
                {
                    GameObject o = Instantiate(Hex, new Vector3(-((radius - 1) * 1.5f) + i * 0.5f + j, -(radius - i - 1) * vOffset, 0), new Quaternion());
                    Controller c = o.GetComponent<Controller>();
                    c.x = i;
                    c.y = j;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
