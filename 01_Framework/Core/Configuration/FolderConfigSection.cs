using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Configuration
{
    public class FolderConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("folders", IsRequired = false)]
        public KeyValueConfigurationCollection Folders
        {
            get { return (KeyValueConfigurationCollection)this["folders"]; }
        }
    }
}
