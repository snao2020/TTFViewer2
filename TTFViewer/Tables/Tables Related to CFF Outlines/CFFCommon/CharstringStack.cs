using System;
using System.Collections.Generic;
using System.Linq;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
    class CharstringStack
    {
        Stack<FixedPoint_16_16> NumberStack;
        Dictionary<Int32, FixedPoint_16_16> Storage;
        List<(Int32 op, FixedPoint_16_16)> HintList;
        public FixedPoint_16_16 Width { get; private set; }

        public CharstringStack()
        {
            NumberStack = new Stack<FixedPoint_16_16>();
            Storage = new Dictionary<int, FixedPoint_16_16>();
            HintList = new List<(int op, FixedPoint_16_16)>();
        }


        public FixedPoint_16_16 Pop()
        {
            if (NumberStack.Count > 0)
                return NumberStack.Pop();
            else
                return null;
        }


        public void Push(FixedPoint_16_16 number)
        {
            NumberStack.Push(number);
        }


        public Int32 GetMaskByteLength()
        {
            return (HintList.Count / 2 + 7) / 8;
        }


        public void PreMask(Int32 vstemhmOp)
        {
            int count = NumberStack.Count / 2 * 2;
            if (count > 0)
            {
                var list = PopN(count);
                if (list != null)
                {
                    HintList = HintList.Concat(list.Select(i => (vstemhmOp, i))).ToList(); ;
                    SetWidth();
                }
            }
        }


        public CharstringTokenResult rmoveto()
        {
            if (PopN(2) != null && SetWidth())
                return CharstringTokenResult.MoveTo;
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hmoveto()
        {
            return PopN(1) != null && SetWidth()
                ? CharstringTokenResult.MoveTo
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult vmoveto()
        {
            return PopN(1) != null && SetWidth()
                ? CharstringTokenResult.MoveTo
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult rlineto()
        {
            return PopPath(i => i > 0 && i % 2 == 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hlineto()
        {
            return PopPath(i=>i > 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult vlineto()
        {
            return PopPath(i => i > 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult rrcurveto()
        {
            return PopPath(i => i > 0 && i % 6 == 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hhcurveto()
        {
            return PopPath(i=>i > 0 && (i % 4 == 0 || i % 4 == 1)) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hvcurveto()
        {
            return PopPath(i => i >= 4 && ((i - 4)% 8 == 0 || (i - 4) % 8 == 1)) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult rcurveline()
        {
            return PopPath(i => i >= 8 && (i - 2) % 6 == 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult rlinecurve()
        {
            return PopPath(i => i >= 8 && (i - 6) % 2 == 0) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult vhcurveto()
        {
            return PopPath(i => i >= 4 && ((i - 4) % 8 == 0 || (i - 4) % 8 == 1)) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult vvcurveto()
        {
            return PopPath(i => i >= 4 && (i % 4 == 0 || i % 4 == 1)) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult flex()
        {
            return PopPath(i => i == 13) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hflex()
        {
            return PopPath(i => i == 7) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult hflex1()
        {
            return PopPath(i => i == 9) != null
                ? CharstringTokenResult.Operator
                : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult flex1()
        {
            return PopPath(i => i == 11) != null
               ? CharstringTokenResult.Operator
               : CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult return0()
        {
            return CharstringTokenResult.Exit;
        }

        public CharstringTokenResult endchar()
        {
            return SetWidth()
                ? CharstringTokenResult.Exit
                : CharstringTokenResult.OperandError;
        }


        public CharstringTokenResult hstem(Int32 op)
        {
            return Stem(op);
        }

        public CharstringTokenResult vstem(Int32 op)
        {
            return Stem(op);
        }

        public CharstringTokenResult hstemhm(Int32 op)
        {
            return Stem(op);
        }

        public CharstringTokenResult vstemhm(Int32 op)
        {
            return Stem(op);
        }

        public CharstringTokenResult hintmask(Int32 vstemhmOp, uint8[] maskBytes)
        {
            if (NumberStack.Count > 0)
            {
                var result = Stem(vstemhmOp);
                if (result != CharstringTokenResult.Operator)
                    return result;
            }
            return CharstringTokenResult.Mask;
        }

        public CharstringTokenResult cntrmask(Int32 vstemhmOp, uint8[] maskBytes)
        {
            if (NumberStack.Count > 0)
            {
                var result = Stem(vstemhmOp);
                if (result != CharstringTokenResult.Operator)
                    return result;
            }
            return CharstringTokenResult.Mask;
        }


        public CharstringTokenResult abs()
        {
            if (NumberStack.Count > 0)
            {
                NumberStack.Push(NumberStack.Pop().Abs());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult add()
        {
            if (NumberStack.Count > 1)
            {
                NumberStack.Push(NumberStack.Pop() + NumberStack.Pop());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult sub()
        {
            if (NumberStack.Count > 1)
            {
                NumberStack.Push(NumberStack.Pop() - NumberStack.Pop());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult div()
        {
            if (NumberStack.Count > 1)
            {
                NumberStack.Push(NumberStack.Pop() / NumberStack.Pop());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult neg()
        {
            if (NumberStack.Count > 0)
            {
                NumberStack.Push(-NumberStack.Pop());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult random()
        {
            var v = FixedPoint_16_16.Random();
            NumberStack.Push(v);
            return CharstringTokenResult.Operator;
        }

        public CharstringTokenResult mul()
        {
            if (NumberStack.Count > 1)
            {
                NumberStack.Push(NumberStack.Pop() * NumberStack.Pop());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult sqrt()
        {
            if (NumberStack.Count > 0)
            {
                NumberStack.Push(NumberStack.Pop().Sqrt());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult drop()
        {
            if (NumberStack.Count > 0)
            {
                NumberStack.Pop();
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult exch()
        {
            if (NumberStack.Count > 1)
            {
                var n0 = NumberStack.Pop();
                var n1 = NumberStack.Pop();
                NumberStack.Push(n0);
                NumberStack.Push(n1);
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult index()
        {
            if (NumberStack.Count > 1)
            {
                Int32 index = (Int32)NumberStack.Pop();
                var array = NumberStack.ToArray();
                if (index < array.Length)
                {
                    NumberStack.Push(array[index]);
                    return CharstringTokenResult.Operator;
                }
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult roll()
        {
            if (NumberStack.Count > 1)
            {
                var j = (Int32)NumberStack.Pop();
                var n = (Int32)NumberStack.Pop();
                j = j % n;
                // stack: num(n-1),,,num1,num0
                var list = new List<FixedPoint_16_16>();
                // list:  num0, num1,,,num(n-1)
                for (int i = 0; i < n; i++)
                    list.Add(NumberStack.Pop());
                // stack: num(j-1),,,num0
                for (int i = j - 1; i >= 0; i--)
                    NumberStack.Push(list[i]);
                // stack: num(j-1),,,num0,num(n-1),,,num(j)
                for (int i = n - 1; i >= j; i--)
                    NumberStack.Push(list[i]);
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult dup()
        {
            if (NumberStack.Count > 0)
            {
                NumberStack.Push(NumberStack.Peek());
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult put()
        {
            if (NumberStack.Count > 1)
            {
                var i = (Int32)NumberStack.Pop();
                var val = NumberStack.Pop();
                Storage.Add(i, val);
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult get()
        {
            if (NumberStack.Count > 0)
            {
                var i = (Int32)NumberStack.Pop();
                if (Storage.TryGetValue(i, out FixedPoint_16_16 val))
                {
                    NumberStack.Push(val);
                    return CharstringTokenResult.Operator;
                }
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult and()
        {
            if (NumberStack.Count > 1)
            {
                var n0 = NumberStack.Pop();
                var n1 = NumberStack.Pop();
                NumberStack.Push(new FixedPoint_16_16((Int16)((bool)n0 && (bool)n1 ? 1 : 0), 0));
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult or()
        {
            if (NumberStack.Count > 1)
            {
                var n0 = NumberStack.Pop();
                var n1 = NumberStack.Pop();
                NumberStack.Push(new FixedPoint_16_16((Int16)((bool)n0 || (bool)n1 ? 1 : 0), 0));
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult not()
        {
            if (NumberStack.Count > 0)
            {
                var n = NumberStack.Pop();
                NumberStack.Push(new FixedPoint_16_16((Int16)((bool)n ? 0 : 1), 0));
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult eq()
        {
            if (NumberStack.Count > 1)
            {
                var n0 = NumberStack.Pop();
                var n1 = NumberStack.Pop();
                NumberStack.Push(new FixedPoint_16_16((Int16)(n0 == n1 ? 1 : 0), 0)); ;
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult ifelse()
        {
            if (NumberStack.Count > 3)
            {
                var v2 = NumberStack.Pop();
                var v1 = NumberStack.Pop();
                var s2 = NumberStack.Pop();
                var s1 = NumberStack.Pop();
                NumberStack.Push(v1 <= v2 ? s1 : s2);
                return CharstringTokenResult.Operator;
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult vsindex()
        {
            if (PopPath(i => i == 1) != null)
                return CharstringTokenResult.Operator;
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult blend()
        {
            if (Pop() is FixedPoint_16_16 f)
            {
                Int32 n = f;
                var count = NumberStack.Count;
                var list = PopN(count);
                if (list != null)
                {
                    for (int i = n; i < 2 * n; i++)
                        NumberStack.Push(list[i]);
                    return CharstringTokenResult.Operator;
                }
            }
            return CharstringTokenResult.OperandError;
        }

        public CharstringTokenResult callsubr()
        {
            return CharstringTokenResult.LocalSubr;
        }

        public CharstringTokenResult callgsubr()
        {
            return CharstringTokenResult.GlobalSubr;
        }


        List<FixedPoint_16_16> PopPath(Func<Int32,bool> func)
        {
            var count = NumberStack.Count;
            if (func(count))
                return PopN(count);
            else
                return null;
        }

        List<FixedPoint_16_16> PopN(Int32 n)
        {
            if (NumberStack.Count >= n)
            {
                var list = new List<FixedPoint_16_16>();
                for (Int32 i = 0; i < n; i++)
                    list.Add(NumberStack.Pop());
                list.Reverse();
                return list;
            }
            return null;
        }


        bool SetWidth()
        {
            bool result = true;
            if (Width == null)
            {
                if (NumberStack.Count == 1)
                    Width = NumberStack.Pop();
                else if (NumberStack.Count == 0)
                    Width = new FixedPoint_16_16(0, 0);
                else
                    result = false;
            }
            else if (NumberStack.Count != 0)
                result = false;

            return result;
        }

        CharstringTokenResult Stem(Int32 op)
        {
            int count = NumberStack.Count / 2 * 2;
            if (count > 0)
            {
                var list = PopN(count);
                if (list != null)
                {
                    HintList = HintList.Concat(list.Select(i => (op, i))).ToList(); ;
                    if (SetWidth())
                        return CharstringTokenResult.Operator; ;
                }
            }
            return CharstringTokenResult.OperandError;
        }
    }
}
