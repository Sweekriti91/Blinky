# Blinky

## Prerequisites

1. Docker
1. Visual Studio 2019

## Blinky.Simple

1. Start Docker.
2. Open the terminal.
3. Go to the repository root.
4. Build the Docker image by executing:
   1. `docker build -t blinky/simple -f .\Blinky.Simple\Dockerfile .\Blinky.Simple\`
   2. A Docker image called `blinky/simple` will be created.
5. Create a container to run that image on your Raspberry Pi:
   1. `docker run --privileged -it blinky/simple
   2. Running it as `--privileged` allows the container to access the host's devices.
   3. See https://stackoverflow.com/a/48234752 for more information.