﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Data
{
    public class NHHDbConfiguration : DbConfiguration
    {
        public NHHDbConfiguration()
        {
            SetDatabaseLogFormatter(
                (context, writeAction) => new NHHDbLogFormatter(context, writeAction));
        }
    }
}
