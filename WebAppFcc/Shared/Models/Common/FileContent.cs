using WebAppFcc.Shared.Common;

namespace WebAppFcc.Shared.Models
{
    public class FileContent : BaseModel
    {
        public byte[] BinaryContent { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
    }
}
