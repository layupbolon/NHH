using NHH.Framework.Configuration;
using NHH.Framework.Data.Configuration;
using System;
using System.Data;
using System.Data.Common;

namespace NHH.Framework.Data
{
    public static class DataCommandManager
    {

        #region GetDataCommand
        public static DataCommand GetDataCommand(string name)
        {
            var config = ConfigManager.GetConfig<DataConfig>();
            if (config != null)
            {
                foreach (var c in config.DataCommands)
                {
                    if (c.Name.ToLower() == name.ToLower())
                        return c;
                }
            }
            return null;
        }
        #endregion
    }
}
