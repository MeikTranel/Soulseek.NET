﻿// <copyright file="ConnectionEventArgs.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham. All rights reserved.
//
//     This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as
//     published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//     of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License along with this program. If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace Soulseek.NET.Tcp
{
    using System;

    internal class ConnectionStateChangedEventArgs : EventArgs
    {
        internal ConnectionStateChangedEventArgs(ConnectionState state, string message = null)
        {
            State = state;
            Message = message;
        }

        public ConnectionState State { get; private set; }
        public string Message { get; private set; }
    }

    internal class ConnectionDataEventArgs : EventArgs
    {
        internal ConnectionDataEventArgs(byte[] data, int currentLength, int totalLength)
        {
            Data = data;
            CurrentLength = currentLength;
            TotalLength = totalLength;
        }

        public byte[] Data { get; private set; }
        public int CurrentLength { get; private set; }
        public int TotalLength { get; private set; }
        public double PercentComplete => CurrentLength / (double)TotalLength;
    }
}
