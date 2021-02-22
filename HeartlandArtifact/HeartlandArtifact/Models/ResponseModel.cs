namespace HeartlandArtifact.Models
{
    public class ResponseModel<T>
    {
        public string message { get; set; }
        public T data { get; set; }
        public string status { get; set; }
    }
}
