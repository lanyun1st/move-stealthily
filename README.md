# Move Stealthily · Unity 潜行战斗原型

一个围绕**移动噪声、敌人感知与战斗选择**制作的 Unity 3D 游戏原型。玩家可以缓步潜行，也可以切换近战与远程攻击；普通移动和攻击会产生噪声，近距离的声音会让已激活的敌人进入追击。

项目重点展示 C# 玩法编程：声音范围广播、NavMesh 寻路、接口化攻击、子弹与炸弹对象池，以及房间、交互和 UI 的协作。

## 演示与源码

- [查看演示视频](https://github.com/lanyun1st/move-stealthily/blob/main/move%20stealthily%20-%20MainMenu%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.53f1c1_%20_DX11_%202026-04-27%2010-09-11.mp4)
- [下载演示视频 MP4](https://raw.githubusercontent.com/lanyun1st/move-stealthily/main/move%20stealthily%20-%20MainMenu%20-%20Windows%2C%20Mac%2C%20Linux%20-%20Unity%202022.3.53f1c1_%20_DX11_%202026-04-27%2010-09-11.mp4)
- [下载项目源码 ZIP](https://github.com/lanyun1st/move-stealthily/archive/refs/heads/main.zip)

视频较大，GitHub 页面不一定支持直接预览，可下载后播放。目前仓库没有发布独立可执行游戏包；源码 ZIP 需要通过 Unity 打开，不能直接作为游戏运行。

## 玩法内容

- **潜行与噪声**：按住左 Shift 降低移动速度，并停止脚步噪声广播；近战和远程攻击分别配置声音范围。
- **敌人感知**：结合距离、视野角度和射线检测识别玩家；使用 NavMeshAgent 巡逻、追击，并在接近玩家时尝试攻击。
- **攻击切换**：通过统一的 `IAttack` 接口调用近战和远程攻击，武器栏显示当前武器与冷却。
- **战斗反馈**：包含生命值、受击闪烁、镜头震动、死亡界面和最大生命值提升道具。
- **关卡与交互**：提供主菜单、两关构建配置、开门交互、关间提示和暂停功能；另有 Boss 激活、移动、连续射击与投弹逻辑。

## 操作

| 输入 | 功能 |
| --- | --- |
| WASD / 方向键 | 移动，使用 Unity 的 Horizontal / Vertical 输入轴 |
| 按住左 Shift | 潜行慢走，不产生脚步噪声 |
| 鼠标移动 | 控制角色朝向 |
| 鼠标左键 | 使用当前攻击，每次点击触发一次 |
| 数字键 1 / 2 | 切换近战 / 远程武器 |
| E | 在交互范围内开门或打开关间面板 |
| Esc | 暂停 / 恢复 |

## 如何运行

1. 下载源码并完整解压，或运行 `git clone https://github.com/lanyun1st/move-stealthily.git`。
2. 安装 **Unity 2022.3.53f1c1**，这是 [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) 记录的项目版本。优先使用相同版本，其他版本的兼容性需要另行验证。
3. 在 Unity Hub 中添加包含 `Assets`、`Packages`、`ProjectSettings` 的项目根目录。
4. 等待素材导入和包解析完成，打开 `Assets/Scenes/MainMenu.unity`，点击 Play，再从菜单进入关卡。

首次运行需要可用的 Unity 许可证，依赖解析可能需要网络。不要只下载 Scripts 文件夹：场景、预制体、素材及 Inspector 引用也是工程的一部分。

构建列表已启用 `MainMenu`、`Level1`、`Level2`。若要自行导出 Windows 游戏，在安装对应 Build Support 后，从 Unity 的 Build Settings 选择目标平台构建；运行时保留生成的 EXE 和全部依赖文件。

## 技术点与代码入口

| 技术点 | 当前实现 | 代码入口 |
| --- | --- | --- |
| 角色控制 | CharacterController 移动、重力、击退；鼠标射线与平面求交控制朝向 | [PlayerController.cs](Assets/Scripts/Player/PlayerController.cs) |
| 声音范围广播 | OverlapSphere 查找范围内敌人，按内外半径传递警报等级 | [NoiseSystem.cs](Assets/Scripts/Player/NoiseSystem.cs) |
| 敌人感知与寻路 | 距离、视野角、射线遮挡检测；巡逻点与追击计时器驱动 NavMeshAgent | [EnemyAI.cs](Assets/Scripts/Enemy/EnemyAI.cs) |
| 接口与多态 | IAttack 统一执行和冷却接口，攻击管理器负责输入与武器选择 | [IAttack.cs](Assets/Scripts/Attack/IAttack.cs)、[CharacterAttackManager.cs](Assets/Scripts/Attack/CharacterAttackManager.cs) |
| 对象池 | 按字符串 Key 管理 Queue，预热、取出、回收，池空时创建新实例 | [PoolManager.cs](Assets/Scripts/PoolManager.cs) |
| 池化对象生命周期 | 子弹启用时重置状态，命中或超时回收；炸弹爆炸后回收 | [Projectile.cs](Assets/Scripts/Scene/Projectile.cs)、[Bomb.cs](Assets/Scripts/Attack/EnemyAttack/Bomb.cs) |
| 事件与表现反馈 | Health 发布生命值变化和死亡事件，HitFeedback 订阅变化并执行闪烁、震动 | [Health.cs](Assets/Scripts/Player/Health.cs)、[HitFeedback.cs](Assets/Scripts/Player/HitFeedback.cs) |
| 房间与敌人配置 | 每个 Room 保存敌人类型、数量和巡逻点，由生成器创建实例 | [Room.cs](Assets/Scripts/Scene/Room.cs)、[EnemySpawner.cs](Assets/Scripts/Enemy/EnemySpawner.cs) |
| Boss 行为时序 | 激活后移动，在持续射击和投弹之间按计时器切换 | [BossController.cs](Assets/Scripts/Enemy/BossController.cs) |
| UI 与场景流程 | UGUI 菜单、武器栏、暂停与场景切换；读取 PlayerPrefs 关卡解锁标记 | [UI 目录](Assets/Scripts/UI) |

主要依赖：URP **14.0.11**、AI Navigation **1.1.7**、UGUI **1.0.0**、TextMesh Pro **3.0.7**。完整依赖以 [manifest.json](Packages/manifest.json) 为准。

## 建议阅读顺序

1. **移动与声音**：从 `PlayerController.HandleFootstepNoise` 跟到 `NoiseSystem.MakeNoise`，再看 `EnemyAI.HearNoise`，理解玩家行为如何影响敌人。
2. **攻击与复用**：从 `CharacterAttackManager` 跟到 `IAttack` 的具体实现，再看 `PoolManager` 和 `Projectile` 的取出、初始化、命中、回收过程。
3. **生命与反馈**：阅读 `Health.TakeDamage` 和 `HitFeedback` 的事件订阅，区分数值更新与视觉反馈。
4. **关卡流程**：阅读 `Room`、`EnemySpawner`、`ExitDoor` 和 UI 脚本，了解场景配置与运行时逻辑的连接方式。

## 当前范围与后续改进

这是用于学习和展示的玩法原型，不把预留接口等同于完整功能：

- 声音系统已有内外圈参数，但当前 `HearNoise` 仅处理内圈警报；外圈声音位置调查尚未实现，也没有声音穿墙衰减模拟。
- 敌人行为由条件判断与计时器组织，尚未拆成独立的有限状态机或行为树；适合后续扩展巡逻、调查、追击、返回等状态。
- 子弹与炸弹已接入对象池，声音和挥砍特效仍使用 Instantiate / Destroy；当前没有性能基准数据，不宣称全部对象池化或零 GC。
- `Health` 已提供事件，但仍直接持有部分 UI 引用；开门逻辑目前激活场景中的所有 EnemyAI，可进一步细化房间边界与职责。
- 菜单代码预留了第三关入口，但当前构建列表只包含两关，不代表已交付三关。

本 README 根据仓库源码和配置整理；此次文档更新未启动 Unity、执行构建或完成通关测试，不代表新的运行验证报告。

## 素材与使用说明

仓库用于项目展示和学习。代码、模型、音效、特效等资源的复用权限应分别确认；没有明确授权的资源，不应仅因仓库公开就默认可用于商业项目。
