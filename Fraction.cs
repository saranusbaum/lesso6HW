namespace OpTable
{
    class Fraction
    {
        int numerator = 0;
        int denominator = 1;

        public Fraction(int numerator, int denominator = 1)
        {
            this.numerator = numerator;
            this.denominator = denominator;

            Simplify();
        }

        public static int gcd(int a, int b)
        {
            int R;
            while ((a % b) > 0)
            {
                R = a % b;
                a = b;
                b = R;
            }
            return b;
        }

        public void Simplify()
        {
            int g = gcd(numerator, denominator);
            numerator /= g;
            denominator /= g;
        }

        public static Fraction operator *(Fraction lhs, Fraction rhs)
        {
            return new Fraction(lhs.numerator * rhs.numerator, lhs.denominator * rhs.denominator);
        }

        public static Fraction operator +(Fraction lhs, Fraction rhs)
        {
            int denominator = lhs.denominator * rhs.denominator;
            int numerator = lhs.numerator * rhs.denominator + rhs.numerator * lhs.denominator;
            return new Fraction(numerator, denominator);
        }

        public override bool Equals(Object? o)
        {
            if (o is null)
                return false;

            if (o is Fraction f)
            {
                return this == f;
            }
            return false;
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            // following code assumes that both fractions are simplified
            return a.numerator == b.numerator && a.denominator == b.denominator; 
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator >=(Fraction a, Fraction b)
        {
            return a.numerator * b.denominator >= b.numerator * a.denominator;
        }

        public static bool operator <=(Fraction a, Fraction b)
        {
            return a.numerator * b.denominator <= b.numerator * a.denominator;
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return a.numerator * b.denominator > b.numerator * a.denominator;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return a.numerator * b.denominator < b.numerator * a.denominator;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(numerator, denominator);
        }

        public override string ToString() => $"{numerator}/{denominator}";

        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }

        public static implicit operator Double(Fraction f)
        {
            return f.numerator / f.denominator;
        }

        public static implicit operator Decimal(Fraction f)
        {
            return f.numerator / f.denominator;
        }
    }
        
}
