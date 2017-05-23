using System;
using System.Collections.Generic;

public enum BType
{
    none = -1,
    money,
    energy,
    score,
}

public class BlackBoard
{
    private Dictionary<BType, int> dictVals = null;

    public BlackBoard()
    {
        dictVals = new Dictionary<BType, int>();
        dictVals.Add(BType.money, 0);
        dictVals.Add(BType.energy, 0);
        dictVals.Add(BType.score, 0);
    }

    public void onChangeValue(BType btype, int val)
    {
        if (dictVals.ContainsKey(btype))
        {
            dictVals[btype] = dictVals[btype] += val;
            Message msg = new Message(MsgCmd.On_BB_Change_Value, this);
            msg["type"] = btype;
            msg["val"] = dictVals[btype];
            msg.Send();
        }
    }

    public int getValue(BType btype)
    {
        if (dictVals.ContainsKey(btype))
        {
            return dictVals[btype];
        }
        return 0;
    }
}

