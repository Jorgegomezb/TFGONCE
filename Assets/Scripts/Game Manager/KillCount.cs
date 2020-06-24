using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public int count = 0;

    public void aumentar()
    {
        this.count++;
    }

    public int getCount()
    {
        print(count);
        return this.count;
    }
}
