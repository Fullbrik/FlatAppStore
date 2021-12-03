using System;
using System.Threading.Tasks;
using System.Timers;

namespace FlatAppStore
{
	class Program
	{
		static void Main(string[] args)
		{
			UI.Framework.Engine.Instance.Run(new UI.Screens.StartupScreen());
		}
	}
}
