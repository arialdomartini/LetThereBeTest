using System;
using System.Diagnostics;

namespace LetThereBeTest
{
	public class PerformanceCounter : IDisposable
	{
		private readonly Stopwatch _stopwatch;
		private readonly string _message;
		private static Action<string, TimeSpan> _action;

		public static IDisposable Start(string message)
		{
			return new PerformanceCounter(message, (msg, elapsed) => Console.WriteLine("{0} took {1}", msg, elapsed));
		}

		public static IDisposable Start(string message, Action<string, TimeSpan> action)
		{
			return new PerformanceCounter(message, action);
		}

		private PerformanceCounter(string message, Action<string, TimeSpan> action)
		{
			_stopwatch = new Stopwatch();
			_message = message;
			_action = action;
			_stopwatch.Start();
		}

		public void Dispose()
		{
			_stopwatch.Stop();
			_action(_message, _stopwatch.Elapsed);
		}
	}
}