# PSS-Search-Algorithms

## Uniformed search - Flood Fill

This is an application that uses two uninformed search algorithms, as described on page 81 of the book "Artificial Intelligence: A Modern Approach.".
`Flood fill` is essentially having the same effect as the `bucket fill` tool from MS Paint.

### Problem
Given a matrix of pixel color values as integers, select the a pixel (row and column) and a new color (an integer value) and all the pixels connected to
the selected one with the same pixel value have to change their values to the new input. This is a classical graph traversal problem.

![SS-0](./Screenshots/UI-Search-SS0.jpg)
### Algorithms
The implemented algorithms are:
- DFS (Depth First Search)
- BFS (Breath FIrst Search
- A small comparative approach (taking into account time, visited nodes, steps, and frontier size)

### Interesting Starting Positions

- (0,0) → top-left corner of color 1 → open area, BFS will expand in rings, DFS will snake through.
- (4,0) → color 5 → narrow corridor around obstacles → tests DFS deep path vs BFS spread.
- (8,8) → color 11 → isolated island in center → small frontier difference.
- (12,0) → mixed colors 13 & 14 → tests algorithm choosing which direction to explore first.

### Installation
1. Make sure you have .NET 10 Runtime and SDK installed 
2. Clone the project locally
3. Run `dotnet restore` to install packages
3. Run `dotnet build` to build binaries
4. Run `dotnet run --project SearchAlgorithms.ConsoleApp` to run the app.

### Visuals
#### Input Menu
![SS-1](./Screenshots/UI-Search-SS1.png)
#### Comparison result
![SS-2](./Screenshots/UI-Search-SS2.png)

## Informed search - Terrain Navigation

This is an application that uses informed search algorithms, also described in "Artificial Intelligence: A Modern Approach", to solve a `terrain-based` pathfinding problem.
Unlike uninformed search, these algorithms use a `heuristic function` to guide the search toward the goal more efficiently.

### Problem

Given a grid representing terrain, where each cell contains a movement cost, the goal is to find a path from a start position to a goal position such that the total traversal cost is minimized.

- Each cell value represents the cost of entering that cell
- Cells with high values can act as obstacles (in this case >= 9 means an obstacle and will not be taken into consideration)
- Movement is allowed in four directions (up, down, left, right)

This problem models robot navigation, where a robot must find the most efficient path through uneven terrain.

![SS-5](./Screenshots/I-Search-SS5.jpg)
### Algorithms

The implemented algorithms are:

- A* (A-Star Search)

    Uses both:
    - g(n) → cost from start to current node
    - h(n) → heuristic estimate to goal
    - f(n) = g(n) + h(n)


- Greedy Best-First Search

    Uses only:
    - h(n) → heuristic estimate


- A small comparative approach (displays comparison results: path found, total cost, number of visited nodes)

### Heuristics
Two heuristic functions are implemented:

#### Manhattan Distance 
(n is current node and its compared to the goal node)
```
h(n) = |x1 - x2| + |y1 - y2|
```
Because of the absolute value, it will try to expand more `grid-aligned` paths (straight).

#### Euclidean Distance
(n is current node and its compared to the goal node)
```
h(n) = sqrt((x1 - x2)^2 + (y1 - y2)^2)
```
Because of the smoother difference it will try more direct paths to the goal as well.

### Installation
1. Make sure you have .NET 10 Runtime and SDK installed
2. Clone the project locally
3. Run `dotnet restore` to install packages
3. Run `dotnet build` to build binaries
4. Run `dotnet run --project SearchAlgorithms.ConsoleApp` to run the app.

### Visuals
#### Input Menu
![SS-3](./Screenshots/I-Search-SS3.png)
#### Comparison result
![SS-4](./Screenshots/I-Search-SS4.png)

## Local search - Sudoku Solver

This is an application that uses local search algorithms, also described in "Artificial Intelligence: A Modern Approach", to solve a `terrain-based` pathfinding problem.
Unlike the previous search algorithms, here we are takling an optimization problem that doesn't concern with a global optimum solution, instead often finding local ones.

### Problem

We are talking about a classical game of sudoku, where you have a 9x9 grid and have to fill in the tiles with digits 1-9 such that there are no duplicates on either row, or 
column. There are some starting tiles already filled, which we'll call `fixed`. A properly formed 9x9 Sudoku must have at least 17 starting clues to have only one unique solution. 
It has been proven that puzzles with 16 or fewer clues do not have a unique solution.

The challenge lies in efficiently finding a valid configuration among a very large search space.

![SS-6](./Screenshots/LS-Search-SS0.jpg)

This problem is well-suited for local search algorithms, where we iteratively improve a candidate solution rather than constructing one step-by-step.

### Algorithms

The application has 3 modes:
- manual mode
- random solver mode (at each step, performs a random modification on non-fixed tiles)
- simulated annealing mode

The way the last one works is the following: First, we have to complete the initial state. Each 3x3 sub-grid is filled with distinct digits from 1-9 in non-fixed spots.

Afterwards, for `neighbour generation`, we select a random 3x3 sub-grid and swap 2 non-fixed values inside. This maintains sub-grid validity while exploring.

For `cost function` I have chosen:
```
Cost = number of duplicate values in all rows + all columns
```
A valid Sudoku board will have a cost of 0 and the lower it is the better.
Next, given:
- current state cost = `C`
- neighbour state cost = `C'`
- temperature `T`
The decision rule is that if we get a lower score for our neighbour, we take it, if not, we can still accept it with the following probability:
```
P = exp((C - C') / T)
```

Finally, the initial temperature is computed as standard deviation of the cost over 200 random states (starting from the same one with fixed tiles) and 
the cooling rate is set to 0.995, though both can be changed.

### Metrics

For now, I have included the following:
- number of iterations
- conflicts
- temperature

### Installation
1. Make sure you have node (at least 20) and npm installed.
2. Clone the project locally
3. Run `cd sudoku-local-search` to enter the React-Vite project directory
3. Run `npm install` to install packages
4. Run `npm run dev` to run the app.
5. Navigate to `http://localhost:5173/` and test it.

### Visuals
#### Input Menu
![SS-7](./Screenshots/LS-Search-SS1.png)
#### Comparison result
![SS-8](./Screenshots/LS-Search-SS2.png)
