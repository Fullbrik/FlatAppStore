using System;
using System.Threading.Tasks;
using System.Timers;

namespace FlatAppStore
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var engine = new UI.Framework.Engine())
				engine.Run(new UI.Screens.StartupScreen());
		}
	}
}
