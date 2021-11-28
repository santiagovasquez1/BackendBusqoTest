using System;
using Newtonsoft.Json;

namespace CotizacionService.Models
{
   public class CotizacionInfo
   {
      [JsonProperty("id")]
      public Int64 Id { get; set; }

      [JsonProperty("valorCotizacion")]
      public Decimal Valor_Cotizacion { get; set; }
      [JsonProperty("nombre")]
      public String Nombre { get; set; }
      [JsonProperty("apellido")]
      public String Apellido { get; set; }
      [JsonProperty("email")]
      public String Email { get; set; }
      [JsonProperty("celular")]
      public String Celular { get; set; }
      [JsonProperty("cedula")]
      public Int64 Cedula { get; set; }
      [JsonProperty("placa")]
      public String Placa { get; set; }
      [JsonProperty("producto")]
      public String Producto { get; set; }
      [JsonProperty("proveedor")]
      public String Proveedor { get; set; }
   }
}