using OpenQA.Selenium;

namespace Automov.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAviator
    {
        /// <summary>
        /// This method will perform single action according to given 'IActionSegment'.
        /// <example>
        /// For example:
        /// <code>
        /// var actionSegment = new ActionSegment
        /// {
        ///     SelectorType = SelectorType.Id,
        ///     SelectorText = "btnSubmit",
        ///     Result = new ValueSegment { } //Optional! When you have expected result.
        /// }
        /// 
        /// _aviator.Operative(actionSegment);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="actionSegment"></param>
        /// <returns></returns>
        IWebElement Operative(IActionSegment actionSegment);

        /// <summary>
        /// This method will perform multiple actions according to given list of 'IActionSegment'.
        /// <example>
        /// For example:
        /// <code>
        /// var actionSegmentList = new List()
        /// {
        ///     new ActionSegment
        ///     {
        ///         SelectorType = SelectorType.Id,
        ///         SelectorText = "btnSubmit",
        ///         Result = new ValueSegment { } //Optional! When you have expected result.
        ///     },
        ///     more...
        /// }
        /// 
        /// _aviator.Operative(actionSegmentList);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="actionSegments"></param>
        /// <returns></returns>
        IWebElement Operative(List<IActionSegment> actionSegments);

        /// <summary>
        /// This method will perform actions according to given url and list of 'IActionSegment'.
        /// <example>
        /// For example:
        /// <code>
        /// var url = "http://localhost:5001/Account/Login"
        /// 
        /// var actionSegmentList = new List()
        /// {
        ///     new ActionSegment
        ///     {
        ///         SelectorType = SelectorType.Id,
        ///         SelectorText = "btnSubmit",
        ///         Result = new ValueSegment { } //Optional! When you have expected result.
        ///     },
        ///     more...
        /// }
        /// 
        /// _aviator.Operative(url, actionSegmentList);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="navigateURL"></param>
        /// <param name="actionSegments"></param>
        /// <returns></returns>
        IWebElement Operative(string navigateURL, List<IActionSegment> actionSegments);

        /// <summary>
        /// This method will insert values according to given list of 'IValueSegment'.
        /// <example>
        /// For example:
        /// <code>
        /// var valueSegmentList = new List()
        /// {
        ///     new ValueSegment
        ///     {
        ///         SelectorType = SelectorType.Id, // Default: 'SelectorType.Id'
        ///         SelectorText = "Email",
        ///         Value = "admin@gmail.com",
        ///         InputType = InputType.Textbox // Default: 'InputType.Textbox'
        ///     },
        ///     more...
        /// }
        /// 
        /// _aviator.Operative(valueSegmentList);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="valueSegments"></param>
        /// <returns></returns>
        IWebElement Operative(List<IValueSegment> valueSegments);

        /// <summary>
        /// This method will insert values according to given list of 'IValueSegment' and url.
        /// <example>
        /// For example:
        /// <code>
        /// var url = "http://localhost:5001/Account/Login"
        /// 
        /// var valueSegments = new List()
        /// {
        ///     new ValueSegment
        ///     {
        ///         SelectorType = SelectorType.Id, // Default: 'SelectorType.Id'
        ///         SelectorText = "Email",
        ///         Value = "admin@gmail.com",
        ///         InputType = InputType.Textbox // Default: 'InputType.Textbox'
        ///     }
        ///     more...
        /// }
        /// 
        /// _aviator.Operative(url, valueSegments);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="navigateURL"></param>
        /// <param name="valueSegments"></param>
        /// <returns></returns>
        IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments);

        /// <summary>
        /// This method will insert values and perform action in given url according to list of 'IValueSegment' and 'IActionSegment'.
        /// <example>
        /// For example:
        /// <code>
        /// var url = "http://localhost:5001/Account/Login"
        /// 
        /// var valueSegmentList = new List()
        /// {
        ///     new ValueSegment
        ///     {
        ///         SelectorType = SelectorType.Id, // Default: 'SelectorType.Id'
        ///         SelectorText = "Email",
        ///         Value = "admin@gmail.com",
        ///         InputType = InputType.Textbox // Default: 'InputType.Textbox'
        ///     },
        ///     more...
        /// }
        /// 
        /// var actionSegment = new ActionSegment
        /// {
        ///     SelectorType = SelectorType.Id,
        ///     SelectorText = "btnSubmit",
        ///     Result = new ValueSegment { } //Optional! When you have expected result.
        /// }
        /// 
        /// _aviator.Operative(url, valueSegmentList, actionSegment);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="navigateURL"></param>
        /// <param name="valueSegments"></param>
        /// <param name="actionSegment"></param>
        /// <returns></returns>
        IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment);
    }
}
