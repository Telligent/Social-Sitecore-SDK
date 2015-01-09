using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Sitecore.Configuration;

namespace Zimbra.Social.RemotingSDK.Sitecore
{
    public static class ExtensionsForXmlNode
    {
        private static readonly XmlNode _defaultNode = Factory.GetConfigNode("Zimbra/hosts");

        /// <summary>
        /// Will try and read an attribute value from the given node, if not present it falls back to the parent node, and finally to the finalValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public static string GetStringAttributeValueOrDefault(this XmlNode node,string attributeName,string finalValue)
        {
            var value = getValue(node, attributeName);
            if (value == null)
                return finalValue;
            return value;
        }

        /// <summary>
        /// Will try and read an attribute value from the given node, if not present it falls back to the parent node, and finally to the finalValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public static int? GetIntAttributeValueOrDefault(this XmlNode node, string attributeName, int? finalValue)
        {
            var value = getValue(node, attributeName);
            if (value == null)
                return finalValue;

            int val = -1;
            if (int.TryParse(value, out val))
                return val;

            return finalValue;
        }

        /// <summary>
        /// Will try and read an attribute value from the given node, if not present it falls back to the parent node, and finally to the finalValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public static Guid? GetGuidAttributeValueOrDefault(this XmlNode node, string attributeName, Guid? finalValue)
        {
            var value = getValue(node, attributeName);
            if (value == null)
                return finalValue;

            Guid val;
            if (Guid.TryParse(value, out val))
                return val;

            return finalValue;
        }

        /// <summary>
        /// Will try and read an attribute value from the given node, if not present it falls back to the parent node, and finally to the finalValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public static bool GetBoolAttributeValueOrDefault(this XmlNode node, string attributeName, bool finalValue)
        {
            var value = getValue(node, attributeName);
            if (value == null)
                return finalValue;

            bool val;
            if (bool.TryParse(value, out val))
                return val;

            return finalValue;
        }
        private static string getValue(XmlNode node,string attrName)
        {
            var attr = node.Attributes[attrName];
            if (attr == null)
                attr = _defaultNode.Attributes[attrName];

            if (attr == null)
                return null;

            return attr.Value;
        }
    }
}
