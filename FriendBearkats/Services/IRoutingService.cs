using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendBearkats.Services
{
    public interface IRoutingService
    {
        Task GoBack();
        Task GoBackModal();
        Task NavigateTo(string route);
    }
}
