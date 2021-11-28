using System;
using Busqo.Base;

namespace CotizacionService.Models
{
   public class CrearCotizacionRequest : RequestBase
   {
      public Cliente Cliente { get; set; }
      public Int64 ProductoId { get; set; }
      public Int64 ProveedorId { get; set; }
      public Decimal valorCotizacion { get; set; }
   }
}