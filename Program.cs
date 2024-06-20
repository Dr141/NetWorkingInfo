using NetWorkingInfo.Utilitarios;

Console.WriteLine("Sistema para consultar os endereços IP´s ativos na rede do computador(IP V4)");
InfoRede rede = new InfoRede();
Console.WriteLine();
Console.Write("Deseja pesquisar qual index: ");
var resultado = Console.ReadLine();
int index;
if(int.TryParse(resultado, out index))
{
    rede.BuscarEndereco(index);    
    Console.Write("Deseja imprimir o ips em uso? (S ou N): ");
    resultado = Console.ReadLine();
    if(resultado is not null)
    {
        switch (resultado.ToUpper())
        {
            case "N":
                break;
            default:
                rede.ImprimirResultado();
                break;
        }
    }    
}
Console.ReadKey();