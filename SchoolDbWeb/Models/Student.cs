namespace SchoolDbWeb.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassStreamId { get; set; }

        // Optional: Navigation property if you want to access class stream details
        public ClassStream ClassStream { get; set; }
    }
}
