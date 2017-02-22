using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    public class AddOrganizationRequest
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 机构排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 父机构id
        /// </summary>
        public int ParentId { get; set; }

    }
}
