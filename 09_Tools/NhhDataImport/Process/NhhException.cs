using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport
{
    /// <summary>
    /// 异常
    /// </summary>
    public class NhhException : System.Exception
    {
        public NhhException(string message)
            : base(message)
        {
        }
    }
}
