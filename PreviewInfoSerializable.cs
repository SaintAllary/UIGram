using System;

namespace RuslanMessager
{
    [Serializable]
    internal class PreviewInfoSerializable : IPreview
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; }
        public string TextPreview { get; set; }
        public string PhoneNumber { get; set; }
    }
}