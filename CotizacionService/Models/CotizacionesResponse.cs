using System.Collections.Generic;
using Busqo.Base;

namespace CotizacionService.Models
{
   public class CotizacionesResponse : ResponseBase
   {
      public List<CotizacionInfo> Cotizaciones { get; set; }
   }
}