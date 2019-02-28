using System;
using System.Collections.Generic;

public class RoleShowWidget
{

    public void setAgent(BaseEntity agent)
    {

    }

    /// <summary>
    /// 接口 播放动画
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="loop"></param>
    /// <param name="add"></param>
    public float playAnim(string anim, bool loop = false, string add = null)
    {
        return 0;
    }


    /// <summary>
    /// 修改动画完成订阅
    /// </summary>
    /// <param name="isAdd"></param>
    public void modifyCompleteEvent(bool isAdd)
    {

    }



    //设置渲染层
    private int maxOrder = 1000;
    private int magnifyOrder = 100;//放大倍率
    public void updateRenderOrder()
    {

    }

    /// <summary>
    /// 加载模型资源
    /// @"AssetBundle\Prefabs\model\role_superman\model\role_superman"
    /// </summary>
    public void loadModel()
    {

    }



    public void changeLookFlag(LookFlag flag)
    {

    }



    /// <summary>
    /// 实体表现刷新
    /// 1 model
    /// </summary>
    public virtual void refresh()
    {

    }

    public void doMove()
    {

    }

    public void onDispose()
    {

    }

    public void drawAttack(AttackData atkData)
    {

    }


}

