using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protractor;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace WebExtensions
{
    public class ByExtension : By
    {
        /// <summary>
        /// This is only for Angular applications as it's using the prefixes for angular binding
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="attribute"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static By Find(string tagName, string attribute, string searchString)
        {
            string find = @"
        var using = arguments[0] || document;
        var tagName = arguments[1];
        var identifier = arguments[2];
        var searchText = arguments[3];
        var prefixes = ['ng-', 'ng_', 'data-ng-', 'x-ng-', 'ng\\:', ''];
        for (var p = 0; p < prefixes.length; ++p) 
        {
            var selector =  tagName + '[' + prefixes[p] + identifier + '=""' + searchText + '""]';
            var inputs = using.querySelectorAll(selector);
            if (inputs.length) 
            {
                return inputs;
            }
        }";
            //return By;
            return new JavaScriptBy(find, tagName.ToString(), attribute, searchString);
        }

        public static By LabelText(string text)
        {
            string FindByLabelText =
          "var using = arguments[0]  || document; " +
          "var searchText = arguments[1];" +
          "var elements = using.querySelectorAll('label');" +
          "var matches = [];  " +
          "for (var i = 0; i < elements.length; ++i) {    " +
          "var element = elements[i];    " +
          "var elementText;    " +
          "if (element.tagName.toLowerCase() == 'label') {      " +
          "elementText = element.innerText || element.textContent;    } " +
          "else {      elementText = element.value;    }    " +
          "if (elementText.trim() === searchText) {      " +
          "matches.push(element);    }  }  return matches; ";

            return new JavaScriptBy(FindByLabelText, text);
        }

        public static By Button(string buttonText)
        {
            string FindButton =
        "var using = arguments[0]  || document; var searchText = arguments[1];" +
        "var elements = using.querySelectorAll('button, input[type=\"button\"], " +
        "input[type=\"submit\"]');  " +
        "var matches = [];  " +
        "for (var i = 0; i < elements.length; ++i) {    " +
        "var element = elements[i];    " +
        "var elementText;    " +
        "if (element.tagName.toLowerCase() == 'button') {      " +
        "elementText = element.innerText || element.textContent;    } " +
        "else {      elementText = element.value;    }    " +
        "if (elementText.trim() === searchText) {      " +
        "matches.push(element);    }  }  return matches; ";

            return new JavaScriptBy(FindButton, buttonText);
        }

        public static By ButtonPartialText(string buttonText)
        {
            string FindButtonByPartialText =
        "var using = arguments[0]  || document; var searchText = arguments[1];" +
        "var elements = using.querySelectorAll('button, input[type=\"button\"], " +
        "input[type=\"submit\"]');  " +
        "var matches = [];  " +
        "for (var i = 0; i < elements.length; ++i) {    " +
        "var element = elements[i];    " +
        "var elementText;    " +
        "if (element.tagName.toLowerCase() == 'button') {      " +
        "elementText = element.innerText || element.textContent;    } " +
        "else {      elementText = element.value;    }    " +
        "if (elementText.indexOf(searchText)  > -1) {      " +
        "matches.push(element);    }  }  return matches; ";

            return new JavaScriptBy(FindButtonByPartialText, buttonText);
        }
    }
}
