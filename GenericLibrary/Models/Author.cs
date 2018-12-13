namespace GenericLibrary.Models
{
    public class Author : IEntity
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get { return string.Format("{0} {1} [{2}]", this.FirstName, this.LastName, this.EmailAddress); } }
    }
}
