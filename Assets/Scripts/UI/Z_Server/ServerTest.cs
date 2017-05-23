using System;
using System.Collections.Generic;

public class ServerGoodsData
{
    public int TempId;
    public int Count;
    public ServerGoodsData(int id, int count)
    {
        TempId = id;
        Count = count;
    }
}

public class ServerTest : Singleton<ServerTest>
{
    //物品数量  模拟服务器 key=tempId val=count
    private List<ServerGoodsData> lstGoods = null;

    public override void init()
    {
        lstGoods = new List<ServerGoodsData>();
        lstGoods.Add(new ServerGoodsData(1001, 78));
        lstGoods.Add(new ServerGoodsData(1003, 98));
        lstGoods.Add(new ServerGoodsData(1004, 44));
        lstGoods.Add(new ServerGoodsData(1005, 32));
        lstGoods.Add(new ServerGoodsData(1007, 232));
        lstGoods.Add(new ServerGoodsData(1008, 54));
        lstGoods.Add(new ServerGoodsData(1009, 7));
        lstGoods.Add(new ServerGoodsData(1010, 4));
        lstGoods.Add(new ServerGoodsData(1011, 3));
        lstGoods.Add(new ServerGoodsData(1012, 90));
        lstGoods.Add(new ServerGoodsData(1013, 32));
        lstGoods.Add(new ServerGoodsData(1014, 54));
        lstGoods.Add(new ServerGoodsData(1015, 31));
        lstGoods.Add(new ServerGoodsData(1019, 88));
        lstGoods.Add(new ServerGoodsData(1020, 99));
    }

    public void initServer()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_Get_All_Goods, OnClientGetAllGoods);
        MessageCenter.Instance.addListener(MsgCmd.On_Goods_Change, onUseGoods);
    }


    //客户端请求所有背包数据
    private void OnClientGetAllGoods(Message msg)
    {
        sendGoodsMsg();
    }
    private void sendGoodsMsg()
    {
        Message goodMsg = new Message(MsgCmd.Get_All_Goods, this);
        goodMsg["lstGoods"] = lstGoods;
        goodMsg.Send();
    }

    //使用物品
    private void onUseGoods(Message msg)
    {
        int tempId = (int)msg["tempId"];
        int count = (int)msg["count"];
        for (int i = 0; i < lstGoods.Count; i++)
        {
            if (lstGoods[i].TempId == tempId)
            {
                lstGoods[i].Count -= count;
            }
        }
        sendGoodsMsg();
    }




}

