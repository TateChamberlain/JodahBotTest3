using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using ClassLibraryCS;
using ClassLibraryDBL;

namespace BotApplication0
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //// calculate something for us to return
                //int length = (activity.Text ?? string.Empty).Length;

                //// return our reply to the user
                //Activity reply = activity.CreateReply("Hello! You sent '" + activity.Text + "'.");
                //await connector.Conversations.ReplyToActivityAsync(reply);

                //var replystring = string.Empty;
                //if (activity.Text.ToLower().Contains("c#"))
                //{
                //    replystring = Class1.GetCSharpString();
                //}
                //else
                //{
                //    try
                //    {
                //        Random rand = new Random();
                //        if (rand.Next(2) == 1)
                //        {
                //            replystring = Class2.GetDBLString();
                //        }
                //        else
                //        {
                //            replystring = Class2.GetAnotherDBLString();
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        replystring = "Oh no! There's a problem with Synergy! " + e.Message;
                //    }
                //}

                //reply = activity.CreateReply(replystring);
                //await connector.Conversations.ReplyToActivityAsync(reply);
                await Conversation.SendAsync(activity, () => new CSDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [Serializable]
        public partial class CSDialog : IDialog
        {

            public async Task StartAsync(IDialogContext context)
            {
                context.Wait(MessageReceivedAsync);
            }

            public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                IMessageActivity message = await argument;

                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //// calculate something for us to return
                //int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                //Activity reply = activity.CreateReply("Hello! You sent '" + activity.Text + "'.");
                //await connector.Conversations.ReplyToActivityAsync(reply);
                await context.PostAsync("Hello from a dialog! You sent '" + message.Text + "'.");

                var replystring = string.Empty;
                if (message.Text.ToLower().Contains("c#"))
                {
                    replystring = Class1.GetCSharpString();
                }
                else
                {
                    try
                    {
                        //replystring = Class2.GetDBLLog();
                        replystring = Class2.GetDBLString();
                    }
                    catch (Exception e)
                    {
                        replystring = "Oh no! There's a problem with Synergy! " + e.Message + " / " + e.StackTrace;
                    }
                }

                await context.PostAsync(replystring);

                //try
                //{
                //    replystring = Class2.GetDBLString();
                //}
                //catch (Exception e)
                //{
                //    replystring = "Oh no! There's a problem with Synergy! " + e.Message + " / " + e.StackTrace;
                //}
                //await context.PostAsync(replystring);

                // Make sure to receive the next message
                context.Wait(MessageReceivedAsync);
            }

        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
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
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}