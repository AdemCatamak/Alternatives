using System;
using System.Collections.Generic;
using System.Data;
using Alternatives.UnitTest.ExtensionsTestClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alternatives.UnitTest.ExtensionTest
{
    [TestClass]
    public class ConvertToDataTableTest
    {
        [TestMethod]
        public void Alternatives_UnitTest_ExtensionsTest__ToDataTable()
        {
            DataTable expected = new DataTable()
                                 {
                                     Columns =
                                     {
                                         new DataColumn
                                         {
                                             DataType = typeof(int),
                                             ColumnName = "Id"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Username"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Email"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "Phone"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(string),
                                             ColumnName = "RequiredPhone"
                                         },
                                         new DataColumn
                                         {
                                             DataType = typeof(int),
                                             ColumnName = "ExtraData"
                                         }
                                     }
                                 };
            DataRow dataRow0 = expected.NewRow();

            dataRow0["Id"] = 1;
            dataRow0["Username"] = "catamak";
            dataRow0["Email"] = "adem.catamak@gmail.com";
            dataRow0["Phone"] = "666 666 66 66";
            dataRow0["ExtraData"] = DBNull.Value;
            expected.Rows.Add(dataRow0);

            DataRow dataRow1 = expected.NewRow();

            dataRow1["Id"] = 5;
            dataRow1["Username"] = "ademcatamak";
            dataRow1["Email"] = "ademcatamak@gmail.com";
            dataRow1["Phone"] = "555 555 55 55";
            dataRow1["ExtraData"] = 12;
            expected.Rows.Add(dataRow1);

            List<DataTableTestClass> dataList = new List<DataTableTestClass>
                                                {
                                                    new DataTableTestClass
                                                    {
                                                        Id = 1,
                                                        Username = "catamak",
                                                        Email = "adem.catamak@gmail.com",
                                                        Phone = "666 666 66 66",
                                                        ExtraData = null
                                                    },
                                                    new DataTableTestClass
                                                    {
                                                        Id = 5,
                                                        Username = "ademcatamak",
                                                        Email = "ademcatamak@gmail.com",
                                                        Phone = "555 555 55 55",
                                                        ExtraData = 12
                                                    }
                                                };


            DataTable actual = dataList.ToDataTable();

            for (int i = 0; i < actual.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"], "Actual.Id is not expected");
                Assert.AreEqual(expected.Rows[i]["Username"], actual.Rows[i]["Username"], "Actual.Username is not expected");
                Assert.AreEqual(expected.Rows[i]["Email"], actual.Rows[i]["Email"], "Actual.Email is not expected");
                Assert.AreEqual(expected.Rows[i]["Phone"], actual.Rows[i]["Phone"], "Actual.Phone is not expected");
                Assert.AreEqual(expected.Rows[i]["ExtraData"], actual.Rows[i]["ExtraData"], "Actual.ExtraData is not expected");
            }
        }
    }
}