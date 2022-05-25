using System;
using System.Threading;
using System.Threading.Tasks;
using NvAPIWrapper.GPU;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Newtonsoft.Json;
using NvAPIWrapper.Native.GPU;

namespace TelegramBotForMiners
{

    class Program
    {

        static ITelegramBotClient bot = new TelegramBotClient("5364426693:AAHZoMR7VYaOStyzugFkznA5HkfR_eGJxVY");


        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                switch (message.Text.ToLower())
                {
                    case "/start":
                        {
                            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                            {
                                new KeyboardButton[] { "отчет по видюхе", "отчет по rvn" },
                                new KeyboardButton[] { "hi", "отчет по eth" }
                            })
                            {
                                ResizeKeyboard = true
                            };
                            ReplyKeyboardMarkup keyboard = replyKeyboardMarkup;
                            await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать хозяин", replyMarkup: keyboard);
                            break;
                        }
                    case "отчет по видюхе":
                        {
                            PhysicalGPU[] gpus = PhysicalGPU.GetPhysicalGPUs();
                            foreach (PhysicalGPU gpu in gpus)
                            {
                                Console.WriteLine(gpu.FullName);//SystemType.Desktop
                                await botClient.SendTextMessageAsync(message.Chat, SystemType.Unknown.ToString());
                                await botClient.SendTextMessageAsync(message.Chat, gpu.FullName);
                                foreach (GPUThermalSensor sensor in gpu.ThermalInformation.ThermalSensors)
                                {
                                    Console.WriteLine(sensor.CurrentTemperature);
                                    await botClient.SendTextMessageAsync(message.Chat, sensor.CurrentTemperature.ToString());

                                }
                            }
                            break;
                        }
                    case "отчет по rvn":
                        {
                            string URI = "https://rvn.2miners.com/api/accounts/RPEZzUregyRi4THAkhnVurX1zRnZc68jYt";
                            string myParameters = "param1=value1&param2=value2&param3=value3";
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                string HtmlResult = wc.UploadString(URI, myParameters);
                                var tmp = JsonConvert.DeserializeObject<Rootobject>(HtmlResult);
                                
                                TimeSpan ts = TimeSpan.FromSeconds(tmp.stats.lastShare+25200);
                                DateTime dt = new DateTime(1970, 1, 1);
                                dt += ts;

                                await botClient.SendTextMessageAsync(message.Chat, ("Текущий Хешрейт "+(tmp.currentHashrate/1000000).ToString()+ " MH/s"));
                                await botClient.SendTextMessageAsync(message.Chat, "Последняя шара в "+ dt.ToString());
                            }

                            break;
                        }
                    case "отчет по eth":
                        {
                            string URI = "https://eth.2miners.com/api/accounts/nano_3t1ghwcf8qaqqjcapuwo1m4dqorwqfnywt44bzhtd1ujc668o8w6ds4s46m8";
                            string myParameters = "param1=value1&param2=value2&param3=value3";
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                string HtmlResult = wc.UploadString(URI, myParameters);
                                var tmp = JsonConvert.DeserializeObject<Rootobject>(HtmlResult);

                                TimeSpan ts = TimeSpan.FromSeconds(tmp.stats.lastShare + 25200);
                                DateTime dt = new DateTime(1970, 1, 1);
                                dt += ts;

                                await botClient.SendTextMessageAsync(message.Chat, ("Текущий Хешрейт " + (tmp.currentHashrate / 1000000).ToString() + " MH/s"));
                                await botClient.SendTextMessageAsync(message.Chat, "Последняя шара в " + dt.ToString());
                            }

                            break;
                        }

                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            //while (true)
            //{

            //}
            Console.ReadLine();
        }
    }
}