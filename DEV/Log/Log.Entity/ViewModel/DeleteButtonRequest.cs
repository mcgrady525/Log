using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    /// <summary>
    /// 删除按钮request
    /// </summary>
    [Serializable]
    public class DeleteButtonRequest
    {
        /// <summary>
        /// 待删除的按钮id
        /// </summary>
        public string DeleteButtonId { get; set; }

    }
}
