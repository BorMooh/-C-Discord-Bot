using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("Test")] // Ime commanda 
        [Summary("testna komanda")]
        public async Task Prva(string ponovi) //Glava metode, parameter je opcionalen lahko
        {
            await Context.Channel.SendMessageAsync(ponovi);
        }
            

        [Command("User")]
        public async Task Druga(IGuildUser user)
        {
            await Context.Channel.SendMessageAsync(user.ToString());
        }

        [Command("Random")]
        public async Task Order(string stevila)
        {
            string[] prvaDruga = new string[2];
            prvaDruga = stevila.Split(',');

            Random r = new Random();
            int rez = r.Next(Convert.ToInt32(prvaDruga[0], Convert.ToInt32(prvaDruga[1])));

            await Context.Channel.SendMessageAsync(rez.ToString());
        }

        //Pridobi tistega ki je command poklical
        [Command("GetUser")]
        public async Task GetUser()
        {
            await Context.Channel.SendMessageAsync(Context.User.Mention.ToString());
            //await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }

        [Command("Idiot")]
        public async Task Idiot()
        {
            Random r = new Random();
            int st =r.Next(0, 500);


            string rez = Context.User.Mention.ToString();

            if(st >= 250)
                 rez += ": Ti NISI idiot :thinking:";

            else
                rez += ": Ti SI idiot lmaokai";


            await Context.Channel.SendMessageAsync(rez);
            //await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
