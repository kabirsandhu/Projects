using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

//Created by Kabir Sandhu and Isabelle Chalhoub
// Date Modified: Nov 17, 2015
namespace Model
{
    /// <summary>
    /// World class that holds a dictionary of cubes currently in the world
    /// </summary>
    public class World
    {
        /// <summary>
        /// Height of the world
        /// </summary>
        readonly int height = 1000;

        /// <summary>
        /// Width of the world
        /// </summary>
        readonly int width = 1000;

        /// <summary>
        /// Dictionary which keeps track of all the cubes in the world.
        /// Cubes are mapped by their unique id
        /// </summary>
        public Dictionary<int, Cube> cubes { get; private set; }

        /// <summary>
        /// Constructor for world class
        /// </summary>
        public World()
        {
            cubes = new Dictionary<int, Cube>();
        }

        /// <summary>
        /// Takes a json string and deserializes it into a cube object in order to 
        /// update the dictionary with new information about the cubes.
        /// </summary>
        /// <param name="json"> a json string to be deserialized into a cube </param>
        public void updateWorld(string json)
        {
            Cube cube = JsonConvert.DeserializeObject<Cube>(json);

            // If the cube got eaten
            if (cube.Mass == 0)
            {
                cubes.Remove(cube.uid);
            }
            // If the cube needs to be updated
            else if (cubes.ContainsKey(cube.uid))
            {
                cubes.Remove(cube.uid);
                cubes.Add(cube.uid, cube);
            }
            // New cube
            else
                cubes.Add(cube.uid, cube);
        }

        /// <summary>
        /// Returns a cube object from a json string.
        /// json string should be the representation of the player's cube.
        /// </summary>
        /// <param name="json"> a json string representing the player's cube </param>
        /// <returns> the player's cube object </returns>
        public Cube firstCube(string json)
        {
            Cube cube = JsonConvert.DeserializeObject<Cube>(json);
            return cube;
        }
    }
}
