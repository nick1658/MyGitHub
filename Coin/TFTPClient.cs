﻿#region COPYRIGHT (c) 2007 by Matthias Fischer
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  This material may not be duplicated in whole or in part, except for 
//  personal use, without the express written consent of the author. 
//
//    Autor:  Matthais Fischer  
//    Email:  mfischer@comzept.de
//
//  Copyright (C) 2007 Matthias Fischer. All Rights Reserved.
#endregion

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Comzept.Genesis.NetworkTools
{
    /// <summary>
    /// Implementation of Basic TFTP Client Functions
    /// </summary>
    public class TFTPClient
    {

        #region -=[ Declarations ]=-

        /// <summary>
        /// TFTP opcodes
        /// </summary>
        public enum Opcodes
        {
            Unknown = 0,
            Read = 1,
            Write = 2,
            Data = 3,
            Ack = 4,
            Error = 5
        }

        /// <summary>
        /// TFTP modes
        /// </summary>
        public enum Modes
        {
            Unknown = 0,
            NetAscii = 1,
            Octet = 2,
            Mail = 3
        }

        /// <summary>
        /// A TFTP Exception
        /// </summary>
        public class TFTPException : Exception
        {

            public string ErrorMessage = "";
            public int ErrorCode = -1;

            /// <summary>
            /// Initializes a new instance of the <see cref="TFTPException"/> class.
            /// </summary>
            /// <param name="errCode">The err code.</param>
            /// <param name="errMsg">The err MSG.</param>
            public TFTPException(int errCode, string errMsg)
            {
                ErrorCode = errCode;
                ErrorMessage = errMsg;
            }

            /// <summary>
            /// Creates and returns a string representation of the current exception.
            /// </summary>
            /// <returns>
            /// A string representation of the current exception.
            /// </returns>
            /// <filterPriority>1</filterPriority>
            /// <permissionSet class="System.Security.permissionSet" version="1">
            /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/>
            /// </permissionSet>
            public override string ToString()
            {
                return String.Format("TFTPException: ErrorCode: {0} Message: {1}", ErrorCode, ErrorMessage);
            }
        }

        private int tftpPort;
        private string tftpServer = "";
        #endregion

        #region -=[ Ctor ]=-

        /// <summary>
        /// Initializes a new instance of the <see cref="TFTPClient"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public TFTPClient(string server)
            : this(server, 69)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TFTPClient"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public TFTPClient(string server, int port)
        {
            Server = server;
            Port = port;

        }

        #endregion

        #region -=[ Public Properties ]=-

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return tftpPort; }
            private set { tftpPort = value; }
        }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server
        {
            get { return tftpServer; }
            private set { tftpServer = value; }
        }

        #endregion

        #region -=[ Public Member ]=-

        /// <summary>
        /// Gets the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        public void Get(string remoteFile, string localFile)
        {
            Get(remoteFile, localFile, Modes.Octet);
        }

        /// <summary>
        /// Gets the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        /// <param name="tftpMode">The TFTP mode.</param>
        public void Get(string remoteFile, string localFile, Modes tftpMode)
        {
            int len = 0;
            int packetNr = 1;
            byte[] sndBuffer = CreateRequestPacket(Opcodes.Read, remoteFile, tftpMode, "command", "null");
            byte[] rcvBuffer = new byte[516];

            BinaryWriter fileStream = new BinaryWriter(new FileStream(localFile, FileMode.Create, FileAccess.Write, FileShare.Read));
            //IPHostEntry hostEntry = Dns.GetHostEntry(tftpServer);
            //IPEndPoint serverEP = new IPEndPoint(hostEntry.AddressList[0], tftpPort);
            IPAddress ipAddress = IPAddress.Parse(tftpServer);
            IPEndPoint serverEP = new IPEndPoint(ipAddress, tftpPort);
            EndPoint dataEP = (EndPoint)serverEP;
            Socket tftpSocket = new Socket(serverEP.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            // Request and Receive first Data Packet From TFTP Server
            tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
            tftpSocket.ReceiveTimeout = 1000;
            try
            {
                len = tftpSocket.ReceiveFrom(rcvBuffer, ref dataEP);

            }
            catch (Exception ex)
            {
                fileStream.Close();
                throw new TFTPException(99, "Exception in ReceiveFrom()");
            }

            // keep track of the TID 
            serverEP.Port = ((IPEndPoint)dataEP).Port;

            while (true)
            {
                // handle any kind of error 
                if (((Opcodes)rcvBuffer[1]) == Opcodes.Error)
                {
                    fileStream.Close();
                    tftpSocket.Close();
                    throw new TFTPException(((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3], Encoding.ASCII.GetString(rcvBuffer, 4, rcvBuffer.Length - 5).Trim('\0'));
                }
                // expect the next packet
                if ((((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3]) == packetNr)
                {
                    // Store to local file
                    fileStream.Write(rcvBuffer, 4, len - 4);

                    // Send Ack Packet to TFTP Server
                    sndBuffer = CreateAckPacket(packetNr++);
                    tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
                }
                // Was ist the last packet ?
                if (len < 516)
                {
                    break;
                }
                else
                {
                    // Receive Next Data Packet From TFTP Server
                    len = tftpSocket.ReceiveFrom(rcvBuffer, ref dataEP);
                }
            }

            // Close Socket and release resources
            tftpSocket.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Puts the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        public void Put(string remoteFile, string localFile)
        {
            Put(remoteFile, localFile, Modes.Octet);
        }

        /// <summary>
        /// Puts the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        /// <param name="tftpMode">The TFTP mode.</param>
        /// <remarks>What if the ack does not come !</remarks>
        public void Put(string remoteFile, string localFile, Modes tftpMode)
        {
            int len = 0;
            int packetNr = 0;
            int send_OK = 0;
            int timeOutNum = 0;
            byte[] sndBuffer = CreateRequestPacket(Opcodes.Write, remoteFile, tftpMode, "command", "null");
            byte[] rcvBuffer = new byte[516];

            BinaryReader br = new BinaryReader(new FileStream(localFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            //IPHostEntry hostEntry = Dns.GetHostEntry(tftpServer);
            //IPEndPoint serverEP = new IPEndPoint(hostEntry.AddressList[0], tftpPort);
            IPAddress ipAddress = IPAddress.Parse(tftpServer);
            IPEndPoint serverEP = new IPEndPoint(ipAddress, tftpPort);
            EndPoint dataEP = (EndPoint)serverEP;
            Socket tftpSocket = new Socket(serverEP.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            // Request Writing to TFTP Server
            tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
            tftpSocket.ReceiveTimeout = 1000;
            timeOutNum = 0;
            while (timeOutNum < 4)//重复4次
            {
                try
                {
                    len = tftpSocket.ReceiveFrom(rcvBuffer, ref dataEP);
                    if (((Opcodes)rcvBuffer[1]) == Opcodes.Error)
                    {
                        br.Close();
                        tftpSocket.Close();
                        throw new TFTPException(((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3], Encoding.ASCII.GetString(rcvBuffer, 4, rcvBuffer.Length - 5).Trim('\0'));
                    }
                    break;
                }
                catch (Exception ex)
                {
                    timeOutNum++;
                    if (timeOutNum == 4)
                    {
                        br.Close();
                        tftpSocket.Close();
                        throw new TFTPException(0xFF, ex.Message + " --TimeOut");
                    }
                    else
                    {
                        tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
                    }
                } 
            }

            // keep track of the TID 
            serverEP.Port = ((IPEndPoint)dataEP).Port;

            while (true)
            {
                sndBuffer = CreateDataPacket(++packetNr, br.ReadBytes(512));
                send_OK = 0;
                timeOutNum = 0;
                while (send_OK == 0)
                {
                    try
                    {
                        tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
                        len = tftpSocket.ReceiveFrom(rcvBuffer, ref dataEP);
                    }
                    catch (Exception ex)
                    {
                        timeOutNum++;
                        if (timeOutNum > 9)
                        {
                            br.Close();
                            tftpSocket.Close();
                            throw new TFTPException(0xFF, ex.Message + " --TimeOut");
                        }
                    }// expect the next packet ack
                    if ((((Opcodes)rcvBuffer[1]) == Opcodes.Ack) && (((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3]) == packetNr)
                    {
                        send_OK = 1;
                    }
                    else if ((((Opcodes)rcvBuffer[1]) == Opcodes.Ack) && (((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3]) == (packetNr - 1))
                    {
                        send_OK = 0;//resend
                    }
                    else if (((Opcodes)rcvBuffer[1]) == Opcodes.Error)// handle any kind of error 
                    {
                        throw new TFTPException(((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3], Encoding.ASCII.GetString(rcvBuffer, 4, rcvBuffer.Length - 5).Trim('\0'));
                    }
                }
                // we are done
                if (sndBuffer.Length < 516)
                {
                    break;
                }
                else
                {
                }
            }
            // Close Socket and release resources
            tftpSocket.Close();
            br.Close();
        }
        public void sendNetCmd (string cmd)
        {
            int len = 0;
            int timeOutNum = 0;
            byte[] sndBuffer = CreateRequestPacket(Opcodes.Write, "system", Modes.Octet, "command", "reset");
            byte[] rcvBuffer = new byte[516];

            IPAddress ipAddress = IPAddress.Parse(tftpServer);
            IPEndPoint serverEP = new IPEndPoint(ipAddress, tftpPort);
            EndPoint dataEP = (EndPoint)serverEP;
            Socket tftpSocket = new Socket(serverEP.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            // Request Writing to TFTP Server
            tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
            tftpSocket.ReceiveTimeout = 3000;
            timeOutNum = 0;
            while (timeOutNum < 4)//重复4次
            {
                try
                {
                    len = tftpSocket.ReceiveFrom(rcvBuffer, ref dataEP);
                    if (((Opcodes)rcvBuffer[1]) == Opcodes.Error)
                    {
                        tftpSocket.Close();
                        throw new TFTPException(((rcvBuffer[2] << 8) & 0xff00) | rcvBuffer[3], Encoding.ASCII.GetString(rcvBuffer, 4, rcvBuffer.Length - 5).Trim('\0'));
                    }
                    break;
                }
                catch (Exception ex)
                {
                    timeOutNum++;
                    if (timeOutNum == 4)
                    {
                        tftpSocket.Close();
                        throw new TFTPException(0xFF, ex.Message + " --TimeOut");
                    }
                    else
                    {
                        tftpSocket.SendTo(sndBuffer, sndBuffer.Length, SocketFlags.None, serverEP);
                    }
                }
            }
        }

        #endregion

        #region -=[ Private Member ]=-

        /// <summary>
        /// Creates the request packet.
        /// </summary>
        /// <param name="opCode">The op code.</param>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="tftpMode">The TFTP mode.</param>
        /// <returns>the ack packet</returns>
        private byte[] CreateRequestPacket(Opcodes opCode, string remoteFile, Modes tftpMode, string op, string op_v)
        {
            // Create new Byte array to hold Initial 
            // Read Request Packet
            int pos = 0;
            string modeAscii = tftpMode.ToString().ToLowerInvariant();
            byte[] ret = new byte[modeAscii.Length + remoteFile.Length + op.Length + op_v.Length + 6];

            // Set first Opcode of packet to indicate
            // if this is a read request or write request
            ret[pos++] = 0;
            ret[pos++] = (byte)opCode;

            // Convert Filename to a char array
            pos += Encoding.ASCII.GetBytes(remoteFile, 0, remoteFile.Length, ret, pos);
            ret[pos++] = 0;
            pos += Encoding.ASCII.GetBytes(modeAscii, 0, modeAscii.Length, ret, pos);
            ret[pos++] = 0;
            if (opCode == Opcodes.Write)
            {
                pos += Encoding.ASCII.GetBytes(op, 0, op.Length, ret, pos);
                ret[pos++] = 0;
                pos += Encoding.ASCII.GetBytes(op_v, 0, op_v.Length, ret, pos);
                ret[pos] = 0;
            }

            return ret;
        }

        /// <summary>
        /// Creates the data packet.
        /// </summary>
        /// <param name="packetNr">The packet nr.</param>
        /// <param name="data">The data.</param>
        /// <returns>the data packet</returns>
        private byte[] CreateDataPacket(int blockNr, byte[] data)
        {
            // Create Byte array to hold ack packet
            byte[] ret = new byte[4 + data.Length];

            // Set first Opcode of packet to TFTP_ACK
            ret[0] = 0;
            ret[1] = (byte)Opcodes.Data;
            ret[2] = (byte)((blockNr >> 8) & 0xff);
            ret[3] = (byte)(blockNr & 0xff);
            Array.Copy(data, 0, ret, 4, data.Length);
            return ret;
        }

        /// <summary>
        /// Creates the ack packet.
        /// </summary>
        /// <param name="blockNr">The block nr.</param>
        /// <returns>the ack packet</returns>
        private byte[] CreateAckPacket(int blockNr)
        {
            // Create Byte array to hold ack packet
            byte[] ret = new byte[4];

            // Set first Opcode of packet to TFTP_ACK
            ret[0] = 0;
            ret[1] = (byte)Opcodes.Ack;

            // Insert block number into packet array
            ret[2] = (byte)((blockNr >> 8) & 0xff);
            ret[3] = (byte)(blockNr & 0xff);
            return ret;
        }

        #endregion
    }
}
