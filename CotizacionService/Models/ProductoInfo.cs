using System;
using Newtonsoft.Json;

namespace CotizacionService.Models
{
   public class ProductoInfo
   {
      [JsonProperty("id")]
      public Int64 Id { get; set; }
      [JsonProperty("nombre")]
      public String Nombre { get; set; }
   }
}