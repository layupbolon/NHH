using NHH.Framework.Configuration;
using System;
using System.Xml.Serialization;

namespace NHH.Framework.Data.Configuration
{
    [Serializable]
    [ConfigFile("data.config")]
    [XmlRoot("dbOperations", IsNullable = false)]
    public class DataConfig
    {
        /// <summary>
        /// 数据库操作命令列表
        /// </summary>
        [XmlElement("dbCommand")]
        public DataCommand[] DataCommands { get; set; }
    }
}
