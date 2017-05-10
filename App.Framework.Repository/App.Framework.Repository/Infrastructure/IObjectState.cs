using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace App.Framework.Repository.Infrastructure
{
    public interface IObjectState
    {
        [NotMapped]
        [XmlIgnore]
        ObjectState ObjectState { get; set; }
        bool Deleted { get; set; }
        bool Active { get; set; }
        DateTime CreatedDate { get; set; }
        Nullable<DateTime> EditedDate { get; set; }
        Nullable<DateTime> DeletedDate { get; set; }
    }
}
