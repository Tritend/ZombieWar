using System;
using System.Collections.Generic;

public class SceneStartGameControl : BaseSceneControl
{

    public override void onStart()
    {
        Message msg = new Message(MsgCmd.Open_Main_Meun_UI, this);
        //EntityMainPlayer player = null; //EntityMgr.Instance.getEntityById(1008611) as EntityMainPlayer;
        //List<BaseEntity> playerLst = EntityMgr.Instance.getEntityByType(EntityType.player);
        //player = playerLst != null && playerLst.Count > 0 ? playerLst[0] as EntityMainPlayer : null;
        //if (player != null)
        //{
        //    msg["Pos"] = player.getCanvasPos();
        //    msg["Rot"] = player.getCanvasRot();
        //}
        msg.Send();
    }

}

