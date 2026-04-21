# InjectFix 实战复盘

## 当前成果

- 已完成 `MainUI` 的最小热修目标配置（`[Patch]` + `HotfixConfig`）。
- 已补齐运行时加载逻辑，并从 `GameMain` 中拆分为独立类 `HotfixPatchLoader`。
- 已确认当前 IFix 版本可用入口为 `IFix.Core.PatchManager.Load(string)`。
- 已在 `ILFixEditor` 中增加补丁输出完整路径日志，便于定位产物。

---

## 核心认知结论

- `Fix`：生成补丁产物（`.patch.bytes`）。
- `Inject`：给主程序集注入桥接能力（未注入则补丁无法挂载）。
- `Load`：运行时加载补丁使修复生效。

生效闭环：

`Fix 产物 + Inject 主包 + Load 补丁 = 热更生效`

---

## 报错复盘与根因

### 1) `Cannot implicitly convert List<Type> to IEnumerator<Type>`
- 根因：`HotfixConfig` 属性返回类型写错（`IEnumerator<Type>`）。
- 处理：改为 `IEnumerable<Type>`。

### 2) `the new assembly must not be inject, please reimport the project!`
- 根因：先执行 `Inject`，再执行 `Fix`，导致用于出补丁的 DLL 已被注入。
- 处理：恢复干净程序集后按正确顺序执行。

### 3) `未找到可用的 IFix.PatchManager.Load 接口`
- 根因：初始实现使用了不匹配当前版本的反射目标命名。
- 处理：确认并改为 `IFix.Core.PatchManager` 强类型调用。

### 4) `assembly may be not injected yet, can not find IFix.ILFixInterfaceBridge`
- 根因：运行时程序集未注入，或注入结果被后续编译覆盖。
- 处理：按固定流程重跑，且 `Inject` 后立即验证。

---

## 固定执行流程（本地 PC 验证）

1. 确保 Unity 无编译错误（`CSxxxx`）。
2. 点击 `InjectFix/Fix` 生成补丁。
3. 将 `Assembly-CSharp.patch.bytes` 放入 `Assets/StreamingAssets/`。
4. 点击 `InjectFix/Inject` 注入当前主程序集。
5. 立即 `Play` 或打包验证，避免再次触发脚本重编译。

---

## 常见误区

- 只生成了补丁，但没有运行时加载逻辑。
- 把 `Inject` 和 `Fix` 的顺序做反。
- `Inject` 后又触发脚本重编译，导致注入失效。
- 用旧补丁去加载新主包（或反过来），造成版本不匹配。

---

## 现阶段建议

- 继续保持“最小改动、快速验证”的节奏。
- 每次验证都走固定顺序：`Fix -> 拷贝 patch -> Inject -> 立即验证`。
- 后续再演进到线上方案：补丁清单、版本绑定、灰度发布、回滚机制。

