using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database.Dto
{
    public class UploadFileModel
    {
        public string uid { get; set; }
        public DateTime? lastModified { get; set; }
        public string name { get; set; }
        public Nullable<long> size { get; set; }
        public string type { get; set; }
        public int percent { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string thumbUrl { get; set; }
        public bool isOldFile { get; set; }
    }
}
