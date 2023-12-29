using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Kernel.Attributes
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class NeedSizeAttribute : Attribute
    {

    }
}
