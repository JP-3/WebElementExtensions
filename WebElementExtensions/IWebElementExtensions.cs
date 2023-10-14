using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebExtensions
{
    public static class IWebElementExtensions
    {
        public static void XEnterTextBox(this IWebElement element, string textToEnter)
        {
            element.Clear();
            element.SendKeys(textToEnter);
        }

        /// <summary>
        /// Need to pass in an ENUM to use this, which forces you to not have hardcoded values in your tests. 
        /// You can call it like this "XSelectCombobox<MyEnums>(MyEnums.Chicago.ToString));"
        /// If your enum has a space or special characters add it to your enum as such "[EnumMember(Value = "IT-7650")] IT7650 = 1,"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="valueToSelect"></param>
        public static void XSelectCombobox<T>(this IWebElement element, string valueToSelect) where T : struct, IConvertible
        {
            bool valueSet = false;
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var enumType = typeof(T);
            EnumMemberAttribute enumMemberAttribute = null;
            SelectElement clickThis;
            string value;
            foreach (var name in Enum.GetNames(enumType))
            {
                try
                {
                    enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                    value = enumMemberAttribute.Value;
                    value = Regex.Replace(value, @"[^A-Za-z0-9]+", "");
                    if (value == valueToSelect)
                    {
                        clickThis = new SelectElement(element);
                        clickThis.SelectByText(enumMemberAttribute.Value);
                        valueSet = true;
                        break;
                    }
                }
                catch { }
            }

            //If value hasn't been set it must not have attribute
            if (!valueSet)
            {
                clickThis = new SelectElement(element);
                clickThis.SelectByText(valueToSelect);
            }
        }

        public static void XEnterComboSimple(this IWebElement element, string valueToSelect)
        {
            SelectElement clickThis = new SelectElement(element);
            clickThis.SelectByText(valueToSelect);
        }


        public static void XCompareComboValues(this IList<IWebElement> elements, List<string> baseList)
        {
            for (int i = 0; i < baseList.Count; i++)
            {
                int b = string.Compare(elements[i].Text, baseList[i]);
            }
        }
    }
}

