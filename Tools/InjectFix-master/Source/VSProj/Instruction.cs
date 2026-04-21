/*
 * Tencent is pleased to support the open source community by making InjectFix available.
 * Copyright (C) 2019 Tencent.  All rights reserved.
 * InjectFix is licensed under the MIT License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
 * This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
 */

namespace IFix.Core
{
    public enum Code
    {
        No,
        Ldelem_I8,
        Ldvirtftn,
        Conv_Ovf_U2,
        Conv_R8,
        Ldelem_I2,
        Unbox,
        Stelem_I1,
        Conv_I8,
        Ldelema,
        Stelem_R4,
        Stelem_I4,
        Stobj,
        Localloc,
        Conv_R_Un,
        Stsfld,
        Callvirt,
        Conv_Ovf_I4,
        Ldind_I,
        Switch,
        Endfilter,
        Leave,
        Stind_Ref,
        Conv_I2,
        Initobj,
        Constrained,
        Ldind_U2,
        Ldelem_I1,
        Tail,
        Ldelem_U2,
        Ldelem_Any,
        Ldc_I4,
        Neg,
        Stelem_Ref,
        Ldelem_U1,
        Conv_U,
        Box,
        Ldelem_R8,
        Cpobj,
        Conv_Ovf_I2,
        Stelem_R8,
        Endfinally,
        Call,
        Stfld,
        Ldind_R4,
        Conv_Ovf_I1,
        Isinst,
        Callvirtvirt,
        Conv_Ovf_U8_Un,
        Ldsflda,
        Unbox_Any,
        Ret,
        Blt,
        Mul_Ovf,
        Readonly,
        Ldlen,
        Blt_Un,
        Stind_I2,
        Xor,
        Ldflda,
        Ldelem_Ref,
        Or,
        Conv_U4,
        Ldind_I4,
        Bge_Un,
        Beq,
        Conv_Ovf_U1,
        Ble_Un,
        Shr_Un,
        //Calli,
        Ldelem_I,
        Conv_I4,
        Rethrow,
        Ldc_R8,
        Cgt_Un,
        Not,
        Ldind_Ref,
        Bne_Un,
        Conv_U1,
        Shl,
        Ldind_U1,
        Ldind_R8,
        Bgt,
        Stelem_I,
        Shr,
        Add_Ovf,
        Conv_U2,
        Ldelem_R4,
        Castclass,
        Ldnull,
        Refanytype,
        Conv_Ovf_U_Un,
        Ldfld,
        Stind_R4,
        Conv_I1,
        Conv_Ovf_I1_Un,
        Stelem_Any,
        Conv_Ovf_U2_Un,
        Unaligned,
        Conv_Ovf_I2_Un,
        CallExtern,
        Div,
        Ckfinite,
        Ldind_I1,
        Ldobj,
        Pop,
        Nop,
        Br,
        Newobj,
        Stelem_I2,
        Ldc_I8,
        Ldloc,
        Conv_Ovf_I,
        Clt,
        Ldelem_I4,
        Break,

        //Pseudo instruction
        StackSpace,
        Ceq,
        Conv_R4,
        Stloc,
        Conv_Ovf_U8,
        Conv_Ovf_I4_Un,
        Cpblk,
        Mul,
        Ldftn,
        Conv_Ovf_I8_Un,
        Volatile,
        Refanyval,
        Stind_I1,
        Stelem_I8,
        Conv_Ovf_U4_Un,
        Ldtoken,
        Sub,
        Ble,
        Conv_I,
        Rem,
        Conv_U8,
        Stind_I,
        Sub_Ovf,
        Stind_I8,
        Ldsfld,
        Mkrefany,
        Brfalse,
        Conv_Ovf_U4,
        Ldvirtftn2,
        Ldloca,
        Brtrue,
        Sub_Ovf_Un,
        Add,
        Ldc_R4,
        Ldtype, // custom
        Ldind_U4,
        Conv_Ovf_I_Un,
        Add_Ovf_Un,
        Bge,
        Ldind_I2,
        Mul_Ovf_Un,
        Starg,
        Conv_Ovf_U1_Un,
        Cgt,
        Ldstr,
        Newanon,
        Bgt_Un,
        Conv_Ovf_U,
        Conv_Ovf_I8,
        Newarr,
        Dup,
        And,
        Rem_Un,
        Div_Un,
        Jmp,
        Arglist,
        Clt_Un,
        Stind_I4,
        Ldarg,
        Sizeof,
        Ldarga,
        Initblk,
        Throw,
        Ldind_I8,
        Stind_R8,
        Ldelem_U4,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Instruction
    {
        /// <summary>
        /// 指令MAGIC
        /// </summary>
        public const ulong INSTRUCTION_FORMAT_MAGIC = 6607045890039607319;

        /// <summary>
        /// 当前指令
        /// </summary>
        public Code Code;

        /// <summary>
        /// 操作数
        /// </summary>
        public int Operand;
    }

    public enum ExceptionHandlerType
    {
        Catch = 0,
        Filter = 1,
        Finally = 2,
        Fault = 4
    }

    public sealed class ExceptionHandler
    {
        public System.Type CatchType;
        public int CatchTypeId;
        public int HandlerEnd;
        public int HandlerStart;
        public ExceptionHandlerType HandlerType;
        public int TryEnd;
        public int TryStart;
    }
}