using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PiApp.Shared;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Peripherals;

namespace PiApp.Services
{
    public sealed class RfidReader : IRfidReader
    {
        private readonly RFIDControllerMfrc522 _rfidController;
        private readonly ILogger<RfidReader> _logger;
        private readonly Subject<CardData> _whenCardDetected;
        private Timer _timer;

        public RfidReader(ILogger<RfidReader> logger)
        {
            _logger = logger;
            _rfidController = new RFIDControllerMfrc522(Pi.Spi.Channel0, 500000, Pi.Gpio[22]);

            _whenCardDetected = new Subject<CardData>();
        }

        public Task StartAsync()
        {
            _logger.LogInformation("RFID Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMilliseconds(1000));

            _logger.LogInformation("RFID Reader Service has started.");

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var status = _rfidController.DetectCard();

            if (status != RFIDControllerMfrc522.Status.AllOk) return;

            var response =_rfidController.ReadCardUniqueId();

            _logger.LogInformation($"UID read: {string.Join(", ", response.Data)}");

            _whenCardDetected.OnNext(new CardData(response.Data));
        }

        public Task StopAsync()
        {
            _logger.LogInformation("RFID Reader Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task<CardData> ReadCardUniqueIdAsync()
        {
            var response = _rfidController.ReadCardUniqueId();
            return Task.FromResult(new CardData(response.Data));
        }

        public IObservable<CardData> WhenCardDetected => _whenCardDetected;
    }
}
