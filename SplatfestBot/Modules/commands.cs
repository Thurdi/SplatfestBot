using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using DiscordBot;
using Discord;
using System.Net;

namespace DiscordBot.Modules {
    public class commands : ModuleBase<SocketCommandContext> {
        [Command("Splatfest")]
        public async Task splatfest(string team) {
            ulong flightId = 0;
            ulong invisId = 0;
            foreach (var role in Context.Guild.Roles) {
                if (role.Name == "Flight") {
                    flightId = role.Id;
                }
                else if (role.Name == "Invisibility") {
                    invisId = role.Id;
                }
            }
            if (team.ToUpper().Contains("Flight".ToUpper())) {
                await ((SocketGuildUser)Context.User).RemoveRolesAsync(Context.Guild.Roles.Where(input => input.Name == "Invisibility"));
                await ((SocketGuildUser)Context.User).AddRoleAsync(Context.Guild.GetRole(flightId));
                await Context.Channel.SendMessageAsync("Adding to team " + Context.Guild.GetRole(flightId).Name);
            }
            else if (team.ToUpper().Contains("Invisibility".ToUpper())) {
                await ((SocketGuildUser)Context.User).RemoveRolesAsync(Context.Guild.Roles.Where(input => input.Name == "Flight"));
                await ((SocketGuildUser)Context.User).AddRoleAsync(Context.Guild.GetRole(invisId));
                await Context.Channel.SendMessageAsync("Adding to team " + Context.Guild.GetRole(invisId).Name);
            }
            else {
                await Context.Channel.SendMessageAsync(team + " is not a valid option.");
            }
        }
    }
}
