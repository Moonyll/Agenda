//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agenda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int idAppointment { get; set; }
        public Nullable<System.DateTime> DateHour { get; set; }
        public int idBroker { get; set; }
        public int idCustomer { get; set; }
        //public DateTime d;
        //public DateTime t;

        public virtual Broker Broker { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
