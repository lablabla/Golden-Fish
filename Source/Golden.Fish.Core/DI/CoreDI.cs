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
    }
}
