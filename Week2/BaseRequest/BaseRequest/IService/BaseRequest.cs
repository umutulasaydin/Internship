using BaseRequest.Models;

namespace BaseRequest.BaseRequest
{
    public interface BaseRequest <T, V>
    { 
       
        T GetAll(string _client);
        T GetById(int id, string _client);
        T Insert(V item, string _client);
        T Delete(int id, string _client);
    }


}
