using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

//Created by Kabir Sandhu and Isabelle Chalhoub
// Date Modified: Nov 17, 2015
namespace ModelTester
{
    /// <summary>
    /// Tests for Cube and World class
    /// </summary>
    [TestClass]
    public class ModelTester
    {
        /// <summary>
        /// Test for Cube constructor
        /// </summary>
        [TestMethod]
        public void CubeTest1()
        {
            Cube cube = new Cube(0, 0, 0, 10, false, "", 100);
            Assert.AreEqual(0, cube.loc_x);
            Assert.AreEqual(0, cube.loc_y);
            Assert.AreEqual(0, cube.argb_color);
            Assert.AreEqual(10, cube.uid);
            Assert.AreEqual(false, cube.food);
            Assert.AreEqual("", cube.Name);
            Assert.AreEqual(100, cube.Mass);
            Assert.AreEqual((int) Math.Pow(100, 0.65), cube.width);
        }

        /// <summary>
        /// Tests the World - Deserialize a json string into a cube that gets added to the dictionary - new cube
        /// </summary>
        [TestMethod]
        public void WorldTest1()
        {
            World world = new World();
            String jsonString = "{'loc_x':813.0,'loc_y':878.0,'argb_color':-2987746,'uid':5318,'food':false,'Name':'Myname','Mass':1029.1106844616961}";
            world.updateWorld(jsonString);
            Cube cube;
            world.cubes.TryGetValue(5318, out cube);
            Assert.AreEqual(1, world.cubes.Count);
            Assert.AreEqual(813.0, cube.loc_x);
            Assert.AreEqual(878.0, cube.loc_y);
            Assert.AreEqual(-2987746, cube.argb_color);
            Assert.AreEqual(5318, cube.uid);
            Assert.AreEqual(false, cube.food);
            Assert.AreEqual("Myname", cube.Name);
            Assert.AreEqual(1029.1106844616961, cube.Mass);
            Assert.AreEqual((int)Math.Pow(1029.1106844616961, 0.65), cube.width);
        }

        /// <summary>
        /// Tests the World - Deserialize a json string into a cube that gets added to the dictionary - updates existing cube
        /// </summary>
        [TestMethod]
        public void WorldTest2()
        {
            World world = new World();
            String jsonString = "{'loc_x':0.0,'loc_y':0.0,'argb_color':0,'uid':5318,'food':true,'Name':'hey','Mass':20}";
            world.updateWorld(jsonString);
            jsonString = "{'loc_x':813.0,'loc_y':878.0,'argb_color':-2987746,'uid':5318,'food':false,'Name':'Myname','Mass':1029.1106844616961}";
            world.updateWorld(jsonString);
            Cube cube;
            world.cubes.TryGetValue(5318, out cube);
            Assert.AreEqual(1, world.cubes.Count);
            Assert.AreEqual(813.0, cube.loc_x);
            Assert.AreEqual(878.0, cube.loc_y);
            Assert.AreEqual(-2987746, cube.argb_color);
            Assert.AreEqual(5318, cube.uid);
            Assert.AreEqual(false, cube.food);
            Assert.AreEqual("Myname", cube.Name);
            Assert.AreEqual(1029.1106844616961, cube.Mass);
            Assert.AreEqual((int)Math.Pow(1029.1106844616961, 0.65), cube.width);
        }

        /// <summary>
        /// Tests the World - Deserialize a json string into a cube that gets added to the dictionary - removes a cube that got eaten
        /// </summary>
        [TestMethod]
        public void WorldTest3()
        {
            World world = new World();
            String jsonString = "{'loc_x':813.0,'loc_y':878.0,'argb_color':-2987746,'uid':5318,'food':false,'Name':'Myname','Mass':1029.1106844616961}";
            world.updateWorld(jsonString);
            jsonString = "{'loc_x':813.0,'loc_y':878.0,'argb_color':-2987746,'uid':5318,'food':false,'Name':'Myname','Mass':0}";
            world.updateWorld(jsonString);
            Assert.AreEqual(0, world.cubes.Count);
        }

        /// <summary>
        /// Tests the World - Deserialize a json string into a cube and returns that cube
        /// </summary>
        [TestMethod]
        public void WorldTest4()
        {
            World world = new World();
            String jsonString = "{'loc_x':813.0,'loc_y':878.0,'argb_color':-2987746,'uid':5318,'food':false,'Name':'Myname','Mass':1029.1106844616961}";
            Cube cube = world.firstCube(jsonString);
            Assert.AreEqual(813.0, cube.loc_x);
            Assert.AreEqual(878.0, cube.loc_y);
            Assert.AreEqual(-2987746, cube.argb_color);
            Assert.AreEqual(5318, cube.uid);
            Assert.AreEqual(false, cube.food);
            Assert.AreEqual("Myname", cube.Name);
            Assert.AreEqual(1029.1106844616961, cube.Mass);
            Assert.AreEqual((int)Math.Pow(1029.1106844616961, 0.65), cube.width);
        }
    }
}
