﻿using System;
using System.Runtime.InteropServices;

namespace Force.AutoTunnel
{
	public static class WinDivert
	{
		public const int LAYER_NETWORK = 0;

		public const int LAYER_NETWORK_FORWARD = 1;

		public const int FLAG_SNIFF = 1;

		public const int FLAG_DROP = 2;

		[StructLayout(LayoutKind.Sequential)]
		public struct WinDivertAddress
		{
			public ulong Timestamp;
			public uint IfIdx;                       /* Packet's interface index. */
			public uint SubIfIdx;                    /* Packet's sub-interface index. */
			public uint Flags;
			/*public byte Direction;                   /* Packet's direction. #1#
			public byte Loopback;
			public byte Impostor;
			public byte PseudoIPChecksum;
			public byte PseudoTCPChecksum;
			public byte PseudoUDPChecksum;
			public byte Reserved;*/

			public uint Direction
			{
				get
				{
					return Flags & 1;
				}
			}

			public uint Loopback
			{
				get
				{
					return Flags & 2;
				}
			}

			public uint Impostor
			{
				get
				{
					return Flags & 4;
				}
			}

			public bool HasPseudoChecksum
			{
				get
				{
					return (Flags & (8 | 16 | 32)) != 0;
				}
			}

			public void DisablePseudoChecksums()
			{
				Flags &= 255 - (8 + 16 + 32);
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct WinDivertIpHeader
		{
			[FieldOffset(0)]
			public byte HdrLengthAndVersion;

			[FieldOffset(1)]
			public byte Tos;

			[FieldOffset(2)]
			public ushort Length;

			[FieldOffset(4)]
			public ushort Id;

			[FieldOffset(6)]
			public ushort FragOff0;

			[FieldOffset(8)]
			public byte Ttl;

			[FieldOffset(9)]
			public byte Protocol;

			[FieldOffset(10)]
			public short Checksum;

			[FieldOffset(12)]
			public uint SrcAddr;

			[FieldOffset(16)]
			public uint DstAddr;
		}

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WinDivertHelperParsePacket(
			byte[] pPacket,
			int packetLen,
			ref WinDivertIpHeader ipHdr,
			IntPtr ipv6Hdr,
			IntPtr icmpHdr,
			IntPtr icmpv6Hdr,
			IntPtr tcpHdr,
			IntPtr updHdr,
			IntPtr ppdataHdr,
			ref int dataLen);

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr WinDivertOpen(string filter, int layer, short priority, ulong flags);

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int WinDivertClose(IntPtr handle);

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int WinDivertRecv(IntPtr handle, byte[] packet, int packetLen, ref WinDivertAddress addr, ref int readLen);

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int WinDivertSend(IntPtr handle, byte[] packet, int packetLen, ref WinDivertAddress addr, ref int writeLen);

		[DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int WinDivertHelperCalcChecksums(byte[] packet, int packetLen, ref WinDivertAddress addr, ulong flags);
	}
}