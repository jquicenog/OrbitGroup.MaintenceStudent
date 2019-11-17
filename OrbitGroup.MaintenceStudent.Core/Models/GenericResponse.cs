namespace OrbitGroup.MaintenceStudent.Core.Models
{
    public class GenericResponse<T> where T : class
    {
        public T Entry {get;set;}
        public Message Message { get; set; }
    }

    public class Message
    {
        public int Status { get; set; }
        public string Messagge { get; set; }
        public string Error { get; set; }
    }
}
