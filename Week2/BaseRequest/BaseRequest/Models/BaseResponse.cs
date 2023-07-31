namespace BaseRequest.Models
{
    public class BaseResponse<T>
    {
        public bool isSucces { get; set; }
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        public List<T> result { get; set; }
    }
}
