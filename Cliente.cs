using System;

public class Cliente
{
    private const decimal Costo = 4000m;
    protected string Nombre { get; set; }
    protected internal DateTime FechaHoraLavado { get; private set; }

    protected internal Cliente(string nombre)
    {
        Nombre = nombre;
        FechaHoraLavado = DateTime.Now;
    }

    public (decimal sinIva, decimal conIva) CalcularCosto(decimal kilos)
    {
        decimal costoSinIva = Costo * kilos;
        decimal iva = costoSinIva * 0.19m;
        decimal costoConIva = costoSinIva + iva;
        return (costoSinIva, costoConIva);
    }

    internal decimal CalcularPrecioConAumento(string tipoRopa, decimal kilos)
    {
        decimal precioBase = Costo * kilos;
        if (tipoRopa == "Algodon" || tipoRopa == "Tennis" || tipoRopa == "Blanca")
        {
            return precioBase * 1.03m; // Aumento del 3%
        }
        return precioBase;
    }

    
    public decimal CalcularUtilidad(decimal kilos)
    {
        decimal costoTotal = Costo * kilos;
        return costoTotal * 0.40m;
    }
}
