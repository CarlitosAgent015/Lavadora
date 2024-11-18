using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

partial class Program
{
    static void Main(string[] args)
    {
        string nombre;
        string tipoRopa;
        decimal kilos = 0;
        int tiempoLavado = 0;
        bool datosValidos;
        bool kilosValidos = false;
        bool tiempoValido = false;
        bool ropaValida = false;
        int estrato;

        Regex MyRegex = new Regex(@"^[a-zA-Z]+$");
        Lavadora ropa = new Lavadora();
        
        List<string> tiposRopa = new List<string>
        {
            "Algodon", "Lycra", "Sedas", "Telas", "Jeans", "Camperas", "Toallas",
            "Sabanas", "Acolchados", "Cortinas", "Tennis"
        };

        do
        {
            Console.WriteLine("Ingrese el nombre del cliente: ");
            nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre) || !MyRegex.IsMatch(nombre))
            {
                Console.WriteLine("Error el nombre solo puede contener letras y no puede estar vacío.");
                
                continue;
            }

            do
            {
            foreach(string tipo in tiposRopa)
        {
            Console.WriteLine("Tipo de ropa:  " + tipo);
        }
        Console.WriteLine("Ingrese el tipo de ropa: ");
        tipoRopa = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(tipoRopa))
        {
            tipoRopa = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tipoRopa.ToLower());
        }
        if (!MyRegex.IsMatch(tipoRopa))
        {
            Console.WriteLine("Error el tipo de ropa solo puede contener letras y no puede estar vacío.");
            ropaValida = false;
            continue;
        }
        else if (!tiposRopa.Contains(tipoRopa))
        {
            Console.WriteLine("Error el tipo de ropa ingresado no es válido. Asegúrese de seleccionar uno de la lista.");
            ropaValida= false;
            continue;
        }
        else
        {
            ropaValida = true;
        }
            if (tipoRopa == "Algodon" || tipoRopa == "Lycra" || tipoRopa == "Sedas" || tipoRopa == "Telas")
            {
                Console.WriteLine("Temperatura recomendada: Agua fría (hasta 20°C)");
            }
            else if (tipoRopa == "Jeans" || tipoRopa == "Camperas" || tipoRopa == "Tennis")
            {
                Console.WriteLine("Temperatura recomendada: Agua tibia (entre 30 a 50°C)");
            }
            else if (tipoRopa == "Toallas" || tipoRopa == "Sabanas" || tipoRopa == "Acolchados" || tipoRopa == "Cortinas")
            {
                Console.WriteLine("Temperatura recomendada: Agua caliente (entre 55 a 90°C)");
            }
            else
            {
                ropaValida = true;
            }
            } while(!ropaValida);
            
            do
            {
                Console.WriteLine("Ingrese la cantidad de kilos de ropa (minimo 5kg, maximo 40kg):");
                if (!decimal.TryParse(Console.ReadLine(), out kilos) || kilos < 5 || kilos > 40)
                {
                    Console.WriteLine("Error: El peso debe estar entre 5 y 40 kg.");
                    kilosValidos = false;
                }
                else
                {
                    kilosValidos = true;
                }
            } while (!kilosValidos);

            
            do
            {
                Console.WriteLine($"La ropa esta {ropa.ObtenerEstadoAleatorio()}");
                Console.WriteLine("Ingrese el tiempo de lavado en minutos (entre 5 y 40 minutos):");
                if (!int.TryParse(Console.ReadLine(), out tiempoLavado) || tiempoLavado < 5 || tiempoLavado > 40)
                {
                    Console.WriteLine("Error: El tiempo debe estar entre 5 y 40 minutos.");
                    tiempoValido = false;
                }
                else
                {
                    tiempoValido = true;
                }
            } while (!tiempoValido);

            Console.WriteLine("Ingrese el estrato del cliente (2 a 5):");
            while (!int.TryParse(Console.ReadLine(), out estrato) || estrato < 2 || estrato > 5)
            {
                Console.WriteLine("Error: El estrato debe estar entre 2 y 5.");
            }
            Cliente cliente = new Cliente(nombre);

            var (sinIva, conIva) = cliente.CalcularCosto(kilos);
            var costoConAumento = cliente.CalcularPrecioConAumento(tipoRopa, kilos);
            var utilidad = cliente.CalcularUtilidad(kilos);

            ropa.LlenadoAgua();
            ropa.Lavado();
            ropa.Enjuague();
            ropa.Secado();
            ropa.CicloTerminado();

            Console.WriteLine($"El cliente atendido es {nombre} ala hora: {cliente.FechaHoraLavado}");
            Console.WriteLine($"Costo sin IVA: {sinIva}");
            Console.WriteLine($"Costo con IVA: {conIva}");
            Console.WriteLine($"Costo con aumento: {costoConAumento}");
            Console.WriteLine($"Utilidad para el empresario: {utilidad}");

            decimal consumoKw = ropa.ConsumoKwDiario(0.2m, tiempoLavado);
            decimal consumoMensual = ropa.ConsumoKwMensual(consumoKw);
            decimal costoMensual = ropa.CostoKw(consumoMensual, estrato);

            Console.WriteLine($"Consumo diario en Kwh: {consumoKw:C} kWh");
            Console.WriteLine($"Consumo mensual en Kwh: {consumoMensual:C} kWh");
            Console.WriteLine($"Costo mensual de energía: ${costoMensual:C} COP");

            Console.WriteLine($"Clientes atendidos: {ropa.ObtenerClientesAtendidos()}");

            Console.WriteLine("¿Desea continuar con otro cliente? Presione Escape para salir o cualquier tecla para continuar ");
            ConsoleKeyInfo teclaPresionada = Console.ReadKey();
            if (teclaPresionada.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\n Programa finalizado, por favor nunca vuelva :D");
                break;
            }
            
        } while (true);
    }
}
