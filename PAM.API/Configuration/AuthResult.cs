using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
