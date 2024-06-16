using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;

namespace OpTable
{
    internal class Program
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }

        //public static double Multiply(double x, double y)
        //{
        //    return x * y;
        //}

        static void Main(string[] args)
        {
            List<Fraction> row_values = new List<Fraction>();
            for (int i = 1; i <= 12; i++)
            {
                row_values.Add(new Fraction(i, 12));
            }
            List<Fraction> col_values = new List<Fraction>();
            for (int i = 1; i < 7; i++)
            {
                col_values.Add(new Fraction(i, 12));
            }
            OperationTable<Fraction> t1 =
            new OperationTable<Fraction>(row_values, col_values, (x, y) => x + y);
            t1.Print();

            //List<double> rowValues = new List<double> { 1.7, 2.0, 3.7, 4.6 };
            //List<double> columnValues = new List<double> { 0.5, 1.5, 2.8 };
           

            // יצירת טבלת פעולות עבור החיבור
            DoubleOperationTable additionTable = new DoubleOperationTable(8, 9, Add);
            additionTable.Print(); // הדפסת הטבלה עם רוחב עמודה 10

            // יצירת טבלת פעולות עבור הכפל
            DoubleOperationTable multiplicationTable = new DoubleOperationTable(4, 3, (double a, double b) => a * b);
            multiplicationTable.Print();
        }
    }

    class OperationTable<T>
    {
        // the followng line defines a _type_ op_func
        public delegate T OpFunc(T x, T y);

        // the following line defines a variable of type op_func
        public OpFunc op;

        protected T[,]? values = null;

        protected List<T>? row_values = null;
        protected List<T>? col_values = null;

        public OperationTable(List<T> _row_values, List<T> _col_values, OpFunc _op)
        {
            op = _op;
            // --> exercise: complete this function
            row_values = _row_values;
            col_values = _col_values;
            InitializeTable();
        }
        public void InitializeTable()
        {
            int rows = row_values.Count;
            int cols = col_values.Count;

            // אתחול המערך values
            values = new T[rows, cols];

            // מילוי הערכים במערך values בהתאם לפונקציה או האופרטור שהועבר
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    values[r, c] = op(row_values[r], col_values[c]);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"==== table ======");

            if ((row_values is null) || (col_values is null) || (values is null))
            {
                return;
            }

            int rows = values.GetLength(0);
            int cols = values.GetLength(1);

            Console.Write($"      : ");
            Console.Write($"{col_values[0],5}");
            for (int c = 1; c < cols; c++)
            {
                Console.Write($" | {col_values[c],5}");
            }
            Console.WriteLine();
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"---------");
            }
            Console.WriteLine();

            for (int r = 0; r < rows; r++)
            {
                Console.Write($"{row_values[r],5} : ");
                Console.Write($"{values[r, 0],5}");
                for (int c = 1; c < cols; c++)
                {
                    Console.Write($" | {values[r, c],5}");
                }
                Console.WriteLine();
            }
        }

    }

          class DoubleOperationTable : OperationTable<double>
        {
            public DoubleOperationTable(int rows, int columns, OpFunc op) 
        : base(GenerateRowValues(rows), GenerateColumnValues(columns), op)
        {
         
        }

      

        private static List<double> GenerateRowValues(int rows)
        {
            List<double> rowValues = new List<double>();
            for (int i = 0; i < rows; i++)
            {
                rowValues.Add(i + 1.5); 
            }
            return rowValues;
        }

        private static List<double> GenerateColumnValues(int columns)
        {
            List<double> colValues = new List<double>();
            for (int i = 0; i < columns; i++)
            {
                colValues.Add(i + 1.7); 
            }
            return colValues;
        }
    }


    //}
}
