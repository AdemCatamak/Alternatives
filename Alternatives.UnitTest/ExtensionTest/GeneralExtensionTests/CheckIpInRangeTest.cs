﻿using System.Net;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class CheckIpInRangeTest
    {
        [Test]
        public void CheckIpInRange_TestExecuteWithDifferentCase()
        {
            bool result;

            result = IPAddress.Parse("0.0.0.0").CheckIpInRange(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("0.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("0.0.0.0").CheckIpInRange(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("125.0.0.0").CheckIpInRange(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("60.0.0.0").CheckIpInRange(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("0.0.0.1").CheckIpInRange(IPAddress.Parse("0.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("100.0.0.1").CheckIpInRange(IPAddress.Parse("100.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsTrue(result);

            result = IPAddress.Parse("0.0.0.0").CheckIpInRange(IPAddress.Parse("100.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsFalse(result);

            result = IPAddress.Parse("130.0.0.0").CheckIpInRange(IPAddress.Parse("100.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsFalse(result);

            result = IPAddress.Parse("125.0.0.1").CheckIpInRange(IPAddress.Parse("100.0.0.0"), IPAddress.Parse("125.0.0.0"));
            Assert.IsFalse(result);
        }
    }
}