# InjectFix 学习目标

## 目标 1：完成最小闭环（今天就能做）

你现在要先跑通最短路径：

1. 等 Unity 编译结束。
2. 点击 `InjectFix/Fix`（先生成补丁）。
3. 点击 `InjectFix/Inject`（再注入程序集）。
4. 看 Console：有无红色报错。
5. 若报错，优先贴这三类信息：
   - ToolKit/IFix.exe 找不到
   - 配置目标为空（无可处理类型/方法）
   - 编译错误（CSxxxx）

---

## 目标 2：达到可验证结果（成功标准）

- 能执行 Inject 与 Fix，不出现阻断性错误。
- 能看到补丁产物文件（或明确知道为什么没生成）。
- 运行时加载补丁后，目标方法行为可观察变化（日志/返回值变化）。

---

## 目标 3：避免常见误区

- 只点了 Inject/Fix，没做运行时加载，就以为“补丁没生效”。
- 一上来就测 Android(IL2CPP)，把平台问题和流程问题混在一起。
- 没有最小 Demo，直接在复杂业务里排错，成本很高。

---

## 目标 4：按阶段推进学习

1. 先跑通 PC/Mono 最小闭环。
2. 再理解 `[Patch]` / `[Interpret]` 使用边界。
3. 最后做 Android/iOS 平台化验证与发布流程固化。

---

## 目标 5：用 `MainUI.cs` 完成第一次最小实操

### 操作目标

- 让 `InjectFix/Fix` 真正生成补丁文件。
- 确认补丁产物默认输出到项目根目录。

### 操作步骤

1. 修改 `Assets/Scripts/UI/MainUI.cs`：
   - 增加 `using IFix;`
   - 给 `OnClickStart()` 增加 `[Patch]` 标记
   - 方法内容改成明显可观察的文案/颜色（用于后续验证）
2. 新建配置文件 `Assets/IFix/Editor/HotfixConfig.cs`：
   - 类上加 `[Configure]`
   - 增加 `[IFix]` 属性，返回 `typeof(MainUI)`
3. 回到 Unity，等待编译完成（确保无 `CSxxxx` 报错）。
4. 依次点击：
   - `InjectFix/Fix`
   - `InjectFix/Inject`
5. 检查项目根目录是否出现：
   - `Assembly-CSharp.patch.bytes`

### 成功判定

- Console 无阻断性红色错误。
- 根目录出现 `Assembly-CSharp.patch.bytes`。

### 常见失败与排查

- 看不到补丁文件：
  - 先确认有没有点 `Fix`
  - 再确认 `MainUI` 是否有 `[Patch]`
  - 再确认 `HotfixConfig` 是否被放在 `Editor` 目录下
- 点 `Fix` 报错：
  - 优先贴出 Console 最后 20 行日志
  - 常见是 ToolKit 路径问题或编译错误

---

## 目标 6：形成可发布的固定流程

1. 明确主包版本号与补丁版本号的绑定规则。
2. 固化发布顺序：`Fix -> Inject -> 打包 -> 运行时加载验证`。
3. 每次主包升级后重新注入并重做补丁验证。
4. 形成补丁回滚策略（加载失败时回退到原逻辑）。

