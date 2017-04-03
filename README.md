# Quine-McCluskey.NET
> A C# implementation of the [Quine-McCluskey algorithm](https://en.wikipedia.org/wiki/Quine%E2%80%93McCluskey_algorithm) for boolean expression minimization.

Includes a class library for the algorithm, unit tests, and a console application for testing expressions via stdin.

## Limitations
This implementation is based on pages 1 through 8 of [the QM Document](document.pdf) by Dr. Nicholas Outram.  
It does **not** include:
* Simplification of 'dont-care' terms from the beginning
* Simplifications with multiple solutions
* Prime implicant charts or coverage tables

## Running the CLI
1. `git clone git@github.com:bcanseco/quine-mccluskey.git qm`
2. `cd qm/Algorithm.CLI`
3. `dotnet restore`
4. `dotnet run`

## Running the Tests
1. `cd ../Algorithm.Tests`
2. `dotnet restore`
3. `dotnet test`

## ToDo
The main thing I see that could be improved is refactoring [Expression](Algorithm/Expression.cs) and [Composite Minterm](Algorithm/Minterms/CompositeMinterm.cs) to be one class. **Pull requests are welcome!** I have put both inline comments and XML documentation throughout the solution to assist with development.

## License

[MIT](LICENSE)
