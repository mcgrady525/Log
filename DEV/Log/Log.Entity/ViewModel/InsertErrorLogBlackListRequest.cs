﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 添加error log黑名单request
    /// </summary>
    public class InsertErrorLogBlackListRequest
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

    }
}