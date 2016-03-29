using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;

using System.Collections.Generic;

//Kabir Sandhu - u0409658
//Date Modified: Oct 1, 2015
namespace SpreadsheetTester
{
    /// <summary>
    /// Tester for Spreadsheet Class
    /// </summary>
    [TestClass]
    public class SpreadsheetTester
    {
        /// <summary>
        /// Test for spreadsheet constructor
        /// </summary>
        [TestMethod]
        public void public_spreadsheetConstructorTest()
        {
            Spreadsheet s = new Spreadsheet();
            List<string> cells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(0, cells.Count);
        }

        /// <summary>
        /// Test for getting nonEmptyCells method
        /// </summary>
        [TestMethod]
        public void public_getNamesOfAllNonemptyCellsTest()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 15);
            s.SetCellContents("B3", "hello");
            s.SetCellContents("C2", new Formula("5+ 7"));
            List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(3, nonEmptyCells.Count);
            Assert.IsTrue(nonEmptyCells.Contains("A1"));
            Assert.IsTrue(nonEmptyCells.Contains("B3"));
            Assert.IsTrue(nonEmptyCells.Contains("C2"));
        }

        /// <summary>
        /// Test for getting nonEmptyCells method
        /// </summary>
        [TestMethod]
        public void public_getNamesofAllNonemptyCellsTest2()
        {
            Spreadsheet s = new Spreadsheet();
            List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(0, nonEmptyCells.Count);

            s.SetCellContents("A1", 25);
            s.SetCellContents("B3", "hello, world");
            s.SetCellContents("C2", new Formula("5+ 7 / (10 + 19)"));

            nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(3, nonEmptyCells.Count);

            s.SetCellContents("A1", ""); //This should cause the cell to be empty
            s.SetCellContents("B3", 10.2); //Changing value shouldn't create a new cell
            nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(2, nonEmptyCells.Count);
        }

        /// <summary>
        /// Test InvalidNameException when null is passed to GetCellContents
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_getCellContentsTest1()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        /// <summary>
        /// Test for InvalidNameException when invalid cell name is passed to getCellContents
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_getCellContentsTest2()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("$E3");
        }

        /// <summary>
        /// Test for setting cell contents with a string
        /// </summary>
        [TestMethod]
        public void public_setCellContentString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("XYZ", "hello, world");
            s.SetCellContents("xyz", "goodbye");
            s.SetCellContents("AbC", "data");
            Assert.AreEqual("hello, world", s.GetCellContents("XYZ"));
            Assert.AreEqual("goodbye", s.GetCellContents("xyz"));
            Assert.AreEqual("data", s.GetCellContents("AbC"));
            Assert.AreEqual("", s.GetCellContents("abc"));
        }

        /// <summary>
        /// Test for ArgumentNullException when trying to set a cell to a null string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void public_setCellContentStringTest2()
        {
            Spreadsheet s = new Spreadsheet();
            string text = null;
            s.SetCellContents("A1", text);
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a cell with an invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentStringTest3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("13_B", "hello");
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a null cell
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentStringTest4()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, "hello");
        }

        /// <summary>
        /// Test for setting a cell's content to a double
        /// </summary>
        [TestMethod]
        public void public_setCellContentDoubleTest()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A123", 15.25);
            s.SetCellContents("B_2", 5.00);
            s.SetCellContents("_aa34", 1e5);

            Assert.AreEqual(15.25, s.GetCellContents("A123"));
            Assert.AreEqual(5.0, s.GetCellContents("B_2"));
            Assert.AreEqual(100000.0, s.GetCellContents("_aa34"));

            s.SetCellContents("A123", 12.45); //Make sure existing cell is modified, instead of new one being created
            Assert.AreEqual(12.45, s.GetCellContents("A123"));
            List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(3, nonEmptyCells.Count);
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a cell with an invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentDoubleTest2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A#", 13.0);
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a null cell
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentDoubleTest3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, 13.0);
        }

        /// <summary>
        /// Test for setting a cell's contents to a formula
        /// </summary>
        [TestMethod]
        public void public_setCellContentFormulaTest()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B45", new Formula("45 * 10 / 2"));
            s.SetCellContents("QWERTY", new Formula("B45 + 100"));
            s.SetCellContents("M0", new Formula("3 + A2 - (100 + 43 * 1e3)"));
            Formula f1 = new Formula("45 * 10 / 2");
            Formula f2 = new Formula("B45 + 100");
            Formula f3 = new Formula("3 + A2 - (100 + 43 * 1e3)");

            Assert.IsTrue(s.GetCellContents("B45") is Formula);
            Assert.IsTrue(s.GetCellContents("QWERTY") is Formula);
            Assert.IsTrue(s.GetCellContents("M0") is Formula);

            Assert.AreEqual(f1, s.GetCellContents("B45"));
            Assert.AreEqual(f2, s.GetCellContents("QWERTY"));
            Assert.AreEqual(f3, s.GetCellContents("M0"));

            s.SetCellContents("B45", new Formula("12 + 2"));
            Assert.AreNotEqual(f1, s.GetCellContents("B45"));
        }

        /// <summary>
        /// Test for CircularException when circular dependency occurs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void public_setCellContentFormulaTest2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A3 + 2"));
            s.SetCellContents("A2", 3.5);
            s.SetCellContents("A3", new Formula("A1 + 3 - 5"));
            s.SetCellContents("A4", new Formula("A5 + A1 + 5"));
            s.SetCellContents("A5", new Formula("A4 + 3"));
        }

        /// <summary>
        /// Test for CircularException when circular dependency occurs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void public_setCellContentFormulaTest3()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A1 + A8"));
            s.SetCellContents("A2", 3.5);
            s.SetCellContents("A3", new Formula("A2 + A1"));
            s.SetCellContents("A4", 12);
            s.SetCellContents("A5", new Formula("12 + A3"));
            s.SetCellContents("A6", new Formula("A5 + A4"));
            s.SetCellContents("A7", new Formula("A5 - 2"));
            s.SetCellContents("A8", new Formula("A7 + 2"));
        }

        /// <summary>
        /// Test for ArgumentNullException when trying to set a cell to a null formula
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void public_setCellContentFormulaTest4()
        {
            Spreadsheet s = new Spreadsheet();
            Formula f = null;
            s.SetCellContents("A1", f);
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a null cell
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentFormulaTest5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, new Formula("3 + 5"));
        }

        /// <summary>
        /// Test for InvalidNameException when trying to set a cell with an invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void public_setCellContentFormulaTest6()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("AA$2", new Formula("3 + 5"));
        }

        /// <summary>
        /// Test for getting the direct dependents of a cell
        /// </summary>
        [TestMethod]
        public void protected_getDirectDependentsTest()
        {
            Spreadsheet s = new Spreadsheet();
            PrivateObject s_private = new PrivateObject(s);
            s.SetCellContents("A1", new Formula("A2 + A3"));
            s.SetCellContents("A2", new Formula("5 + A3"));
            s.SetCellContents("A3", 15.5);


            List<string> A1_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A1");
            List<string> A2_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A2");
            List<string> A3_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A3");

            Assert.AreEqual(0, A1_Dependents.Count);
            Assert.AreEqual(1, A2_Dependents.Count);
            Assert.AreEqual(2, A3_Dependents.Count);
        }

        /// <summary>
        /// Test for getting the direct dependents of a cell
        /// </summary>
        [TestMethod]
        public void protected_getDirectDependentsTest2()
        {
            Spreadsheet s = new Spreadsheet();
            PrivateObject s_private = new PrivateObject(s);
            s.SetCellContents("A1", new Formula("A2 + A3"));
            s.SetCellContents("A2", new Formula("5 + A3"));
            s.SetCellContents("A3", 15.5);

            s.SetCellContents("A1", 1002.3);


            List<string> A1_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A1");
            List<string> A2_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A2");
            List<string> A3_Dependents = (List<string>)s_private.Invoke("GetDirectDependents", "A3");

            Assert.AreEqual(0, A1_Dependents.Count);
            Assert.AreEqual(0, A2_Dependents.Count);
            Assert.AreEqual(1, A3_Dependents.Count);
        }

        /// <summary>
        /// Test for ArgumentNullException when trying to get dependents of a null cell
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void protected_getDirectDependentsTest3()
        {
            Spreadsheet s = new Spreadsheet();
            PrivateObject s_private = new PrivateObject(s);
            string name = null;
            s_private.Invoke("GetDirectDependents", name);
        }

        /// <summary>
        /// Test for InvalidNameException when trying to get the dependents of a cell with an invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void protected_getDirectDependentsTest4()
        {
            Spreadsheet s = new Spreadsheet();
            PrivateObject s_private = new PrivateObject(s);
            s_private.Invoke("GetDirectDependents", "&1");
        }

        /// <summary>
        /// Stress test to ensure underlying data structure can handle a large amount of cells
        /// </summary>
        [TestMethod]
        public void stressTest()
        {
            Spreadsheet s = new Spreadsheet();

            for (int i = 0; i < 1000; i++)
            {
                s.SetCellContents("A" + i, 1.0 + i);
                s.SetCellContents("B" + i, "#" + i);
                s.SetCellContents("C" + i, new Formula("A1" + " + 10"));
            }

            List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(3000, nonEmptyCells.Count);


            for (int i = 0; i < 1000; i++)
            {
                s.SetCellContents("A" + i, "hello");
            }

            nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(3000, nonEmptyCells.Count);

            for (int i = 0; i < 1000; i++)
            {
                s.SetCellContents("A" + i, "");
                s.SetCellContents("D" + 1, "");
            }

            nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(2000, nonEmptyCells.Count);

        }

        //------------Grading tests---------------//

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        // EMPTY SPREADSHEETS
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test1()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test2()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("1AA");
        }

        [TestMethod()]
        public void Test3()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("A2"));
        }

        // SETTING CELL TO A DOUBLE
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test4()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, 1.5);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("1A1A", 1.5);
        }

        [TestMethod()]
        public void Test6()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", 1.5);
            Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        }

        // SETTING CELL TO A STRING
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test7()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A8", (string)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test8()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, "hello");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test9()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("1AZ", "hello");
        }

        [TestMethod()]
        public void Test10()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", "hello");
            Assert.AreEqual("hello", s.GetCellContents("Z7"));
        }

        // SETTING CELL TO A FORMULA
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test11()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A8", (Formula)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test12()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents(null, new Formula("2"));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test13()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("1AZ", new Formula("2"));
        }

        [TestMethod()]
        public void Test14()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", new Formula("3"));
            Formula f = (Formula)s.GetCellContents("Z7");
            Assert.AreEqual(new Formula("3"), f);
            Assert.AreNotEqual(new Formula("2"), f);
        }

        // CIRCULAR FORMULA DETECTION
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test15()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2"));
            s.SetCellContents("A2", new Formula("A1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test16()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A3", new Formula("A4+A5"));
            s.SetCellContents("A5", new Formula("A6+A7"));
            s.SetCellContents("A7", new Formula("A1+A1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test17()
        {
            Spreadsheet s = new Spreadsheet();
            try
            {
                s.SetCellContents("A1", new Formula("A2+A3"));
                s.SetCellContents("A2", 15);
                s.SetCellContents("A3", 30);
                s.SetCellContents("A2", new Formula("A3*A1"));
            }
            catch (CircularException e)
            {
                Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                throw e;
            }
        }

        // NONEMPTY CELLS
        [TestMethod()]
        public void Test18()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void Test19()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "");
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void Test20()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "hello");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test21()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", 52.25);
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test22()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", new Formula("3.5"));
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test23()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("C1", "hello");
            s.SetCellContents("B1", new Formula("3.5"));
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        }

        // RETURN VALUE OF SET CELL CONTENTS
        [TestMethod()]
        public void Test24()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "hello");
            s.SetCellContents("C1", new Formula("5"));
            Assert.IsTrue(s.SetCellContents("A1", 17.2).SetEquals(new HashSet<string>() { "A1" }));
        }

        [TestMethod()]
        public void Test25()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("C1", new Formula("5"));
            Assert.IsTrue(s.SetCellContents("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test26()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("B1", "hello");
            Assert.IsTrue(s.SetCellContents("C1", new Formula("5")).SetEquals(new HashSet<string>() { "C1" }));
        }

        [TestMethod()]
        public void Test27()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A2", 6);
            s.SetCellContents("A3", new Formula("A2+A4"));
            s.SetCellContents("A4", new Formula("A2+A5"));
            Assert.IsTrue(s.SetCellContents("A5", 82.5).SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
        }

        // CHANGING CELLS
        [TestMethod()]
        public void Test28()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A1", 2.5);
            Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        }

        [TestMethod()]
        public void Test29()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A1", "Hello");
            Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        }

        [TestMethod()]
        public void Test30()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "Hello");
            s.SetCellContents("A1", new Formula("23"));
            Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
            Assert.AreNotEqual(new Formula("24"), (Formula)s.GetCellContents("A1"));
        }

        // STRESS TESTS
        [TestMethod()]
        public void Test31()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("B1+B2"));
            s.SetCellContents("B1", new Formula("C1-C2"));
            s.SetCellContents("B2", new Formula("C3*C4"));
            s.SetCellContents("C1", new Formula("D1*D2"));
            s.SetCellContents("C2", new Formula("D3*D4"));
            s.SetCellContents("C3", new Formula("D5*D6"));
            s.SetCellContents("C4", new Formula("D7*D8"));
            s.SetCellContents("D1", new Formula("E1"));
            s.SetCellContents("D2", new Formula("E1"));
            s.SetCellContents("D3", new Formula("E1"));
            s.SetCellContents("D4", new Formula("E1"));
            s.SetCellContents("D5", new Formula("E1"));
            s.SetCellContents("D6", new Formula("E1"));
            s.SetCellContents("D7", new Formula("E1"));
            s.SetCellContents("D8", new Formula("E1"));
            ISet<String> cells = s.SetCellContents("E1", 0);
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }
        [TestMethod()]
        public void Test32()
        {
            Test31();
        }
        [TestMethod()]
        public void Test33()
        {
            Test31();
        }
        [TestMethod()]
        public void Test34()
        {
            Test31();
        }

        [TestMethod()]
        public void Test35()
        {
            Spreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetCellContents("A" + i, new Formula("A" + (i + 1)))));
            }
        }
        [TestMethod()]
        public void Test36()
        {
            Test35();
        }
        [TestMethod()]
        public void Test37()
        {
            Test35();
        }
        [TestMethod()]
        public void Test38()
        {
            Test35();
        }
        [TestMethod()]
        public void Test39()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetCellContents("A" + i, new Formula("A" + (i + 1)));
            }
            try
            {
                s.SetCellContents("A150", new Formula("A50"));
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }
        [TestMethod()]
        public void Test40()
        {
            Test39();
        }
        [TestMethod()]
        public void Test41()
        {
            Test39();
        }
        [TestMethod()]
        public void Test42()
        {
            Test39();
        }

        [TestMethod()]
        public void Test43()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 0; i < 500; i++)
            {
                s.SetCellContents("A1" + i, new Formula("A1" + (i + 1)));
            }
            HashSet<string> firstCells = new HashSet<string>();
            HashSet<string> lastCells = new HashSet<string>();
            for (int i = 0; i < 250; i++)
            {
                firstCells.Add("A1" + i);
                lastCells.Add("A1" + (i + 250));
            }
            Assert.IsTrue(s.SetCellContents("A1249", 25.0).SetEquals(firstCells));
            Assert.IsTrue(s.SetCellContents("A1499", 0).SetEquals(lastCells));
        }
        [TestMethod()]
        public void Test44()
        {
            Test43();
        }
        [TestMethod()]
        public void Test45()
        {
            Test43();
        }
        [TestMethod()]
        public void Test46()
        {
            Test43();
        }

        [TestMethod()]
        public void Test47()
        {
            RunRandomizedTest(47, 2519);
        }
        [TestMethod()]
        public void Test48()
        {
            RunRandomizedTest(48, 2521);
        }
        [TestMethod()]
        public void Test49()
        {
            RunRandomizedTest(49, 2526);
        }
        [TestMethod()]
        public void Test50()
        {
            RunRandomizedTest(50, 2521);
        }

        public void RunRandomizedTest(int seed, int size)
        {
            Spreadsheet s = new Spreadsheet();
            Random rand = new Random(seed);
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            s.SetCellContents(randomName(rand), 3.14);
                            break;
                        case 1:
                            s.SetCellContents(randomName(rand), "hello");
                            break;
                        case 2:
                            s.SetCellContents(randomName(rand), randomFormula(rand));
                            break;
                    }
                }
                catch (CircularException)
                {
                }
            }
            ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(size, set.Count);
        }

        private String randomName(Random rand)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        }

        private String randomFormula(Random rand)
        {
            String f = randomName(rand);
            for (int i = 0; i < 10; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        f += "+";
                        break;
                    case 1:
                        f += "-";
                        break;
                    case 2:
                        f += "*";
                        break;
                    case 3:
                        f += "/";
                        break;
                }
                switch (rand.Next(2))
                {
                    case 0:
                        f += 7.2;
                        break;
                    case 1:
                        f += randomName(rand);
                        break;
                }
            }
            return f;
        }


    }
}
