using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Burger.Api.Models
{
    public enum Sales : short
    {
        Light = 1,
        MuitaCarne = 2,
        MuitoQueijo = 3,
    }

    public static class SalesExtensions
    {
        public static string GetName(this Sales item)
        {
            switch (item)
            {
                case Sales.Light: return "Light";
                case Sales.MuitaCarne: return "Muita carne";
                case Sales.MuitoQueijo: return "Muito queijo";
                default: return string.Empty;
            }
        }
    }
}