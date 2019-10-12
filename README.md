# Blinky

## Prerequisites

1. Docker
1. Visual Studio 2019 / VSCode

## Blinky.Simple

1. Start Docker.
2. Open the terminal.
3. Go to the repository root.
4. Build the Docker image by executing:
   1. `docker build -t blinky/simple .` OR `docker build -t blinky/bme .`
   2. A Docker image called `blinky/simple` OR `blinky/bme` will be created.
5. Create a container to run that image on your Raspberry Pi:
   1. `docker run --device /dev/gpiomem blinky/simple` OR `docker run --device /dev/i2c-1 --device /dev/gpiomem blinky/bme`
   2. As a security precaution, system devices are not exposed by default inside Docker containers. You can expose specific devices to your container using the `--device` option to `docker run`
