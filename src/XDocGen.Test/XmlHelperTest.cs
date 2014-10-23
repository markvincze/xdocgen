using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XDocGen.Common.Xml;

namespace XDocGen.Test
{
    [TestClass]
    public class XmlHelperTest
    {
        [TestMethod]
        public void GetMainParts_EmptyXml_NullsReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts("", out summary, out remarks, out returns);

            Assert.IsNull(summary);
            Assert.IsNull(remarks);
            Assert.IsNull(returns);
        }

        [TestMethod]
        public void GetMainParts_InvalidXml_NullsReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts("this is < not an >< xml}{", out summary, out remarks, out returns);

            Assert.IsNull(summary);
            Assert.IsNull(remarks);
            Assert.IsNull(returns);
        }

        [TestMethod]
        public void GetMainParts_XmlWithoutAny_NullsReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts(
@"<member name='M:MyNamespace.MyProject.MyClass.MyMethod(System.String,System.Int32,System.String,System.Action{System.Object,SmartCasco.Otp.startWorkflowSynchCompletedEventArgs},System.String,System.String,System.String)'>
    <param name='languageCode'>Milyen nyelvű legyen a fizető felület. Default 'hu'.</param>
    <param name='exchange'>Pénznem Default: HUF</param>
    <param name='shopComment'>Bolt kommentje ami kikerül az OTP-s fizetős felületre.</param>
</member>", out summary, out remarks, out returns);

            Assert.IsNull(summary);
            Assert.IsNull(remarks);
            Assert.IsNull(returns);
        }

        [TestMethod]
        public void GetMainParts_XmlWithOnlySummary_SummaryReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts(
@"<member name='M:MyNamespace.MyProject.MyClass.MyMethod(System.String,System.Int32,System.String,System.Action{System.Object,SmartCasco.Otp.startWorkflowSynchCompletedEventArgs},System.String,System.String,System.String)'>
    <summary>
    This is summary.
    </summary>
    <param name='transactionId'>Egyedi tranzakció azonosító</param>
    <param name='amount'>Fizetendő összeg</param>
    <param name='backUrl'>A felhasználót hova kell visszairányítani a fizetés végén.</param>
    <returns></returns>
</member>", out summary, out remarks, out returns);

            Assert.AreEqual("This is summary.", summary);
            Assert.IsNull(remarks);
        }

        [TestMethod]
        public void GetMainParts_XmlWithOnlyRemarks_RemarksReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts(
@"<member name='M:MyNamespace.MyProject.MyClass.MyMethod(System.String,System.Int32,System.String,System.Action{System.Object,SmartCasco.Otp.startWorkflowSynchCompletedEventArgs},System.String,System.String,System.String)'>
    <param name='transactionId'>Egyedi tranzakció azonosító</param>
    <param name='amount'>Fizetendő összeg</param>
    <param name='backUrl'>A felhasználót hova kell visszairányítani a fizetés végén.</param>
    <remarks>
    This is remarks.
    </remarks>
    <returns></returns>
</member>", out summary, out remarks, out returns);

            Assert.IsNull(summary);
            Assert.AreEqual("This is remarks.", remarks);
        }

        [TestMethod]
        public void GetMainParts_XmlWithOnlyReturns_ReturnsReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts(
@"<member name='M:MyNamespace.MyProject.MyClass.MyMethod(System.String,System.Int32,System.String,System.Action{System.Object,SmartCasco.Otp.startWorkflowSynchCompletedEventArgs},System.String,System.String,System.String)'>
    <param name='transactionId'>Egyedi tranzakció azonosító</param>
    <param name='amount'>Fizetendő összeg</param>
    <param name='backUrl'>A felhasználót hova kell visszairányítani a fizetés végén.</param>
    <returns>This is returns.</returns>
</member>", out summary, out remarks, out returns);

            Assert.IsNull(summary);
            Assert.IsNull(remarks);
            Assert.AreEqual("This is returns.", returns);
        }

        [TestMethod]
        public void GetMainParts_XmlWithAll_AllReturned()
        {
            string summary;
            string remarks;
            string returns;

            XmlHelper.GetMainParts(
@"<member name='M:MyNamespace.MyProject.MyClass.MyMethod(System.String,System.Int32,System.String,System.Action{System.Object,SmartCasco.Otp.startWorkflowSynchCompletedEventArgs},System.String,System.String,System.String)'>
    <summary>
    This is summary.
    </summary>
    <param name='transactionId'>Egyedi tranzakció azonosító</param>
    <param name='amount'>Fizetendő összeg</param>
    <param name='backUrl'>A felhasználót hova kell visszairányítani a fizetés végén.</param>
    <remarks>
    This is remarks.
    </remarks>
    <returns>This is returns.</returns>
</member>", out summary, out remarks, out returns);

            Assert.AreEqual("This is summary.", summary);
            Assert.AreEqual("This is remarks.", remarks);
            Assert.AreEqual("This is returns.", returns);
        }
    }
}