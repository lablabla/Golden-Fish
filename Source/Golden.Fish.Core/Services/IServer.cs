using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Core.Services
{
    public interface IServer
    {
        void StartListening();
        void StopListening();
    }
}
