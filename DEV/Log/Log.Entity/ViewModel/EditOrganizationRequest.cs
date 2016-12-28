using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    [Serializable]
    public class EditOrganizationRequest
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 上级机构
        /// </summary>
        public int ParentId { get; set; }

    }
}
