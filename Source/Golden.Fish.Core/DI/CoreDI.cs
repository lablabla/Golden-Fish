using Dna;
using Golden.Fish.Core.Services;

namespace Golden.Fish.Core
{
    public static class CoreDI
    {

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();

        /// <summary>
        /// A shortcut to access the <see cref="IEventScheduler"/>
        /// </summary>
        public static IEventScheduler EventScheduler => Framework.Service<IEventScheduler>();

        /// <summary>
        /// A shortcut to access the <see cref="IValveManager"/>
        /// </summary>
        public static IValveManager ValveManager => Framework.Service<IValveManager>();
    }
}
