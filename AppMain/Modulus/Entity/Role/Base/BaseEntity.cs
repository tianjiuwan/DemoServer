using TwlPhy;

/// <summary>
/// 实体基类
/// 碰撞不再使用Unity
/// </summary>
public class BaseEntity : IDispose
{
    public float speed = 3f;
    public float jumpHeight = 4f;

    private float height = 0f;
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            DyBox.UpdateHeight(height);
            HitBox.UpdateHeight(height);
        }
    }
    //玩家真实位置 有高度
    private Vector3 pos = Vector3.zero;
    public Vector3 Pos
    {
        get
        {
            pos.x = DyBox.centerX;
            pos.x = DyBox.centerY;
            pos.z = height;
            return pos;
        }
    }

    ////移动相关
    public Vector2 moveVec = Vector2.zero;
    public Vector2 moveDir = Vector2.zero;

    public LookFlag lookFlag = LookFlag.Right;
    //状态机
    protected FSM fsm;
    private FSM_Flag flag = FSM_Flag.None;
    public FSM_Flag Flag
    {
        get
        {
            return flag;
        }
        set
        {
            flag = value;
        }
    }

    private RoleData roleData = null;
    public RoleData RoleData
    {
        get
        {
            return roleData;
        }
        set
        {
            roleData = value;
            initData();
        }
    }
    //加载状态
    private E_EntityLoadState loadState = E_EntityLoadState.Waiting;
    public E_EntityLoadState LoadState
    {
        get
        {
            return this.loadState;
        }
    }

    public long UID
    {
        get
        {
            return this.roleData.uid;
        }
    }

    //移动碰撞盒
    protected DymicBox3D dyBox;
    public DymicBox3D DyBox
    {
        get
        {
            return dyBox;
        }
    }
    //受击碰撞盒
    private DymicBox3D hitBox;
    public DymicBox3D HitBox
    {
        get
        {
            return hitBox;
        }
    }

    //实体展示 包括mesh
    private RoleShowWidget showWidget;
    public RoleShowWidget ShowWidget
    {
        get
        {
            return showWidget;
        }
        set
        {
            showWidget = value;
        }
    }

    /// <summary>
    /// 接口 播放动画
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="loop"></param>
    /// <param name="add"></param>
    public float playAnim(string anim, bool loop = false, string add = null)
    {
        if (ShowWidget != null)
            return ShowWidget.playAnim(anim, loop, add);
        return 0;
    }

    /// <summary>
    /// 修改动画完成订阅
    /// </summary>
    /// <param name="isAdd"></param>
    public void modifyCompleteEvent(bool isAdd)
    {
        if (ShowWidget != null)
        {
            ShowWidget.modifyCompleteEvent(isAdd);
        }
    }

    /// <summary>
    /// 接口改变朝向
    /// </summary>
    /// <param name="flag"></param>
    public void changeLookFlag(LookFlag flag)
    {
        if (this.lookFlag == flag) return;
        this.lookFlag = flag;
        if (ShowWidget != null)
        {
            ShowWidget.changeLookFlag(flag);
        }
    }

    /// <summary>
    /// 接口 转换状态
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="args"></param>
    public void transFsm(FSM_Flag flag, FSMArgs args = null, bool forceTrans = false)
    {
        if (this.fsm != null)
        {
            bool isSuccess = this.fsm.transFsm(flag, args, forceTrans);
            if (isSuccess)
            {
                Flag = flag;
            }
        }
    }

    // Update is called once per frame
    public void Tick()
    {
        if (this.fsm != null)
        {
            this.fsm.tick();
        }
    }

    protected virtual void initFsm()
    {
        if (fsm == null)
        {
            fsm = new FSM(this);
            fsm.addState(FSM_Flag.Idle, new IdleState());
            fsm.addState(FSM_Flag.Run, new PhyRunState());
            fsm.addState(FSM_Flag.Jump, new JumpState());
            fsm.addState(FSM_Flag.Fall, new FallState());
            fsm.addState(FSM_Flag.Hit, new HitState());
            fsm.addState(FSM_Flag.StandHit, new StandHitState());
            fsm.addState(FSM_Flag.AirHit, new AirHitState());
        }
    }

    protected void doRun()
    {
        transFsm(FSM_Flag.Run);
    }

    public void updateRenderOrder()
    {
        if (this.ShowWidget != null)
        {
            ShowWidget.updateRenderOrder();
        }
    }

    /// <summary>
    /// 加载模型资源
    /// @"AssetBundle\Prefabs\model\role_superman\model\role_superman"
    /// </summary>
    public void loadModel()
    {
        if (this.ShowWidget != null)
        {
            ShowWidget.loadModel();
        }
    }

    protected virtual void initData()
    {
        //fsm
        initFsm();
        //刷新数据
        refresh();
        //其他初始化
        onStart();
    }

    /// <summary>
    /// 刷新实体函数
    /// 初始化和实体数据被编辑的时候 刷新整个实体
    /// </summary>
    public virtual void refresh()
    {
        //碰撞器位置和大小
        dyBox = new DymicBox3D(RoleData.pos.x, RoleData.pos.y, RoleData.moveBox.x, RoleData.moveBox.y, RoleData.height);
        hitBox = new DymicBox3D(RoleData.pos.x, RoleData.pos.y, RoleData.hitBox.x, RoleData.hitBox.y, RoleData.height);
        //高度
        this.Height = RoleData.height;
        //速度 跳跃速度
        this.speed = RoleData.speed;
        this.jumpHeight = RoleData.jumpSpeed;
        //默认状态
        transFsm(FSM_Flag.Idle);
        //客户读创建显示
        createDisplay();
    }

    private void createDisplay()
    {

    }

    protected virtual void onStart()
    {

    }

    public void doMove(Vector2 dir)
    {
        dyBox.AddCenter(dir.x, dir.y);
        hitBox.AddCenter(dir.x, dir.y);
        if (ShowWidget != null)
        {
            ShowWidget.doMove();
        }
    }

    /// <summary>
    /// 清理 回收
    /// </summary>
    public void onDispose()
    {
        if (ShowWidget != null)
        {
            ShowWidget.onDispose();
        }
        //回收data
        Pool.PoolMgr.Instance.recyleData(roleData);
        this.RoleData = null;
    }
}
