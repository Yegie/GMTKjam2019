using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public static readonly Color COLOR_1 = new Color32(0xf9, 0xe1, 0xe0, 0xff);
    public static readonly Color COLOR_2 = new Color32(0xfe, 0xad, 0xb9, 0xff);
    public static readonly Color COLOR_3 = new Color32(0xbc, 0x85, 0xa3, 0xff);
    public static readonly Color COLOR_BG = new Color32(0x89, 0xae, 0xb2, 0xff);
    public static readonly Color COLOR_FAIL = new Color32(0x00, 0x00, 0x00, 0xff);
    private readonly float vOffset = 0.88f;

    public GameObject Hex;
    public GameObject HexBg;
    public int[,] model;
    public bool active;
    public int radius;
    private bool advancedMode;
    private int level;
    private int minDiff;
    private int maxDiff;
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

        if (active)
        {
            CheckWin(true);
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

    private bool CheckWin(bool real)
    {
        bool won = false;
        int encountered = 0;

        for (int i = 0; i < realSize; ++i)
        {
            for (int j = 0; j < realSize; ++j)
            {
                if(!won && model[i,j] != 0)
                {
                    encountered = model[i, j];
                    won = true;
                } else if(won && (model[i, j] != encountered && model[i,j] != 0))
                {
                    won = false;
                    return won;
                }
            }
        }

        if (won && real)
            StartCoroutine(Reset(encountered));
        return won;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        minDiff = 1;
        maxDiff = 1;
        active = false;
        advancedMode = false;
        Generate();
    }

    public void Generate()
    {
        int baseCol;
        if (advancedMode)
            baseCol = Random.Range(1, 4);
        else
            baseCol = Random.Range(1, 3);

        if (radius < 3) radius = 3;

        realSize = radius * 2 - 1;
        model = new int[realSize, realSize];
        Camera.main.orthographicSize = 1 + radius;

        GameObject bg = Instantiate(HexBg, new Vector3(0, 0, 20), new Quaternion());
        bg.transform.localScale = new Vector3(radius * 2 + 0.5f, radius * 2 + 0.5f, radius * 2 + 0.5f);
        bg.transform.Rotate(new Vector3(0f, 0f, 90f));

        for (int i = 0; i < radius; ++i)
        {
            if (i < radius - 1)
            {
                for (int j = radius - 1 - i; j < realSize; ++j)
                {
                    model[i, j] = baseCol;
                }

                for (int j = 0; j < radius + i; ++j)
                {
                    model[realSize - 1 - i, j] = baseCol;
                }
            }
            else
            {
                for (int j = 0; j < realSize; ++j)
                {
                    model[i, j] = baseCol;
                }
            }
        }

        CreatePrefabs();

        Randomize(minDiff, maxDiff);

        active = true;
    }

    public IEnumerator Reset(int cur)
    {
        active = false;
        GameObject bg = GameObject.FindGameObjectWithTag("GameBackground");
        Background bgScript = bg.GetComponent<Background>();

        yield return new WaitForSeconds(0.25f);
        switch (cur)
        {
            case 1:
                bgScript.target = COLOR_1;
                break;
            case 2:
                bgScript.target = COLOR_2;
                break;
            case 3:
                bgScript.target = COLOR_3;
                break;
            default:
                bgScript.target = COLOR_FAIL;
                break;
        }
        for (int i = 0; i < realSize; ++i)
        {
            for (int j = 0; j < realSize; ++j)
            {
                model[i, j] = cur;
            }
        }
        yield return new WaitForSeconds(2.3f);
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("GameBoard");
        foreach(GameObject o in tiles)
        {
            Destroy(o);
        }
        bgScript.target = COLOR_BG;
        yield return new WaitForSeconds(1.55f);
        Destroy(bg);
        if (cur > -2)
        {
            ++level;
            if (level % 2 == 0)
            {
                minDiff += 1;
                maxDiff += 2;
            }
            if (level % 4 == 0)
            {
                ++radius;
                minDiff = (int)(minDiff * 1.2f);
                minDiff = (int)(maxDiff * 1.2f);
            }
            if (level == 14)
            {
                minDiff /= 3;
                maxDiff /= 3;
                radius -= 3;
                advancedMode = true;
            }
        }
        Generate();
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

    private void Randomize(int min, int max)
    {
        int goal = Random.Range(min, max);
        for(int i = 0; i < goal; ++i)
        {
            int x = Random.Range(0, realSize);
            int y = Random.Range(0, realSize);
            if (model[x, y] != 0)
            {
                ClickOnTile(x, y);
            } else
            {
                --i;
            }
        }
        if (CheckWin(false))
        {
            Randomize(min, max);
        }
    }

    void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset level");
            StartCoroutine(Reset(-2));
        }
        if (active && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Skipped level");
            StartCoroutine(Reset(-1));
        }
    }
}
