using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//Kabir Sandhu - u0409658
//Date Modified: Oct 1, 2015
namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Dictionary which maps cell names to the cell objects.
        /// Contains only non-empty cells
        /// </summary>
        private Dictionary<string, Cell> nonEmptyCells;

        /// <summary>
        /// Dependency graph which keeps track of the dependencies between cells
        /// </summary>
        private DependencyGraph Dependencies;

        /// <summary>
        /// Constructor for spreadsheet class.
        /// </summary>
        public Spreadsheet()
        {
            nonEmptyCells = new Dictionary<string, Cell>();
            Dependencies = new DependencyGraph();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            List<string> cellNames = new List<string>(nonEmptyCells.Keys); // Create list of names of non-empty cells from dictionary and yield return items in the list
            foreach (string cell in cellNames)
                yield return cell;
        }


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        public override object GetCellContents(String name)
        {
            if (ReferenceEquals(name, null) || !isValid(name))
                throw new InvalidNameException();

            Cell cell;
            if (nonEmptyCells.TryGetValue(name, out cell))
                return cell.content;
            else
                return ""; // If the cell is not in the dictionary then it must be empty so return empty string
        }


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, double number)
        {
            if (ReferenceEquals(name, null) || !isValid(name))
                throw new InvalidNameException();

            Cell cell;
            if (nonEmptyCells.TryGetValue(name, out cell)) // If the cell already has content, replace its content
                cell.content = number;
            else
            {
                cell = new Cell(name, number); // If cell is not already in dictionary, make a new one and add it
                nonEmptyCells.Add(name, cell);
            }
            Dependencies.ReplaceDependents(name, new List<string>()); //Remove all the previous dependencies of the cell since it doesn't depend on any other cells
            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, String text)
        {
            if (ReferenceEquals(text, null))
                throw new ArgumentNullException();

            else if (ReferenceEquals(name, null) || !isValid(name))
                throw new InvalidNameException();

            Cell cell;
            if (nonEmptyCells.TryGetValue(name, out cell))
            {
                if (text.Equals("")) // Empty string represents empty cells so existing cells are removed from the dictionary
                    nonEmptyCells.Remove(name);
                else
                    cell.content = text;
            }
            else
            {
                if (text.Equals("")) // If the cell doesn't exist in the dictionary then it is already empty
                    return new HashSet<string>();

                cell = new Cell(name, text); // If cell is not already in dictionary, make a new one and add it
                nonEmptyCells.Add(name, cell);
            }

            Dependencies.ReplaceDependents(name, new List<string>()); //Remove all the previous dependencies of the cell since it doesn't depend on any other cells
            return new HashSet<string>(GetCellsToRecalculate(name)); // GetCellsToRecalculate method contains all direct and indirect dependents of the cell
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, Formula formula)
        {
            if (ReferenceEquals(formula, null))
                throw new ArgumentNullException();
            else if (ReferenceEquals(name, null) || !isValid(name))
                throw new InvalidNameException();

            Cell cell;
            object oldContent = null; //Keeps track of previous value in cell
            if(nonEmptyCells.TryGetValue(name, out cell)) //If cell is already in dictionary, save its contents and replace them with new formula
            {
                oldContent = cell.content;
                cell.content = formula;
            }
            else //Otherwise if it cell is not in dictionary, create new cell with formula and add it to the dictionary
            {
                cell = new Cell(name, formula);
                nonEmptyCells.Add(name, cell);
            }
            try //Update the dependency graph and try to return set of cells to recalculate
            {
                Dependencies.ReplaceDependents(name, formula.GetVariables());
                return new HashSet<string>(GetCellsToRecalculate(name));
            }
            catch(CircularException e) //If circular exception is thrown by getCellsToRecalculate, catch it
            {
                if(oldContent == null) //If the previous content of the cell was empty, delete the cell from the dictionary
                {
                    nonEmptyCells.Remove(name);
                    Dependencies.ReplaceDependents(name, new List<string>());
                }
                else if(oldContent is Formula)  //If the previous content of the cell was a formula, update the content and dependency graph accordingly
                {
                    Formula oldFormula = (Formula) oldContent;
                    cell.content = oldContent;
                    Dependencies.ReplaceDependents(name, oldFormula.GetVariables());
                }
                else //Otherwise update the content and dependency graph
                {
                    cell.content = oldContent;
                    Dependencies.ReplaceDependents(name, new List<string>());
                }
                throw e; //Throw circularException
            }
       }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException();
            else if (!isValid(name))
                throw new InvalidNameException();

            return Dependencies.GetDependees(name); // The dependees are the cells that are dependent on this cell
        }

        /// <summary>
        /// Checks if the passed string is a valid cell name.
        /// Returns true if it is, otherwise false.
        /// The string is checked using a regular expression.
        /// </summary>
        /// <param name="cellName">name to be checked</param>
        /// <returns></returns>
        private bool isValid(string cellName)
        {
            return Regex.IsMatch(cellName, "^[a-zA-Z_]+[a-zA-z0-9]+$"); //Regular expression is identical to the variable checker from PS3
        }

        /// <summary>
        /// Cell class represents a cell object which has a name and a content
        /// The class contains methods for setting and getting the name and content
        /// </summary>
        private class Cell
        {
            /// <summary>
            /// Sets and gets the name of this cell
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Sets and gets the content of this cell
            /// </summary>
            public object content { get; set; }

            /// <summary>
            /// Constructor for cell object
            /// </summary>
            /// <param name="name">Name of cell</param>
            /// <param name="content">Content of cell</param>
            public Cell(string name, object content)
            {
                this.name = name;
                this.content = content;
            }
        }
    }




}
