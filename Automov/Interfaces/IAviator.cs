﻿using OpenQA.Selenium;

namespace Automov.Interfaces
{
    public interface IAviator
    {
        IWebElement Operative(IActionSegment actionSegment);
        IWebElement Operative(List<IActionSegment> actionSegments);
        IWebElement Operative(string navigateURL, List<IActionSegment> actionSegments);
        IWebElement Operative(List<IValueSegment> valueSegments);
        IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments);
        IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment);
    }
}
