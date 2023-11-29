import typer
import subprocess


app = typer.Typer(add_completion=False)


@app.command()
def last_action_url(build: str):
    command = f"gh run list --workflow={build}.yml --repo https://github.com/wkEatqua/wk --json status,url --jq [.[]][0].url"
    p = subprocess.Popen(command.split(" "), stdout=subprocess.PIPE)
    out, _ = p.communicate()
    p.wait()

    print(str(out, "utf-8").strip())


@app.command()
def run_ci(build: str, scene: str):
    command = f"gh workflow run {build}.yml --repo https://github.com/wkEatqua/wk -f scene={scene}"
    subprocess.Popen(command.split(" ")).wait()


if __name__ == "__main__":
    app()
