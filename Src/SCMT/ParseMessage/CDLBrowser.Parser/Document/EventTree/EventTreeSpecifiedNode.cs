// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeSpecifiedNode.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the EventTreeSpecifiedNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    /// <summary>
    /// The event tree q node.
    /// </summary>
    public class EventTreeSpecifiedNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventTreeSpecifiedNode));
        private  static MSScriptControl.ScriptControl script =  new MSScriptControl.ScriptControl { Language = "JavaScript" };

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return this.ConfigurationNode.GetAttribute("Describe") + " : " + this.ConvertedValue;
            }
        }

        /// <summary>
        /// Gets the value append unit.
        /// </summary>
        public string ConvertedValue
        {
            get
            {
                string functionContent = this.ConfigurationNode.GetAttribute("Method");
                string returnValue = this.DisplayValue(functionContent, this.Value);

                return returnValue.Substring(0, returnValue.Length - 1);
            } 
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public override IConfigNodeWrapper Clone()
        {
            return new EventTreeSpecifiedNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }

        /// <summary>
        /// The display value.
        /// </summary>
        /// <param name="functionContent">
        /// The function content.
        /// </param>
        /// <param name="targetValue">
        /// The target value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DisplayValue(string functionContent, object targetValue)
        {
            string stringValue = targetValue.ToString();

            if (stringValue.IndexOf(' ') == 0)
            {
                stringValue = stringValue.Substring(1);
            }

            string[] valueList = stringValue.Split(' ');

            string returnValue = null;

            foreach (var value in valueList)
            {
                returnValue += this.GetValueFromFunction(functionContent, value) + " ";
            }

            if (returnValue == null)
            {
                Log.Error("需要二次计算的数值结果为空");
                return null;
            }

            return returnValue;
        }

        /// <summary>
        /// The get function code.
        /// </summary>
        /// <param name="functionContent">
        /// The function content.
        /// </param>
        /// <param name="targetValue">
        /// The target value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetValueFromFunction(string functionContent, string targetValue)
        {
            var functionCode = functionContent.Replace("x", targetValue);
            var resultValue = script.Eval(functionCode);
            return resultValue.ToString("0.0000");
        }
    }
}
