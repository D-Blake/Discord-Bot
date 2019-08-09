using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace WhalesFargo
{

    /**
     * Chat
     * Class that handles the Chat response portion of the program.
     * A chat module is created here with commands that interact with the ChatService.
     */
    [Name("Chat")]
    [Summary("Chat module to interact with text chat.")]
    public class ChatModule : ModuleBase
    {
        // Private variables
        private readonly ChatService m_Service;

        // Remember to add an instance of the AudioService
        // to your IServiceCollection when you initialize your bot!
        public ChatModule(ChatService service)
        {
            m_Service = service;
            m_Service.SetParentModule(this); // Reference to this from the service.
        }

        // Reply will allow the AudioService to reply in the correct text channel.
        public async Task ServiceReplyAsync(string s)
        {
            await ReplyAsync(s);
        }

        // Playing will allow the AudioService to set the current game.
        public async Task ServicePlayingAsync(string s)
        {
            await (Context.Client as DiscordSocketClient).SetGameAsync(s);
        }

        [Command("botStatus")]
        [Alias("botstatus")]
        [Remarks("!botstatus [status]")]
        [Summary("Allows admins to set the bot's current game to [status]")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetBotStatus([Remainder] string botStatus)
        {
            m_Service.SetStatus(botStatus);
            await Task.Delay(0);
        }

        [Command("say")]
        [Alias("say")]
        [Remarks("!say [msg]")]
        [Summary("The bot will respond in the same channel with the message said.")]
        public async Task Say([Remainder] string usr_msg = "")
        {
            m_Service.SayMessage(usr_msg);
            await Task.Delay(0);
        }

        [Command("bio")]
        [Alias("bio")]
        [Remarks("!bio [name]")]
        [Summary("Returns the biography of the specified League of Legends character.")]
        public async Task Sub(string name)
        {
            m_Service.Bio(name);
            await Task.Delay(0);
        }

        [Command("sub")]
        [Alias("sub")]
        [Remarks("!sub [num1] [num2]")]
        [Summary("The bot will subtract the numbers and return the result into the chat")]
        public async Task Sub(int num1, int num2)
        {
            m_Service.SubNums(num1, num2);
            await Task.Delay(0);
        }

        [Command("mult")]
        [Alias("mult")]
        [Remarks("!mult [num1] [num2]")]
        [Summary("The bot will multiply the numbers and return the result into the chat")]
        public async Task Mult(int num1, int num2)
        {
            m_Service.MultNums(num1, num2);
            await Task.Delay(0);
        }

        [Command("roll")]
        [Alias("roll")]
        [Remarks("!roll")]
        [Summary("The bot will return a number between 1 and 100 randomly")]
        public async Task Roll()
        {
            m_Service.RollDie();
            await Task.Delay(0);
        }

        [Command("div")]
        [Alias("div")]
        [Remarks("!div [num1] [num2]")]
        [Summary("The bot will divide the numbers and return the result into the chat")]
        public async Task Div(int num1, int num2)
        {
            if (num1!=0)
            {
                m_Service.MultNums(num1, num2);
                await Task.Delay(0);
            }
            else
            {
                m_Service.DivZeroError();
                await Task.Delay(0);
            }
        }

        [Command("add")]
        [Alias("add")]
        [Remarks("!add [num1] [num2]")]
        [Summary("The bot will add the numbers and return the result into the chat")]
        public async Task Add(int num1, int num2)
        {
            m_Service.AddNums(num1, num2);
            await Task.Delay(0);
        }

        [Command("Clear")]
        [Remarks("!clear [num]")]
        [Summary("Allows admins to clear [num] amount of messages from current channel")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task ClearMessages([Remainder] int num = 0)
        {
            await m_Service.ClearMessages(Context.Guild, Context.Channel, Context.User, num);
        }

        [Command("mute")]
        [Remarks("!mute [user]")]
        [Summary("This allows admins to mute users.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task MuteUser([Remainder] IGuildUser user = null)
        {
            await m_Service.MuteUser(Context.Guild, Context.User, Context.Channel);      
        }

        [Command("unmute")]   
        [Remarks("!unmute [user]")]
        [Summary("This allows admins to unmute users.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task UnmuteUser([Remainder] IGuildUser user = null)
        {
            await m_Service.UnmuteUser(Context.Guild, Context.User, Context.Channel);
        }

    }
}


       