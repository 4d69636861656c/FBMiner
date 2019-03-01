namespace ConsoleApplication
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Locale { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Website { get; set; }
        public Newtonsoft.Json.Linq.JContainer Likes { get; set; }
        public Newtonsoft.Json.Linq.JContainer Feed { get; set; }
        public Newtonsoft.Json.Linq.JContainer Friends { get; set; }
    }
}