using System;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

//只有contorl才能发消息给服务器  UI那边不能这样做(虽然你可以这么做)
public class KnapsackControl : BaseControl
{
    private bool isInit = false;
    private Dictionary<int, KnapsackItemData> dictGoods = new Dictionary<int, KnapsackItemData>();

    private bool isOpen = false;
    public override void initEnum()
    {
        this.uiEnum = UIEnum.knapsack;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_Knapsack_UI, onOpenUI);
        MessageCenter.Instance.addListener(MsgCmd.Close_Knapsack_UI, onCloseUI);
        //监听服务器下发的 背包数据列表List<ServerGoodsData>
        MessageCenter.Instance.addListener(MsgCmd.Get_All_Goods, onGetAllGoodsMsg);
        //客户端使用物品消息
        MessageCenter.Instance.addListener(MsgCmd.Client_Use_Goods, onUseGoodsMsg);
    }

    private void onOpenUI(Message msg)
    {
        if (isInit)
        {
            if (!isOpen)
            {
                UIMgr.Instance.openUI(this.uiEnum, new KnapsackData(this.dictGoods));
                isOpen = true;
            }
            else
            {
                UIMgr.Instance.closeUI(this.uiEnum);
                isOpen = false;
            }
        }
        else
        {
            MessageCenter.Instance.SendMessage(MsgCmd.On_Get_All_Goods, this);
        }
    }
    private void onCloseUI(Message msg)
    {

    }

    //客户端接收到使用物品消息
    private void onUseGoodsMsg(Message msg)
    {
        int tempId = (int)msg["tempId"];
        int count = (int)msg["count"];
        Message netMsg = new Message(MsgCmd.On_Goods_Change, this);
        netMsg["tempId"] = tempId;
        netMsg["count"] = count;
        netMsg.Send();
    }

    //当客户端接收到服务器消息
    //1001 10
    //1002 
    private void onGetAllGoodsMsg(Message msg)
    {
        List<ServerGoodsData> lstGoods = new List<ServerGoodsData>(msg["lstGoods"] as List<ServerGoodsData>);
        for (int i = 0; i < lstGoods.Count; i++)
        {
            int tempId = lstGoods[i].TempId;
            if (!dictGoods.ContainsKey(tempId))
            {
                ItemConfigData config = getGoodsConfig(tempId);
                KnapsackItemData dt = new KnapsackItemData();
                //客户端配置表数据 
                dt.TempId = config.tempId;
                dt.Name = config.name;
                dt.Type = config.type;
                dt.SonType = config.sonType;
                dt.Desc = config.desc;
                //服务器数据 count 
                dt.Count = lstGoods[i].Count;
                dictGoods.Add(tempId, dt);
            }
            else
            {
                dictGoods[tempId].Count = lstGoods[i].Count;
            }
        }
        isInit = true;
        List<KnapsackItemData> lst = new List<KnapsackItemData>(dictGoods.Values);
        lst.Sort((m, n) => { return m.Type < n.Type ? -1 : m.Type == n.Type && m.SonType < n.SonType ? -1 : 1; });
        Dictionary<int, KnapsackItemData> dict = new Dictionary<int, KnapsackItemData>();
        for (int i = 0; i < lst.Count; i++)
        {
            dict.Add(lst[i].TempId, lst[i]);
        }
        updateUI(new KnapsackData(dict));
    }

    //根据模板id获取物品config
    private ItemConfigData getGoodsConfig(int tempId)
    {
        ItemConfigData config = null;
        List<ItemConfigData> lst = GameData.ItemConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].tempId == tempId)
            {
                config = lst[i];
            }
        }
        return config;
    }

    //弃用 应该使用服务器与客户端静态数据结合的数据
    private KnapsackData getData()
    {
        KnapsackData data = new KnapsackData();
        List<ItemConfigData> lst = GameData.ItemConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            KnapsackItemData dt = new KnapsackItemData();
            dt.TempId = lst[i].tempId;
            dt.Name = lst[i].name;
            dt.Type = lst[i].type;
            dt.SonType = lst[i].sonType;
            dt.Count = lst[i].count;
            dt.Desc = lst[i].desc;
            //data.lst.Add(dt);
        }
        return data;
    }


}

