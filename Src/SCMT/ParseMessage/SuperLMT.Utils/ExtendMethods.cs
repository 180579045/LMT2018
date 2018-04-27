// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendMethods.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the ExtendMethods type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// The extend methods.
    /// </summary>
    public static class ExtendMethods
    {
        /// <summary>
        /// The get description.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
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
