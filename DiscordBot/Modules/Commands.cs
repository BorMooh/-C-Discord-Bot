using Discord.Commands;
using Discord;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Diagnostics;

namespace DiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //Javne spremenljivke
        Random r = new Random();

        //Gradniki za oblikovanje outputa 
        EmbedBuilder embed = new EmbedBuilder();
        StringBuilder sb = new StringBuilder();


        //--------------------------------------------------------------KOMANDE--------------------------------------------------------------//
        [Command("Test")] // Ime commanda 
        [Summary("testna komanda")]
        public async Task Prva(string ponovi) //Glava metode, parameter je opcionalen lahko
        {
            await Context.Channel.SendMessageAsync(ponovi);
        }


        [Command("Info")]
        public async Task Info()
        {
            string infoS = "Random x,y - get a random number between X and Y\n" +
                           "GetUser - tag the user who called the command \n" +
                           "Sort x y z - sort the specified numbers\n" +
                           "Start - start the MC server" +
                           "RPS x - Rock paper scissors";

            await Context.Channel.SendMessageAsync(infoS);
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

        //50/50 - Ali je poklicana oseba idiot? 
        [Command("Idiot")]
        public async Task Idiot()
        {
            Random r = new Random();
            int st =r.Next(0, 500);
            string rez = Context.User.Mention.ToString();

            if(st >= 250)
                 rez += ": Ti NISI idiot :thinking:";
            else
                rez += ": Ti SI idiot :thinking:";
            await Context.Channel.SendMessageAsync(rez);
            //await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }

        //Komanda za sortiranje številk 
        [Command("Sort")]
        public async Task Sort(params int[] array)
        {
            List<int> arrayTemp = new List<int>();
            string vrn = "";

            if (array.Length >= 2)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    arrayTemp.Add(array[i]);
                }
                arrayTemp.Sort();
                foreach (int st in arrayTemp)
                {
                    vrn += st + " ";
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Enter at least 2 numbers");
                return;
            }


            await Context.Channel.SendMessageAsync(vrn);
        }

        //Komanda za minecraft server startup
        [Command("Start")] 
        public async Task Start() //Glava metode
        {
            //Začene se batch datoteka, ki začene program tipa .jar
            Process.Start("Batch/ServerStart.bat");


            await Context.Channel.SendMessageAsync("Server has been started!");
        }

        //Rock paper scissors
        [Command("RPS")]
        public async Task RPS(string izbira)
        {
            //Validacija če je uporabnik dejansko napisal dovoljen niz
            if(izbira.ToLower() == "rock" || izbira.ToLower() == "paper" || izbira.ToLower() == "scissors")
            {
                //Bot dobi random vrednost iz arraya "moznosti"
                int rand = r.Next(0, 3);
                string[] moznosti = { "rock", "paper", "scissors" };
                string AISelected = moznosti[rand];


                //Vrednost nastavimo na tied ker se v switch pogoju ne preverja če je tied
                string result = "tied";

                //Program preveri najprej vnos uporabnika
                switch(izbira)
                {
                    //Če je uporabnik izbral "rock"
                    case "rock":
                        if(AISelected == "paper") //Ali je bot izbral "paper"?
                        {
                            result = "AI won!";
                        }
                        else if(AISelected == "scissors")
                        {
                            result = "player won!";
                        }
                        break;

                    //Če je uporabnik izbral "paper"
                    case "paper":
                        if (AISelected == "scissors")
                        {
                            result = "AI won!";
                        }
                        else if (AISelected == "rock")
                        {
                            result = "player won!";
                        }
                        break;

                    //Če je uporabnik izbral "scissors"
                    case "scissors":
                        if (AISelected == "rock")
                        {
                            result = "AI won!";
                        }
                        else if (AISelected == "paper")
                        {
                            result = "player won!";
                        }
                        break;
                }

                //Izpis
                embed.Title = "ROCK PAPER SCISSORS";
                embed.Color = Color.DarkBlue;
                sb.AppendLine("Player used: " + izbira);
                sb.AppendLine();
                sb.AppendLine("BOT used: " + AISelected);
                sb.AppendLine();
                sb.AppendLine($"RESULT: **{result}**");

                embed.Description = sb.ToString();
                await ReplyAsync(null, false, embed.Build());
            }
            else
            {
                //Napiše error da je uporabnik vnesel napačen niz.
                await Context.Channel.SendMessageAsync("Check you input!\nHas to be \"rock\", \"paper\" or \"scissors\".");
            }

        }


    }

}
