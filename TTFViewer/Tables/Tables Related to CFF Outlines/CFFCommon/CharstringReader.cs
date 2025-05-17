using System;
using System.Collections.Generic;
using System.Linq;
using TTFViewer.DataTypes;

namespace TTFViewer.Tables
{
    enum CharstringTokenResult
    {
        None = 0,
        Operand = 1,
        Operator = 2,
        Mask = 3,
        MoveTo = 4,
        LocalSubr = 5,
        GlobalSubr = 6,
        Exit = 7,        // CFF:return/end, CFF2:cannot read b0
        Cancel = 8,
        ReadError = 9,  // cannot read after b0
        MaskError = 10,  // get ctrlmask/hintmask while no hstemhm/vstemhm
                         // or get ctrlmask/hintmask while analyzing sub-proc
        OperandError = 11,
    }


    class CharstringReader
    {
        //public bool IsCFF => SubrSelector.IsCFF;

        PrimitiveReader PrimitiveReader;
        Int32 GID;
        SubrSelector SubrSelector;

        public CharstringReader(PrimitiveReader reader, Int32? fontIndex, Int32 gid, SubrSelector subrSelector)
        {
            if(subrSelector != null)
            {
                PrimitiveReader = reader;
                GID = gid;
                SubrSelector = subrSelector;
            }
            else
                throw new ArgumentException();
        }



        public static IEnumerable<(CFFTokenKind tokenKind, uint8[] token)> EnumToken(IEnumerator<uint8> uint8Enumerator, Func<Int32> getMaskByteLength, bool isCFF)
        {
            while (TokenHelper.ReadByte(uint8Enumerator) is uint8 b0)
            {
                CFFTokenKind tokenKind = CharstringHelper.GetTokenKind(b0);
                var tokenLength = CharstringHelper.GetTokenLength(tokenKind);
                if (tokenLength > 0)
                {
                    uint8[] token = TokenHelper.ReadToken(uint8Enumerator, b0, tokenLength);

                    if (tokenKind.HasFlag(CFFTokenKind.Operator)
                        && CharstringHelper.GetOperatorInteger(token) is Int32 op
                        && CharstringHelper.IsMaskOperator(isCFF, op))
                    {
                        if (getMaskByteLength != null)
                        {
                            Int32 maskByteLength = getMaskByteLength();
                            var maskBytes = TokenHelper.ReadBytes(uint8Enumerator, maskByteLength);
                            token = token.Concat(maskBytes).ToArray();
                        }
                        else
                        {
                            // if unexpected mask operator, exit
                            yield return (tokenKind, token);
                            break;
                        }
                    }
                    yield return (tokenKind, token);
                }
                else
                {
                    // if tokenLength == 0
                    yield return (tokenKind, null);
                    yield break;
                }
            }
            yield break;

        }


        public void Interpret(IEnumerator<uint8> uint8Enumerator, CharstringStack stack, Func<CharstringTokenResult,bool> abortFunc, List<uint8[]> log)
        {
            Func<uint8[], CharstringStack, CharstringTokenResult> dispatchOperator;
            bool isCFF = SubrSelector.IsCFF;
            if (isCFF)
                dispatchOperator = CharstringDispatchHelper.DispatchCFFOperator;
            else
                dispatchOperator = CharstringDispatchHelper.DispatchCFF2Operator;

            Func<Int32> getMaskByteLength 
                = () =>
                {
                    Int32 hintOp = isCFF ? (Int32)CFFCharstringOperators.vstemhm : (Int32)CFF2CharStringOperators.vstemhm;
                    stack.PreMask(hintOp);
                    return stack.GetMaskByteLength();
                };

            foreach (var tokenKindToken in EnumToken(uint8Enumerator, getMaskByteLength, isCFF))
            {
                log?.Add(tokenKindToken.token);

                if (tokenKindToken.tokenKind.HasFlag(CFFTokenKind.Operand))
                {
                    var num = CharstringHelper.GetNumber(tokenKindToken.tokenKind, tokenKindToken.token);
                    if (num != null)
                        stack.Push(num);
                    else
                    {
                        log?.Clear();
                        break;
                    }
                }
                else if(tokenKindToken.tokenKind.HasFlag(CFFTokenKind.Operator))
                {
                    var ret = dispatchOperator(tokenKindToken.token, stack);

                    if (ret == CharstringTokenResult.LocalSubr)
                    {
                        var subrNumber = stack.Pop();
                        var subrReader = SubrSelector.GetLocalSubrReader(PrimitiveReader, GID, subrNumber);
                        if (subrReader != null)
                        {
                            Interpret(subrReader.GetEnumerator(), stack, abortFunc, null);
                        }
                        else
                        {
                            log?.Clear();
                            break;
                        }
                    }
                    else if (ret == CharstringTokenResult.GlobalSubr)
                    {
                        var subrNumber = stack.Pop();
                        var subrReader = SubrSelector.GetGlobalSubrReader(PrimitiveReader, subrNumber);
                        if(subrReader != null)
                        { 
                            Interpret(subrReader.GetEnumerator(), stack, abortFunc, null);
                        }
                        else
                        {
                            log?.Clear();
                            break;
                        }
                    }

                    if (abortFunc != null && abortFunc(ret))
                    {
                        break;
                    }
                }
            }
        }
    }
}
