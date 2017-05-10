using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using App.Framework.Repository.Infrastructure;

namespace App.Framework.Repository.Entity
{
    public abstract class Entity : IObjectState
    {
        public Entity()
        {
            this.Active = true;
            this.Deleted = false;
            this.EditedDate = null;
            this.DeletedDate = null;
            this.CreatedDate = DateTime.Now;
        }

        [NotMapped]
        [XmlIgnore]
        public ObjectState ObjectState { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
