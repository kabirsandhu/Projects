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
    /// Our Cube object class that has JSON properties:
    /// x location, y location, color, unique ID, food status, and mass.
    /// Also has a non-JSON property - width calculated from mass.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Cube
    {
        /// <summary>
        /// X location of the cube
        /// </summary>
        [JsonProperty]
        public double loc_x { get; private set; }

        /// <summary>
        /// Y location of the cube
        /// </summary>
        [JsonProperty]
        public double loc_y {get; private set;}

        /// <summary>
        /// Color of the cube in ARGB
        /// </summary>
        [JsonProperty]
        public int argb_color { get;  private set; }

        /// <summary>
        /// Unique id of the cube
        /// </summary>
        [JsonProperty]
        public int uid { get; private set; }

        /// <summary>
        /// Food status of the cube.
        /// True if the cube is a player, false otherwise
        /// </summary>
        [JsonProperty]
        public bool food { get; private set; }

        /// <summary>
        /// Name of the cube if the cube is a player
        /// </summary>
        [JsonProperty]
        public string Name { get;   private set; }

        /// <summary>
        /// Mass of the cube
        /// </summary>
        [JsonProperty]
        public double Mass { get; private set; }

        /// <summary>
        /// Width of the cube defined as the mass to the power of 0.65
        /// </summary>
        public int width { get; private set; }

        /// <summary>
        /// Team Id of the cube used to keep track of split cubes 
        /// </summary>
        [JsonProperty]
        public double team_id { get; private set; }

        /// <summary>
        /// Constructor for Cube object
        /// </summary>
        public Cube(double x, double y, int color, int id, bool food_status, string name, double mass)
        {
            this.loc_x = x;
            this.loc_y = y;
            this.argb_color = color;
            this.uid = id;
            this.food = food_status;
            this.Name = name;
            this.Mass = mass;
            this.width = (int) Math.Pow(mass, 0.65);
        }
    }
}
