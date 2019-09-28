using Golden.Fish.Core.Models;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Golden.Fish.Core.Services
{
    public interface ITaskManager
    {
        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a proxy for the
        /// task returned by function.
        /// </summary>
        /// <param name="function">The work to execute asynchronously</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        /// <returns>A task that represents a proxy for the task returned by function.</returns>
        /// <exception cref="ArgumentNullException">The function parameter was null.</exception>
        Task Run(Func<Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a proxy for the
        /// Task(TResult) returned by function.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
        /// <param name="function">The work to execute asynchronously</param>
        /// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        /// <returns>A Task(TResult) that represents a proxy for the Task(TResult) returned by function.</returns>
        /// <exception cref="ArgumentNullException">The function parameter was null.</exception>
        /// <exception cref="TaskCanceledException">The task has been canceled.</exception>
        /// <exception cref="ObjectDisposedException">The System.Threading.CancellationTokenSource associated with cancellationToken was disposed.</exception>
        Task<TResult> Run<TResult>(Func<Task<TResult>> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        /// <summary>
        /// Queues the specified work to run on the thread pool and returns a proxy for the
        /// task returned by function.
        /// </summary>
        /// <param name="function">The work to execute asynchronously</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        /// <remarks>
        ///     The passed in Task cannot be awaited as it is to be run and forgotten.
        ///     Any errors thrown will get logged to the ILogger in the DI provider
        ///     and then swallowed and not re-thrown to the caller thread
        /// </remarks>
        /// <returns>A task that represents a proxy for the task returned by function.</returns>
        /// <exception cref="ArgumentNullException">The function parameter was null.</exception>
        void RunAndForget(Func<Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        /// <summary>
        /// Schedule a recurring event
        /// </summary>
        /// <param name="event">The event to run</param>
        /// <returns></returns>
        Task<bool> Schedule(Event @event);
    }
}
