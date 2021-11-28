using System.Collections.Generic;
using Busqo.Base;

namespace CotizacionService.Models
{
   public class ProvedorResponse : ResponseBase
   {
      public List<ProvedorInfo> Proveedores { get; set; }
   }
}