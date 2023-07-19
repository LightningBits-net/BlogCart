using Microsoft.AspNetCore.Components;
using ECommerce_Server.Service;
using System.Threading.Tasks;
using ECommerce_Server.Service.IService;

namespace ECommerce_Server
{
    public class CustomComponentBase : ComponentBase
    {
        [Inject]
        protected IClientService ClientService { get; set; }

        protected int ClientId { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            ClientId = await ClientService.GetClientIdAsync();
        }
    }
}
