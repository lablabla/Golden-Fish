using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace Golden.Fish.Core.Models
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Message
    {
        public byte[] Header => new byte[]{ 0x0A, 0x0B, 0x0C, 0x0D };
        public uint PayloadSize { get; set; }
        public MessageType MessageType { get; set; }
        public ICollection<ISerializable> Payload { get; set; }
    }
}
