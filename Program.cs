

using Google.OrTools.LinearSolver;

namespace LinearProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Solver solver = Solver.CreateSolver("CLP_LINEAR_PROGRAMMING");
            Variable x1 = solver.MakeNumVar(0.0, double.PositiveInfinity, "x1");
            Variable x2 = solver.MakeNumVar(0.0, double.PositiveInfinity, "x2");
            Variable x3 = solver.MakeNumVar(0.0, double.PositiveInfinity, "x3");
            Variable x4 = solver.MakeNumVar(0.0, double.PositiveInfinity, "x4");
            Variable x5 = solver.MakeNumVar(0.0, double.PositiveInfinity, "x5");

            // Maximize 2*y+x.
            Objective objective = solver.Objective();
            objective.SetCoefficient(x1, 0);
            objective.SetCoefficient(x2, 0);
            objective.SetCoefficient(x3, 2);
            objective.SetCoefficient(x4, -2);
            objective.SetCoefficient(x5, -1);
            objective.SetMinimization();

            // 0 <= x <= 15 
            Constraint c0 = solver.MakeConstraint(4, 4);
            c0.SetCoefficient(x3, -2);
            c0.SetCoefficient(x4, 1);
            c0.SetCoefficient(x5, 1);

            // 0 <= y <= 8
            Constraint c1 = solver.MakeConstraint(2, 2);
            c1.SetCoefficient(x3, 3);
            c1.SetCoefficient(x4, -1);
            c1.SetCoefficient(x5, 2);

            var resultStatus = solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("The problem does not have an optimal solution!");
                return;
            }

            Console.WriteLine("Problem solved in " + solver.WallTime() + " milliseconds");

            // The objective value of the solution.
            Console.WriteLine("Optimal objective value = " + solver.Objective().Value());

            // The value of each variable in the solution.
            foreach (var v in solver.variables())
            { Console.WriteLine($"{v.Name()} : {v.SolutionValue()} "); };
        }
    }
}