using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using NHH.Framework.Configuration;

namespace NHH.Framework.Configuration
{

    #region ServiceConfig
    [Serializable]
    [ConfigFile("Services.config")]
    [XmlRoot("ServiceConfig", IsNullable = false)]
    public class ServiceConfig
    {
        [XmlElement("service")]
        public List<ServiceItemInfo> Services { get; set; }
    } 
    #endregion

    #region ServiceItemInfo
    public class ServiceItemInfo
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        private Type m_ServiceType;
        [XmlIgnore]
        public Type ServiceType
        {
            get
            {
                if (m_ServiceType == null)
                {
                    m_ServiceType = Type.GetType(ServiceName);
                }
                return m_ServiceType;
            }
        }


        [XmlAttribute("service")]
        public string ServiceName { get; set; }

        private Type m_ClassType;
        [XmlIgnore]
        public Type ClassType
        {
            get
            {
                if (m_ClassType == null)
                {
                    m_ClassType = Type.GetType(ClassName);
                }
                return m_ClassType;
            }
        }


        [XmlAttribute("class")]
        public string ClassName { get; set; }
    } 
    #endregion
}
