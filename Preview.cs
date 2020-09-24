using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    [Serializable]
    class Preview : IPreview
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; }
        public string TextPreview { get; set; }
    }
}
