Kabir Sandhu
Oct. 1, 2015:
	Initial Design thoughts;
		To implement the spreadsheet class I will start by making a Cell object which has a name, value, and content. The name will be the name of the cell,
		The content will be what the cell hold, (i.e Formula, string, or double) and the value is the value of the cell which is the evaluated formula if the 
		cell content is a formula. The spreadsheet will have a dictionary which maps the cell names to the cells. This will give access to any cell by its name.

		The cell class will have methods for getting and setting its name, content, and value.

		I'll start by making a constructor for the spreadsheet class. It will initialize the dictionary of cells. 

		Methods:
			GetNameOfAllNonemptyCells: This method will return the list of keys from the cell dictionary

			GetCellContents: This method will return the value in the cell by getting the value the name maps to in the cell dictionary

			SetCellContents: These methods will use the cell's set method to set the contents of the cell and use the dependency graph to get all of the cell's dependents

			GetDirectDependents: This method will use the dependency graph to get the cell's dependents

			I will be using the most recent versions of PS2 and PS3 that have passed all grading tests.

Kabir Sandhu
Oc 1, 2015:
	Implementation notes:

		The spreadsheet is implemented using a dictionary which maps non-empty cell names to the cell objects. The cell object is a private class
		within the spreadsheet class. It represents a cell with a name and content. Cells are created in the setCellContents methods of the 
		spreadsheet class. They cannot be created anywhere else. Cells are 'deleted' or emptied by setting their value to the empty string.
		This removes them from the dictionary of non-empty cells. Since the value of the cell is not used in this current implementation of the spreadsheet
		class, it is not represented in the cell class. It will probably be necessary in the future though. 

		A Dependency graph is used to keep track of dependencies between cells. A cell can only have dependents if it contains a formula since its
		value may be dependent on other cells to be evaluated. Strings and doubles are final values and do not rely on other cells. The dependcies are updated 
		in the setCellContents methods. Teh getDirectDependents methods uses the getDependees method of the dependencygraph since in this implementation the direct 
		dependents of the cell are defined as the cells that directly depend on that cell to be evaluated. The getCellsToRecalculate method gets all direct and indirect
		dependents of a cell

		The method implementations make use of dictionary, and dependency graph to add, remove, get, and modify cells. There are helper methods to check the validity of
		cell names and to check for circular depdencies.

		Test cases test for functionality of all the overwritten methods. The test cases have complete code coverage of the spreadsheet class.

