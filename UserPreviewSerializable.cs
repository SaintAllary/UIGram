using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    [Serializable]
  public  class UserPreviewSerializable 
    {
        public UserPreviewSerializable()
        {

        }

        public long ID;
        public string UserName;
        public string PictureURL;
        public string PhoneNumber;
       
    }
}
