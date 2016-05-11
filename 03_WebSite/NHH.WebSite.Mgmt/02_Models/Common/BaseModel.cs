﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// Model基类
    /// </summary>
    public class BaseModel
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        [JsonIgnore]
        public CrumbInfo CrumbInfo { get { return _crumbInfo; } }
    }
}
