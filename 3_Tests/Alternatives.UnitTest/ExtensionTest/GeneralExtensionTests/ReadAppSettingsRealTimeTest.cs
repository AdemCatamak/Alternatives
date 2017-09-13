using System;
using System.Collections.Generic;
using Alternatives.Extensions;
using NUnit.Framework;

namespace Alternatives.UnitTest.ExtensionTest.GeneralExtensionTests
{
    public class ReadAppSettingsRealTimeTest
    {
        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ReadConfigValueRealTime_NullTest()
        {
            Assert.Throws<ArgumentNullException>(() => GeneralExtensions.ReadAppSettingsRealTime<string>(null));
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ReadConfigValueRealTime_NotExistAppSettingsTest()
        {
            Assert.Throws<KeyNotFoundException>(() => GeneralExtensions.ReadAppSettingsRealTime<string>("NotExistConfigKey"));
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ReadConfigValueRealTime_ExistAppSettingsTest_NotConvertableType()
        {
            Assert.Throws<FormatException>(() => GeneralExtensions.ReadAppSettingsRealTime<int>("TestConfigKey"));
        }

        [Test]
        public void Alternatives_UnitTest_ExtensionsTest__ReadConfigValueRealTime_ExistAppSettingsTest_ValidType()
        {
            string value = GeneralExtensions.ReadAppSettingsRealTime<string>("TestConfigKey");

            Assert.AreEqual("test", value);
        }
    }
}