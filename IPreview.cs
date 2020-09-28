namespace RuslanMessager
{
    public interface IPreview
    {
        long ID { get; set; }
        string UserName { get; set; }
        string PictureURL { get; set; }
        string TextPreview { get; set; }
        string PhoneNumber { get; set; }
    }
}