import typer
import subprocess


app = typer.Typer(add_completion=False)


@app.command()
def last_action_url(build: str):
    command = f'gh run list --workflow={build}.yml --repo https://github.com/wkEatqua/wk --json status,url --jq "[.[]][0].url"'
    p = subprocess.Popen(command, stdout=subprocess.PIPE)
    out, _ = p.communicate()
    p.wait()

    print(str(out, "utf-8").strip())


@app.command()
def run_ci(build: str, scene: str):
    subprocess.Popen(
        [
            "gh",
            "workflow",
            "run",
            f"{build}.yml",
            "--repo",
            "https://github.com/wkEatqua/wk",
            "-f",
            f"scene={scene}",
        ]
    ).wait()


if __name__ == "__main__":
    app()
