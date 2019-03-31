using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eshop.Models.Core
{
    public abstract class ModelBase<T>
    {
        public static bool Map(T dbEntity, T newEntity)
        {
            bool different = true;
            Type type = typeof(T);

            foreach (PropertyInfo property in type.GetProperties())
            {
                // Ne checke pas l'id, ni le salt et hashedpassword par cette méthode
                if(!property.Name.EndsWith("Id") && !property.Name.Equals("ASalt") && !property.Name.Equals("APassword"))
                {
                    var DbValue = property.GetValue(dbEntity);
                    var NewValue = property.GetValue(newEntity);

                    // DbValue == null => null.Equals(xxx) => genere une exception
                    if (DbValue == null && NewValue == null)
                        continue;
                    if ((DbValue == null && NewValue != null) || !DbValue.Equals(NewValue))
                    {
                        different = true;
                        property.SetValue(dbEntity, NewValue);
                    }
                }
            }

            return different;
        }
    }
}
