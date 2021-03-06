/*
 * MIT License
 *
 * Copyright (c) Evgeny Nazarchuk.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Playwright.Synchronous;

namespace Playwright.FluentAssertions.Tests;

public class ElementHandleTests : PageTest
{
    [SetUp]
    public void SetUp()
    {
        Page.SetContent(@"
<html>
    <body style='color:#F3F3F3;font-family:monospace'>
        <div disable></div>
    </body>
</html>
");
    }

    [Test]
    public void HaveAttributeWhenCorrect()
    {
        Page.QuerySelector("div")!
            .Should().HaveAttribute("disable");
    }

    [Test]
    public void HaveAttributeWhenIncorrectValue()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("div")!
                .Should().HaveAttribute("enable");
        });

        Assert.AreEqual(@"
HaveAttribute Assert Exception
Expected attribute name: enable
Because: no reason given
", ex?.Message);
    }

    [Test]
    public void HaveNotAttributeWhenCorrect()
    {
        Page.QuerySelector("div")!
            .Should().HaveNotAttribute("enable");
    }

    [Test]
    public void HaveNotAttributeWhenAttributeExist()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("div")!
                .Should().HaveNotAttribute("disable");
        });

        Assert.AreEqual(@"
HaveNotAttribute Assert Exception
Not expected attribute name: disable
Because: no reason given
", ex?.Message);
    }

    [Test]
    public void HaveAttributeValueWhenCorrectEmptyValue()
    {
        Page.QuerySelector("div")!
            .Should().HaveAttributeValue("disable", "");
    }

    [Test]
    public void HaveAttributeValueWhenCorrectValue()
    {
        Page.QuerySelector("body")!
            .Should().HaveAttributeValue("style", "color:#F3F3F3;font-family:monospace");
    }

    [Test]
    public void HaveAttributeValueWhenAttributeNotFound()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("div")!
                .Should().HaveAttributeValue("notexist", "");
        });

        Assert.AreEqual("Attribute not found. Attibute name: notexist", ex?.Message);
    }

    [Test]
    public void HaveAttributeValueWhenIncorrectValue()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("body")!
                .Should().HaveAttributeValue("style", "color:#F1F1F1");
        });

        Assert.AreEqual(@"
HaveAttributeValue Assert Exception
Attribute name: style
Expected attribute value: color:#F1F1F1
Actual attribute value: color:#F3F3F3;font-family:monospace
Because: no reason given
", ex?.Message);
    }

    [Test]
    public void HaveComputedStyleWhenCorrect()
    {
        Page.QuerySelector("body")!
            .Should().HaveComputedStyle("color", "rgb(243, 243, 243)")
            .Should().HaveComputedStyle("fontFamily", "monospace");
    }

    [Test]
    public void HaveComputedStyleWhenIncorrectValue()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("body")!
                .Should().HaveComputedStyle("fontFamily", "Arial");
        });

        Assert.AreEqual(@"
HaveComputedStyle Assert Exception
Style name: fontFamily
Expected style value: Arial
Actual style value: monospace
Because: no reason given
", ex?.Message);
    }

    [Test]
    public void HaveComputedStyleWhenStyleNotFound()
    {
        var ex = Assert.Catch(() =>
        {
            Page.QuerySelector("body")!
                .Should().HaveComputedStyle("center", "");
        });

        Assert.AreEqual("Style not found. Style name: center", ex?.Message);
    }
}