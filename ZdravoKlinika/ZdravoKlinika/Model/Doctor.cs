
using System;

namespace ZdravoKlinika.Model
{
   public class Doctor : User
   {
      public string specialization{ get; set;}
      public string? roomId{ get; set;}
   
   }
}