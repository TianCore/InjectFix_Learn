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
                typeof(MainUI)
            };
        }
    }
}