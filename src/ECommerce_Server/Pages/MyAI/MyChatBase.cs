//using System;
//using ECommerce_Server.Service;
//using ECommerce_Server.Service.IService;
//using Microsoft.AspNetCore.Components;
//using MudBlazor;
//using SharedServices.Data;
//using SharedServices.Models;
//using SharedServices.Repository.IRepository;

//namespace ECommerce_Server.Pages.MyAI
//{
//    public class ChatBase : CustomComponentBase
//    {

//        [Inject]
//        protected ClientService _clientService { get; set; }

//        [Inject]
//        protected IMessageRepository _messageRepository { get; set; }

//        [Inject]
//        protected IConversationRepository _conversationRepository { get; set; }

//        [Inject]
//        protected IOpenAiApiService _openAiApiService { get; set; }

//        [Inject]
//        protected NavigationManager _navigationManager { get; set; }

//        [Inject]
//        protected MudBlazor.ISnackbar snackBar { get; set; }

//        protected int clientId;
//        protected List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
//        protected IEnumerable<ConversationDTO> Conversations { get; set; } = new List<ConversationDTO>();

//        protected bool IsProcessing { get; set; } = false;
//        protected bool MessageIsProcessing { get; set; } = false;

//        protected override async Task OnInitializedAsync()
//        {
//            IsProcessing = true;

//            await base.OnInitializedAsync();

//            clientId = await ClientService.GetClientIdAsync(); // Retrieve clientId from the service

//            if (clientId != 0) // Assuming 0 represents no clientId
//            {
//                await LoadConversations();
//                await LoadMessages();
//            }
//            else
//            {
//                // Handle the case where ClientId is null or zero (e.g., user is not logged in or there's no client associated with this user)
//            }

//            IsProcessing = false;

//            if (ConversationId == 0)
//            {
//                OpenDrawer(Anchor.Bottom);
//            }
//        }

//        // ... Move other common methods here.
//    }

//}

