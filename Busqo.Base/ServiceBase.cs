using System;

namespace Busqo.Base
{
   public abstract class ServiceBase
   {
      public abstract ResponseBase Execute();
      public abstract ResponseBase Execute(RequestBase request);
      public virtual String ServiceName()
      {
         return this.GetType().Name;
      }
   }
}