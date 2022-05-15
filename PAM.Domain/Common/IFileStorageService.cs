using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PAMS.Domain.Common
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Validates the file.
        /// </summary>
        /// <param name="allowedExt">The allowed ext.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ValidateFile(string[] allowedExt, string fileName);
    }

    public class FileStorageService : IFileStorageService
    {
        /// <summary>
        /// Validates the file.
        /// </summary>
        /// <param name="allowedExt">The allowed ext.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ValidateFile(string[] allowedExt, string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            return allowedExt.Any(x => $".{x}".Equals(ext, StringComparison.InvariantCultureIgnoreCase));
        }
        
    }
}
