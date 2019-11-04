using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stash
{
    public class Updater : IDisposable
    {
        bool cancel = false;
        bool update = false;
        readonly object updateSync = new object();
        Task task;
      
        public event EventHandler Disposed;

        public void SignalToUpdate()
        {
            CheckTaskState();

            if (Monitor.TryEnter(updateSync))
            {
                update = true;
                Monitor.Exit(updateSync);
            }
        }

        private void CheckTaskState()
        {
            if (task == null ||
                task.IsCompleted ||
                task.IsCanceled ||
                task.IsFaulted)
            {
                throw new Exception("Task is not running.");
            }
        }

        private bool HasSignal()
        {
            bool result = true;

            if (Monitor.TryEnter(updateSync))
            {
                result = update;
                if (result) update = false;
                Monitor.Exit(updateSync);
            }

            return result;
        }

        public Task Run(Action action, int delay = 1000)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (task != null) throw new Exception("Task already running.");
            if (delay < 0) throw new ArgumentOutOfRangeException(nameof(delay));

            return task = Task.Factory.StartNew(() =>
            {
                while (!cancel)
                {
                    if (HasSignal()) action();
                    Thread.Sleep(delay);
                }
            });
        }

        public void Dispose()
        {
            cancel = true;
            task.Wait();
            System.Diagnostics.Debug.WriteLine("Disposed.");
            Disposed?.Invoke(this, null);
        }
    }
}
