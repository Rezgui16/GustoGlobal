using GustoLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GustoLib.Class.Validator
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string saisie = (string)value;
            var dbContext = validationContext.GetService(typeof(GustoDbContext)) as GustoDbContext;

            var propertyName = validationContext.MemberName;
            var classType = validationContext.ObjectType;

            //var tt = dbContext.Championships.Any(x => x.Name == saisie);


            if (dbContext != null)
            {
                //Principe de la Reflexion pour pouvoir utiliser le validateur sur n'importe quel class
                dynamic dbSet = dbContext.GetType()
                    .GetProperties()
                    .SingleOrDefault(x => x.PropertyType.FullName.Contains(classType.FullName));

                var query = (IQueryable)dbSet.GetValue(dbContext);
                foreach (var item in query)
                {
                    dynamic temp = Convert.ChangeType(item, classType);
                    if (saisie.Equals(temp.GetType().GetProperty(propertyName).GetValue(temp, null)))
                        return new ValidationResult("L'élément existe déjà.");
                }
                return ValidationResult.Success;

            }
            return new ValidationResult("Impossible de faire le test unique.");
        }
    }
}
