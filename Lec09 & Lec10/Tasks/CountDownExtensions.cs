using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public static class CountDownExtensions
    {
        public static void StartWithTask(this Threads.Solution solution)
        {
            solution.Enabled = true;
            Task.Run(() => {
                while (solution.Enabled)
                {
                    Thread.Sleep(solution.SecondsPerTick*1000);
                    solution.Tick();
                }
            });
        }
    }
}
