using System;
using System.Threading;
using System.Text.RegularExpressions;
//using System.Media;
public class Lavadora
{
    /*
        SoundPlayer player4 = new SoundPlayer("/home/carlitos/Descargas/audio4.wav"); 
        SoundPlayer player3 = new SoundPlayer("/home/carlitos/Descargas/audio3.wav");
        SoundPlayer player2 = new SoundPlayer("/home/carlitos/Descargas/audio2.wav");
        SoundPlayer player1 = new SoundPlayer("/home/carlitos/Descargas/audio1.wav");
     */ 
    private const double costo = 4000;
    private const double IVA = 0.19;
    private const double utilidad = 0.40;
    private const double aumento = 0.03;
    private int clientesAtendidos;

    Regex MyRegex = new Regex(@"^[a-zA-Z]+$");

    private decimal tiempoLavado;

    internal Lavadora()
    {
        clientesAtendidos = 0;
        tiempoLavado = 0;
    }

    internal void TiempoLavado(decimal tiempo)
    {
        tiempoLavado = tiempo;
    }

    public void CicloTerminado()
    {
        //player1.Play();
        Console.WriteLine("El ciclo ha terminado. Gracias por usar la lavadora.");
        //player1.Stop();
    }

    internal void LlenadoAgua()
    {
        Console.WriteLine("Llenando el agua...");
        for (int i = 0; i < 7; i++)
        {
            //player2.Play();
            Console.WriteLine("\rLlenando...");
            Thread.Sleep(1000);
            Console.Write("\r   ");
            Thread.Sleep(1000);
        }
        Console.WriteLine("\rAgua llena.");
        //player2.Stop();
    }

    internal void Lavado()
    {
       //player3.Play();
        Console.WriteLine("Iniciando lavado...");
        Thread.Sleep(3000);
        Console.WriteLine("Lavado iniciado.");
        //player3.Stop();
    }

    internal void Enjuague()
    {
        //player4.Play();
        Console.WriteLine("Iniciando enjuague...");
        Thread.Sleep(1000);
        Console.WriteLine("Enjuague iniciado.");
        //player4.Stop();
    }

    internal void Secado()
    {
        bool response = false;
        do{
            string respuesta;
        Console.WriteLine("¿Desea secar las prendas? (si/no): ");
        respuesta = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(respuesta) || !MyRegex.IsMatch(respuesta))
        {
        }
        if (respuesta == "si")
        {
            //player1.Play();
            Console.WriteLine("Secado en proceso...");
            Thread.Sleep(1500);
            Console.WriteLine("Secado iniciado.");
            //player1.Stop();
            response = true;
        }
        else if (respuesta == "no")
        {
            Console.WriteLine("Proceso detenido.");
            Console.WriteLine("Precione cualquier tecla para reanudar");
            Console.ReadLine();
            response = true;
        }
        else
        {
            Console.WriteLine("Por favor escoja alguna de las 2 opciones para que el profe vea la validacion");
        }
        } while(!response);
    }

    internal string ObtenerEstadoAleatorio()
    {
        string[] estados = { "sucia", "muy sucia" };
        Random random = new Random();
        int indice = random.Next(estados.Length);
        return estados[indice];
    }

    internal int ObtenerClientesAtendidos()
    {
        clientesAtendidos++;
        return clientesAtendidos;
    }

    internal decimal CalcularKilovatios(decimal kilos, string tipoRopa, decimal tiempo)
    {
        decimal consumoBase = 0.5m;
        decimal consumoTotal = kilos * consumoBase * (tiempoLavado / 60m);

        if (tipoRopa == "Algodon" || tipoRopa == "Jeans")
        {
            consumoTotal *= 1.2m;
        }
        else if (tipoRopa == "Sedas" || tipoRopa == "Lycra")
        {
            consumoTotal *= 0.8m;
        }

        return (decimal)consumoTotal;
}
    internal decimal ConsumoKwDiario(decimal potenciaKw, decimal tiempoLavado)
    {
        decimal horas = tiempoLavado / 60m;
        return potenciaKw * horas;
    }

    internal decimal ConsumoKwMensual(decimal consumoDiario)
    {
        return consumoDiario * 30;
    }

    internal decimal CostoKw(decimal consumoMensual, int estrato)
    {
        decimal costoPorKw;
        switch (estrato)
        {
            case 2:
                costoPorKw = 867.8m;
                break;
            case 3:
                costoPorKw = 737.6m;
                break;
            case 4:
                costoPorKw = 867.8m;
                break;
            case 5: 
                costoPorKw = 1041m;
                break;
            default:
            throw new ArgumentException("Estrato invalido");
        }
        return consumoMensual * costoPorKw;
    }

}
