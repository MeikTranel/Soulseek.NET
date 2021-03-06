﻿// <copyright file="PeerInitRequest.cs" company="JP Dillingham">
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

namespace Soulseek.NET.Messaging.Requests
{
    public class PeerInitRequest
    {
        #region Public Constructors

        public PeerInitRequest(string username, string transferType, int token)
        {
            Username = username;
            TransferType = transferType;
            Token = token;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Token { get; set; }
        public string TransferType { get; set; }
        public string Username { get; set; }

        #endregion Public Properties

        #region Public Methods

        public Message ToMessage()
        {
            return new MessageBuilder()
                .Code(0x1)
                .WriteString(Username)
                .WriteString(TransferType)
                .WriteInteger(Token)
                .Build();
        }

        #endregion Public Methods
    }
}