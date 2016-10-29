using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using TheBot.Services;
using TheBot.BusinessObjects;
using System.Text;

namespace TheBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly IBotManager manager;
        public MessagesController()
        {
            manager = new BotManager();
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            bool askedForUserName = false;
            string userName = "";
            StringBuilder replyMessage = new StringBuilder();
            if (activity.Type == ActivityTypes.Message)
            {

                StateClient sc = activity.GetStateClient();
                BotData userData = await sc.BotState.GetPrivateConversationDataAsync(activity.ChannelId, activity.Conversation.Id, activity.From.Id);
                askedForUserName=userData.GetProperty<bool>("AskedForUserName");
                userName= userData.GetProperty<string>("UserName") ?? "";
                if (!askedForUserName)
                {
                    replyMessage.Append($"Hi ! I am your friend Pluto. What's your name? ");
                    userData.SetProperty<bool>("AskedForUserName", true);
                }
                else
                {
                    if (userName == "")
                    {
                        replyMessage.Append($"Hello {activity.Text}. How can I help you?");
                        userData.SetProperty<string>("UserName", activity.Text);
                    }
                    else
                    {
                        var model = await manager.GetModelFromLuis(activity.Text);
                        var answer = await manager.ProcessResponse(model);
                        replyMessage.Append(answer);

                    }

                }
                await sc.BotState.SetPrivateConversationDataAsync(activity.ChannelId, activity.Conversation.Id, activity.From.Id,userData);
                //var model = await manager.GetModelFromLuis(activity.Text);
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
      
                // return our reply to the user
               // var answer =await manager.ProcessResponse(model);
               // replyMessage.Append(answer);
                Activity reply = activity.CreateReply(replyMessage.ToString());
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
                
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
                StateClient sc = message.GetStateClient();
                BotData userData = sc.BotState.GetPrivateConversationData(message.ChannelId, message.Conversation.Id, message.From.Id);
                userData.SetProperty<string>("UserName", "");
                userData.SetProperty<bool>("AskedForUserName", false);
                // Save BotUserData
                sc.BotState.SetPrivateConversationData(
                    message.ChannelId, message.Conversation.Id, message.From.Id, userData);
                // Reply message
                ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                Activity replyMessage = message.CreateReply("user data has been deleted.");
                return replyMessage;


            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
                return message.CreateReply($"use is typing");
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}