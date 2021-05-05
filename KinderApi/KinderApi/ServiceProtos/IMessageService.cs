using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IMessageService
    {
         Task SendMessage(int fromId, int toId, string message);
         Task<List<Message>> GetMesageThread(int fromId, int toId);
    }
}