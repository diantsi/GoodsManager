using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.Common
{

    //// A record to hold the pair of the actual Enum value and its user-friendly string representation.
    public sealed record EnumWithName<TEnum>(TEnum Value, string DisplayName) where TEnum : struct, Enum;

        public static class EnumExtensions
        {

        /// <summary>
        /// Retrieves the custom string from the [Display(Name = "...")] attribute attached to an Enum field.
        /// If the attribute is missing, it falls back to the default field name.
        /// </summary>
            public static string GetDisplayName(this Enum value)
            {
                var type = value.GetType();
                var name = Enum.GetName(type, value);
                if (name is null) return value.ToString();

                var field = type.GetField(name);
                var display = field?.GetCustomAttribute<DisplayAttribute>();

                return display?.Name ?? name;
            }

            public static EnumWithName<TEnum> GetEnumWithName<TEnum>(this TEnum value) where TEnum : struct, Enum
            {
                return new EnumWithName<TEnum>(value, value.GetDisplayName());
            }

        /// <summary>
        /// Scans an entire Enum type and returns an array of EnumWithName records.
        /// </summary>
        public static EnumWithName<TEnum>[] GetValueWithNames<TEnum>() where TEnum : struct, Enum
            {
                var values = Enum.GetValues<TEnum>();
                var result = new EnumWithName<TEnum>[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    result[i] = values[i].GetEnumWithName();
                }
                return result;
            }
        }
    }

