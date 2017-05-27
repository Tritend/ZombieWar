using System;
using System.Collections.Generic;
using UnityEngine;

public class TestDel : MonoBehaviour
{
    public Transform target;
    public float angle = 0;


    private void Update()
    {
        //Quaternion rot = Quaternion.Euler(angle, 0, 0) * this.transform.rotation;
        //target.transform.position = rot * this.transform.position;

        //Debug.DrawRay(this.transform.position, this.transform.forward, Color.red, 2f);

        //Vector3 dir = target.position - this.transform.position;
        //Debug.DrawRay(this.transform.position, dir, Color.blue, 2f);
        //Debug.DrawRay(this.transform.position, dir * Mathf.Cos(0.3f), Color.red, 2f);
    }



    //private void Start()
    //{
    //    foo1((ff, str) => { Debug.LogWarning("foo1 --> debug " + ff + str); });
    //    foo2((str) =>
    //    {
    //        Debug.Log("foo2  debug->" + str);
    //        return 0.1f;
    //    });
    //}

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

