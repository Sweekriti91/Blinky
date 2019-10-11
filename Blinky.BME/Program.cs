using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Units;

namespace Blinky.BME
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Bme280!");

            //bus id on the raspberry pi 3
            const int busId = 1;

            var i2cSettings = new I2cConnectionSettings(busId, Bme280.DefaultI2cAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            var i2CBmpe80 = new Bme280(i2cDevice);

            //LED setup
            var pin = 17;
            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 200;
            

            using (i2CBmpe80)
            {
                while (true)
                {
                    //set mode forced so device sleeps after read
                    i2CBmpe80.SetPowerMode(Bmx280PowerMode.Forced);

                    //set samplings
                    i2CBmpe80.SetTemperatureSampling(Sampling.UltraLowPower);
                    i2CBmpe80.SetPressureSampling(Sampling.UltraLowPower);
                    i2CBmpe80.SetHumiditySampling(Sampling.UltraLowPower);

                    //read values
                    Temperature tempValue = await i2CBmpe80.ReadTemperatureAsync();
                    Console.WriteLine($"Temperature: {tempValue.Celsius} C");
                    double humValue = await i2CBmpe80.ReadHumidityAsync();
                    Console.WriteLine($"Humidity: {humValue} %");

                    // Sleeping it so that we have a chance to get more measurements.
                    Thread.Sleep(5000);
                    humValue = await i2CBmpe80.ReadHumidityAsync();
                    if(humValue > 30.00)
                    {
                        using (GpioController controller = new GpioController())
                        {
                            controller.OpenPin(pin, PinMode.Output);
                            Console.WriteLine($"GPIO pin enabled for use: {pin}");

                            // Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                            // {
                            //     controller.Dispose();
                            // };

                            Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                            controller.Write(pin, PinValue.High);
                            Thread.Sleep(lightTimeInMilliseconds);
                            Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                            controller.Write(pin, PinValue.Low);
                            Thread.Sleep(dimTimeInMilliseconds);
                        }
                    }
                    //set mode forced and read again
                    i2CBmpe80.SetPowerMode(Bmx280PowerMode.Forced);

                    // //read values
                    // tempValue = await i2CBmpe80.ReadTemperatureAsync();
                    // Console.WriteLine($"Temperature: {tempValue.Celsius} C");
                    // humValue = await i2CBmpe80.ReadHumidityAsync();
                    // Console.WriteLine($"Humidity: {humValue} %");
                    // Thread.Sleep(5000);
                }
            }
        }
    }
}
