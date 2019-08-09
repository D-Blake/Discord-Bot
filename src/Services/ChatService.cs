using Discord;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WhalesFargo
{
    /**
    * Chat Services
    */
    public class ChatService
    {
        // We have a reference to the parent module to perform actions like replying and setting the current game properly.
        private ChatModule m_ParentModule = null;

        /**
         *  SetParentModule
         *  Sets the parent module when we start the client in AudioModule.
         *  This should always be called in the module constructor to 
         *  provide a direct reference to the parent module.
         *  
         *  @param parent - Parent AudioModule    
         */
        public void SetParentModule(ChatModule parent) { m_ParentModule = parent; }

        /**
         *  DiscordReply
         *  Replies in the text channel using the parent module.
         *  
         *  @param s - Message to reply in the channel
         */
        private async void DiscordReply(string s)
        {
            if (m_ParentModule == null) return;
            await m_ParentModule.ServiceReplyAsync(s);
        }

        /**
         *  DiscordPlaying
         *  Sets the playing string using the parent module.
         *  
         *  @param s - Message to set the playing message to.
         */
        private async void DiscordPlaying(string s)
        {
            if (m_ParentModule == null) return;
            await m_ParentModule.ServicePlayingAsync(s);
        }

        /*
         AddNums
         Sends reply of the input numbers added

        @param num1
        @param num2
        */
        public void AddNums(int num1, int num2)
        {
            DiscordReply(""+(num1+num2));
        }

        /*
         SubNums
         Sends reply of the input numbers subtracted

        @param num1
        @param num2
        */
        public void SubNums(int num1, int num2)
        {
            DiscordReply("" + (num1 + num2));
        }

        /*
         Bio
         Sends reply of the input numbers subtracted

        @param num1
        @param num2
        */
        public void Bio(string name)
        {
            string bioString = "";
            try
            {
                bioString = File.ReadAllText((name + ".txt"));
            }catch(Exception e)
            {
                bioString = "Unable to find that bio.";
            }
            if (bioString.ToLower().Equals("jarvan"))
            {
                bioString = "Jarvan IV";
            }
            DiscordReply(bioString);
        }

        /*
         MultNums
         Sends reply of the input numbers Multiplied

        @param num1
        @param num2
        */
        public void MultNums(int num1, int num2)
        {
            DiscordReply("" + (num1 + num2));
        }

        /*
         DivNums
         Sends reply of the input numbers Divided

        @param num1
        @param num2
        */
        public void DivNums(int num1, int num2)
        {
            DiscordReply("" + (num1 + num2));
        }

        /*
         DivZeroError
         Sends reply of the input numbers Divided
        */
        public void DivZeroError()
        {
            DiscordReply("Cannot divide by zero");
        }

        /*
         RollDie
         Sends reply of the number rolled on a D100
        */
        public void RollDie()
        {
            Random rand = new Random();
            DiscordReply(""+(rand.Next(100)+1));
        }

        /**
         *  SayMessage
         *  Replies in the text channel using the parent module.
         *  
         *  @param s - Message to reply in the channel
         */
        public void SayMessage(string s)
        {
            DiscordReply(s);
        }

        /**
         *  SetStatus
         *  Sets the bot playing status.
         *  
         *  @param s - Message to set the playing message to.
         */
        public void SetStatus(string s)
        {
            DiscordPlaying(s);
        }

        /**
         *  ClearMessages
         *  Clears [num] number of messages from the current text channel.
         *  
         *  @param guild
         *  @param channel
         *  @param num
         */
        public async Task ClearMessages(IGuild guild, IMessageChannel channel, IUser user , int num)
        {
            // Check usage case.
            if (num == 0) // Check if Delete is 0, int cannot be null.
            {
                await channel.SendMessageAsync("`You need to specify the amount | !clear (amount) | Replace (amount) with anything`");
                return;
            }

            // Check permissions.
            var GuildUser = await guild.GetUserAsync(user.Id);
            if (!GuildUser.GetPermissions(channel as ITextChannel).ManageMessages)
            {
                await channel.SendMessageAsync("`You do not have enough permissions to manage messages`");
                return;
            }

            // Delete.
            var messages = (await channel.GetMessagesAsync((int)num + 1).FlattenAsync());
            await ((ITextChannel)channel).DeleteMessagesAsync(messages);
            
            // Reply with status.
            await channel.SendMessageAsync($"`{user.Username} deleted {num} messages`");
        }

        /**
         *  MuteUser
         *  Mutes the specific user.
         *  
         *  @param guild
         *  @param user
         *  @param channel
         */
        public async Task MuteUser(IGuild guild, IUser user, IMessageChannel channel)
        {
            var role = guild.Roles.FirstOrDefault(x => x.Name == "mute");
            await (user as IGuildUser).AddRoleAsync(role);
            await channel.SendMessageAsync(user.Mention + " has been muted.");
        }

        /**
         *  UnmuteUser
         *  Unmutes the specific user.
         *  
         *  @param guild
         *  @param user
         *  @param channel
         */
        public async Task UnmuteUser(IGuild guild, IUser user, IMessageChannel channel)
        {

            var role = guild.Roles.FirstOrDefault(x => x.Name == "mute");
            await (user as IGuildUser).RemoveRoleAsync(role);
            await channel.SendMessageAsync(user.Mention + " has been unmuted.");
        }
    }

}