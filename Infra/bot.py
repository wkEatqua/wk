import asyncio
import os

import discord
from discord.ext import commands


class WkBot(commands.Bot):
    def __init__(self, *, intents: discord.Intents):
        super().__init__(command_prefix="!", intents=intents)

    async def setup_hook(self):
        self.tree.copy_global_to(guild=discord.Object(id=os.environ["GUILD_ID"]))
        await self.tree.sync(guild=discord.Object(id=os.environ["GUILD_ID"]))


intents = discord.Intents.all()
bot = WkBot(intents=intents)


@bot.event
async def on_ready():
    print(f"Logged in as {bot.user} (ID: {bot.user.id})")
    print("------")


@bot.tree.command()
async def hello(interaction: discord.Interaction):
    await interaction.response.send_message(f"Hi, {interaction.user.mention}")


@bot.command()
async def delete_commands(ctx):
    bot.tree.clear_commands(guild=None)
    await bot.tree.sync()
    await ctx.send("Commands deleted.")


async def main():
    async with bot:
        await bot.load_extension(f"Cogs.ci")
        await bot.start(os.environ["DISCORD_TOKEN"])


asyncio.run(main())
