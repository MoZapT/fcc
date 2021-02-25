using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppFcc.Shared.Models
{
    public class PersonPhoto
    {
        private string _base64url;

        [Key]
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid FileContentId { get; set; }
        public FileContent FileContent { get; set; }

        public string GetBase64String { get 
            {
                if (FileContent?.BinaryContent == null)
                    return null;

                if (string.IsNullOrWhiteSpace(_base64url))
                    _base64url = string.Format("data:{0};base64,{1}",
                        FileContent?.FileType,
                        Convert.ToBase64String(FileContent?.BinaryContent));

                return _base64url; 
            } 
        }
    }
}
