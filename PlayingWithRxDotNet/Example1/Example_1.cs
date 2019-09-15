﻿using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithRxDotNet.Example1
{
  public class Example_1
  {
    public static async Task DoItAsync(CancellationToken cancellationToken)
    {
      using (MonitorDirectory monitor = new MonitorDirectory("monitor"))
      {
        Task task = monitor.ChangeObservable.ForEachAsync(
          async (MonitorChangeState state) =>
          {
            await processChangeStateAsync(state);
          }, cancellationToken);

        await task;
      }
    }

    private static Task processChangeStateAsync(MonitorChangeState state)
    {
      Console.WriteLine(state);

      return Task.CompletedTask;
    }
  }
}