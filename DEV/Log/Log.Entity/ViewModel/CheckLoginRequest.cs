using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.ViewModel
{
    public class CheckLoginRequest
    {
        public string loginId { get; set; }

        public string loginPwd { get; set; }

        public string remember { get; set; }

    }
}
