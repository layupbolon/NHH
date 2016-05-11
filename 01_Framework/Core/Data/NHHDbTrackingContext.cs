using NHH.Framework.Configuration;
using NHH.Framework.Data;
using NHH.Framework.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Service
{
    public class NHHDbTrackingContext : IDisposable
    {

        public NHHDbTrackingContext(DbContext context)
        {
            this.Context = context;
        }

        #region Monitors
        private static Hashtable m_Monitors;
        protected static Hashtable Monitors
        {
            get
            {
                if (m_Monitors == null)
                {
                    var config = ConfigManager.GetConfig<OptLogConfig>();
                    m_Monitors = Hashtable.Synchronized(new Hashtable(config.Entities.Count));
                    foreach (var entity in config.Entities)
                    {
                        m_Monitors.Add(entity.Name, entity);
                    }
                }
                return m_Monitors;
            }
        }
        #endregion

        #region Context
        public DbContext Context { get; private set; }
        #endregion

        #region TrackingChanges
        public List<TrackingItem> TrackingChanges { get; private set; }
        #endregion

        #region Extract
        public void Extract()
        {
            this.TrackingChanges = new List<TrackingItem>();

            if (Monitors.Count > 0 && this.Context != null && this.Context.ChangeTracker.HasChanges())
            {
                var entries = this.Context.ChangeTracker.Entries()
                                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

                foreach (var e in entries)
                {
                    var ename = (e.State==EntityState.Added?e.Entity.GetType():e.Entity.GetType().BaseType).Name;
                    if (Monitors.ContainsKey(ename))
                    {
                        var m = Monitors[ename] as EntityInfo;
                        if (m != null && !string.IsNullOrEmpty(m.Key))
                        {
                            this.TrackingChanges.Add(new TrackingItem(m, e));
                        }
                    }
                }
            }

        }
        #endregion

        public void Flush(Action<TrackingItem> write)
        {
            if (this.TrackingChanges == null || this.TrackingChanges.Count == 0)
                return;

            if (write != null)
            {
                foreach (var item in this.TrackingChanges)
                {
                    write(item);
                }
            }

            this.TrackingChanges.Clear();
        }

        #region Dispose
        public void Dispose()
        {
            this.TrackingChanges = null;
            this.Context = null;
        }
        #endregion
    }

    public class TrackingItem
    {
        public TrackingItem(EntityInfo schema, DbEntityEntry source)
        {
            this.Schema = schema;
            this.SourceEntry = source;
            this.State = source.State;
            this.OldValues = new Dictionary<string, object>();
            this.NewValues = new Dictionary<string, object>();
            switch (this.State)
            {
                case EntityState.Added:
                    foreach (var c in schema.Columns)
                    {
                        if (source.CurrentValues.PropertyNames.Contains(c.Name))
                        {
                            var p = source.Property(c.Name);
                            this.NewValues.Add(c.Name, p.CurrentValue);
                        }
                    }
                    break;
                case EntityState.Modified:
                    foreach (var c in schema.Columns)
                    {
                        if (source.CurrentValues.PropertyNames.Contains(c.Name))
                        {
                            var p = source.Property(c.Name);
                            if (p.IsModified && !object.Equals(p.OriginalValue, p.CurrentValue))
                            {
                                this.OldValues.Add(c.Name, p.OriginalValue);
                                this.NewValues.Add(c.Name, p.CurrentValue);
                            }
                        }
                    }
                    break;
                case EntityState.Deleted:
                    foreach (var c in schema.Columns)
                    {
                        if (source.OriginalValues.PropertyNames.Contains(c.Name))
                        {
                            var p = source.Property(c.Name);
                            this.OldValues.Add(c.Name, p.OriginalValue);
                        }
                    }
                    break;
                default : break;
            
            }

            var eb = source.Entity as IEditable;
            if (eb != null)
            {
                this.NewValues["OptUser"] = eb.EditUser ?? eb.InUser;
                this.NewValues["OptTime"] = eb.EditDate ?? eb.InDate;
            }

        }
        public EntityInfo Schema { get; set; }

        public EntityState State { get; set; }


        public Dictionary<string, object> OldValues { get; set; }

        public Dictionary<string, object> NewValues { get; set; }


        public DbEntityEntry SourceEntry { get; set; }

        public string GetEntityName()
        {
            return this.Schema.Name;
        }

        public string GetEntityKey()
        {
            return string.Format("{0}", this.SourceEntry.Property(this.Schema.Key).CurrentValue);
        }

        public string GetActionName()
        {
            return this.State.ToString();
        }

        public string GetActionUser()
        {
            return string.Format("{0}", this.NewValues["OptUser"]);
        }

        public DateTime? GetActionTime()
        {
            return this.NewValues["OptTime"] as DateTime?;
        }

        public string GetDetailInfo()
        {
            var rlt = string.Empty;
            var id = this.GetEntityKey();
            
            switch (this.State)
            {
                case EntityState.Added:
                    if (this.Schema.Columns != null && this.Schema.Columns.Count > 0)
                    {
                        foreach (var col in this.Schema.Columns)
                        {
                            var name = col.Alias??col.Name;
                            var val = this.NewValues.ContainsKey(col.Name)?this.NewValues[col.Name]:null;
                            rlt += string.Format("{0}:{1};", name, val);
                        }
                    }
                    break;
                case EntityState.Deleted:
                   
                    if (this.Schema.Columns != null && this.Schema.Columns.Count > 0)
                    {
                        foreach (var col in this.Schema.Columns)
                        {
                            var name = col.Alias ?? col.Name;
                            var val = this.OldValues.ContainsKey(col.Name) ? this.OldValues[col.Name] : null;
                            rlt += string.Format("{0}:{1};", name, val);
                        }
                    }
                    break;
                case EntityState.Modified:
                    if (this.Schema.Columns != null && this.Schema.Columns.Count > 0)
                    {
                        foreach (var col in this.Schema.Columns)
                        {
                            var name = col.Alias ?? col.Name;
                            var ov = this.OldValues.ContainsKey(col.Name) ? this.OldValues[col.Name] : null;
                            var nv = this.NewValues.ContainsKey(col.Name) ? this.NewValues[col.Name] : null;
                            if (ov != null || nv != null)
                            {
                                rlt += string.Format("{0}:{1}->{2};", name, ov, nv);
                            }
                        }
                    }
                    break;
                default: 
                    break;
            }
            return rlt;
        }
    }
}
