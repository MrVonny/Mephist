using Mephist.Models;
using Mephist.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Mephist
{
    public class EducationalMaterial
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        [StringLength(4000)]
        public string Description { get; set; }
        public string Subject { get; set; }
        public EducationalMaterialType Type { get; set; }
        public virtual List<Media> Materials { get; set; }

        public string GetTypeName()
        {
            var value = Type;
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

    }
}