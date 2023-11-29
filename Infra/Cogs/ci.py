import discord
import time
import subprocess
from discord import app_commands
from discord.ext import commands


def run_ci(build: str, scene: str) -> str:
    subprocess.Popen(["python3", "scripts.py", "run-ci", build, scene]).wait()
    time.sleep(3)
    p = subprocess.Popen(
        ["python3", "scripts.py", "last-action-url", build], stdout=subprocess.PIPE
    )
    out, _ = p.communicate()
    p.wait()
    return str(out, "utf-8").strip()


class WkCiCog(commands.Cog):
    def __init__(self, bot: commands.Bot) -> None:
        self.bot = bot

    group = app_commands.Group(name="ci", description="CI 관련 커맨드들 모음")

    @group.command(name="run-daily-ci", description="특정 CI를 실행합니다.")
    @app_commands.describe(build="빌드 방식 선택")
    @app_commands.choices(
        build=[
            app_commands.Choice(name="Daily CI", value="daily-ci"),
            app_commands.Choice(name="Commit CI", value="ci"),
        ]
    )
    @app_commands.describe(scenes="포함할 씬 선택")
    @app_commands.choices(
        scenes=[
            app_commands.Choice(name="연공전 씬만", value="contest"),
            app_commands.Choice(name="모든 씬", value="all"),
        ]
    )
    async def run(
        self,
        interaction: discord.Interaction,
        build: app_commands.Choice[str],
        scenes: app_commands.Choice[str],
    ) -> None:
        await interaction.response.send_message(
            f"{build.name} {scenes.name}로 빌드를 시작합니다."
        )
        url = run_ci(build.value, scenes.value)
        await interaction.channel.send(f"Action Url: {url}")


async def setup(bot: commands.Bot) -> None:
    await bot.add_cog(WkCiCog(bot))
