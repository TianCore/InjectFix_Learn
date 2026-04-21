using System.Collections.Generic;
using IFix;
using System;

[Configure]
public class HotfixConfig
{
    [IFix]
    static IEnumerable<Type> IFixList
    {
        get
        {
            return new List<Type>
            {
                // UI 入口：玩家最直接可见的交互
                typeof(MainUI),
                typeof(UIManager),
                // 业务入口：启动流程与 UI 加载编排
                typeof(GameMain),
                typeof(GameManager),
            };
        }
    }
}