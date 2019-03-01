using System;
using Pool;
using TwlPhy;

[System.Serializable]
public class RoleData : PoolObject
{
    public long uid;
    public string nickName;
    public string resName;
    //实体位置
    public Vector2 pos = new Vector2(0, -3);
    //移动碰撞盒
    public Vector2 moveBox = new Vector2(3, 0.1f);
    //实体高度
    public float height = 0;
    public float speed = 3;
    public float jumpSpeed = 3;
    //缩放
    public Vector3 scale = new Vector3(1, 1, 1);
    public Role_Type roleType = Role_Type.None;
    //受击碰撞盒
    public Vector2 hitBox = new Vector2(3, 6);
    //面向
    public LookFlag lookFlag = LookFlag.Right;
}

