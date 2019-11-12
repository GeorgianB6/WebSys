namespace InterviewManagement
{
    public class Applicant
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; } 
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

        public Applicant(int id, string email, string name, string password, string phone, string status)
        {
            Id = id;
            Email = email;
            Name = name;
            Password = password;
            Phone = phone;
            Status = status;
        }
    }
}