using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NHH.Framework.Configuration
{
    [Serializable]
    [ConfigFile("OptLog.config")]
    [XmlRoot("OperationLogConfig", IsNullable = false)]
    public class OptLogConfig
    {

        [XmlElement("entity")]
        public List<EntityInfo> Entities { get; set; }
    }

    public class EntityInfo
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("alias")]
        public string Alias { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlElement("column")]
        public List<ColumnInfo> Columns { get; set; }
    }

    public class ColumnInfo
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("alias")]
        public string Alias { get; set; }
    }
}
