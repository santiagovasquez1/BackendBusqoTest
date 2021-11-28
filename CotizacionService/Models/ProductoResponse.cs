using System.Collections.Generic;
using Busqo.Base;

namespace CotizacionService.Models
{
   public class ProductoResponse:ResponseBase
   {
      public List<ProductoInfo> Productos { get; set; }
   }
}