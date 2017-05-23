using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class MsgCmd
{
    public const string Get_All_Goods = "Get_All_Goods";
    public const string On_Get_All_Goods = "On_Get_All_Goods";
    public const string On_Goods_Change = "On_Goods_Change";

    public const string Client_Use_Goods = "Client_Use_Goods";


    public const string Open_Knapsack_UI = "Open_Knapsack_UI";
    public const string Close_Knapsack_UI = "Close_Knapsack_UI";

    public const string On_Weather_Msg = "On_Weather_Msg";

    //武器系统UI
    public const string Open_WeaponSystem_UI = "Open_WeaponSystem_UI";
    public const string Close_WeaponSystem_UI = "Close_WeaponSystem_UI";
    public const string On_Buy_Weapon = "On_Buy_Weapon";
    public const string On_Change_Weapon = "On_Change_Weapon";

    //属性发生改变
    public const string On_Change_Value = "On_Change_Value";
    public const string On_BB_Change_Value = "On_BB_Change_Value";

    //血量变换 玩家
    public const string On_HP_Change_Value = "On_HP_Change_Value";

    //血量变化 水晶
    public const string On_Crystal_HP_Change = "On_Crystal_HP_Change";

    //场景加载完毕 抛出事件
    public const string On_Scene_Load_Finished = "On_Scene_Load_Finished";

    //MainUI 打开事件
    public const string Open_Main_Meun_UI = "Open_Main_Meun_UI";

    //玩家死亡
    public const string Die_Main_Player = "Die_Main_Player";
    //水晶死亡
    public const string Die_Crystal_Entity = "Die_Crystal_Entity";
    //怪物死亡
    public const string Die_Monster_Entity = "Die_Monster_Entity";
}

