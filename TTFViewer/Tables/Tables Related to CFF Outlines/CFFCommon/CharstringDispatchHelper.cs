using System;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
    static class CharstringDispatchHelper
    {
        /*
        The first stack-clearing operator, which must be one of 
            hstem,hstemhm, vstem, vstemhm, cntrmask, hintmask, hmoveto, vmoveto, rmoveto, or endchar
        takes an additional argument — the width (as
        described earlier), which may be expressed as zero or one numeric
        argument.
        */
        public static CharstringTokenResult DispatchCFFOperator(uint8[] token, CharstringStack stack)
        {
            CharstringTokenResult result;
            
            var op = (CFFCharstringOperators)CharstringHelper.GetOperatorInteger(token);
            switch (op)
            {
                // 4.1 Path Construction Operators
                case CFFCharstringOperators.rmoveto: result = stack.rmoveto(); break;
                case CFFCharstringOperators.hmoveto: result = stack.hmoveto(); break;
                case CFFCharstringOperators.vmoveto: result = stack.vmoveto(); break;

                case CFFCharstringOperators.rlineto: result = stack.rlineto(); break;
                case CFFCharstringOperators.hlineto: result = stack.hlineto(); break;
                case CFFCharstringOperators.vlineto: result = stack.vlineto(); break;
                case CFFCharstringOperators.rrcurveto: result = stack.rrcurveto(); break;
                case CFFCharstringOperators.hhcurveto: result = stack.hhcurveto(); break;
                case CFFCharstringOperators.hvcurveto: result = stack.hvcurveto(); break;
                case CFFCharstringOperators.rcurveline: result = stack.rcurveline(); break;
                case CFFCharstringOperators.rlinecurve: result = stack.rlinecurve(); break;
                case CFFCharstringOperators.vhcurveto: result = stack.vhcurveto(); break;
                case CFFCharstringOperators.vvcurveto: result = stack.vvcurveto(); break;
                case CFFCharstringOperators.flex: result = stack.flex(); break;
                case CFFCharstringOperators.hflex: result = stack.hflex(); break;
                case CFFCharstringOperators.hflex1: result = stack.hflex1(); break;
                case CFFCharstringOperators.flex1: result = stack.flex1(); break;

                // 4.2 Operator for Finishing a Path
                case CFFCharstringOperators.return0: result = stack.return0(); break;
                case CFFCharstringOperators.endchar: result = stack.endchar(); break;

                // 4.3 Hint Operators
                /*
                2) Hints: zero or more of each of the following hint
                    operators, in exactly the following order: hstem, hstemhm,
                    vstem, vstemhm, cntrmask, hintmask. Each entry is optional,
                    and each may be expressed by one or more occurrences of
                    the operator. The hint operators cntrmask and/or hintmask
                    must not occur if the charstring has no stem hints.
                */
                case CFFCharstringOperators.hstem: result = stack.hstem((Int32)op); break;
                case CFFCharstringOperators.vstem: result = stack.vstem((Int32)op); break;
                case CFFCharstringOperators.hstemhm: result = stack.hstemhm((Int32)op); break;
                case CFFCharstringOperators.vstemhm: result = stack.vstemhm((Int32)op); break;

                case CFFCharstringOperators.hintmask:
                    result = stack.hintmask((Int32)CFFCharstringOperators.vstemhm, CharstringHelper.GetMaskBytes(token));
                    break;
                case CFFCharstringOperators.cntrmask:
                    result = stack.cntrmask((Int32)CFFCharstringOperators.vstemhm, CharstringHelper.GetMaskBytes(token));
                    break;

                // 4.4 Arithmetic Operators
                case CFFCharstringOperators.abs: result = stack.abs(); break;
                case CFFCharstringOperators.add: result = stack.add(); break;//         = 0x0c0a,   // CFF2:Reserved
                case CFFCharstringOperators.sub: result = stack.sub(); break;//         = 0x0c0b,   // CFF2:Reserved
                case CFFCharstringOperators.div: result = stack.div(); break;//         = 0x0c0c,   // CFF2:Reserved
                case CFFCharstringOperators.neg: result = stack.neg(); break;//         = 0x0c0e,   // CFF2:Reserved
                case CFFCharstringOperators.random: result = stack.random(); break;//      = 0x0c17,   // CFF2:Reserved
                case CFFCharstringOperators.mul: result = stack.mul(); break;//         = 0x0c18,   // CFF2:Reserved
                case CFFCharstringOperators.sqrt: result = stack.sqrt(); break;//        = 0x0c1a,   // CFF2:Reserved
                case CFFCharstringOperators.drop: result = stack.drop(); break;//        = 0x0c12,   // CFF2:Reserved
                case CFFCharstringOperators.exch: result = stack.exch(); break;//        = 0x0c1c,   // CFF2:Reserved
                case CFFCharstringOperators.index: result = stack.index(); break;//       = 0x0c1d,   // CFF2:Reserved
                case CFFCharstringOperators.roll: result = stack.roll(); break;//        = 0x0c1e,   // CFF2:Reserved
                case CFFCharstringOperators.dup: result = stack.dup(); break;//         = 0x0c1b,   // CFF2:Reserved

                // 4.5 Storage Operators
                case CFFCharstringOperators.put: result = stack.put(); break;
                case CFFCharstringOperators.get: result = stack.get(); break;

                // 4.6 Conditional Operators
                case CFFCharstringOperators.and: result = stack.and(); break;
                case CFFCharstringOperators.or: result = stack.or(); break;//0x0c04,   // CFF2:Reserved
                case CFFCharstringOperators.not: result = stack.not(); break;//         = 0x0c05,   // CFF2:Reserved
                case CFFCharstringOperators.eq: result = stack.eq(); break;//          = 0x0c0f,   // CFF2:Reserved
                case CFFCharstringOperators.ifelse: result = stack.ifelse(); break;//      = 0x0c16,   // CFF2:Reserved

                // 4.7 Subroutine Operators
                case CFFCharstringOperators.callsubr: result = stack.callsubr(); break;
                case CFFCharstringOperators.callgsubr: result = stack.callgsubr(); break;

                // no work
                //case CFFCharstringOperators.escape://      = 0x0c,

                default:
                    result = CharstringTokenResult.ReadError;
                    break;
            }
            return result;
        }


        public static CharstringTokenResult DispatchCFF2Operator(uint8[] token, CharstringStack stack)
        {
            CharstringTokenResult result;

            var op = (CFF2CharStringOperators)CharstringHelper.GetOperatorInteger(token);
            switch (op)
            {
                case CFF2CharStringOperators.rmoveto: result = stack.rmoveto(); break;
                case CFF2CharStringOperators.hmoveto: result = stack.hmoveto(); break;
                case CFF2CharStringOperators.vmoveto: result = stack.vmoveto(); break;

                case CFF2CharStringOperators.rlineto: result = stack.rlineto(); break;
                case CFF2CharStringOperators.hlineto: result = stack.hlineto(); break;
                case CFF2CharStringOperators.vlineto: result = stack.vlineto(); break;

                case CFF2CharStringOperators.rrcurveto: result = stack.rrcurveto(); break;
                case CFF2CharStringOperators.hhcurveto: result = stack.hhcurveto(); break;
                case CFF2CharStringOperators.hvcurveto: result = stack.hvcurveto(); break;
                case CFF2CharStringOperators.rcurveline: result = stack.rcurveline(); break;
                case CFF2CharStringOperators.rlinecurve: result = stack.rlinecurve(); break;
                case CFF2CharStringOperators.vhcurveto: result = stack.vhcurveto(); break;
                case CFF2CharStringOperators.vvcurveto: result = stack.vvcurveto(); break;
                case CFF2CharStringOperators.flex: result = stack.flex(); break;
                case CFF2CharStringOperators.hflex: result = stack.hflex(); break;
                case CFF2CharStringOperators.hflex1: result = stack.hflex1(); break;
                case CFF2CharStringOperators.flex1: result = stack.flex1(); break;

                case CFF2CharStringOperators.hstem: result = stack.hstem((Int32)op); break;
                case CFF2CharStringOperators.vstem: result = stack.vstem((Int32)op); break;
                case CFF2CharStringOperators.hstemhm: result = stack.hstemhm((Int32)op); break;
                case CFF2CharStringOperators.vstemhm: result = stack.vstemhm((Int32)op); break;

                case CFF2CharStringOperators.hintmask:
                    result = stack.hintmask((Int32)CFF2CharStringOperators.vstemhm, CharstringHelper.GetMaskBytes(token));
                    break;
                case CFF2CharStringOperators.cntrmask:
                    result = stack.cntrmask((Int32)CFF2CharStringOperators.vstemhm, CharstringHelper.GetMaskBytes(token));
                    break;

                case CFF2CharStringOperators.callsubr: result = stack.callsubr(); break;
                case CFF2CharStringOperators.callgsubr: result = stack.callgsubr(); break;

                case CFF2CharStringOperators.vsindex: result = stack.vsindex(); break;
                case CFF2CharStringOperators.blend: result = stack.blend(); break;

                default:
                    result = CharstringTokenResult.ReadError;
                    break;
            }
            return result;
        }
    }
}
