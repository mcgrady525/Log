using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    [Serializable]
    public class DeleteOrganizationRequest
    {
        /// <summary>
        /// 删除的所有机构id，以','分隔
        /// </summary>
        public string DeleteOrgIds { get; set; }
    }
}
