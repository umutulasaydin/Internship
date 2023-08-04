namespace CouponManagementService.Core.Models
{
    public class BaseResponse<T>
    {
        public T result { get; set; }
        public bool isSucces { get; set; }
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
    }
}
