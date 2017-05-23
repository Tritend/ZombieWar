using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackData : BaseData
{
    public Dictionary<int, KnapsackItemData> dict = null;

    public KnapsackData()
    {
        dict = new Dictionary<int, KnapsackItemData>();
    }

    public KnapsackData(Dictionary<int, KnapsackItemData> dt)
    {
        dict = new Dictionary<int, KnapsackItemData>();
        this.dict = dt;
    }

}
