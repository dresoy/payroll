using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Utilities
{
    public static class StringManager
    {

        public static string UpperOnlyFirstLetter(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            List<char> specialCharacters = new List<char>() { ' ', ',', '.', ':' };

            for (int i = 1; i < array.Length; i++)
            {
                if (specialCharacters.Contains(array[i - 1]))
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);


        }

    }
}