using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetWorkingInfo.Utilitarios
{
    public class InfoRede
    {
        private int _contador;
        private IPHostEntry _hostEntry;
        public List<string> RedeList { get; set; }
        
        public InfoRede()
        {
            _hostEntry = Dns.GetHostEntry(Environment.MachineName);
            RedeList = new List<string>();
            ImprimirListaIps();
        }

        public void ImprimirListaIps()
        {
            try
            {
                if (_hostEntry is not null && _hostEntry.AddressList is not null) 
                {
                    Console.WriteLine($"Nome host: {_hostEntry.HostName}");
                    Console.WriteLine("Index");
                    Console.WriteLine("*");
                    foreach (var address in _hostEntry.AddressList) 
                    {
                        _contador++;
                        Console.WriteLine("{0} -> IP: {1}; ", _contador, address.MapToIPv4().ToString());
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
        }

        public void BuscarEndereco(int index)
        {
            if(index > 1) index--;
            string[] partesDoEndereco = _hostEntry.AddressList[index].MapToIPv4().ToString().Split('.');
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "teste";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            for(int i = 1; i <= 254; i++)
            {
                string address = string.Concat(partesDoEndereco[0], ".", partesDoEndereco[1], ".", partesDoEndereco[2],".", i);
                PingReply reply = pingSender.Send(address, timeout, buffer, options);
                Console.WriteLine("Ping Address: {0} - result: {1}", reply.Address.ToString(), Enum.GetName(reply.Status));
                if (reply.Status == IPStatus.Success)
                {
                    RedeList.Add(address);
                }
            }            
        }

        public void ImprimirResultado()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Endereços ips encontrados:");
            foreach (string address in RedeList)
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(address);
                    Console.WriteLine("IP: {0}; Nome do host: {1}", address, host.HostName);
                }
                catch { Console.WriteLine("IP: {0}; Nome do host não encontrado", address); }
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine();
        }
    }
}
