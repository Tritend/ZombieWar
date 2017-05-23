using UnityEngine.UI;

public class WeaponSysTips:BaseUI
{
    private Text name;
    private Text desc;
    public override void resetUIInfo()
    {
        uiEnum = UIEnum.weaponSysTips;
        this.uiNode = UINode.pop;
    }

    public override void onStart()
    {
        name = this.cacheTrans.Find("Tipsname").GetComponent<Text>();
        desc = this.cacheTrans.Find("Tipsdesc").GetComponent<Text>(); 

    }

    public override void refreshUI()
    {
        WeaponSysItemData dt = this.data as WeaponSysItemData;
        if (dt!=null)
        {
            name.text = dt.Name;
            desc.text = dt.Desc;
        }
    }
}
