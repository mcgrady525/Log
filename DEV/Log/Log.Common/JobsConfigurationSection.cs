using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.Frameworks.Common.Extends;

namespace Log.Common
{
    /// <summary>
    /// 表示调度工作项的配置节
    /// </summary>
    public class JobsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public JobCollection Items
        {
            get
            {
                return (JobCollection)base[""];
            }
        }
    }

    /// <summary>
    /// 表示调度工作项的配置集合
    /// </summary>
    public class JobCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Job();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Job)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "job";
            }
        }

        public Job this[int index]
        {
            get
            {
                return (Job)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }

    /// <summary>
    /// 表示一个调度工作项的配置数据
    /// </summary>
    public class Job : ConfigurationElement
    {
        #region 配置属性

        [ConfigurationProperty("name", IsKey = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            get
            {
                return (this["enabled"].ToString()).ToBoolNew();
            }
            set
            {
                this["enabled"] = value.ToString();
            }
        }

        [ConfigurationProperty("trigger")]
        public string Trigger
        {
            get
            {
                return this["trigger"] as string;
            }
            set
            {
                this["trigger"] = value;
            }
        }

        [ConfigurationProperty("scheduleExp")]
        public string ScheduleExp
        {
            get
            {
                return this["scheduleExp"] as string;
            }
            set
            {
                this["scheduleExp"] = value;
            }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get
            {
                return this["type"] as string;
            }
            set
            {
                this["type"] = value;
            }
        }

        #endregion
    }
}
