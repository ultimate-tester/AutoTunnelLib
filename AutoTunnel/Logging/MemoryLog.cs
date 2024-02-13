using Force.AutoTunnel.Logging;

namespace Force.AutoTunnelLib.Logging
{
    internal class MemoryLog : ILog
    {
        private string Buffer { get; set; }

        public void WriteLine(string line)
        {
            Buffer += line + "\r\n";
        }

        public override string ToString()
        {
            return Buffer;
        }
    }
}
