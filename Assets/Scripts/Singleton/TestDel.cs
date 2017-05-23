using System;
using System.Collections.Generic;
using UnityEngine;

public class TestDel : MonoBehaviour
{
    private void Start()
    {
        foo1((ff, str) => { Debug.LogWarning("foo1 --> debug " + ff + str); });
        foo2((str) =>
        {
            Debug.Log("foo2  debug->" + str);
            return 0.1f;
        });
    }

    private void foo1(Action<float, string> call)
    {
        call(0.2f, "coco");
    }

    private void foo2(Func<string, float> call)
    {
        float ff = call("coco");
    }

    private float func()
    {
        //
        //
        return 0.1f;
    }
}

