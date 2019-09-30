using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Golden.Fish.Core.Services
{
    public interface IGoldenFishServer
    {
        public void Initialize();
        public void Listen();
    }
}
