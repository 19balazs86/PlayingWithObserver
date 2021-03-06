﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PlayingWithRxDotNet.Example3;

namespace PlayingWithRxDotNet
{
  public class Example_6
  {
    private static readonly Random _random = new Random();

    public static async Task DoItAsync(CancellationToken cancelToken)
    {
      // https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.dataflow
      BufferBlock<Temperature> queue = new BufferBlock<Temperature>();

      IObservable<Temperature> observable = queue.AsObservable();
      IObserver<Temperature> observer     = queue.AsObserver();

      using (observable.Subscribe(new TemperatureReporter("BufferBlock-Reporter")))
      {
        for (int i = 0; i < 5; i++)
        {
          try
          {
            await Task.Delay(_random.Next(500, 2000), cancelToken);
            observer.OnNext(Temperature.GetRandom());
          }
          catch (OperationCanceledException)
          {
            break;
          }
        }
      }
    }
  }
}
