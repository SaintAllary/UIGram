using System;

namespace RuslanMessager
{
    [Serializable]
    public class UserPreviewSerializable
    {
        public UserPreviewSerializable() {
            LastMSG = new Message();
        }

        public Message LastMSG;
        public long ID;
        public string UserName;
        public string PictureURL;
        public string PhoneNumber;
    }
}