using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using NHH.Framework.Configuration;

namespace NHH.Framework.Configuration
{

    #region ParamConfig
    [Serializable]
    [ConfigFile("Params.config")]
    [XmlRoot("ParamConfig", IsNullable = false)]
    public class ParamConfig
    {
        [XmlElement("param")]
        public List<ParamItemInfo> ParamItems { get; set; }
    } 
    #endregion

    #region ParamItemInfo
    public class ParamItemInfo
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Content { get; set; }
    } 
    #endregion

}
