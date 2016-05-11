using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class BaseEntity
    {
        private int _status = 1;
        private DateTime _inDate = DateTime.Now;
        private int _inUser = 0;
        private DateTime _editDate = DateTime.Now;
        private int _editUser = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DateTime InDate
        {
            get { return _inDate; }
            set { _inDate = value; }
        }

        public int InUser
        {
            get { return _inUser; }
            set { _inUser = value; }
        }

        public DateTime EditDate
        {
            get { return _editDate; }
            set { _editDate = value; }
        }

        public int EditUser
        {
            get { return _editUser; }
            set { _editUser = value; }
        }
    }
}
