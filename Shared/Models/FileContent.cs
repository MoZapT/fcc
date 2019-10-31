using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class FileContent : BaseModel
    {
        public byte[] BinaryContent { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
    }
}
